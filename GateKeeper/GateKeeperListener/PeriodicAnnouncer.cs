using System;
using System.Threading;


namespace GateKeeperListener
{


    public class PeriodicAnnouncer
    {
        public PeriodicAnnouncer()
        { }

#pragma warning disable RECS0135 // Function does not reach its end or a 'return' statement by any of possible execution paths
        public void StartTimer()
#pragma warning restore RECS0135 // Function does not reach its end or a 'return' statement by any of possible execution paths
        {
			ThreadStart _ts1 = delegate { TSender(); };

			// Se declara los hilos
			Thread th1 = new Thread(_ts1);
		}

        private static void TSender()
        {
			System.Timers.Timer aTimer = new System.Timers.Timer(100);
            aTimer.Elapsed += RouteXML.SendRoutingTables;
			aTimer.Enabled = true;
        }

	}
}
