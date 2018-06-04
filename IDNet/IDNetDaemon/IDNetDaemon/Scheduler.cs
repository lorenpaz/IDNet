using System;
using Quartz;
using log4net;
using Quartz.Impl;
using System.Configuration;
using ConnectionLibrary;
using ConstantsLibrary;
using System.IO;

namespace IDNetDaemon
{
    public class Scheduler
    {
        //Objeto para mostrar información
		static readonly ILog log = LogManager.GetLogger(typeof(Scheduler));
		
        //Planificación- interfaz
        static IScheduler _scheduler;

        /*
         * Método para comenzar la planificación
         * */
		public void Start()
		{
            StdSchedulerFactory factory = new StdSchedulerFactory();
            _scheduler = factory.GetScheduler().Result;
			_scheduler.Start();
            log.Info("Directorio configuration:" + Constants.CONFIG);

    		StartMyJob();
		}

        /*
         * Método para parar la planificación
         * */
		public void Shutdown()
		{
			if (null != _scheduler)
				_scheduler.Shutdown();
		}

        /*
         * Método para comenzar el trabajo
         * */
		void StartMyJob()
		{
            //Registramos el demonio en el GateKeeper
            log.Info("Registrando el cliente en el GateKeeper");
            Usuario user = new Usuario();
            RegisterClient register = new RegisterClient(user);
            log.Info("Comprobando la conexión con el GateKeeper");
           /* if (register.comprobarConexion(Constants.GATEKEEPER))
            {
                log.Info("Registrado en el GateKeeper");
                register.StartClient(Constants.GATEKEEPER);
            }else{
                log.Info("No se ha registrado el cliente en el GaTeKeeper");
            }*/
            //Empieza a escuchar el demonio
            log.Info("Comienzamos a arrancar el servidor IDNetDaemon");
            Server s = new Server();
            s.StartListening();
		}

        private static void CheckConfigurationFIle()
        {
            if (!Directory.Exists(Constants.CONFIG))
            {
                Directory.CreateDirectory(Constants.CONFIG);
            }
        }
    }
}
