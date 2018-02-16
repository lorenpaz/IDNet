using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace GateKeeperListener
{
    public class Sender
    {
        private Socket sender;

        public Sender(Socket s)
        {
            sender = s;
        }

		//Le pasas el mensaje y el host a quién nos vamos a conectar
        public string Send(string mensaje, string hostName, IPEndPoint remoteEP)
		{
			// Data buffer for incoming data.
			byte[] respuesta = new byte[1024];

			// Connect to a remote device.
			try
			{
				// Connect the socket to the remote endpoint. Catch any errors.
				try
				{
					sender.Connect(remoteEP);

					Console.WriteLine("Socket connected to {0}",
						sender.RemoteEndPoint.ToString());

					// Encode the data string into a byte array.
					byte[] msg = Encoding.ASCII.GetBytes(mensaje);

					// Send the data through the socket.
					int bytesSent = sender.Send(msg);

					// Receive the response from the remote device.
					int bytesRec = sender.Receive(respuesta);
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
			catch (Exception e)
			{
				return false;
			}

		}
    }
}
