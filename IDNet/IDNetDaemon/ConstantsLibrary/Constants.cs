using System;
using System.IO;
using System.Linq;
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
        private int _code;
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
        public int Code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }
        /*
         * Constructor
         * */
        public Usuario()
        {
            ParseConf();

            //IP tuya
            string aux = new WebClient().DownloadString("http://icanhazip.com");
            this._ip = IPAddress.Parse(aux.Replace("\n", String.Empty));
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
                bool firstParam = false, secondParam = false;
                string user = "", codigo = "";
                /*
                 * 
                 * nombre=userName|code:codigoNumerico;
                 * 
                 * Ejemplo:
                 * nombre=lorenzo|code:123456789;
                 * 
                 */
                //Leemos el parámetro
                while (line[i] != ';')
                {
                    if (line[i] == '=')
                    {
                        firstParam = true;
                    }
                     else if(line[i] == '|')
                    {
                        firstParam = false;
                    }
                    else if (line[i] == ':')
                    {
                        secondParam = true;
                    }
                    else if (firstParam)
                    {
                        user += line[i];
                    }
                    else if (secondParam)
                    {
                        codigo += line[i];
                    }
                    i++;
                }
                this._nombre = user;
                this._code = Int32.Parse(codigo);
                line = conFile.ReadLine();
            }
            conFile.Close();
        }

    }

    public static class Constants
    {
        public static string MONGODB = "mongodb";
        public static string MYSQL = "mysql";
        public static string CONFIG = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "configuration");
        public static string CONFIG_FILE_INFO_USER = Path.Combine(CONFIG,@"info.conf");
        public static string CONF_BLACK_LIST = Path.Combine(CONFIG,@"blackList.conf");
        public static string CONF_DATABASES = Path.Combine(CONFIG,@"databases.conf");

        public static string CONF_PUBLIC_KEY = Path.Combine(CONFIG,@"publicKeyDaemon.pem");
        public static string CONF_PRIVATE_KEY = Path.Combine(CONFIG, @"privateKeyDaemon.pem");

        public static byte[] SYMMETRIC_KEY = Convert.FromBase64String("AAECAwQFBgcICQoLDA0ODw==");
        public static byte[] SYMMETRIC_IV = Convert.FromBase64String("AAECAwQFBgcICQoLDA0ODw==");

        public static string MENSAJE_REGISTRO = @"010";
        public static string CONEXION = "001";
        public static string CONEXION_A = "001a";
        public static string CONEXION_B = "001b";
		public static string SCHEMA = "002";
		public static string SELECT = "003";
		public static string ACKCONEXION = "004";
        public static string ACKCONEXION_A = "004a";
        public static string ACKCONEXION_B = "004b";
		public static string ACKSCHEMA = "005";
		public static string ACKSELECT = "006";

        //IP en AWS
        public const string GATEKEEPER = @"18.130.70.74";
        //public static string GATEKEEPER = @"192.168.100.102";
        //public static string GATEKEEPER = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();

        public const int GATEKEEPER_PORT = 11000;

        public static Usuario usuario = new Usuario();

        public static string PathClavePublica(string source)
        {
            return Path.Combine(CONFIG, "publicKeyNeighbour" + source + ".pem");
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
