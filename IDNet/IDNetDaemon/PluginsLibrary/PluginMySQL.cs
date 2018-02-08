using System;
using System.Xml;
using MySql.Data.MySqlClient;
using System.Data;

using log4net;

namespace PluginsLibrary
{
    public class PluginMySQL
    {
        //Propiedades
        private string _databaseName;
        private string _usernameDatabase;
        private string _passwordDatabase;
        private string _salida;
        private string _connectionString;
		static readonly ILog log = LogManager.GetLogger(typeof(PluginMySQL));

		//Constructor
		public PluginMySQL(string databaseName)
		{
			this._databaseName = databaseName;
            this._connectionString ="Server=localhost;Database="+this._databaseName+";User ID=root;Password=1907;Pooling=false;";
            this._usernameDatabase = null;
            this._passwordDatabase = null;
        }

        public PluginMySQL(string databaseName,string userDatabase,string passwordDatabase){
            this._databaseName = databaseName;
            this._usernameDatabase = userDatabase;
            this._passwordDatabase = passwordDatabase;
            this._connectionString = "Server=localhost;Database="
                + this._databaseName + ";User ID="+this._usernameDatabase+
                ";Password="+this._passwordDatabase+";Pooling=false;";
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
		//Método para la creación del esquema de la BBDD
		private XmlDocument CreateXMLSchema(MySqlConnection dbcon)
		{

			XmlDocument doc = new XmlDocument();

			XmlElement database = doc.CreateElement("database");
			database.SetAttribute("name", dbcon.Database);
			doc.AppendChild(database);

			DataTable tableSchema = dbcon.GetSchema("Tables");
			foreach (DataRow row in tableSchema.Rows)
			{
				XmlElement tabla = doc.CreateElement("table");
				string nombreTabla = row["TABLE_NAME"].ToString();
				tabla.SetAttribute("name", nombreTabla);
				database.AppendChild(tabla);

				var colSchema = dbcon.GetSchema("Columns");
				foreach (DataRow row1 in colSchema.Rows)
				{
					if (row1["TABLE_NAME"].ToString() == nombreTabla)
					{
						XmlElement columna = doc.CreateElement("col");
						columna.SetAttribute("name", row1["COLUMN_NAME"].ToString());
						columna.SetAttribute("type", row1["DATA_TYPE"].ToString());
						tabla.AppendChild(columna);
					}
				}
			}
			/*
              <?xml version="1.0"?>
                <database name="usuarios">
                  <table name="clientes">
                      <col>
                        <name>id</name>
                      </col>
                      <col name="nombre" type="varchar">
                      </col>
                      <col name="apellidos" type="varchar">
                      </col>
                  </table>
                  <table name="empleados">
                      <col name="id" type="int">
                      </col>
                      <col name="nombre" tipo="varchar">
                      </col>
                      <col name="apellidos" tipo="varchar">
                      </col>
                  </table>
                </database>

             */
			string aux = doc.ToString();
			return doc;
		}

		//Realizar consulta a la BBDD
        public XmlDocument SelectRequest(XmlNode body)
        {
            MySqlConnection dbcon = null;

            ConsultaMysql consultamysql = new ConsultaMysql(body);

			try
			{
				dbcon = new MySqlConnection(this._connectionString);
				dbcon.Open();
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = dbcon;
                cmd.CommandText = consultamysql.Consulta;

                consultamysql.ResultadoConsulta = cmd.ExecuteReader();
                return consultamysql.ObtenerXMLResultado();
			}
			catch (MySqlException ex)
			{
                return consultamysql.ErrorObtenido(ex.Message);
			}
			finally
			{
				if (dbcon != null)
				{
                    dbcon.Close();
				}

			}
         }
    }

    public class ConsultaMysql
    {
        string _selectTarget;
        string _fromTarget;
        string _whereTarget;
        string _orderByTarget;
        string _consulta;
        MySqlDataReader _resultadoConsulta;

		public String  SelectTarget
		{
			get
			{
                return this._selectTarget;
			}
			set
			{
                this._selectTarget = value;
			}
		}
		public String FromTarget
		{
			get
			{
                return this._fromTarget;
			}
			set
			{
				this._fromTarget = value;
			}
		}
		public String WhereTarget
		{
			get
			{
                return this._whereTarget;
			}
			set
			{
                this._whereTarget = value;
			}
		}

		public String OrderByTarget
		{
			get
			{
                return this._orderByTarget;
			}
			set
			{
                this._orderByTarget = value;
			}
		}

		public String Consulta
		{
			get
			{
                return this._consulta;
			}
			set
			{
                this._consulta = value;
			}
		}

        public MySqlDataReader ResultadoConsulta
		{
			get
			{
                return this._resultadoConsulta;
			}
			set
			{
				this._resultadoConsulta = value;
			}
		}

        public ConsultaMysql(XmlNode body)
        {
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(body.InnerXml);
            this._selectTarget = doc.GetElementsByTagName("select")[0].InnerText;
			this._fromTarget = doc.GetElementsByTagName("from")[0].InnerText;
			this._whereTarget = doc.GetElementsByTagName("where")[0].InnerText;
            this._orderByTarget = doc.GetElementsByTagName("orderby")[0].InnerText;

            string etiquetaSelect = "SELECT " + this._selectTarget;
            string etiquetaFrom = " FROM " + this._fromTarget;
            string etiquetaWhere = "",etiquetaOrderBy = "";

            if(this._whereTarget != "")
            {
                etiquetaWhere = " WHERE " +this._whereTarget;
			}

            if(this._orderByTarget != ""){
                etiquetaOrderBy = " ORDER BY "+this._orderByTarget;
			}

            this._consulta =  etiquetaSelect + etiquetaFrom + etiquetaWhere + etiquetaOrderBy;
		}

        public XmlDocument ObtenerXMLResultado()
        {
            XmlDocument doc = new XmlDocument();
			XmlElement root = doc.DocumentElement;

			//Creamos elemento result
			XmlElement elementRoot = doc.CreateElement("result");
			doc.AppendChild(elementRoot);

            var rdr = this._resultadoConsulta;

            while (rdr.Read())
            {
				//Creamos el elemento 
				XmlElement row = doc.CreateElement("row");

                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    string nombreCampo = rdr.GetName(i);
                    string campo = Convert.ToString(rdr[nombreCampo]);
                    row.SetAttribute(nombreCampo, campo);
                }
				elementRoot.AppendChild(row);
            }

            if(rdr != null)
            {
                rdr.Close();
            }

			return doc;
        }

		public XmlDocument ErrorObtenido(string error)
		{
			XmlDocument doc = new XmlDocument();
			XmlElement root = doc.DocumentElement;

			//Creamos elemento result
			XmlElement elementRoot = doc.CreateElement("error");
            elementRoot.InnerText = error;
            doc.AppendChild(elementRoot);

			return doc;
		}
    }

}
