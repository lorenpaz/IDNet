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
		private static ManualResetEvent allDone = new ManualResetEvent(false);
        private static readonly ILog log = LogManager.GetLogger(typeof(PeriodicAnnouncer));
        private static String _msg;

        public PeriodicAnnouncer()
        { }

#pragma warning disable RECS0135 // Function does not reach its end or a 'return' statement by any of possible execution paths
        public static void StartListening()
#pragma warning restore RECS0135 // Function does not reach its end or a 'return' statement by any of possible execution paths
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
				if (content.IndexOf("<EOF>", StringComparison.Ordinal) > -1)
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

            foreach (XmlNode node in d)
            {
                //Load the routing information from the message received
                String dir_dest = node.ChildNodes[0].InnerText;
                String dir_hop = node.ChildNodes[1].InnerText;
                String nombre = node.ChildNodes[2].InnerText;
                int distance = Int32.Parse(node.ChildNodes[3].InnerText);

                XmlNodeList r = routes.GetElementsByTagName("route");

                /* If the distance to dir_dest through dir_hop is less than
                 * the distance to dir_dest which is already store in our file.
                 * We switch them onto our routes.xml file and update the value.
                 * */
                foreach (XmlNode route in r)
                {
                    XmlNodeList existe = routes.SelectNodes("/route[d_node='" + dir_dest + "']");

                    if (existe.Count > 0){
                        
                        if (route.ChildNodes[0].InnerText == dir_dest){
                            
                            if (Int32.Parse(route.LastChild.InnerText) > distance){
                                route.ChildNodes[0].InnerText = dir_dest;
                                route.ChildNodes[1].InnerText = dir_hop;
                                route.ChildNodes[2].InnerText = nombre;
                                route.ChildNodes[3].InnerText = distance.ToString();
                            }
                        }
                    }
                    else{
                        AñadirATablaRutas(routes, dir_dest, dir_hop, distance, nombre, routes.DocumentElement);
                    }
                }
            }

            //Saves the file
            routes.Save("./Config/routes.xml");
        }


		private static void SendRoutingTables(Object source, System.Timers.ElapsedEventArgs e)
		{
			Pathfinder p = new Pathfinder(true);

            XmlDocument neighbours = new XmlDocument();
            neighbours.LoadXml("./Config/neighbours.xml");

            XmlDocument routes = new XmlDocument();
            routes.LoadXml("./Config/routes.xlm");

            XmlNodeList n = neighbours.GetElementsByTagName("node");
            XmlNodeList r = routes.GetElementsByTagName("route");

			XmlDocument table = new XmlDocument();

			//Creamos el nodo routes
			XmlElement elementRoot = table.CreateElement("routes");
			table.AppendChild(elementRoot);

            //Para cada nodo
			foreach(XmlNode neigh in n)
            {
                String dir_neighbour = neigh.InnerText;
                foreach(XmlNode route in r)
                {
                    String dir_dest = route.ChildNodes[0].InnerText;
                    String dir_hop = route.ChildNodes[1].InnerText;
                    String nombre = route.ChildNodes[2].InnerText;
					int distance = Int32.Parse(route.ChildNodes[3].InnerText);

                    if (dir_hop != dir_neighbour)
                    {
                        AñadirATablaRutas(table, dir_dest, dir_hop, distance, nombre, elementRoot);
                    }
                }
                p.ProcessMsg(table.ToString());
			}
		}

        private static void AñadirATablaRutas(XmlDocument table, string dir_dest, string dir_hop, int distance, String nombre, XmlElement root)
        {
            //Creamos el elemento route
            XmlNode route = table.CreateElement("route");
            root.AppendChild(route);

            //Creamos el nodo con la dirección destino
            XmlNode dest = table.CreateElement("d_node");
            dest.InnerText = dir_dest;
            route.AppendChild(dest);

            //Creamos el nodo con la dirección de salto
            XmlNode hop = table.CreateElement("d_hop");
            hop.InnerText = dir_hop;
            route.AppendChild(hop);

			//Creamos el nodo con la dirección de salto
			XmlNode name = table.CreateElement("name");
			name.InnerText = nombre;
			route.AppendChild(name);

            //Creamos el nodo con la distancia
            XmlNode distancia = table.CreateElement("distance");
            distancia.InnerText = distance.ToString();
            route.AppendChild(distancia);
        }
	}
}
