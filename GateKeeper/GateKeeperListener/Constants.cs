using System;
using System.IO;

namespace GateKeeperListener
{
    public static class Constants
    {
		public static readonly string CONFIG = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Config");
        public static readonly string XMLROUTES = Path.Combine(CONFIG, @"routes.xml");
        public static readonly string XMLNEIGHBOURS = Path.Combine(CONFIG, @"neighbours.xml");
        public static readonly string XMLLOG = Path.Combine(CONFIG,"EventLogConfig.xml");
        public static readonly string MYSQL_REMOTE = @"Server=" + MYSQL_REMOTE_SERVERBBDD + ";Database=" 
            + MYSQL_REMOTE_NAMEBBDD + ";User ID=root;Password=admin1234;Pooling=false;";
        private static readonly string MYSQL_REMOTE_NAMETABLE = @"usuariosIDNet";
        private static readonly string MYSQL_REMOTE_SERVERBBDD = @"mysqlinstance.crfd5ylvvpz8.eu-west-2.rds.amazonaws.com";
        private static readonly string MYSQL_REMOTE_NAMEBBDD = @"IDNet";

        public static readonly int PORT_CLIENT = 11000;
        public static readonly int PORT_GATEKEEPER = 12000;

        public static string MysqlRemoteSelect()
        {
			return "SELECT * FROM " + MYSQL_REMOTE_NAMETABLE + " WHERE code=@code";
        }
    }
}
