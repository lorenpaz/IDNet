using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Net.NetworkInformation;

namespace ConnectionLibraryS
{
    public class Client
    {
        public Client()
        {
        }

        /*
         * Método para comenzar la conexión del cliente al servidor
         * Le pasamos como argumento el mensaje junto con el hostDestino
         * */
        public string StartClient(string mensaje, string hostName)
        {
            // Data buffer for incoming data.
            byte[] respuesta = new byte[4096];

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

               // IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(hostName), 11000);
                    
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

        /*
         * Método booleano para comprobar la conexión.
         * Se utiliza previamente a la conexión con el servidor
         * */
        public bool comprobarConexion(string hostName)
        {
            // Connect to a remote device.
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(hostName), 11000);

            // Create a TCP/IP  socket.
            Socket sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
                
            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                sender.Connect(remoteEP);
                return true;
            }catch(Exception){
                return false;
            }

        }
    }
}