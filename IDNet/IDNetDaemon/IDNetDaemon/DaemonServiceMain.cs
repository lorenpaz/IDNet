using System;
using System.ServiceProcess;
using log4net;
namespace IDNetDaemon
{
    public class DaemonServiceMain
    {
        static readonly ILog log = LogManager.GetLogger(typeof(DaemonServiceMain));


#if (DEBUG != true)
        public static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new ServiceDaemon() };
            ServiceBase.Run(ServicesToRun);
        }
#endif
	}
}
