﻿using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Xml;
using ConstantsLibrary;

namespace ConnectionLibrary
{
    public class RegisterClient
    {
        //Mensaje
        private XmlDocument _mensaje;

        /*
         * Constructor
         * */
        public RegisterClient()
        {
            ConstruccionMensajeRegistro();
        }

        /*
         * Método privado para la construcción del mensaje de registro
         * */
        private void ConstruccionMensajeRegistro()
        {
            this._mensaje = new XmlDocument();

            XmlElement root = this._mensaje.DocumentElement;

            //Creamos elemento root
            XmlElement elementRoot = this._mensaje.CreateElement("root");
            this._mensaje.AppendChild(elementRoot);

            //Creamos el elemento message_type
            XmlNode message_type = this._mensaje.CreateElement("message_type");
            message_type.InnerText = Constants.MENSAJE_REGISTRO;
            elementRoot.AppendChild(message_type);

            //Creamos elemento origen
            XmlNode source = this._mensaje.CreateElement("source");
            source.InnerText = Constants.usuario.Nombre;
            elementRoot.AppendChild(source);

            //Creamos el elemento destino
            XmlNode ip = this._mensaje.CreateElement("ip");
            ip.InnerText = Constants.usuario.IP.ToString();
            elementRoot.AppendChild(ip);

            //Creamos el elemento destino
            XmlNode destination = this._mensaje.CreateElement("destination");
            destination.InnerText = Constants.GATEKEEPER;
            elementRoot.AppendChild(destination);

            //Creamos elemento code
            XmlNode code = this._mensaje.CreateElement("code");
            code.InnerText = "0123456789";
            elementRoot.AppendChild(code);

        }

        /*
         * Método para comenzar la conexión del cliente al servidor
         * Le pasamos como argumento el mensaje junto con el hostDestino
         * */
        public string StartClient(string hostName)
        {
            // Data buffer for incoming data.
            byte[] respuesta = new byte[4096];

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

                //IPAddress ipAddress = ipHostInfo.AddressList[0];
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
                    byte[] msg = Encoding.ASCII.GetBytes(this._mensaje.InnerXml);

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
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.
            Socket sender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                sender.Connect(remoteEP);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}