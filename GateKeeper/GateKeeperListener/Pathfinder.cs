using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using System.IO;

namespace GateKeeperListener
{
    public class Pathfinder
    {
        private int _port;
        private Dictionary<String, IPAddress> _clientes;

        public Pathfinder(bool cliente)
        {
            if (cliente)
                this._port = 11000;
            else
                this._port = 12000;

            CargarClientes();
        }

		public Pathfinder(int port)
		{
            this._port = port;

			CargarClientes();
		}

        public void PathDiscovery(ref Queue<string> q)
        {
            //Lanzamos un thread que procese los mensajes
            Pathfinder p = new Pathfinder(this._port);
            Thread t = new Thread( new ThreadStart( p.ProcessMsg(q)));
			t.Start();
        }

        public void ProcessMsg(ref Queue<string> q)
        {

			Queue<string> init, processed;

			while (q.Count != 0)
			{
                string content;

                do
                {
                    init = q;
                    processed = q;
    				content = processed.Dequeue();

					// Compares q to init. If they are not equal, then another
                    // thread has updated the running queue since this loop
                    // started. CompareExchange does not update q.
                    // CompareExchange returns the contents of q, which do not
                    // equal init, so the loop executes again.
				}while(init != Interlocked.CompareExchange(ref q, processed, init));


				//parseamos el mensaje para pasar los parámetros de conexion
				XmlDocument xDoc = new XmlDocument();
				xDoc.LoadXml(content);

				String clienteDestino = xDoc.GetElementsByTagName("destination")[0].ToString();
				if (this._clientes.ContainsKey(clienteDestino))
				{
					BindSocket(this._clientes[clienteDestino], content, clienteDestino);
				}
				else
				{
					String clienteOrigen = xDoc.GetElementsByTagName("source")[0].ToString();
					BindSocket(this._clientes[clienteOrigen], "No se encuentra ese vecino en tu OV", clienteOrigen);
				}
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

        private void CargarClientes()
        {
            //Archivo a leer
            StreamReader conFile = File.OpenText("clients.conf");
            string line = conFile.ReadLine();
            this._clientes = new Dictionary<string, IPAddress>();

            //Voy leyendo línea por línea
            while (line != null)
            {
                int i = 0;
                bool param = true;
                string parameter = "", valor = "";
                /*
                 * 
                 * nameClient=ipClient;
                 * 
                 * Ejemplos:
                 * lorenzo=172.16.0.34;
                 * pepe=172.16.3.45;
                 * 
                 */

                //Leemos el parámetro
                while (line[i] != ';')
                {
                    //Ignoramos el igual y lo usamos como marca que separa el parámetro de su valor
                    if (line[i] == '=')
                    {
                        param = false;
                    }
                    else if(param)
                    {
                        parameter += line[i]; 
                    }else{
                        valor += line[i];
                    }
                    i++;
                }

                if (!this._clientes.ContainsKey(parameter))
                {
                    this._clientes.Add(parameter, IPAddress.Parse(valor));
                }

                line = conFile.ReadLine();
            }
        conFile.Close();
        }
    }
}
