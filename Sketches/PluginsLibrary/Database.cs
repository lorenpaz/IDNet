using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ConvertionLibrary;

namespace PluginsLibrary
{
    //Clase constante con el archivo de configuración de las bases de datos
	static class Constants
	{
		public const string ConfigFile = "databases.conf";
	}

    //Clase base de datos
    public class Database
    {
        //Diccionario tipoBBDD -> [nombreBBDD1,nombreBBDD2]
        private Dictionary<string, List<string>> _databases;

		public Database()
		{
			ParseConf();
		}

        public Dictionary<string, List<string>> Databases
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
            StreamReader conFile = File.OpenText(Constants.ConfigFile);

			string line= conFile.ReadLine();
            this._databases = new Dictionary<string, List<string>>();
			
            //Voy leyendo línea por línea
            while (line != null)
			{
				int i = 0;
				bool param = true;
				string parameter = "",value="";
				/*
                 * 
                 * database_type=mongodb;
                 * database_name=empleados;
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
						value += line[i];

					i++;
				}

                if(this._databases.ContainsKey(parameter))
                {
                    this._databases[parameter].Add(value);
                }else{
                    List<string> aux = new List<string>();
                    aux.Add(value);
                    this._databases.Add(parameter, aux);
                }

                line = conFile.ReadLine();
			}
		}

		//Solicitud de la estructura de la base de datos
		public XmlDocument EstructureRequest(string databaseType, string databaseName)
        {
            XmlDocument xmldocument = new XmlDocument();
            switch(databaseType)
            {
                case("mongodb"):
                    PluginMongo mongo = new PluginMongo(databaseName);
                    mongo.EstructureRequest();
                    xmldocument = Convertion.JsonToXml(mongo.EstructureRequest());
                    break;

                case("mysql"):
                    PluginMySQL mysql = new PluginMySQL(databaseName);
                    mysql.EstructureRequest();
                    xmldocument = mysql.EstructureRequest();
                    break;
            }
            return xmldocument;
        }

    }
}
