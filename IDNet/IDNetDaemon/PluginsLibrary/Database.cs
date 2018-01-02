using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ConvertionLibrary;
using ConstantsLibrary;
using CriptoLibrary;

namespace PluginsLibrary
{

    //Clase base de datos
    public class Database
    {
        //Diccionario tipoBBDD -> [(nombreBBDD1,usuario1,contrasenia1),
                                    //(nombreBBDD2,usuario2,contrasenia2)]
        private Dictionary<string, List<Tuple<string,string,string>>> _databases;

		public Database()
		{
			ParseConf();
		}

        public Dictionary<string, List<Tuple<string, string, string>>> Databases
        {
            get
            {
                return this._databases;
            }
            set
            {
                this._databases = value;
            }
        }

        //Lee del fichero de configuración
		private void ParseConf()
		{
            //Archivo a leer
            StreamReader conFile = File.OpenText(Constants.CONF_DATABASES);
            string line = conFile.ReadLine();
            this._databases = new Dictionary<string, List<Tuple<string, string, string>>>();

            //Voy leyendo línea por línea
            while (line != null)
            {
                int i = 0;
                bool param = true, secondParam = false, thirdParam = false;
                string parameter = "", valor = "",usuario="",contrasenia="";
                /*
                 * 
                 * database_type=database_name;
                 * 
                 * Ejemplos:
                 * mysql*usuarios|pepe*contrasenia;
                 * mongodb*empleados;
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
               /* else
                    contrasenia = Cripto.DecryptString(contrasenia);*/
                if (this._databases.ContainsKey(parameter))
                {
                    Tuple<string, string, string> tupla = new Tuple<string, string, string>(valor, usuario, contrasenia);
                    this._databases[parameter].Add(tupla);
                }
                else
                {
                    List<Tuple<string,string,string>> aux = new List<Tuple<string, string, string>>();
					Tuple<string, string, string> tupla = new Tuple<string, string, string>(valor, usuario, contrasenia);
					aux.Add(tupla);
                    this._databases.Add(parameter, aux);
                }

                line = conFile.ReadLine();
            }
            conFile.Close();
		}


		private Tuple<string, string,string> devuelveTupla(string tipoBBDD, string nombreBBDD)
		{
            foreach (var tupla in this._databases[tipoBBDD])
			{
				if (tupla.Item1 == nombreBBDD)
				{
					return tupla;
				}
			}
			return null;
		}

        private string getUser(string databaseType, string databaseName){
            int index = this._databases[databaseType].IndexOf(devuelveTupla(databaseType, databaseName));
            return this._databases[databaseType][index].Item2 == null ? null: this._databases[databaseType][index].Item2;
        }
		private string getPassword(string databaseType, string databaseName)
		{
			int index = this._databases[databaseType].IndexOf(devuelveTupla(databaseType, databaseName));
			return this._databases[databaseType][index].Item3 == null ? null : this._databases[databaseType][index].Item3;
		}

		//Solicitud de la estructura de la base de datos
		public XmlDocument EstructureRequest(string databaseType, string databaseName)
        {
            XmlDocument xmldocument = new XmlDocument();
            PluginMongo mongo;
            PluginMySQL mysql;
            switch(databaseType)
            {
                case("mongodb"):
                    mongo = new PluginMongo(databaseName);
                    xmldocument = Convertion.JsonToXml(mongo.EstructureRequest());
                    break;

                case("mysql"):
                    string username = getUser(databaseType, databaseName);
                    string password = getPassword(databaseType, databaseName);

                    if (username == null)
                        mysql = new PluginMySQL(databaseName);
                    else
                        mysql = new PluginMySQL(databaseName,username,password);
                    xmldocument = mysql.EstructureRequest();
                    break;
            }
            return xmldocument;
        }

		//Realizar consulta a la base de datos
        public XmlDocument SelectRequest(string databaseType, string databaseName,XmlNode body)
		{
			XmlDocument xmldocument = new XmlDocument();
            PluginMySQL mysql;
            PluginMongo mongo;
			switch (databaseType)
			{
				case ("mongodb"):
					mongo = new PluginMongo(databaseName);
					xmldocument = Convertion.JsonToXml(mongo.SelectRequest(body));
					break;

				case ("mysql"):
					string username = getUser(databaseType, databaseName);
                    string password = getPassword(databaseType, databaseName);

					if (username == null)
                         mysql = new PluginMySQL(databaseName);
                    else
					     mysql = new PluginMySQL(databaseName, username, password);
                    xmldocument = mysql.SelectRequest(body);
					break;
			}
			return xmldocument;
		}
    }
}
