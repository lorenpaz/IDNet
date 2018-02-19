using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Xml;

namespace GateKeeperListener
{
    public class Pathfinder
    {
        private int _port;

        public Pathfinder(bool cliente)
        {
            if (cliente)
                _port = 11000;
            else
                _port = 12000;
        }

        public void PathDiscovery(Queue<string> q)
        {
            while(q.Count == 0){};

            //Lanzamos un thread que procese los mensajes
            Thread t = new Thread(new ThreadStart(ProcessMsg(q)));
            t.Start();
        }

        private void ProcessMsg(Queue<string> q)
        {
            while(q.Count != 0)
            {
                string content = q.Dequeue();
                //parseamos el mensaje para pasar los parámetros de conexion
                //En este caso usaremos una IP fija para el otro GK y el puerto 12000
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(content);

				IPAddress ip = IPAddress.Parse(xDoc.GetElementsByTagName("destination").ToString());

                BindSocket(ip, content, "lorenzo");
            }
        }

        private void BindSocket(IPAddress ip, string msg, string hostname)
        {
			// Establish the remote endpoint for the socket.
			// This example uses port 12000 on the local computer.
			//IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

			IPEndPoint remoteEP = new IPEndPoint(ip, _port);

			// Create a TCP/IP  socket.
			Socket sender = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

            Sender s = new Sender(sender);

            s.Send(msg, hostname, remoteEP);
        }
    }
}
