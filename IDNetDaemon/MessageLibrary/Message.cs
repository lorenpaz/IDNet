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

        public Message (XmlDocument doc)
		{
            parserMessage(doc);
		}

        private void parserMessage(XmlDocument doc)
        {
            //Cogemos el destinatario del mensaje
            _destination = doc.DocumentElement.GetElementsByTagName("destination")[0].InnerText;
            //Cogemos el origen del mensaje
            _source = doc.DocumentElement.GetElementsByTagName("source")[0].InnerText;
            //Cogemos el path donde guardamos temporalmente el xml
            _file_path = Constants.TEMPORAL_FILE_PATH + doc.DocumentElement.GetElementsByTagName("seq_id")[0].InnerText;
            //Cogemos el código del mensaje
            _messageType = doc.DocumentElement.GetElementsByTagName("message_type")[0].InnerText;

			//Si existe, guardamos el tipo de la base de datos
			if (doc.DocumentElement.GetElementsByTagName("db_type").Count > 0)
				_db_name = doc.DocumentElement.GetElementsByTagName("db_type")[0].InnerText;
		    
            //Si existe, guardamos el nombre de la base de datos
            if (doc.DocumentElement.GetElementsByTagName("db_name").Count > 0)
                _db_name = doc.DocumentElement.GetElementsByTagName("db_name")[0].InnerText;

            _body = doc.DocumentElement.GetElementsByTagName("data")[0].InnerXml;
        }
	}
}

