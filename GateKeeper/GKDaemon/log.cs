using System;
using log4net;
using log4net.Config;

using GateKeeperListener;

namespace GKDaemon
{
    public static class Log
	{
		private static bool isConfigured = false;
        private static readonly ILog log = LogManager.GetLogger(typeof(Log));

		public static void Configure()
		{
			if (isConfigured)
				return;

            XmlConfigurator.Configure(new System.IO.FileInfo(Constants.XMLLOG));
            isConfigured = true;
		}

		public static void Debug(object message) { Configure(); log.Debug(message); }
		public static void Debug(object message, Exception exception) { Configure(); log.Debug(message, exception); }

		public static void Error(object message) {
            Configure();
            log.Error(message); 
        }
		public static void Error(object message, Exception exception) { Configure(); log.Error(message, exception); }

		public static void Fatal(object message) { Configure(); log.Fatal(message); }
		public static void Fatal(object message, Exception exception) { Configure(); log.Fatal(message, exception); }

		public static void Info(object message) { Configure(); log.Info(message); }
		public static void Info(object message, Exception exception) { Configure(); log.Info(message, exception); }

		public static void Warn(object message) { Configure(); log.Warn(message); }
		public static void Warn(object message, Exception exception) { Configure(); log.Warn(message, exception); }

	}
}
