﻿using System;
using Quartz;
using log4net;
using Quartz.Impl;
using System.Configuration;
using ConnectionLibrary;
using ConstantsLibrary;

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
            RegisterClient register = new RegisterClient();
            if(register.comprobarConexion(Constants.GATEKEEPER))
                register.StartClient(Constants.GATEKEEPER);

            //Empieza a escuchar el demonio
            log.Info("Comienzamos a arrancar el servidor IDNetDaemon");
            Server s = new Server();
            s.StartListening();
		}
    }
}
