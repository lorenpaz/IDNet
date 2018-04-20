using System;
using System.Collections.Generic;
using System.IO;

using ConstantsLibraryS;

using MongoDB.Bson;
using MongoDB.Driver;
using System.Data;
using MySql.Data.MySqlClient;

using System.Xml;

namespace DatabaseLibraryS
{

    public class Databases
    {

		//Diccionario tipoBBDD -> [(nombreBBDD1,usuario1,contrasenia1),
		//(nombreBBDD2,usuario2,contrasenia2)]
		private Dictionary<string, List<Tuple<string, string, string>>> _databasesPropias;

		public Databases()
		{
            this._databasesPropias = new Dictionary<string, List<Tuple<string, string, string>>>();
			ParseConf();
		}

		public Dictionary<string, List<Tuple<string, string, string>>> DatabasesPropias
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

		//Lee del fichero de configuración
		private void ParseConf()
		{
            //Archivo a leer
            if (File.Exists(Constants.ConfigFileDatabases))
            {
                StreamReader conFile = File.OpenText(Constants.ConfigFileDatabases);
                string line = conFile.ReadLine();

                //Voy leyendo línea por línea
                while (line != null)
                {
                    int i = 0;
                    bool param = true, secondParam = false, thirdParam = false;
                    string parameter = "", valor = "", usuario = "", contrasenia = "";
                    /*
                     * 
                     * database_type=database_name;
                     * 
                     * Ejemplo:
                     * mongodb*empleados|pepe*contrasenia;
                     * 
                     */

                    //Leemos el parámetro
                    while (line[i] != ';')
                    {
                        //Ignoramos el igual y lo usamos como marca que separa el parámetro de su valor
                        if (line[i] == '*')
                        {
                            param = false;
                            if (secondParam)
                                thirdParam = true;
                        }
                        else if (line[i] == '|')
                        {
                            secondParam = true;
                            param = true;
                        }
                        else if (param && !secondParam && !thirdParam)
                            parameter += line[i];
                        else if (!param && !secondParam && !thirdParam)
                            valor += line[i];
                        else if (param && secondParam && !thirdParam)
                            usuario += line[i];
                        else if (thirdParam)
                            contrasenia += line[i];
                        i++;
                    }
                    if (usuario == "")
                        usuario = null;
                    if (contrasenia == "")
                        contrasenia = null;

                    if (this._databasesPropias.ContainsKey(parameter))
                    {
                        Tuple<string, string, string> tupla = new Tuple<string, string, string>(valor, usuario, contrasenia);
                        this._databasesPropias[parameter].Add(tupla);
                    }
                    else
                    {
                        List<Tuple<string, string, string>> aux = new List<Tuple<string, string, string>>();
                        Tuple<string, string, string> tupla = new Tuple<string, string, string>(valor, usuario, contrasenia);
                        aux.Add(tupla);
                        this._databasesPropias.Add(parameter, aux);
                    }

                    line = conFile.ReadLine();
                }
                conFile.Close();
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

		private Tuple<string, string, string> devuelveTupla(string tipoBBDD, string nombreBBDD)
		{
            foreach (var tupla in this._databasesPropias[tipoBBDD])
			{
				if (tupla.Item1 == nombreBBDD)
				{
					return tupla;
				}
			}
			return null;
		}

        /*
         * Método para añadir una base de datos
         * */
        public bool addDatabase(string tipoBBDD,string nombreBBDD,string usuarioDatabase, string passwordDatabase)
        {
            if(this._databasesPropias.ContainsKey(tipoBBDD) &&
               devuelveTupla(tipoBBDD,nombreBBDD) != null)
            {
                return false;
            }

            try
            {
                using (StreamWriter w = File.AppendText(Constants.ConfigFileDatabases))
                {
                    if (usuarioDatabase == null)
                        w.WriteLine(tipoBBDD + "*" + nombreBBDD + ";");
                    else
                    {
                        w.WriteLine(tipoBBDD + "*" + nombreBBDD + "|" + usuarioDatabase + "*" + passwordDatabase + ";");
                    }

                    if (this._databasesPropias.ContainsKey(tipoBBDD))
                    {
                        Tuple<string, string, string> tupla = new Tuple<string, string, string>(nombreBBDD, usuarioDatabase, passwordDatabase);
                        this._databasesPropias[tipoBBDD].Add(tupla);
                    }
                    else
                    {
                        List<Tuple<string, string, string>> aux = new List<Tuple<string, string, string>>();
                        Tuple<string, string, string> tupla = new Tuple<string, string, string>(nombreBBDD, usuarioDatabase, passwordDatabase);
                        aux.Add(tupla);
                        this._databasesPropias.Add(tipoBBDD, aux);
                    }

                    return true;
                }
            }catch(Exception){
                
                return false;
            }
        }

        /*
         * Método para modificar la base de datos
         * */
        public bool ModifyDatabase(List<string> bbdd,string nuevoTipoBBDD, string nuevoNombreBBDD,string nuevoUserDatabase, string nuevoPasswordDatabase)
        {
			if (!this._databasesPropias.ContainsKey(bbdd[0]) ||
                !this._databasesPropias[bbdd[0]].Contains(devuelveTupla(bbdd[0],bbdd[1])))
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
                if(linea.StartsWith(bbdd[0] + "*" + bbdd[1] + "|") || linea.StartsWith(bbdd[0] + "*" + bbdd[1] + ";"))
                {
                    lineExactly = linea;
                }    
            }

            string line = null,lineToWrite;

            if (nuevoUserDatabase == null)
                lineToWrite = nuevoTipoBBDD + "*" + nuevoNombreBBDD + ";";
            else
            {
                lineToWrite = nuevoTipoBBDD + "*" + nuevoNombreBBDD + "|" + nuevoUserDatabase + "*" + nuevoPasswordDatabase + ";";
            }
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
         * Método para borrar una base de datos
         * */
        public bool DeleteDatabase(List<string> bbdd, string antiguoTipoBBDD, string antiguoNombreBBDD, string antiguoUserDatabase, string antiguoPasswordDatabase)
        {
            if (!this._databasesPropias.ContainsKey(bbdd[0]) ||
                !this._databasesPropias[bbdd[0]].Contains(devuelveTupla(bbdd[0], bbdd[1])))
            {
                return false;
            }

            if (!File.Exists(Constants.ConfigFileDatabases))
            {
                throw new Exception("No hay archivo de configuración");
            }

            string tempFile = Constants.CONFIG + "temp.txt";

            string[] lines = File.ReadAllLines(Constants.ConfigFileDatabases);
            string lineExactly = null;

            foreach (string linea in lines)
            {
                if (linea.StartsWith(bbdd[0]+"*"+bbdd[1]+"|") || linea.StartsWith(bbdd[0] + "*" + bbdd[1]+ ";"))
                {
                    lineExactly = linea;
                }
            }

            string line = null;

            //Escribo en un archivo temporal mientras que leo
            using (StreamReader reader = new StreamReader(Constants.ConfigFileDatabases))
            using (StreamWriter writer = new StreamWriter(tempFile))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    //Sustituyo la linea que he modificado
                    if (line != lineExactly)
                    {
                        writer.WriteLine(line);
                    }
                }
            }

            //Borro el original,copio creando el original y borro el temporal
            File.Delete(Constants.ConfigFileDatabases);
            File.Copy(tempFile, Constants.ConfigFileDatabases);
            File.Delete(tempFile);

            return true;
        }

        public bool ComprobacionServidor(string databaseType, string databaseName,
            string usernameDatabase, string passwordDatabase){
            try
            {
                if (databaseType == Constants.MYSQL)
                {
                    return ComprobacionMysql(databaseName, usernameDatabase, passwordDatabase);
                }
                else if (databaseType == Constants.MONGODB)
                {
                    return ComprobacionMongodb(databaseName, usernameDatabase, passwordDatabase);
                }
            }catch(Exception){
                return false;
            }
            return false;
        }

		/*
         * Método para comprobar que está el servidor MYSQL activo
         * */
		public bool ComprobacionMysql()
        {
            bool check = false;

			//Control de errores
            if (!this._databasesPropias.ContainsKey(Constants.MYSQL))
			{
				return false;
			}

			//Primera base de datos con mysql
            string db_name = this._databasesPropias[Constants.MYSQL][0].Item1;

            string connection;
            if(this._databasesPropias[Constants.MYSQL][0].Item2 == null)
                connection = "Server=localhost;Database=" + db_name + ";Pooling=false;";
            else
				connection = "Server=localhost;Database=" + db_name + 
                    ";User ID="+this._databasesPropias[Constants.MYSQL][0].Item2 +
                                    ";Password="+this._databasesPropias[Constants.MYSQL][0].Item3 +";Pooling=false;";
			try
            {
                MySqlConnection dbcon = new MySqlConnection(connection);
                dbcon.Open();

                check = true;

			} catch (MySqlException)
			{
				check = false;
				/*switch (ex.Number)
				{
					//http://dev.mysql.com/doc/refman/5.0/en/error-messages-server.html
					case 1042: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
                        throw new Exception(Constants.UNABLE_CONNECT_MYSQL_HOSTS);
					case 0: // Access denied (Check DB name,username,password)
                        throw new Exception(Constants.ACCESS_DENIED_MYSQL);
					default:
						break;
				}*/
			}

            return check;
        }

		/*
         * Método para comprobar que está el servidor MYSQL activo
         * */
		public bool ComprobacionMysql(string databaseName,string usernameDatabase,string passwordDatabase)
		{
			bool check = false;

			//Control de errores
            if (!this._databasesPropias.ContainsKey(Constants.MYSQL))
			{
				return false;
			}

			//Primera base de datos con mysql
            string db_name = databaseName;

			string connection;
            if (usernameDatabase == null)
				connection = "Server=localhost;Database=" + db_name + ";Pooling=false;";
			else
				connection = "Server=localhost;Database=" + db_name +
                    ";User ID=" + usernameDatabase +
                    ";Password=" + passwordDatabase+ ";Pooling=false;";
			try
			{
				MySqlConnection dbcon = new MySqlConnection(connection);
				dbcon.Open();

				check = true;

			}
			catch (MySqlException)
			{
				check = false;
				/*switch (ex.Number)
				{
					//http://dev.mysql.com/doc/refman/5.0/en/error-messages-server.html
					case 1042: // Unable to connect to any of the specified MySQL hosts (Check Server,Port)
						throw new Exception(Constants.UNABLE_CONNECT_MYSQL_HOSTS);
					case 0: // Access denied (Check DB name,username,password)
						throw new Exception(Constants.ACCESS_DENIED_MYSQL);
					default:
						break;
				}*/
			}

			return check;
		}

        /*
         * Método para comprobar que está el servidor MongoDB activo
         * */
        public bool ComprobacionMongodb(string databaseName, string usernameDatabase, string passwordDatabase)
        {
            string connectionString = Constants.LOCALHOST_MONGODB;

            //Control de errores
            if(!this._databasesPropias.ContainsKey(Constants.MONGODB))
            {
                return false;
            }

            //Primera base de datos con mongodb
            string db_name = databaseName;

            //Comprobamos si está activo
            var client = new MongoClient(connectionString);
            var server = client.GetDatabase(db_name);
			bool mongodbAlive = server.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);

            return mongodbAlive;
        }

		/*
         * Método para comprobar que está el servidor MongoDB activo
         * */
		public bool ComprobacionMongodb()
		{
            string connectionString = Constants.LOCALHOST_MONGODB;

			//Control de errores
            if (!this._databasesPropias.ContainsKey(Constants.MONGODB))
			{
				return false;
			}

			//Primera base de datos con mongodb
            string db_name = this._databasesPropias[Constants.MONGODB][0].Item1;

			//Comprobamos si está activo
			var client = new MongoClient(connectionString);
			var server = client.GetDatabase(db_name);
			bool mongodbAlive = server.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);

			return mongodbAlive;
		}

    }
}
