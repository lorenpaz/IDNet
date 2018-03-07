using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using log4net;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace GateKeeperListener
{
    public class PeriodicAnnouncer
    {
		public ManualResetEvent allDone = new ManualResetEvent(false);
        static readonly ILog log = LogManager.GetLogger(typeof(PeriodicAnnouncer));
        String _msg;

        public PeriodicAnnouncer()
        { }

		//Iniciamos el servicio escuchando por el puerto. Lanzamos un hilo con
        //el temporizador.
		public string StartClient()
		{
			ThreadStart _ts1 = delegate { timedAnnounce(); };
			Thread temporizador = new Thread(_ts1);


			// Data buffer for incoming data.

            // Connect to a remote device.
            send();
			return System.Text.Encoding.UTF8.GetString(respuesta);
		}

        private void send()
        {
			byte[] respuesta = new byte[4096];

			try
			{
				// Establish the remote endpoint for the socket.
				// This example uses port 11000 on the local computer.
				IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

				IPAddress ipAddress = ipHostInfo.AddressList[0];
				IPEndPoint remoteEP = new IPEndPoint(ipAddress, 12000);

				// Create a TCP/IP  socket.
				Socket sender = new Socket(AddressFamily.InterNetwork,
					SocketType.Stream, ProtocolType.Tcp);

				// Connect the socket to the remote endpoint. Catch any errors.
				try
				{
					sender.Connect(remoteEP);

					Console.WriteLine("Socket connected to {0}",
						sender.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					byte[] msg = Encoding.ASCII.GetBytes(_msg);

					// Send the data through the socket.
					int bytesSent = sender.Send(msg);

					// Receive the response from the remote device.
					Console.WriteLine("Echoed test = {0}",
						Encoding.ASCII.GetString(respuesta, 0, bytesRec));

					// Release the socket.
					sender.Shutdown(SocketShutdown.Both);
					sender.Close();
				}
				catch (ArgumentNullException ane)
				{
					Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
				}
				catch (SocketException se)
				{
					Console.WriteLine("SocketException : {0}", se.ToString());
				}
				catch (Exception e)
				{
					Console.WriteLine("Unexpected exception : {0}", e.ToString());
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
        }

        private void timedAnnounce()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(Sender.send(msg));
        }

        public bool comprobarConexion(string hostName)
		{
			// Connect to a remote device.
			try
			{
				// Establish the remote endpoint for the socket.
				// This example uses port 11000 on the local computer.
				IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

				IPAddress ipAddress = ipHostInfo.AddressList[0];
				IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

				Ping pingSender = new Ping();
				PingReply reply = pingSender.Send(ipAddress);

				return reply.Status == IPStatus.Success;

			}
			catch (Exception)
			{
				return false;
			}
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
					else if (param)
					{
						parameter += line[i];
					}
					else
					{
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

		public static Dictionary<string, IPAddress> CargarClientesD()
		{
			//Archivo a leer
			StreamReader conFile = File.OpenText("clients.conf");
			string line = conFile.ReadLine();
			Dictionary<string, IPAddress> ipdict = new Dictionary<string, IPAddress>();

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
					else if (param)
					{
						parameter += line[i];
					}
					else
					{
						valor += line[i];
					}
					i++;
				}

				if (!ipdict.ContainsKey(parameter))
				{
					ipdict.Add(parameter, IPAddress.Parse(valor));
				}

				line = conFile.ReadLine();
			}
			conFile.Close();
			return ipdict;
		}
    }
}
