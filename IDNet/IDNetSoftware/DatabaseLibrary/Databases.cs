using System;
using System.Collections.Generic;
using System.IO;

using ConstantsLibraryS;

using MongoDB.Bson;
using MongoDB.Driver;
using System.Data;
using MySql.Data.MySqlClient;

namespace DatabaseLibraryS
{

    public class Databases
    {

		//Diccionario tipoBBDD -> [nombreBBDD1,nombreBBDD2]
		private Dictionary<string, List<string>> _databasesPropias;

		public Databases()
		{
			ParseConf();
		}

		public Dictionary<string, List<string>> DatabasesPropias
		{
			get
			{
				return this._databasesPropias;
			}
			set
			{
				this._databasesPropias = value;
			}
		}

        /*
         * Método para actualizar las bases de datos
         * */
        public void update()
        {
            this._databasesPropias.Clear();
            ParseConf();    
        }

		//Lee del fichero de configuración
		private void ParseConf()
		{
            //Archivo a leer
            if(!File.Exists(Constants.ConfigFileDatabases))
            {
                throw new Exception("No hay archivo de configuración");
            }
            using (StreamReader conFile = File.OpenText(Constants.ConfigFileDatabases))
            {

                string line = conFile.ReadLine();

                //Inicializamos el atributo
                this._databasesPropias = new Dictionary<string, List<string>>();

                //Voy leyendo línea por línea
                while (line != null)
                {
                    int i = 0;
                    bool param = true;
                    string parameter = "", valor = "";
                    /*
                     * 
                     * database_type=database_name;
                     * 
                     * Ejemplo:
                     * mongodb=empleados;
                     * 
                     */

                    //Leemos el parámetro
                    while (line[i] != ';')
                    {
                        //Ignoramos el igual y lo usamos como marca que separa el parámetro de su valor
                        if (line[i] == '=')
                            param = false;
                        else if (param)
                            parameter += line[i];
                        else if (!param)
                            valor += line[i];

                        i++;
                    }

                    if (this._databasesPropias.ContainsKey(parameter))
                    {
                        this._databasesPropias[parameter].Add(valor);
                    }
                    else
                    {
                        List<string> aux = new List<string>();
                        aux.Add(valor);
                        this._databasesPropias.Add(parameter, aux);
                    }

                    line = conFile.ReadLine();
                }
            }
		}

        /*
         * Método para añadir una base de datos
         * */
        public bool addDatabase(string tipoBBDD,string nombreBBDD)
        {
            if(this._databasesPropias.ContainsKey(tipoBBDD) &&
               this._databasesPropias[tipoBBDD].Contains(nombreBBDD) )
            {
                return false;
            }

			if (!File.Exists(Constants.ConfigFileDatabases))
			{
				throw new Exception("No hay archivo de configuración");
			}

            using (StreamWriter w = File.AppendText(Constants.ConfigFileDatabases))
            {
                w.WriteLine(tipoBBDD+"="+nombreBBDD+";");

                if (this._databasesPropias.ContainsKey(tipoBBDD))
				{
					this._databasesPropias[tipoBBDD].Add(nombreBBDD);
				}
				else
				{
					List<string> aux = new List<string>();
					aux.Add(nombreBBDD);
					this._databasesPropias.Add(tipoBBDD, aux);
				}

                return true;
            }
        }

        /*
         * Método para modificar la base de datos
         * */
        public bool ModifyDatabase(List<string> bbdd,string nuevoTipoBBDD, string nuevoNombreBBDD)
        {
			if (!this._databasesPropias.ContainsKey(bbdd[0]) ||
             !this._databasesPropias[bbdd[0]].Contains(bbdd[1]))
			{
				return false;
			}

			if (!File.Exists(Constants.ConfigFileDatabases))
			{
				throw new Exception("No hay archivo de configuración");
			}

            string tempFile = Constants.CONFIG+"temp.txt";

            string[] lines = File.ReadAllLines(Constants.ConfigFileDatabases);
            string lineExactly = null;

            foreach(string linea in lines)
            {
                if(linea.Contains(bbdd[0]) && linea.Contains(bbdd[1]))
                {
                    lineExactly = linea;
                }    
            }

            string line = null;
            string lineToWrite = nuevoTipoBBDD + "=" + nuevoNombreBBDD + ";";

            //Escribo en un archivo temporal mientras que leo
            using (StreamReader reader = new StreamReader(Constants.ConfigFileDatabases))
			using (StreamWriter writer = new StreamWriter(tempFile))
			{
				while ((line = reader.ReadLine()) != null)
				{
                    //Sustituyo la linea que he modificado
                    if (line==lineExactly)
					{
						writer.WriteLine(lineToWrite);
					}
					else
					{
						writer.WriteLine(line);
					}
				}
			}

            //Borro el original,copio creando el original y borro el temporal
            File.Delete(Constants.ConfigFileDatabases);
            File.Copy(tempFile,Constants.ConfigFileDatabases);
            File.Delete(tempFile);

            return true;
        }


		/*
         * Método para comprobar que está el servidor MYSQL activo
         * */
		public bool ComprobacionMysql()
        {
            bool check = false;

			//Control de errores
			if (!this._databasesPropias.ContainsKey("mysql"))
			{
				return false;
			}

			//Primera base de datos con mysql
			string db_name = this._databasesPropias["mysql"][0];

            string connection = "Server=localhost;Database=" + db_name + ";User ID=root;Password=1907;Pooling=false;";
            try
            {
                MySqlConnection dbcon = new MySqlConnection(connection);
                dbcon.Open();

                check = true;

			} catch (MySqlException ex)
			{
				check = false;
				switch (ex.Number)
				{
					//http://dev.mysql.com/doc/refman/5.0/en/error-messages-server.html
					case 1042: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                        throw new Exception(Constants.UNABLE_CONNECT_MYSQL_HOSTS);
					case 0: // Access denied (Check DB name,username,password)
                        throw new Exception(Constants.ACCESS_DENIED_MYSQL);
					default:
						break;
				}
			}

            return check;
        }

        /*
         * Método para comprobar que está el servidor MongoDB activo
         * */
        public bool ComprobacionMongodb()
        {
			string connectionString = "mongodb://localhost";

            //Control de errores
            if(!this._databasesPropias.ContainsKey("mongodb"))
            {
                return false;
            }

            //Primera base de datos con mongodb
            string db_name = this._databasesPropias["mongodb"][0];

            //Comprobamos si está activo
            var client = new MongoClient(connectionString);
            var server = client.GetDatabase(db_name);
			bool mongodbAlive = server.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);

            return mongodbAlive;
        }

    }
}
