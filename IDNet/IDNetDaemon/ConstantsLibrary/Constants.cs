﻿using System;
using System.IO;
using System.Net;

namespace ConstantsLibrary
{
    /*
     * Clase con información del usuario
     * */
    public class Usuario
    {
        private string _nombre;
        private IPAddress _ip;
        public string Nombre
        {
            get
            {
                return this._nombre;
            }
            set
            {
                this._nombre = value;
            }
        }
        public IPAddress IP
        {
            get
            {
                return this._ip;
            }
            set
            {
                this._ip = value;
            }
        }

        /*
         * Constructor
         * */
        public Usuario()
        {
            ParseConf();

            //IP tuya
            this._ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
        }

        /*
         * Método privado para leer el fichero de configuración
         * */
        private void ParseConf()
        {
            //Archivo a leer
            StreamReader conFile = File.OpenText(Constants.CONFIG_FILE_INFO_USER);
            string line = conFile.ReadLine();

            //Voy leyendo línea por línea
            while (line != null)
            {
                int i = 0;
                bool firstParam = true;
                string user = "";
                /*
                 * 
                 * nombre=userName;
                 * 
                 * Ejemplo:
                 * nombre=lorenzo;
                 * 
                 */
                //Leemos el parámetro
                while (line[i] != ';')
                {
                    if (line[i] == '=')
                    {
                        firstParam = false;
                    }
                    else if (!firstParam)
                    {
                        user += line[i];
                    }
                    i++;
                }
                this._nombre = user;
                line = conFile.ReadLine();
            }
            conFile.Close();
        }

    }

    public static class Constants
    {
        public static string MONGODB = "mongodb";
        public static string MYSQL = "mysql";
        public static string TEMPORAL_FILE_PATH = @"./temp_files/";
        public static string CONFIG = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "/configuration");
        public static string CONFIG_FILE_INFO_USER = Path.Combine(CONFIG,@"info.conf");
        public static string CONF_BLACK_LIST = Path.Combine(CONFIG,@"blackList.conf");
        public static string CONF_DATABASES = Path.Combine(CONFIG,@"databases.conf");
        public static string CONF_PUBLIC_KEY = Path.Combine(CONFIG,@"publicKeyDaemon.pem");
        public static string CONF_PRIVATE_KEY = Path.Combine(CONFIG, @"privateKeyDaemon.pem");
        public static string MENSAJE_REGISTRO = @"010";
        public static string CONEXION = "001";
		public static string SCHEMA = "002";
		public static string SELECT = "003";
		public static string ACKCONEXION = "004";
        public static string ACKCONEXION_A = "004a";
        public static string ACKCONEXION_B = "004b";
		public static string ACKSCHEMA = "005";
		public static string ACKSELECT = "006";

        public static string GATEKEEPER = @"192.168.1.85";

        public static Usuario usuario = new Usuario();

        public static string PathClavePublica(string source)
        {
            return CONFIG + "publicKey" + source + ".pem";
        }


        /*
         * Método estático para borrar recursos
         * */
		public static void BorrarRecursos()
		{
			File.Delete(Constants.CONF_PUBLIC_KEY);
			File.Delete(Constants.CONF_PRIVATE_KEY);
		}
    }

    public enum DatabasesEnum {
        MONGODB,
        MYSQL
    }
    /*
     * Enumerado con los tipos de mensaje
     * */
	public enum typesMessages
	{
		CONEXION,
		SCHEMA,
		SELECT,
		ACKCONEXION,
        ACKCONEXION_A,
        ACKCONEXION_B,
		ACKSCHEMA,
		ACKSELECT
	}
}
