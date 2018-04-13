using System.ServiceProcess;
using log4net;

namespace GKDaemon
{
	public class ServiceDaemon : ServiceBase
	{
		static readonly ILog log = LogManager.GetLogger(typeof(ServiceDaemon));
		Scheduler _scheduler;

		public ServiceDaemon()
		{
			ServiceName = "GKDaemon";
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

        #if DEBUG
		public static void Main(string[] args)
		{
			ServiceDaemon serv = new ServiceDaemon();
			serv.OnStart(new string[1]);
			ServiceBase.Run(serv);
		}
        #endif
	}
}

