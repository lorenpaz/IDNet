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

        public void ejecutar(Message m)
        {
            int a;
            switch(m.MessageType)
             {
                case("001"):
                    
                    break;
                case("002"):
                    _db.EstructureRequest(m.Db_type, m.Db_name);
                    break;
                case("003"):
                    a = 3;
                    break;

                case("004"):
                    a = 6;
                    break;

                case("005"):
                    a = 8;
                    break;

                case("006"):
                    a = 1;
                    break;
            }
        }

	}
}
