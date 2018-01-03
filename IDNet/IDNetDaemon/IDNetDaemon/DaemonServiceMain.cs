using System;
using System.ServiceProcess;

namespace IDNetDaemon
{
    public class DaemonServiceMain
    {
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
