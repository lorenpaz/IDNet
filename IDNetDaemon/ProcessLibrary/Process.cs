using System;
using PluginsLibrary;
using MessageLibrary;
using System.Xml;

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
            int a;
            XmlDocument xmlDoc = null;
            switch(m.MessageType)
             {
                case("001"):
                    
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

	}
}
