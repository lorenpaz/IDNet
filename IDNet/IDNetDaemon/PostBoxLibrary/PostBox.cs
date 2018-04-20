using System;
using System.Net;
using System.Text;

using System.Xml;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.IO;

using CriptoLibrary;
using MessageLibrary;
using ProcessLibrary;
using ConvertionLibrary;
using ConstantsLibrary;

using log4net;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;

namespace PostBoxLibrary
{
    public class PostBox
    {
        static readonly ILog log = LogManager.GetLogger(typeof(PostBox));

		//Mensaje recibido
		Message _messageRecieve;

        //Proceso
        Process _process;

        //Mensaje de respuesta
        Message _messageResponse;

        //Clave publica-privada
        Cripto _keyPair;

        //Clave publica del cliente
		RsaKeyParameters _publicKeyClient;

        //Clave simétrica
        SymmetricAlgorithm _symmetricKey;

		public RsaKeyParameters PublicKeyClient
		{
			get
			{
                return this._publicKeyClient;
			}
            set
            {
                this._publicKeyClient = value; 
            }
		}

		public Message MessageRecieve
		{
			get
			{
                return this._messageRecieve;
			}
			set
			{
                this._messageRecieve = value;
			}
		}
		public Message MessageResponse
		{
			get
			{
				return this._messageResponse;
			}
			set
			{
                this._messageResponse = value;
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
         * Constructor
         * */
		public PostBox(Cripto keyPair)
        {
            this._process = new Process();
			this._messageRecieve = new Message();
            this._messageResponse = new Message();
            this._keyPair = keyPair;
        }

        /*
         * Método principal para procesar una respuesta
         * */
        public string procesar(string document,Dictionary<string, Tuple<RsaKeyParameters,SymmetricAlgorithm>> keyPairClients)
        {
            //Convertimos el string a xml
            XmlDocument xmlDoc = Convertion.stringToXml(document);

            string respuesta = "";

            this._messageRecieve.parserStartRecievedMessage(xmlDoc);

            if (this._messageRecieve.MessageType == Constants.CONEXION_A)
            {
                AlmacenarClavePublica(xmlDoc);
				//Aquí iria una funcion para quitar el cifrado asimetrico
				// cript.CheckKey(this._messageRecieve.Source, this._messageRecieve.Key);
				respuesta = responderConexion();
            }
            else if(this._messageRecieve.MessageType == Constants.CONEXION_B)
            {
                this._publicKeyClient = keyPairClients[this._messageRecieve.Source].Item1;
                DesencriptarParteDelDocumentoAsimetrico(xmlDoc);
                AlmacenarClaveSimetrica(xmlDoc);
                respuesta = responderConexion();
            }
            else
            {
                string usuario = this._messageRecieve.Source;
                this._publicKeyClient = keyPairClients[usuario].Item1;
                this._symmetricKey = keyPairClients[usuario].Item2;

                //Desencriptamos
                xmlDoc = DesencriptarParteDelDocumentoSimetrico(xmlDoc);

                //Parseamos el mensaje
				this._messageRecieve.parserMessageRecieve(xmlDoc);
				
                //Ejecutamos el proceso
				XmlDocument xmlDocResponse = this._process.ejecutar(this._messageRecieve);

				//Creamos la respuesta
				respuesta = responder(xmlDocResponse);
            }

            //Mostramos la respuesta en el log
            log.Info("\n"+respuesta+"\n");

            return respuesta;
		}

        /*
         * Método privado para responder un mensaje
         * */
        private string responder(XmlDocument doc)
        {
            string respuesta="";

            //Empezamos a realizar el mensaje de respuesta
            this._messageResponse = Message.respuestaAotro(this._messageRecieve);

            //Si hay cuerpo de la respuesta
            if(doc != null){
                this._messageResponse.Body = doc as XmlNode;
            }

            //Creamos un XMLDocument con el mensaje de respuesta
            XmlDocument xmlDocRespuesta = this._messageResponse.createMessage();

            //Devolvemos la respesta en forma de string
            xmlDocRespuesta = encriptarParteDelDocumentoSimetrico(xmlDocRespuesta);
            respuesta = xmlDocRespuesta.InnerXml;

            return respuesta;
        }

        /*
         * Método privado para responder un mensaje de conexión
         * */
        private string responderConexion()
        {
            this._messageResponse.Source = this._messageRecieve.Destination;
            this._messageResponse.Destination = this._messageRecieve.Source;
            if(this._symmetricKey == null)
            {
                this._messageResponse.MessageType = Constants.ACKCONEXION_A;

				XmlDocument xmlDocRespuesta = this._messageResponse.createMessageConexion(this._keyPair);

				return xmlDocRespuesta.InnerXml;

            }else{
                this._messageResponse.MessageType = Constants.ACKCONEXION_B;

                XmlDocument xmlDocBodyRespuesta = this._process.ejecutar(this.MessageRecieve);

                //Si hay cuerpo de la respuesta
                if (xmlDocBodyRespuesta != null)
                {
                    this._messageResponse.Body = xmlDocBodyRespuesta as XmlNode;
                }
                XmlDocument xmlDocRespuesta = this._messageResponse.createMessageConexion();
                encriptarParteDelDocumentoAsimetrico(xmlDocRespuesta);
				return xmlDocRespuesta.InnerXml;
            }

        }

        /*
         * Método privado para almacenar la clave pública
         * */
		private void AlmacenarClavePublica(XmlDocument xmlDoc)
		{
            string path = Constants.PathClavePublica(this.MessageRecieve.Source);
            string publicKey = xmlDoc.DocumentElement.GetElementsByTagName("key")[0].InnerText;
            File.WriteAllText(path, publicKey);
            this._publicKeyClient = Cripto.ImportPublicKey(path);
		}

        /*
         * Método privado para almacenar la clave simétrica
         * */
		private void AlmacenarClaveSimetrica(XmlDocument xmlDoc)
		{
            Rijndael symmetricKey = new RijndaelManaged();
            symmetricKey.Key =  Convert.FromBase64String(xmlDoc.DocumentElement.GetElementsByTagName("key")[0].InnerText);
            symmetricKey.IV = Convert.FromBase64String(xmlDoc.DocumentElement.GetElementsByTagName("IV")[0].InnerText);
            this._symmetricKey = symmetricKey;
        }

        /*
         * Método para desencriptar parte del documento de forma asimétrica
         * */
		private XmlDocument DesencriptarParteDelDocumentoAsimetrico(XmlDocument doc)
		{
			string xmlADesencriptar = doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml;

			string xmlDesencriptado = Cripto.Decryption(Convert.FromBase64String(xmlADesencriptar), this._keyPair.PrivateKey);

            doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml = xmlDesencriptado;

			return doc;
		}

        /*
         * Método para encriptar parte del documento de forma asimétrica
         * */
		private XmlDocument encriptarParteDelDocumentoAsimetrico(XmlDocument doc)
		{
			string xmlAEncriptar = doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml;

            try
            {
                string xmlEncriptado = Cripto.Encryption(xmlAEncriptar, this._publicKeyClient);

                doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml = xmlEncriptado;

			}catch(Exception e){
                log.Info("Error producido:"+e.Message);
            }

			return doc;
		}

        /*
         * Método para desencriptar parte del documento de forma simétrica
         * */
		private XmlDocument DesencriptarParteDelDocumentoSimetrico(XmlDocument doc)
		{

            try
            {
                Cripto.DecryptSymmetric(doc, this._symmetricKey);
            }catch(Exception e)
            {
                log.Info("Error producido:"+e.Message);
            }

			return doc;
		}

        /*
         * Método para encriptar parte del documento de forma simétrica
         * */
		private XmlDocument encriptarParteDelDocumentoSimetrico(XmlDocument doc)
		{
            log.Info(doc.InnerXml);
			Cripto.EncryptSymmetric(doc, "encripted", this._symmetricKey);
            log.Info(doc.InnerXml);
            return doc;
		}

	}
}
