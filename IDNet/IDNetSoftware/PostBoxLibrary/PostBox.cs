using System;
using System.IO;
using System.Text;

using MessageLibraryS;
using ProcessLibraryS;
using ConvertionLibraryS;
using CriptoLibraryS;
using ConstantsLibraryS;

using System.Xml;
using System.Security.Cryptography;
using System.Collections.Generic;

using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;

namespace PostBoxLibraryS
{
    public class PostBox
    {
        //Mensaje de solitud
        Message _messageRequest;

        //Proceso
        Process _process;

        //Mensaje de respuesta
        Message _messageResponse;

        //Clave pública-privada
        Cripto _keyPair;

        RsaKeyParameters _publicKeyClient;

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
		public RsaKeyParameters PublicKeyClient
		{
			get
			{
                return this._publicKeyClient;
			}
		}

		public PostBox(string source, string destination, string tipoMensaje, string db_name,
					  string db_type, XmlNode body, Cripto keyPair,RsaKeyParameters publicKeyClient)
		{
			this._process = new Process();
			this._messageRequest = new Message(source, destination, tipoMensaje, db_name, db_type, body);
			this._messageResponse = new Message();
            this._keyPair = keyPair;
            this._publicKeyClient = publicKeyClient;
		}

        //Constructor para conexión
        public PostBox(string source, string destination, string tipoMensaje, Cripto keyPair)
		{
			this._process = new Process();
			this._messageRequest = new Message(source, destination, tipoMensaje,null);
			this._messageResponse = new Message();
            this._keyPair = keyPair;
		}

		/*
        * Realizamos el envio
        */
		public string ProcesarEnvio()
		{
			XmlDocument doc = this._messageRequest.createMessage();
            doc = encriptarParteDelDocumento(doc);
            string respuesta = doc.InnerXml;

			return respuesta;
		}

		/*
        * Realizamos el envio de Conexión
        */
		public string ProcesarEnvioConexion()
		{
            //Aqui falta llamar a la funcion de generar clave
            XmlDocument doc = this._messageRequest.createMessageConnection(this._keyPair.PublicKeyString());
			return doc.InnerXml;
		}

		/**
         * Procesamos la respuesta
         * */
		public void ProcesarRespuesta(string response)
		{
			//Convertimos el string a xml
			XmlDocument xmlDoc = Convertion.stringToXml(response);


            //Clave para desencriptar
            //RijndaelManaged key = new RijndaelManaged();

            //Desencriptamos el mensaje
            //Security.Decrypt(xmlDoc, key);
            DesencriptarParteDelDocumento(xmlDoc);

			//Parseamos el mensaje
			this._messageResponse.parserMessageRecieve(xmlDoc);
		}

		/**
     * Procesamos la respuesta de conexión
     * */
		public void ProcesarRespuestaConexion(string response)
		{
			//Convertimos el string a xml
			XmlDocument xmlDoc = Convertion.stringToXml(response);


			//Clave para desencriptar
			//RijndaelManaged key = new RijndaelManaged();

			//Desencriptamos el mensaje
			//Security.Decrypt(xmlDoc, key);

			//Parseamos el mensaje
			this._messageResponse.parserMessageRecieve(xmlDoc);

            AlmacenarClavePublica(xmlDoc);
		}

        private void AlmacenarClavePublica(XmlDocument xmlDoc)
        {
			string path = Constants.CONFIG + "publicKey" + this._messageResponse.Source + ".pem";
			if(!File.Exists(path))
            {
				string publicKey = xmlDoc.DocumentElement.GetElementsByTagName("key")[0].InnerText;
				File.WriteAllText(path, publicKey);
			}
			this._publicKeyClient = Cripto.ImportPublicKey(path);
		}

        private XmlDocument encriptarParteDelDocumento(XmlDocument doc)
        {
            string xmlAEncriptar = doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml;

            string xmlEncriptado = Cripto.Encryption(xmlAEncriptar,this._publicKeyClient);

            doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml = xmlEncriptado;

            return doc;
        }

		private XmlDocument DesencriptarParteDelDocumento(XmlDocument doc)
		{
			string xmlADesencriptar = doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml;

            string xmlDesencriptado = Cripto.Decryption(Convert.FromBase64String(xmlADesencriptar), this._keyPair.PrivateKey);

			doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml = xmlDesencriptado;

			return doc;
		}

	}
}