using System;
using System.Xml;
using MySql.Data.MySqlClient;
using System.Data;

namespace PluginsLibrary
{
    public class PluginMySQL
    {
        //Propiedades
        private string databaseName;
        private string salida;
        private string connectionString;
       
        //Constructor
		public PluginMySQL(string databaseName)
		{
			this.databaseName = databaseName;
            this.connectionString ="Server=localhost;Database="+this.databaseName+";User ID=root;Password=;Pooling=false;";
		}

        public string Salida
        {
            get
            {
                return salida;
            }
            set
            {
                salida = value;
            }
        }

		//Solicitud de la estructura de la base de datos
        public  XmlDocument EstructureRequest()
        {
            XmlDocument document = new XmlDocument();

            MySqlConnection dbcon = new MySqlConnection(connectionString);
            dbcon.Open();
            DataTable schemas = dbcon.GetSchema();
            return document;
        }
    }
}
