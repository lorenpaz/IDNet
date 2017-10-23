using System;
namespace ConstantsLibrary
{
    public static class Constants
    {
        public static string MONGODB = "mongodb";
        public static string MYSQL = "mysql";
    }

    public enum DatabasesEnum {
        MONGODB,
        MYSQL
    }
}
