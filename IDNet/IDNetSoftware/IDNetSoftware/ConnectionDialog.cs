using System;

using MessageLibraryS;
using ConnectionLibraryS;
using PostBoxLibraryS;

namespace IDNetSoftware
{
    public partial class ConnectionDialog : Gtk.Dialog
    {
        private Message _messageRequest;
        private Message _messageResponse;
        private string _destination;
        private string _db_name;
        private string _db_type;
        private string _body;

		public Message MessageRequest
		{
			get
			{
				return this._messageRequest;
			}
		}
		public Message MessageResponse
		{
			get
			{
				return this._messageResponse;
			}
		}

        public ConnectionDialog(string destination, string db_type, string db_name)
        {
            this.Build();

            this._destination = destination;
            this._db_name = db_name;
            this._db_type = db_type;
            this._body = "";

        }


        protected void OnButtonEsquemaClicked(object sender, EventArgs e)
        {
            solicitarEsquema();
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
            PostBox post = new PostBox("Lorenzo",this._destination, "002",this._db_name,this._db_type,this._body);
			msg = post.ProcesarEnvio();

			//Creo el cliente y le envio el mensaje
			Client c = new Client();
			response = c.StartClient(msg, "localhost");

			//Proceso la respuesta
			post.ProcesarRespuesta(response);

			this._messageRequest = post.MessageRequest;
			this._messageResponse = post.MessageResponse;
		}
    }
}
