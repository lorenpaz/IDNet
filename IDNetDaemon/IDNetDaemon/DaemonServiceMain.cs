using System;
using System.ServiceProcess;
//using System.ComponentModel

namespace IDNetDaemon
{
    public class DaemonServiceMain
    {
		public static void Main(string[] args)
		{
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[] { new ServiceDemon.Service() };
			ServiceBase.Run(ServicesToRun);
		}
    }
}
