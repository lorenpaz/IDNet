using System;
namespace ConstantsLibrary
{
    public static class Constants
    {
        public static string MONGODB = "mongodb";
        public static string MYSQL = "mysql";
        public static string TEMPORAL_FILE_PATH = "./temp_files/";
    }

    public enum DatabasesEnum {
        MONGODB,
        MYSQL
    }
}
