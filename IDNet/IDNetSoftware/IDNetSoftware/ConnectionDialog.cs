using System;
using System.Security.Cryptography;
using System.Xml;
using System.Collections.Generic;

using MessageLibraryS;
using ConnectionLibraryS;
using PostBoxLibraryS;
using ConstantsLibraryS;
using CriptoLibraryS;
using Org.BouncyCastle.Crypto.Parameters;


namespace IDNetSoftware
{
    public partial class    ConnectionDialog : Gtk.Dialog
    {
        //Pipes de mensajes
        private PipeMessage _connection;
        private PipeMessage _schema;
		private PipeMessage _select;

        //Atributos para la construcción de los mensajes
        private string _source;
        private string _destination;
        private string _db_name;
        private string _db_type;
        private XmlNode _body;

        //Auxiliar
        PostBox _auxiliarConexion;

        //Atributo para cifrado asimétrico
        private Cripto _keyPair;

        private RsaKeyParameters _publicKeyClient;

        //Atributo para cifrado simétrico
        private SymmetricAlgorithm _symmetricKey;

        //Atributo para saber cómo ha cerrado el diálogo
        private string _typeOutPut;

        //Atributo del diálogo de consulta Select-Mysql
        private SelectDialog _selectDialog;

        //Atributo del diálogo de consulta Select-MongoDB
        private FindDialog _findDialog;


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
        public Cripto KeyPair
		{
			get
			{
                return this._keyPair;
			}
			set
			{
                this._keyPair = value;
			}
		}
		public RsaKeyParameters PublicKeyClient
		{
			get
			{
				return this._publicKeyClient;
			}
		}
		public SymmetricAlgorithm SymmetricKey
		{
			get
			{
				return this._symmetricKey;
			}
			set
			{
				this._symmetricKey = value;
			}
		}

		/*
         * Constructor del diálogo de conexión
         * */
        public ConnectionDialog(string source, string destination, string db_type, string db_name, Cripto keyPair)
        {
            this.Build();

            this._source = source;
            this._destination = destination;
            this._db_name = db_name;
            this._db_type = db_type;
            this._body = new XmlDocument();
            this._typeOutPut = Constants.CANCEL;
			this._keyPair = keyPair;

			this._connection = new PipeMessage();
			this._schema = new PipeMessage();
            this._select = new PipeMessage();

            buttonEsquema.Sensitive = false;
			buttonSelect.Sensitive = false;
		}

		/*
         * Constructor del diálogo
         * */
        public ConnectionDialog(string source,string destination, string db_type, string db_name, 
                                PipeMessage pipeConexion,Cripto keyPair)
		{
			this.Build();

            this._source = source;
			this._destination = destination;
			this._db_name = db_name;
			this._db_type = db_type;
			this._body = new XmlDocument();
            this._typeOutPut = Constants.CANCEL;
            this._keyPair = keyPair;

            this._connection = pipeConexion;
			this._schema = new PipeMessage();
			this._select = new PipeMessage();

            buttonConexion.Sensitive = false;
			buttonEsquema.Sensitive = true;
			buttonSelect.Sensitive = false;
		}

		/*
         * Constructor del diálogo de conexión
         * */
		public ConnectionDialog(string source,string destination, string db_type, string db_name,
                                PipeMessage pipeConexion, PipeMessage pipeSchema,Cripto keyPair)
		{
			this.Build();

            this._source = source;
			this._destination = destination;
			this._db_name = db_name;
			this._db_type = db_type;
			this._body = new XmlDocument();
            this._typeOutPut = Constants.CANCEL;
            this._keyPair = keyPair;

			this._connection = pipeConexion;
			this._schema = pipeSchema;
            this._select = new PipeMessage();

            //Activar o desactivar los botones
            if (pipeConexion == null)
			{
                buttonEsquema.Sensitive = false;
                buttonSelect.Sensitive = false;
			}
			else
			{
                buttonConexion.Sensitive = false;
                buttonEsquema.Sensitive = true;
			}

            if(pipeSchema == null)
            {
				buttonSelect.Sensitive = false;
            }else{
                buttonConexion.Sensitive = false;
				buttonSelect.Sensitive = true;
			}

		}

        /*
         * Método del botón 'Cancel'
         * */
		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			this.Destroy();
		}

        /*
         * Método para realizar la conexión
         * */
        protected void OnButtonConexionClicked(object sender, EventArgs e)
        {


            // Create a new Rijndael key.
            RijndaelManaged key = new RijndaelManaged();
            this._symmetricKey = key;

            if (conexionPrimera())
            {
                if(conexionSegunda())
                    this._typeOutPut = Constants.MENSAJE_CONEXION;
                else
                    this._typeOutPut = Constants.ERROR_CONNECTION;

            }
            else
            {
                this._typeOutPut = Constants.ERROR_CONNECTION;
            }

            this.Destroy();
        }

        private bool conexionPrimera()
        {
			string msg, response;

            this._auxiliarConexion = new PostBox(this._source, this._destination, Constants.MENSAJE_CONEXION_A, this._keyPair);
            msg = this._auxiliarConexion.ProcesarEnvioConexion();

			//Creo el cliente y le envio el mensaje
			Client c = new Client();
            bool conexion = c.comprobarConexion(Constants.GATEKEEPER);
            if (conexion)
            {
                response = c.StartClient(msg, Constants.GATEKEEPER);

                //Proceso la respuesta
                this._auxiliarConexion.ProcesarRespuestaConexion(response);

                this._publicKeyClient = this._auxiliarConexion.PublicKeyClient;
                return true;
            }else{
                return false;
            }
		}

        private bool conexionSegunda()
        {
			string msg, response;

            PostBox post = new PostBox(this._source, this._destination,Constants.MENSAJE_CONEXION_B, this._keyPair,this._publicKeyClient,this._symmetricKey);
			msg = post.ProcesarEnvioConexion();

			//Creo el cliente y le envio el mensaje
			Client c = new Client();
            bool conexion = c.comprobarConexion(Constants.GATEKEEPER);
            if (conexion)
            {
                response = c.StartClient(msg, Constants.GATEKEEPER);

                //Proceso la respuesta
                post.ProcesarRespuestaConexion(response);

                this._connection = new PipeMessage(this._auxiliarConexion.MessageRequest,this._auxiliarConexion.MessageResponse,post.MessageRequest, post.MessageResponse, post.PublicKeyClient, this._symmetricKey);
                return true;
            }else{
                return false;
            }
		}

		/*
         * Método para la solicitud del esquema
         * */
		protected void OnButtonEsquemaClicked(object sender, EventArgs e)
		{
			if (this._schema.MessageRequest == null)
			{
                if (solicitarEsquema())
                    this._typeOutPut = Constants.MENSAJE_ESQUEMA;
                else
                    this._typeOutPut = Constants.ERROR_CONNECTION;
            }else{
                this._typeOutPut = Constants.MENSAJE_ESQUEMA;
            }

			this.Destroy();
		}

		/*
         * Método privado para solicitar esquema
         * */
		private bool solicitarEsquema()
		{
			string msg, response;

			//Proceso el envio
            PostBox post = new PostBox(this._source, this._destination, Constants.MENSAJE_ESQUEMA, this._db_name, this._db_type, this._body, this._connection.SymmetricKey);
			msg = post.ProcesarEnvio();

			//Creo el cliente y le envio el mensaje
			Client c = new Client();
            bool conexion = c.comprobarConexion(Constants.GATEKEEPER);
            if (conexion)
            {
                response = c.StartClient(msg, Constants.GATEKEEPER);

                //Proceso la respuesta      
                post.ProcesarRespuesta(response);

                this._schema.MessageRequest = post.MessageRequest;
                this._schema.MessageResponse = post.MessageResponse;
                return true;
            }else{
                return false;
            }
		}

        /*
         * Método para realizar consulta SELECT
         */
        protected void OnButtonSelectClicked(object sender, EventArgs e)
        {
			string msg, response;
            this.Hide();
            if (this._db_type == Constants.MYSQL)
            {
                BodyRespuesta002MySQL schema = new BodyRespuesta002MySQL(this._schema.MessageResponse.Body.InnerXml);

                this._selectDialog = new SelectDialog(this._destination, this._db_name, schema);
                this._selectDialog.Run();

                switch (this._selectDialog.TypeOutPut)
                {
                    case Constants.CANCEL:

                        break;
                    case Constants.MENSAJE_CONSULTA:
                        XmlNode bodyMessage = (XmlNode)this._selectDialog.Body;
                        PostBox post = new PostBox(this._source, this._destination, Constants.MENSAJE_CONSULTA, this._db_name, this._db_type, bodyMessage, this._connection.SymmetricKey);
                        msg = post.ProcesarEnvio();

                        //Creo el cliente y le envio el mensaje
                        Client c = new Client();
                        bool conexion = c.comprobarConexion(Constants.GATEKEEPER);
                        if (conexion)
                        {
                            response = c.StartClient(msg, Constants.GATEKEEPER);

                            //Proceso la respuesta

                            post.ProcesarRespuesta(response);

                            this._select.MessageRequest = post.MessageRequest;
                            this._select.MessageResponse = post.MessageResponse;

                            this._typeOutPut = Constants.MENSAJE_CONSULTA;
                        }else{
                            this._typeOutPut = Constants.ERROR_CONNECTION;   
                        }

                        break;

                    case Constants.ERROR_CONNECTION:

                        this._typeOutPut = Constants.ERROR_CONNECTION;

                        break;
                }

            }   //Caso MongoDB
            else if(this._db_type == Constants.MONGODB)
            {
                BodyRespuesta002MongoDB schema = new BodyRespuesta002MongoDB(this._schema.MessageResponse.Body.InnerXml);

                this._findDialog = new FindDialog(this._destination, this._db_name, schema);
                this._findDialog.Run();

                    switch (this._findDialog.TypeOutPut)
				{
                    case Constants.CANCEL:

						break;

                    case Constants.MENSAJE_CONSULTA:
                        XmlNode bodyMessage = (XmlNode)this._findDialog.Body;
                        PostBox post = new PostBox(this._source, this._destination, Constants.MENSAJE_CONSULTA, this._db_name, this._db_type, bodyMessage, this._connection.SymmetricKey);
						msg = post.ProcesarEnvio();

						//Creo el cliente y le envio el mensaje
						Client c = new Client();
                        bool conexion = c.comprobarConexion(Constants.GATEKEEPER);
                        if (conexion)
                        {
                            response = c.StartClient(msg, Constants.GATEKEEPER);

                            //Proceso la respuesta

                            post.ProcesarRespuesta(response);

                            this._select.MessageRequest = post.MessageRequest;
                            this._select.MessageResponse = post.MessageResponse;

                            this._typeOutPut = Constants.MENSAJE_CONSULTA;
                        }else{
                            this._typeOutPut = Constants.ERROR_CONNECTION;
                        }
						break;

                    case Constants.ERROR_CONNECTION:    

                        this._typeOutPut = Constants.ERROR_CONNECTION;

                        break;
				}
            }

            this.Destroy();
        }

    }
}
    