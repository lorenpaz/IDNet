﻿using System;
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
    public partial class ConnectionDialog : Gtk.Dialog
    {
        //Pipes de mensajes
        private PipeMessage _connection;
        private PipeMessage _schema;
		private PipeMessage _select;

        //Atributos para la construcción de los mensajes
        private string _destination;
        private string _db_name;
        private string _db_type;
        private XmlNode _body;

        //Atributo para cifrado asimétrico
        private Cripto _keyPair;

        private RsaKeyParameters _publicKeyClient;

        //Atributo para cifrado simétrico
        private SymmetricAlgorithm _symmetricKey;

        //Atributo para saber cómo ha cerrado el diálogo
        private string _typeOutPut;

        //Atributo del diálogo de consulta Select
        private SelectDialog _selectDialog;

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
        public ConnectionDialog(string destination, string db_type, string db_name, Cripto keyPair)
        {
            this.Build();

            this._destination = destination;
            this._db_name = db_name;
            this._db_type = db_type;
            this._body = new XmlDocument();
            this._typeOutPut = "Cancel";
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
        public ConnectionDialog(string destination, string db_type, string db_name, 
                                PipeMessage pipeConexion,Cripto keyPair)
		{
			this.Build();

			this._destination = destination;
			this._db_name = db_name;
			this._db_type = db_type;
			this._body = new XmlDocument();
			this._typeOutPut = "Cancel";
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
		public ConnectionDialog(string destination, string db_type, string db_name,
                                PipeMessage pipeConexion, PipeMessage pipeSchema,Cripto keyPair)
		{
			this.Build();

			this._destination = destination;
			this._db_name = db_name;
			this._db_type = db_type;
			this._body = new XmlDocument();
			this._typeOutPut = "Cancel";
            this._keyPair = keyPair;

			this._connection = pipeConexion;
			this._schema = pipeSchema;

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

            conexionPrimera();

            conexionSegunda();

            this._typeOutPut = "001";

            this.Destroy();
        }

        private void conexionPrimera()
        {
			string msg, response;

			PostBox post = new PostBox("Lorenzo", this._destination, "001a", this._keyPair);
			msg = post.ProcesarEnvioConexion();

			//Creo el cliente y le envio el mensaje
			Client c = new Client();
			response = c.StartClient(msg, "localhost");

			//Proceso la respuesta
			post.ProcesarRespuestaConexion(response);

            this._publicKeyClient = post.PublicKeyClient;
		}

        private void conexionSegunda()
        {
			string msg, response;

            PostBox post = new PostBox("Lorenzo", this._destination, "001b", this._keyPair,this._publicKeyClient,this._symmetricKey);
			msg = post.ProcesarEnvioConexion();

			//Creo el cliente y le envio el mensaje
			Client c = new Client();
			response = c.StartClient(msg, "localhost");

			//Proceso la respuesta
			post.ProcesarRespuestaConexion(response);

			this._connection = new PipeMessage(post.MessageRequest, post.MessageResponse, post.PublicKeyClient, this._symmetricKey);
		}

		/*
         * Método para la solicitud del esquema
         * */
		protected void OnButtonEsquemaClicked(object sender, EventArgs e)
		{
			if (this._schema.MessageRequest == null)
			{
				solicitarEsquema();
			}
			this._typeOutPut = "002";

			this.Destroy();
		}

		/*
         * Método privado para solicitar esquema
         * */
		private void solicitarEsquema()
		{
			string msg, response;

			//Proceso el envio
			PostBox post = new PostBox("Lorenzo", this._destination, "002", this._db_name, this._db_type, this._body, this._connection.SymmetricKey);
			msg = post.ProcesarEnvio();

			//Creo el cliente y le envio el mensaje
			Client c = new Client();
			response = c.StartClient(msg, "localhost");

			//Proceso la respuesta
			post.ProcesarRespuesta(response);

			this._schema.MessageRequest = post.MessageRequest;
			this._schema.MessageResponse = post.MessageResponse;
		}

        /*
         * Método para realizar consulta SELECT
         */
        protected void OnButtonSelectClicked(object sender, EventArgs e)
        {
			string msg, response;

            BodyRespuesta002MySQL schema = new BodyRespuesta002MySQL(this._schema.MessageResponse.Body.InnerXml);

			this._selectDialog = new SelectDialog(this._destination, this._db_type, this._db_name,schema);
            this._selectDialog.Run();

            switch (this._selectDialog.TypeOutPut)
            {
                case "Cancel":

                    break;
                case "003":
                    XmlNode bodyMessage = this._selectDialog.Body.GetElementsByTagName("body")[0];
                    PostBox post = new PostBox("Lorenzo", this._destination, "003", this._db_name, this._db_type, bodyMessage,this._connection.SymmetricKey);
					msg = post.ProcesarEnvio();

					//Creo el cliente y le envio el mensaje
					Client c = new Client();
					response = c.StartClient(msg, "localhost");

					//Proceso la respuesta
					post.ProcesarRespuesta(response);

					this._typeOutPut = "003";
                    break;
            }

            this.Destroy();
        }

    }
}
    