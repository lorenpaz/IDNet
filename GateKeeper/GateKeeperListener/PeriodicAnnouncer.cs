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
            while(true)
            {
                RouteXML.SendRoutingTables();
                Thread.Sleep(Constants.TIME_TO_SLEEP);
            }
		}
	}
}
