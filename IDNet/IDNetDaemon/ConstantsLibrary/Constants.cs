using System;
namespace ConstantsLibrary
{
    public static class Constants
    {
        public static string MONGODB = "mongodb";
        public static string MYSQL = "mysql";
        public static string TEMPORAL_FILE_PATH = "./temp_files/";
        public static string CONF_BLACK_LIST = "config/blackList.conf";
		public static string CONEXION = "001";
		public static string SCHEMA = "002";
		public static string SELECT = "003";
		public static string ACKCONEXION = "004";
		public static string ACKSCHEMA = "005";
		public static string ACKSELECT = "006";
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
