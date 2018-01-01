using System.Xml;
using System.Security.Cryptography;
using System.Text;
using System;

using CriptoLibrary;

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;

namespace MessageLibrary 
{
	public class Message
	{
		private string _destination;
		private string _source;
		private string _messageType;
		private string _db_name;
        private string _db_type;
        private XmlNode _body;

		public string Destination
		{
			get
			{
                return this._destination;
			}
			set
			{
				this._destination = value;
			}
		}
		public string Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}
		public string MessageType
		{
			get
			{
				return this._messageType;
			}
			set
			{
				this._messageType = value;
			}
		}
		public string Db_name
		{
			get
			{
				return this._db_name;
			}
			set
			{
				this._db_name = value;
			}
		}
		public string Db_type
		{
			get
			{
				return this._db_type;
			}
			set
			{
				this._db_type = value;
			}
		}
		public XmlNode Body
		{
			get
			{
                return this._body;
			}
			set
			{
				this._body = value;
			}
		}


        public void parserStartRecievedMessage(XmlDocument doc)
        {
			this._messageType = doc.DocumentElement.GetElementsByTagName("message_type")[0].InnerText;
			this._source = doc.DocumentElement.GetElementsByTagName("source")[0].InnerText;
			this._destination = doc.DocumentElement.GetElementsByTagName("destination")[0].InnerText;
           /* if(this._messageType == "001")
                this._key = SymmetricAlgorithm.Create(doc.DocumentElement.GetElementsByTagName("key")[0].InnerText);*/
        }


        /**
         * Método para parsear un mensaje recibido como un XmlDocument
         * */
        public void parserMessageRecieve(XmlDocument doc)
        {
                      
			//Si existe, guardamos el tipo de la base de datos
			if (doc.DocumentElement.GetElementsByTagName("db_type").Count > 0)
                this._db_type = doc.DocumentElement.GetElementsByTagName("db_type")[0].InnerText;
		    
            //Si existe, guardamos el nombre de la base de datos
            if (doc.DocumentElement.GetElementsByTagName("db_name").Count > 0)
                this._db_name = doc.DocumentElement.GetElementsByTagName("db_name")[0].InnerText;

            this._body = doc.DocumentElement.GetElementsByTagName("body")[0];
        }
        /*
         * Método para al creación de un XmlDocument a partir del mensaje
         * */
        public XmlDocument createMessage()
        {
            XmlDocument xmlDoc = new XmlDocument();
			XmlElement root = xmlDoc.DocumentElement;

            //Creamos elemento root
			XmlElement elementRoot = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(elementRoot);

			//Creamos el elemento tipoDeMensaje
			XmlNode message_type = xmlDoc.CreateElement("message_type");
			message_type.InnerText = this._messageType;
			elementRoot.AppendChild(message_type);

            //Creamos elemento origen
            XmlNode source = xmlDoc.CreateElement("source");
            source.InnerText = this._source;
            elementRoot.AppendChild(source);

            //Creamos el elemento destino
            XmlNode destination = xmlDoc.CreateElement("destination");
			destination.InnerText = this._destination;
			elementRoot.AppendChild(destination);

            //Creamos el elemento encripted
            XmlNode encripted = xmlDoc.CreateElement("encripted");
            elementRoot.AppendChild(encripted);

            //Creamos el elemento nombreBBDD
			XmlNode db_name = xmlDoc.CreateElement("db_name");
            db_name.InnerText = this._db_name;
            encripted.AppendChild(db_name);

            //Creamos el elemento tipoDeBBDD
			XmlNode db_type = xmlDoc.CreateElement("db_type");
            db_type.InnerText = this._db_type;
            encripted.AppendChild(db_type);

            //Creamos el elemento Cuerpo
			XmlNode body = xmlDoc.CreateElement("body");
            if (this._body == null || this._body.InnerXml == "")
			{
				encripted.AppendChild(body);
			}
			else
			{
                body.InnerXml = this._body.InnerXml;

                encripted.AppendChild(body);
            }

			return xmlDoc;
        }

        /*
         * Método para al creación de un XmlDocument a partir del mensaje de conexion
         * */
        public XmlDocument createMessageConexion(Cripto keyPair)
        {

			XmlDocument xmlDoc = new XmlDocument();
			XmlElement root = xmlDoc.DocumentElement;

			//Creamos elemento root
			XmlElement elementRoot = xmlDoc.CreateElement("root");
			xmlDoc.AppendChild(elementRoot);

			//Creamos el elemento tipoDeMensaje
			XmlNode message_type = xmlDoc.CreateElement("message_type");
			message_type.InnerText = this._messageType;
			elementRoot.AppendChild(message_type);

			//Creamos elemento origen
			XmlNode source = xmlDoc.CreateElement("source");
			source.InnerText = this._source;
			elementRoot.AppendChild(source);

			//Creamos el elemento destino
			XmlNode destination = xmlDoc.CreateElement("destination");
			destination.InnerText = this._destination;
			elementRoot.AppendChild(destination);

			//Creamos el elemento destino
			XmlNode key = xmlDoc.CreateElement("key");

            key.InnerText = keyPair.PublicKeyString();
			elementRoot.AppendChild(key);

			return xmlDoc;
        }

		/*
         * Método para al creación de un XmlDocument a partir del mensaje de conexion
         * */
        public XmlDocument createMessageConexion(Cripto keyPair,SymmetricAlgorithm symmetricKey)
		{

			XmlDocument xmlDoc = new XmlDocument();
			XmlElement root = xmlDoc.DocumentElement;

			//Creamos elemento root
			XmlElement elementRoot = xmlDoc.CreateElement("root");
			xmlDoc.AppendChild(elementRoot);

			//Creamos el elemento tipoDeMensaje
			XmlNode message_type = xmlDoc.CreateElement("message_type");
			message_type.InnerText = this._messageType;
			elementRoot.AppendChild(message_type);

			//Creamos elemento origen
			XmlNode source = xmlDoc.CreateElement("source");
			source.InnerText = this._source;
			elementRoot.AppendChild(source);

			//Creamos el elemento destino
			XmlNode destination = xmlDoc.CreateElement("destination");
			destination.InnerText = this._destination;
			elementRoot.AppendChild(destination);

			//Creamos el elemento encripted
			XmlNode encripted = xmlDoc.CreateElement("encripted");
			elementRoot.AppendChild(encripted);

			//Creamos el elemento key
			XmlNode key = xmlDoc.CreateElement("key");
			key.InnerText = Convert.ToBase64String(symmetricKey.Key);
            encripted.AppendChild(key);


			XmlNode iv = xmlDoc.CreateElement("IV");
			iv.InnerText = Convert.ToBase64String(symmetricKey.IV);

            encripted.AppendChild(iv);

			return xmlDoc;
		}

        /*
         * Sirve para realizar una respuesta. Una ayuda para ello
         * */
        public static Message respuestaAotro(Message recibido)
        {
            Message contestacion = new Message();

            //Intercambiamos el destino y el origen
            contestacion.Destination = recibido.Source;
            contestacion.Source = recibido.Destination;
            string typeMessage = "";
			
            //Según el tipodeMensaje recibido tendremos un tipodeMensaje de contestación
            switch (recibido.MessageType)
			{
				case ("001"):
                    typeMessage = "004";
					break;
				case ("002"):
					typeMessage = "005";
					break;
				case ("003"):
					typeMessage = "006";
					break;
			}
            contestacion.MessageType = typeMessage;

            //Para que no sean null
            contestacion.Db_name = recibido.Db_name;
            contestacion.Db_type = recibido.Db_type;
            contestacion.Body = null;

            return contestacion;
        }
	}
}

