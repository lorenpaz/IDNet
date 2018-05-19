using System;
using System.Xml;
using System.Security.Cryptography;
 

namespace MessageLibraryS
{
    /*
     * Clase con información de los mensajes
     * */
	public class Message
	{
		private string _destination;
		private string _source;
		private string _messageType;
		private string _db_name;
		private string _db_type;
		private XmlNode _body;
        private string _keyPair;

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
        public string KeyPair
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

        public Message()
        {
            
        }

        /*
         * Constructor
         * */
        public Message(string source, string destination,string messageType,string db_name,
                       string db_type, XmlNode body)
        {
            this._source = source;
            this._destination = destination;
            this._body = body;
            this._messageType = messageType;
            this._db_name = db_name;
            this._db_type = db_type;
        }

        //Constructor para mensaje de conexión
        public Message(string source, string destination, string messageType)
		{
			this._source = source;
			this._destination = destination;
			this._messageType = messageType;
		}

		/**
         * Método para parsear un mensaje recibido como un XmlDocument
         * */
		public void parserMessageRecieve(XmlDocument doc)
		{
			//Cogemos el destinatario del mensaje
			this._destination = doc.DocumentElement.GetElementsByTagName("destination")[0].InnerText;

			//Cogemos el origen del mensaje
			this._source = doc.DocumentElement.GetElementsByTagName("source")[0].InnerText;

			//Cogemos el código del mensaje
			this._messageType = doc.DocumentElement.GetElementsByTagName("message_type")[0].InnerText;

			//Si existe, guardamos el tipo de la base de datos
			if (doc.DocumentElement.GetElementsByTagName("db_type").Count > 0)
				this._db_type = doc.DocumentElement.GetElementsByTagName("db_type")[0].InnerText;

			//Si existe, guardamos el nombre de la base de datos
			if (doc.DocumentElement.GetElementsByTagName("db_name").Count > 0)
				this._db_name = doc.DocumentElement.GetElementsByTagName("db_name")[0].InnerText;

            //Si existe, guardamos keypair de la base de datos
            if (doc.DocumentElement.GetElementsByTagName("key").Count > 0)
                this._keyPair = doc.DocumentElement.GetElementsByTagName("key")[0].InnerText;
            
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
            if(this._body.InnerXml == "")
            {
                encripted.AppendChild(body);
            }
            else if(this._messageType == "003") {
                body.InnerXml = this._body.InnerXml;
                encripted.AppendChild(body);
            }else{
                body = this._body;
                encripted.AppendChild(this._body);
            }

			return xmlDoc;
		}

		/*
         * Método para al creación de un XmlDocument a partir del mensaje
         * */
		public XmlDocument createMessageConnection(string keyPair)
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

			//Creamos el elemento nombreBBDD
			XmlNode key = xmlDoc.CreateElement("key");
            key.InnerText = keyPair;
            this._keyPair = keyPair;
			elementRoot.AppendChild(key);

			return xmlDoc;
		}

        /*
         * Mensaje para la creación de un mesaje para solicitar los vecinos
         * */
        public XmlDocument createMessageNeighbour(string ip)
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

            //Creamos el elemento IP
            XmlNode ipNode = xmlDoc.CreateElement("ip");
            ipNode.InnerText = ip;
            elementRoot.AppendChild(ipNode);

            //Creamos el elemento destino
            XmlNode destination = xmlDoc.CreateElement("destination");
            destination.InnerText = this._destination;
            elementRoot.AppendChild(destination);

            return xmlDoc;
        }


		/*
         * Método para la creación de un XmlDocument a partir del mensaje
         * */
        public XmlDocument createMessageConnection(string keyPair,SymmetricAlgorithm symmetricKey)
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

            //Creamos el elemento IV
			XmlNode iv = xmlDoc.CreateElement("IV");
            iv.InnerText = Convert.ToBase64String(symmetricKey.IV);

            encripted.AppendChild(iv);

			return xmlDoc;
		}
	}
}

