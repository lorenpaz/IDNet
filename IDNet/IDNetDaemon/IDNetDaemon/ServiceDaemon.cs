using System;
using System.ServiceProcess;
using log4net;

using ConstantsLibrary;

namespace IDNetDaemon
{
    public class ServiceDaemon : ServiceBase
    {
		static readonly ILog log = LogManager.GetLogger(typeof(ServiceDaemon));
		Scheduler _scheduler;

		public ServiceDaemon()
		{
			ServiceName = "IDNetDaemon";
			_scheduler = new Scheduler();
		}

		protected override void OnStart(string[] args)
		{
			log.Info("Service starting");
			_scheduler.Start();
		}

		protected override void OnStop()
		{
			log.Info("Service shutting down");
            log.Info("Deleting all resources like Crytography keyPairs");
            Constants.BorrarRecursos();
			_scheduler.Shutdown();
		}
#if DEBUG
		public static void Main(string[] args)
		{
			ServiceDaemon serv = new ServiceDaemon();
            serv.OnStart(new string[1]);
            ServiceBase.Run(serv);
		}
	}
#endif
}

