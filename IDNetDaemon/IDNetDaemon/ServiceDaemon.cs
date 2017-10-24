using System;
using System.ServiceProcess;
using log4net;

namespace IDNetDaemon
{
    public class ServiceDaemon : ServiceBase
    {
		static readonly ILog log = LogManager.GetLogger(typeof(ServiceDaemon));
		Scheduler _scheduler;

		public ServiceDaemon()
		{
			ServiceName = "ServiceDaemon";
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
			_scheduler.Shutdown();
		}

    }
}
