using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using log4net;
using System.Xml;

namespace GateKeeperListener
{
    public class PeriodicAnnouncer
    {
		private ManualResetEvent allDone = new ManualResetEvent(false);
        private static readonly ILog log = LogManager.GetLogger(typeof(PeriodicAnnouncer));
        private static String _msg;

        public PeriodicAnnouncer()
        { }

		public static void StartListening()
		{

			ThreadStart _ts1 = delegate { TSender(); };

			// Se declara los hilos
			Thread th1 = new Thread(_ts1);

			// Data buffer for incoming data.  
			byte[] bytes = new Byte[1024];

			// Establish the local endpoint for the socket.  
			// The DNS name of the computer  
			// running the listener is "host.contoso.com".  
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ipAddress = ipHostInfo.AddressList[0];
			IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

			// Create a TCP/IP socket.  
			Socket listener = new Socket(ipAddress.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);

			// Bind the socket to the local endpoint and listen for incoming connections.  
			try
			{
				listener.Bind(localEndPoint);
				listener.Listen(100);

				while (true)
				{
					// Set the event to nonsignaled state.  
					allDone.Reset();

					// Start an asynchronous socket to listen for connections.  
					Console.WriteLine("Waiting for a connection...");
					listener.BeginAccept(
						new AsyncCallback(AcceptCallback),
						listener);

					// Wait until a connection is made before continuing.  
					allDone.WaitOne();
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();

		}

        private static void TSender()
        {
			System.Timers.Timer aTimer = new System.Timers.Timer(5000);
            aTimer.Elapsed += SendRoutingTables;
			aTimer.Enabled = true;
        }

        public static void AcceptCallback(IAsyncResult ar)
		{
			// Signal the main thread to continue.  
			allDone.Set();

			// Get the socket that handles the client request.  
			Socket listener = (Socket)ar.AsyncState;
			Socket handler = listener.EndAccept(ar);

			// Create the state object.  
			StateObject state = new StateObject();
			state.workSocket = handler;
			handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
				new AsyncCallback(ReadCallback), state);
		}

		public static void ReadCallback(IAsyncResult ar)
		{
			String content = String.Empty;

			// Retrieve the state object and the handler socket  
			// from the asynchronous state object.  
			StateObject state = (StateObject)ar.AsyncState;
			Socket handler = state.workSocket;

			// Read data from the client socket.   
			int bytesRead = handler.EndReceive(ar);

			if (bytesRead > 0)
			{
				// There  might be more data, so store the data received so far.  
				state.sb.Append(Encoding.ASCII.GetString(
					state.buffer, 0, bytesRead));

				// Check for end-of-file tag. If it is not there, read   
				// more data.  
				content = state.sb.ToString();
				if (content.IndexOf("<EOF>") > -1)
				{
					// All the data has been read from the   
					// client. Display it on the console.  
					Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
						content.Length, content);

                    //We want to merge the content of this file with ours
                    merge(content);
				}
				else
				{
					// Not all data received. Get more.  
					handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
					new AsyncCallback(ReadCallback), state);
				}
			}
		}

        private static void merge(string content)
        {
            //Loads both, our routes.xml file and the routing information
            //received in the message onto a XmlDocument object.
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);

            XmlDocument routes = new XmlDocument();
            routes.LoadXml("./Config/routes.xml");

            XmlNodeList d = doc.GetElementsByTagName("route");

            for (int i = 0; i < d.Count; i++)
            {
                //Load the routing information from the message received
                String dir_dest = d[i].ChildNodes[0].Value;
                String dir_hop = d[i].ChildNodes[1].Value;
                int distance = Int32.Parse(d[i].ChildNodes[2].Value);

                XmlNodeList r = routes.GetElementsByTagName("route");

                /* If the distance to dir_dest through dir_hop is less than
                 * the distance to dir_dest which is already store in our file.
                 * We switch them onto our routes.xml file and update the value.
                 * */
                for (int j = 0; j < r.Count; j++)
                {
                    if(r[j].ChildNodes[0].Value == dir_dest)
                    {
                        if(Int32.Parse(r[j].LastChild.Value) > distance)
                        {
                            r[j].ChildNodes[0].Value = dir_dest;
                            r[j].ChildNodes[1].Value = dir_hop;
                            r[j].ChildNodes[3].Value = distance.ToString();
						}
                    }
                }
            }

            //Saves the file
            routes.Save("./Config/routes.xml");
        }


		private static void SendRoutingTables(Object source, System.Timers.ElapsedEventArgs e)
		{
            String routingTable;
			Pathfinder p = new Pathfinder(true);

            XmlDocument neighbours = new XmlDocument();
            neighbours.LoadXml("./Config/neighbours.xml");

            XmlDocument routes = new XmlDocument();
            routes.LoadXml("./Config/routes.xlm");

            XmlNodeList n = neighbours.GetElementsByTagName("node");
            XmlNodeList r = routes.GetElementsByTagName("route");

            foreach(XmlNode neigh in n)
            {
                XmlDocument table = new XmlDocument();

                table.AppendChild(new XmlNode());
                String dir_neighbour = n[i].Value;
                foreach(XmlNode route in r)
                {
					String dir_dest = r[j].ChildNodes[0].Value;
					String dir_hop = r[j].ChildNodes[1].Value;
					int distance = Int32.Parse(r[j].ChildNodes[2].Value);

                    if(dir_hop != dir_neighbour)
                    {
                        
                    }
                }
				p.ProcessMsg(routingTable);
			}
		}
	}
}
