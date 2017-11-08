using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;

using ConstantsLibrary;
using PluginsLibrary;


namespace MessageLibrary 
{
	public class Message
	{
		private string _destination;
		private string _source;
		private string _messageType;
		private string _file_path;
		private string _db_name;
        private string _db_type;
        private string _body;

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
		public string Body
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


        /**
         * Método para parsear un mensaje recibido como un XmlDocument
         * */
        public void parserMessageRecieve(XmlDocument doc)
        {
            //Cogemos el destinatario del mensaje
            this._destination = doc.DocumentElement.GetElementsByTagName("destination")[0].InnerText;

            //Cogemos el origen del mensaje
            this._source = doc.DocumentElement.GetElementsByTagName("source")[0].InnerText;

            //Cogemos el path donde guardamos temporalmente el xml
            //this._file_path = Constants.TEMPORAL_FILE_PATH + doc.DocumentElement.GetElementsByTagName("seq_id")[0].InnerText;

            //Cogemos el código del mensaje
            this._messageType = doc.DocumentElement.GetElementsByTagName("message_type")[0].InnerText;

			//Si existe, guardamos el tipo de la base de datos
			if (doc.DocumentElement.GetElementsByTagName("db_type").Count > 0)
				this._db_name = doc.DocumentElement.GetElementsByTagName("db_type")[0].InnerText;
		    
            //Si existe, guardamos el nombre de la base de datos
            if (doc.DocumentElement.GetElementsByTagName("db_name").Count > 0)
                this._db_name = doc.DocumentElement.GetElementsByTagName("db_name")[0].InnerText;

            this._body = doc.DocumentElement.GetElementsByTagName("body")[0].InnerXml;
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
			
            //Creamos elemento origen
            XmlNode source = xmlDoc.CreateElement("source");
            source.InnerText = this._source;
            xmlDoc.AppendChild(source);

            //Creamos el elemento destino
            XmlNode destination = xmlDoc.CreateElement("destination");
			destination.InnerText = this._destination;
			xmlDoc.AppendChild(destination);

            //Creamos el elemento tipoDeMensaje
			XmlNode message_type = xmlDoc.CreateElement("message_type");
            message_type.InnerText = this._messageType;
            xmlDoc.AppendChild(message_type);

            //Creamos el elemento nombreBBDD
			XmlNode db_name = xmlDoc.CreateElement("db_name");
            db_name.InnerText = this._db_name;
            xmlDoc.AppendChild(db_name);

            //Creamos el elemento tipoDeBBDD
			XmlNode db_type = xmlDoc.CreateElement("db_type");
            db_type.InnerText = this._db_type;
            xmlDoc.AppendChild(db_type);

            //Creamos el elemento Cuerpo
			XmlNode body = xmlDoc.CreateElement("body");
            body.InnerText = this._body;
            xmlDoc.AppendChild(body);

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
            contestacion.Db_name = "";
            contestacion.Db_type = "";
            contestacion.Body = "";

            return contestacion;
        }
	}
}

