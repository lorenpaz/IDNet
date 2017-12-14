﻿using System;
using System.Xml;
using MySql.Data.MySqlClient;
using System.Data;

namespace PluginsLibrary
{
    public class PluginMySQL
    {
        //Propiedades
        private string _databaseName;
        private string _salida;
        private string _connectionString;

		//Constructor
		public PluginMySQL(string databaseName)
		{
			this._databaseName = databaseName;
            this._connectionString ="Server=localhost;Database="+this._databaseName+";User ID=root;Password=1907;Pooling=false;";
		}

        public string Salida
        {
            get
            {
                return this._salida;
            }
            set
            {
                this._salida = value;
            }
        }

		//Solicitud de la estructura de la BBDD
        public  XmlDocument EstructureRequest()
        {
            XmlDocument document = null;
            try
            {
                MySqlConnection dbcon = new MySqlConnection(this._connectionString);
                dbcon.Open();
                // DataTable schemas = dbcon.GetSchema("Tables");
                var a = dbcon.GetSchema("Columns");
                document = CreateXMLSchema(dbcon);
           
            }catch(MySqlException ex)
            {
				switch (ex.Number)
				{
					//http://dev.mysql.com/doc/refman/5.0/en/error-messages-server.html
					case 1042: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
						break;
					
                    case 0: // Access denied (Check DB name,username,password)
						throw new Exception("Access Denied: Check DB name, username, password");
					
                    default:
						break;
				}
            }
            return document;
        }

		//Realizar consulta a la BBDD
		public XmlDocument SelectRequest()
        {
            XmlDocument document = new XmlDocument();

            return document;
        }

        //Método para la creación del esquema de la BBDD
        private XmlDocument CreateXMLSchema(MySqlConnection dbcon)
        {

            XmlDocument doc = new XmlDocument();
			
            XmlElement database = doc.CreateElement("database");
			doc.AppendChild(database);
			
            //Nombre BBDD
			XmlNode nombreBBDD = doc.CreateElement("name");
            nombreBBDD.InnerText = dbcon.Database;
            database.AppendChild(nombreBBDD);

            DataTable tableSchema = dbcon.GetSchema("Tables");
			 foreach(DataRow row in tableSchema.Rows)
             {
                 XmlNode tabla = doc.CreateElement("table");
                 database.AppendChild(tabla);

                 XmlNode nombreTabla = doc.CreateElement("name");
                 nombreTabla.InnerText = row["TABLE_NAME"].ToString();
                 tabla.AppendChild(nombreTabla);

                 XmlNode columnas = doc.CreateElement("cols");
                 tabla.AppendChild(columnas);

                 // var colSchema = dbcon.GetSchema("Columns", new[] { nombreBBDD.InnerText, null, nombreTabla.InnerText });
                 var colSchema = dbcon.GetSchema("Columns");
                 foreach (DataRow row1 in colSchema.Rows)
                 {
                        if (row1["TABLE_NAME"].ToString() == nombreTabla.InnerText)
                        {
                            XmlNode columna = doc.CreateElement("col");
                            XmlNode nombreColumna = doc.CreateElement("name");
                            XmlNode tipoColumna = doc.CreateElement("type");

                            nombreColumna.InnerText = row1["COLUMN_NAME"].ToString();
                            tipoColumna.InnerText = row1["DATA_TYPE"].ToString();
                            
                            columna.AppendChild(nombreColumna);
                            columna.AppendChild(tipoColumna);
                            columnas.AppendChild(columna);
                        }
                 }
              }
			/*
              <?xml version="1.0"?>
                <database>
                  <name>usuarios</name>
                  <table>
                    <name>clientes</name>
                    <cols>
                      <col>
                        <name>id</name>
                      </col>
                      <col>
                        <name>nombre</name>
                      </col>
                      <col>
                        <name>apellidos</name>
                      </col>
                    </cols>
                  </table>
                  <table>
                    <name>empleados</name>
                    <cols>
                      <col>
                        <name>id</name>
                        <type>NUMBER</type>
                      </col>
                      <col>
                        <name>nombre</name>
                        <type>string</type>
                      </col>
                      <col>
                        <name>apellidos</name>
                        <type>string</type>
                      </col>
                    </cols>
                  </table>
                </database>

             */
			string aux = doc.ToString();
            return doc;
        }
    }
}
