using System;
using System.IO;
using System.Net;

//ESCRITORIO

namespace GateKeeperListener
{
    public static class Constants
    {
		public static readonly string CONFIG = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Config");
        public static readonly string XMLROUTES = Path.Combine(CONFIG, @"routes.xml");
        public static readonly string XMLNEIGHBOURS = Path.Combine(CONFIG, @"neighbours.xml");
        public static readonly string XMLLOG = Path.Combine(CONFIG,@"EventLogConfig.xml");
		public static readonly int TIME_TO_SLEEP = 3000;
        public static readonly string MYSQL_REMOTE = @"Server=" + MYSQL_REMOTE_SERVERBBDD + ";Database=" 
          + MYSQL_REMOTE_NAMEBBDD + ";User ID=root;Password=admin1234;Pooling=false;";
        private static readonly string MYSQL_REMOTE_NAMETABLE = @"usuariosIDNet";
        private static readonly string MYSQL_REMOTE_SERVERBBDD = @"mysqlinstance.crfd5ylvvpz8.eu-west-2.rds.amazonaws.com";
        private static readonly string MYSQL_REMOTE_NAMEBBDD = @"IDNet";

        public static readonly IPAddress ipPublica = IpPublica();

        public static readonly int PORT_LISTENING_FROM_CLIENT = 11000;
        public static readonly int PORT_LISTENING_FROM_GATEKEEPER = 12000;
		public static readonly int PORT_SENDING_TO_GATEKEEPER = 12000;
		public static readonly int PORT_SENDING_TO_CLIENT = 11000;
        //FOR LOCAL DEBUGGING
		//public static readonly int PORT_SENDING_TO_CLIENT = 13000;
		//public static readonly int PORT_SENDING_TO_GATEKEEPER = 14000;

        public static string MysqlRemoteSelect(){
			return "SELECT * FROM " + MYSQL_REMOTE_NAMETABLE + " WHERE code=@code";
        }

        public static IPAddress IpPublica(){
            string aux = new WebClient().DownloadString("http://icanhazip.com");
            return IPAddress.Parse(aux.Replace("\n", String.Empty));
        }
    }
}
