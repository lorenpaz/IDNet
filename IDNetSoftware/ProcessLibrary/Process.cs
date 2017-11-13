using System;
using DatabaseLibrary;
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
            XmlDocument xmlDoc = null;
            switch(m.MessageType)
            {
                case("004"):
                    
                    break;
                case("005"):
                    xmlDoc = this._db.EstructureRequest(m.Db_type, m.Db_name);
                    break;
                case("006"):
                    xmlDoc = this._db.SelectRequest(m.Db_type,m.Db_name,m.Body);
                    break;
            }
            return xmlDoc;
        }
   }
}
