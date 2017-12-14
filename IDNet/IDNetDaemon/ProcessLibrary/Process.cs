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
                case("001"):
                    Security sec = new Security();
                    if (sec.checkBlackList(m.Source)) 
                       {
                               
                       }
                    break;
                case("002"):
                     xmlDoc = this._db.EstructureRequest(m.Db_type, m.Db_name);
                    break;
                case("003"):
                    xmlDoc = this._db.SelectRequest(m.Db_type,m.Db_name,m.Body.InnerXml);
                    break;
            }
            return xmlDoc;
        }

	}
}
