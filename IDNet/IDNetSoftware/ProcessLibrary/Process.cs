﻿using System;
using DatabaseLibraryS;
using MessageLibraryS;
using System.Xml;

namespace ProcessLibraryS
{

    public class Process
    {
        private Databases _dbs;

        public Process()
        {
            _dbs = new Databases();            
        }

      /*  public XmlDocument ejecutar(Message m)
        {
            XmlDocument xmlDoc = null;
            switch(m.MessageType)
            {
                case("004"):
                    
                    break;
                case("005"):
                    xmlDoc = this._dbs.EstructureRequest(m.Db_type, m.Db_name);
                    break;
                case("006"):
                    xmlDoc = this._dbs.SelectRequest(m.Db_type,m.Db_name,m.Body);
                    break;
            }
            return xmlDoc;
        }*/
   }
}