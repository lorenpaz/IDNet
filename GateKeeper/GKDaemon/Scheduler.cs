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
		static readonly ILog log = LogManager.GetLogger(typeof(Scheduler));
		static IScheduler _scheduler;
        static Queue<string> _msgQueue;

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

		void StartMyJob()
		{
            GKListener gk = new GKListener();
            ClientListener cl = new ClientListener();

            ThreadStart _ts1 = delegate { gk.StartListening(_msgQueue); };
            ThreadStart _ts2 = delegate { cl.StartListening(_msgQueue); };

            // Se declara los hilos
            Thread hilo1 = new Thread(_ts1);
            Thread hilo2 = new Thread(_ts2);

            // Se ejecutan los hilos
            hilo1.Start();
            hilo2.Start();

            hilo1.Join();
            hilo2.Join();
		}
	}
}
