﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ConvertionLibrary;
using ConstantsLibrary;
using CriptoLibrary;
using System.Security.Cryptography;
using System.Text;

namespace PluginsLibrary
{

    //Clase base de datos
    public class Database
    {
        //Diccionario tipoBBDD -> [(nombreBBDD1,usuario1,contrasenia1),
                                    //(nombreBBDD2,usuario2,contrasenia2)]
        private Dictionary<string, List<Tuple<string,string,string>>> _databases;
        private RijndaelManaged _symmetric;

        public RijndaelManaged Symmetric
        {
            get
            {
                return this._symmetric;
            }
            set
            {
                this._symmetric = value;
            }
        }

		public Database()
		{
            this._symmetric = new RijndaelManaged();
            this._symmetric.Key = Constants.SYMMETRIC_KEY;
            this._symmetric.IV = Constants.SYMMETRIC_IV;
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

        /*
         * Método privado para leer el fichero de configuración
         * */
		private void ParseConf()
		{
            if (File.Exists(Constants.CONF_DATABASES))
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
                    string parameter = "", valor = "", usuario = "", contrasenia = "";
                    /*
                     * 
                     * database_type=database_name;
                     * 
                     * Ejemplos:
                     * mysql*usuarios|pepe*contraseniaEncriptada;
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
                    else
                        contrasenia = Cripto.DecryptStringFromBytes(this._symmetric, Convert.FromBase64String(contrasenia));
                    if (this._databases.ContainsKey(parameter))
                    {
                        Tuple<string, string, string> tupla = new Tuple<string, string, string>(valor, usuario, contrasenia);
                        this._databases[parameter].Add(tupla);
                    }
                    else
                    {
                        List<Tuple<string, string, string>> aux = new List<Tuple<string, string, string>>();
                        Tuple<string, string, string> tupla = new Tuple<string, string, string>(valor, usuario, contrasenia);
                        aux.Add(tupla);
                        this._databases.Add(parameter, aux);
                    }

                    line = conFile.ReadLine();
                }
                conFile.Close();
            }else{
                this._databases = new Dictionary<string, List<Tuple<string, string, string>>>();
            }
		}

        /*
         * Método privado para devolver la primera tupla que coincida
         * el nombre de la base de datos
         * */
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

        /*
         * Método privado para la obtención del nombre de un usuario
         * */
        private string getUser(string databaseType, string databaseName){
            int index = this._databases[databaseType].IndexOf(devuelveTupla(databaseType, databaseName));
            return this._databases[databaseType][index].Item2 == null ? null: this._databases[databaseType][index].Item2;
        }

        /*
         * Método privado para la obtención de la contraseña de un usuario
         * */
		private string getPassword(string databaseType, string databaseName)
		{
			int index = this._databases[databaseType].IndexOf(devuelveTupla(databaseType, databaseName));
			return this._databases[databaseType][index].Item3 == null ? null : this._databases[databaseType][index].Item3;
		}

		/*
		 * Método público para la solicitud de la estructura de la base de datos
		 * */
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

		/*
		 * Método público para realizar consultas a la base de datos
		 * */
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
