using System;
using System.ServiceProcess;

namespace IDNetDaemon
{
    public class DaemonServiceMain
    {
		public static void Main(string[] args)
		{
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[] { new ServiceDaemon() };
			ServiceBase.Run(ServicesToRun);
		}
    }
}
