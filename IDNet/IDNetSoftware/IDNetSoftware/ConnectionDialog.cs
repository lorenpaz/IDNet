using System;
using System.Security.Cryptography;
using System.Xml;

using MessageLibraryS;
using ConnectionLibraryS;
using PostBoxLibraryS;

namespace IDNetSoftware
{
    public partial class ConnectionDialog : Gtk.Dialog
    {
        private PipeMessage _connection;
        private PipeMessage _schema;
		private PipeMessage _select;

        private string _destination;
        private string _db_name;
        private string _db_type;
        private XmlNode _body;

        private string _typeOutPut;

		public PipeMessage Connection
		{
			get
			{
                return this._connection;
			}
			set
			{
                this._connection = value;
			}
		}
		public PipeMessage Schema
		{
			get
			{
                return this._schema;
			}
			set
			{
                this._schema = value;
			}
		}
		public PipeMessage Select
		{
			get
			{
                return this._select;
			}
            set
            {
                this._select = value;
            }
		}
		public String TypeOutPut
		{
			get
			{
                return this._typeOutPut;
			}
			set
			{
                this._typeOutPut = value;
			}
		}

        public ConnectionDialog(string destination, string db_type, string db_name)
        {
            this.Build();

            this._destination = destination;
            this._db_name = db_name;
            this._db_type = db_type;
            this._body = new XmlDocument();
            this._typeOutPut = "Cancel";
        }

		public ConnectionDialog(string destination, string db_type, string db_name,
            PipeMessage pipeConexion, PipeMessage pipeSchema)
		{
			this.Build();

			this._destination = destination;
			this._db_name = db_name;
			this._db_type = db_type;
			this._body = new XmlDocument();
			this._typeOutPut = "Cancel";

			this._connection = pipeConexion;
			this._schema = pipeSchema;
		}
		public ConnectionDialog(string destination, string db_type, string db_name,
            PipeMessage pipeConexion)
		{
			this.Build();

			this._destination = destination;
			this._db_name = db_name;
			this._db_type = db_type;
			this._body = new XmlDocument();
			this._typeOutPut = "Cancel";

			this._connection = pipeConexion;
		}

        protected void OnButtonEsquemaClicked(object sender, EventArgs e)
        {
            solicitarEsquema();
            this._typeOutPut = "002";
            this.Destroy();
        }

		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			this.Destroy();
		}

		private void solicitarEsquema()
		{
			string msg, response;

			//Proceso el envio
            PostBox post = new PostBox("Lorenzo Jose",this._destination, "002",this._db_name,this._db_type,this._body);
			msg = post.ProcesarEnvio();

			//Creo el cliente y le envio el mensaje
			Client c = new Client();
			response = c.StartClient(msg, "localhost");

			//Proceso la respuesta
			post.ProcesarRespuesta(response);

            this._schema = new PipeMessage(post.MessageRequest,post.MessageResponse);
		}

        protected void OnButtonConexionClicked(object sender, EventArgs e)
        {
            string msg, response;

			// Create a new Rijndael key.
			RijndaelManaged key = new RijndaelManaged();
            key.GenerateKey();
            key.GenerateIV();

			PostBox post = new PostBox("Lorenzo", this._destination, "001",key);
            msg = post.ProcesarEnvioConexion();

			//Creo el cliente y le envio el mensaje
			Client c = new Client();
			response = c.StartClient(msg, "localhost");

			//Proceso la respuesta
			post.ProcesarRespuesta(response);

            this._connection = new PipeMessage(post.MessageRequest,post.MessageResponse);

            this._typeOutPut = "001";

            this.Destroy();
        }
    }
}
