using System;
using System.ServiceProcess;
using log4net;

using ConstantsLibrary;

namespace IDNetDaemon
{
    public class ServiceDaemon : ServiceBase
    {
        //Objeto para mostrar información
		static readonly ILog log = LogManager.GetLogger(typeof(ServiceDaemon));
		
        //Planificación del demonio
        Scheduler _scheduler;

        /*
         * Constructor del demonio
         * */
		public ServiceDaemon()
		{
			ServiceName = "IDNetDaemon";
			_scheduler = new Scheduler();
		}

        /*
         * Método para iniciar el demonio
         * */
		protected override void OnStart(string[] args)
		{
			log.Info("Service starting");
			_scheduler.Start();
		}

        /*
         * Método para parar el demonio
         * */
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
        #endif
	}

}

