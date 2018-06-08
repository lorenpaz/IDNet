using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

using log4net;

namespace GateKeeperListener
{
    public class Sender
    {
        ILog log = LogManager.GetLogger(typeof(Sender));

        private Socket sender;

        public Sender(Socket s)
        {
            sender = s;
        }

        public static void Send(Socket handler, String data)
		{
			// Convert the string data to byte data using ASCII encoding.
			byte[] byteData = Encoding.ASCII.GetBytes(data);

			// Begin sending the data to the remote device.
			handler.BeginSend(byteData, 0, byteData.Length, 0,
				new AsyncCallback(SendCallback), handler);
		}

		private static void SendCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.
				Socket handler = (Socket)ar.AsyncState;

				// Complete sending the data to the remote device.
				int bytesSent = handler.EndSend(ar);
				//log.Info("Sent " + bytesSent + " bytes to client.");

				handler.Shutdown(SocketShutdown.Both);
				handler.Close();

			}
			catch (Exception )
			{
			}
		}


		//Le pasas el mensaje y el host a quién nos vamos a conectar
        public string SendEP(string mensaje, string hostName, IPEndPoint remoteEP)
		{
			// Data buffer for incoming data.
			byte[] respuesta = new byte[4096];

			// Connect to a remote device.
			try
			{
				// Connect the socket to the remote endpoint. Catch any errors.
				try
				{
					sender.Connect(remoteEP);

                    log.Debug("Socket connected to "+sender.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					byte[] msg = Encoding.ASCII.GetBytes(mensaje);

					// Send the data through the socket.
					int bytesSent = sender.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender.Receive(respuesta);
                    log.Debug("Echoed received = "+Encoding.ASCII.GetString(respuesta, 0, bytesRec));

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
			return System.Text.Encoding.UTF8.GetString(respuesta);
		}

		public bool comprobarConexion()
		{
			// Connect to a remote device.
			try
			{
				bool part1 = sender.Poll(1000, SelectMode.SelectRead);
				bool part2 = (sender.Available == 0);
				if (part1 && part2)
					return false;
				else
					return true;
			}
            catch (Exception)
			{
				return false;
			}

		}
    }
}
