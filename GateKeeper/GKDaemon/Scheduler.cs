using System;
using Quartz;
using log4net;
using Quartz.Impl;
using System.Configuration;
using System.Threading;
using GateKeeperListener;
using System.Collections.Generic;

namespace GKDaemon
{
	public class Scheduler
	{
		static IScheduler _scheduler;

		public void Start()
		{
			ISchedulerFactory factory = new StdSchedulerFactory();
            _scheduler = factory.GetScheduler().Result;
            _scheduler.Start();
			StartMyJob();
		}


		public void Shutdown()
		{
			if (null != _scheduler)
				_scheduler.Shutdown();
		}

		public void StartMyJob()
		{
            Log.Info("Inicializamos los Listener");
            Listener gk = new Listener(false);
            Listener cl = new Listener(true);

            Log.Info("Inicializamos el anuncio periodico");
            PeriodicAnnouncer pa = new PeriodicAnnouncer();

            ThreadStart _ts1 = delegate { gk.StartListening(); };
            Log.Info("Iniciado el servicio escuchando en el puerto "+Constants.PORT_GATEKEEPER+"...");
            ThreadStart _ts2 = delegate { cl.StartListening(); };
            Log.Info("Iniciado el servicio escuchando en el puerto "+Constants.PORT_CLIENT+"...");
            ThreadStart _ts3 = delegate { pa.StartTimer(); };
            Log.Info("Iniciado el servicio de anuncio de rutas...");


			// Se declara los hilos
			Thread hilo1 = new Thread(_ts1);
            Thread hilo2 = new Thread(_ts2);
            Thread hilo3 = new Thread(_ts3);

            // Se ejecutan los hilos
            hilo1.Start();
            hilo2.Start();
            hilo3.Start();

            hilo1.Join();
            hilo2.Join();
            hilo3.Join();
		}
	}
}
