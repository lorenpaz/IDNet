using System;
using PluginsLibrary;
using MessageLibrary;
using System.Xml;
using SecurityLibrary;

namespace ProcessLibrary
{
    public class Process
    {
		private Database _db;

        public Process()
        {
            _db = new Database();            
        }

        public XmlDocument ejecutar(Message m)
        {
            XmlDocument xmlDoc = null;
            switch(m.MessageType)
             {
                case("001b"):
                    /* Security sec = new Security();
                     if (sec.checkBlackList(m.Source)) 
                        {

                        }*/
                    xmlDoc = GetInfoAllDatabases();
                     break;
                case("002"):
                     xmlDoc = this._db.EstructureRequest(m.Db_type, m.Db_name);
                    break;
                case("003"):
                    xmlDoc = this._db.SelectRequest(m.Db_type,m.Db_name,m.Body);
                    break;
            }
            return xmlDoc;
        }

        private XmlDocument GetInfoAllDatabases() {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.DocumentElement;

            //Creamos elemento databases
            XmlElement elementRoot = doc.CreateElement("databases");
            doc.AppendChild(elementRoot);
            foreach(var db in this._db.Databases)
            {
                foreach(var tupla in db.Value)
                {
                    //Creamos el elemento database
                    XmlNode database = doc.CreateElement("database");

                    XmlAttribute attribute = doc.CreateAttribute("db_type");
                    attribute.Value = db.Key;
                    database.InnerText = tupla.Item1;

                    database.Attributes.Append(attribute);
                    elementRoot.AppendChild(database);
                }
            }

            return doc;
        }

	}
}
