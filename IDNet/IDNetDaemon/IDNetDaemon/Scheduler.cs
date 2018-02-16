using System;
using Quartz;
using log4net;
using Quartz.Impl;
using System.Configuration;
using ConnectionLibrary;

namespace IDNetDaemon
{
    public class Scheduler
    {
		static readonly ILog log = LogManager.GetLogger(typeof(Scheduler));
		static IScheduler _scheduler;

		public void Start()
		{
            StdSchedulerFactory factory = new StdSchedulerFactory();
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
            Server s = new Server();
            s.StartListening();
			//var seconds = Int16.Parse(ConfigurationManager.AppSettings["MyJobSeconds"]);
			//log.InfoFormat("Start MyJob. Execute once in {0} seconds", seconds);

			/*IJobDetail job = JobBuilder.Create<Jobs.MyJob>()
				.WithIdentity("MyJob", "group1")
				.UsingJobData("Param1", "Hello MyJob!")
				.Build();

			ITrigger trigger = TriggerBuilder.Create()
				.WithIdentity("MyJobTrigger", "group1")
				.StartNow()
				.WithSimpleSchedule(x => x.WithIntervalInSeconds(seconds).RepeatForever())
				.Build();

			_scheduler.ScheduleJob(job, trigger);*/
		}
    }
}
