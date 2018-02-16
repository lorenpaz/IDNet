using System;
using System.IO;

namespace ConstantsLibrary
{
    public static class Constants
    {
        public static string MONGODB = "mongodb";
        public static string MYSQL = "mysql";
        public static string TEMPORAL_FILE_PATH = @"./temp_files/";
        public static string CONF = @"./configuration/";
        public static string CONF_BLACK_LIST = CONF+@"blackList.conf";
		public static string CONF_DATABASES = CONF+@"databases.conf";
        public static string CONF_PUBLIC_KEY = CONF+@"publicKeyDaemon.pem";
        public static string CONF_PRIVATE_KEY = CONF + @"privateKeyDaemon.pem";
		public static string CONEXION = "001";
		public static string SCHEMA = "002";
		public static string SELECT = "003";
		public static string ACKCONEXION = "004";
		public static string ACKSCHEMA = "005";
		public static string ACKSELECT = "006";

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
	public enum typesMessages
	{
		CONEXION,
		SCHEMA,
		SELECT,
		ACKCONEXION,
		ACKSCHEMA,
		ACKSELECT
	}
}
