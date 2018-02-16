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
		private Database _db;

		public Message ()
		{
			_db = new Database();
		}

		//Recibe una petición aceptada y pide la estructura de la BD
		public string EstructureRequest (string databaseType, string databaseName)
		{
            string request="";
			_db.EstructureRequest(databaseType, databaseName);
            return request;
		}

		public void DBListReply ()
		{
			Dictionary<string, List<string>> _databases = _db.Databases;
			//XMLDBListReply(_databases);
		}

		public void ReceiveMessage(string file_path)
		{
			this._file_path = file_path;
		}

	/*	public void XMLDBListReply (Dictionary<string, List<string>> databases)
		{
			//El numero de entradas en el diccionario
			int _sizeDB = databases.Count;
		   
			//Preparamos el escritor de XML
			XmlTextWriter writer;
            writer = new XmlTextWriter(this._file_path, Encoding.UTF8);
	   		writer.Formatting = Formatting.Indented;

			//Abrimos el documento
	   		writer.WriteStartDocument();

			//<destination>***</destination>
			writer.WriteStartElement("destination");
            writer.WriteElementString(this._source);
			writer.WriteEndElement();

			//<source>***</source>
			writer.WriteStartElement ("source");
			writer.WriteElementString(this._destination);
			writer.WriteEndElement();

			/*
			 * <databases>
			 * 		<database>
			 * 			<name>***</name>
			 * 			<type>***</type>
			 * 		</database>
			 * </databases>
			 */
		/*	writer.WriteStartElement ("databases");

            //Por cada clave del diccionarioforeach (Suit suit in Enum.GetValues(typeof(Suit)))
            for (int elementType=0; elementType < _sizeDB; elementType++) 
            {
				//Cogemos el numero de bases de datos por tipo
				int _sizeList = databases[databasesEnum[elementType]].Count;
				string _type = databases.Keys[elementType];

				for (int elementName=0; elementName < _sizeList; elementName++)
				{
					writer.WriteStartElement ("database");

						writer.WriteStartElement ("name");
						writer.WriteElementString(databases[databasesEnum[elementType]][elementName]);
						writer.WriteEndElement();

						writer.WriteStartElement ("type");
						writer.WriteElementString(_type);
						writer.WriteEndElement();

					writer.WriteEndElement();
				}
			}

	        writer.WriteEndElement();

			//<messageType>***</messageType>
			writer.WriteStartElement ("messageType");
			writer.WriteElementString("DBListReply");
			writer.WriteEndElement();

			//Cerramos y limpiamos el documento
			writer.WriteEndDocument();
   			
			writer.Flush();
   			writer.Close();
		}*/

	}
}

