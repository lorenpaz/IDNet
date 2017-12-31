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

		RsaKeyParameters _publicKeyClient;

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
		public PostBox(Cripto keyPair)
        {
            this._process = new Process();
			this._messageRecieve = new Message();
            this._messageResponse = new Message();
            this._keyPair = keyPair;
        }

        public string procesar(string document,Dictionary<string, RsaKeyParameters> keyPairClients)
        {
            //Convertimos el string a xml
            XmlDocument xmlDoc = Convertion.stringToXml(document);

            //Recogemos la información de inicio
            //SymmetricAlgorithm simKey;

            this._messageRecieve.parserStartRecievedMessage(xmlDoc);

            if (this._messageRecieve.MessageType == "001")
            {
                AlmacenarClavePublica(xmlDoc);
                //Aquí iria una funcion para quitar el cifrado asimetrico
               // cript.CheckKey(this._messageRecieve.Source, this._messageRecieve.Key);
			}
            else
            {
                
                string usuario = this._messageRecieve.Source;
                this._publicKeyClient = keyPairClients[usuario];
                log.Info("usuario:"+usuario);
                log.Info("insideOKKK");
                    //  simKey = cript.CheckKey(this._messageRecieve.Source);
                //Desencriptamos
                xmlDoc = DesencriptarParteDelDocumento(xmlDoc);

                //Parseamos el mensaje
				this._messageRecieve.parserMessageRecieve(xmlDoc);
            }

            String respuesta="";

			if (!(this._messageRecieve.MessageType == "001"))
            {
				//Ejecutamos el proceso
				XmlDocument xmlDocResponse = this._process.ejecutar(this._messageRecieve);

                //Creamos la respuesta
                respuesta = responder(xmlDocResponse);

            }else{
				respuesta = responderConexion();
                log.Info(respuesta);
            }

            return respuesta;
		}

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
            xmlDocRespuesta = encriptarParteDelDocumento(xmlDocRespuesta);
            respuesta = xmlDocRespuesta.InnerXml;
            log.Info("ya encriptado el xml: "+respuesta);
            return respuesta;
        }

        private string responderConexion()
        {
            this._messageResponse.Source = this._messageRecieve.Destination;
            this._messageResponse.Destination = this._messageRecieve.Source;
            this._messageResponse.MessageType = "004";

			XmlDocument xmlDocRespuesta = this._messageResponse.createMessageConexion(this._keyPair);

            return xmlDocRespuesta.InnerXml;
        }

		private void AlmacenarClavePublica(XmlDocument xmlDoc)
		{
            string path = Constants.CONF + "publicKey" + this.MessageRecieve.Source + ".pem";
            if (!File.Exists(path))
            {
                string publicKey = xmlDoc.DocumentElement.GetElementsByTagName("key")[0].InnerText;
                File.WriteAllText(path, publicKey);
                this._publicKeyClient = Cripto.ImportPublicKey(path);
            }
		}

		private XmlDocument DesencriptarParteDelDocumento(XmlDocument doc)
		{
			string xmlADesencriptar = doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml;

			string xmlDesencriptado = Cripto.Decryption(Convert.FromBase64String(xmlADesencriptar), this._keyPair.PrivateKey);
			log.Info("Ya desencriptado en el demonio:" + xmlDesencriptado);
			doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml = xmlDesencriptado;

			return doc;
		}
		private XmlDocument encriptarParteDelDocumento(XmlDocument doc)
		{
			string xmlAEncriptar = doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml;
			log.Info("ANTES DE ENCRIPTAR:" + xmlAEncriptar);
            try
            {
                string xmlEncriptado = Cripto.Encryption(xmlAEncriptar, this._publicKeyClient);
				log.Info("despues de encriptar:" + xmlEncriptado);
				doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml = xmlEncriptado;

			}catch(Exception e){
                log.Info("mensaje error:"+e.Message);
            }

			return doc;
		}
	}
}
