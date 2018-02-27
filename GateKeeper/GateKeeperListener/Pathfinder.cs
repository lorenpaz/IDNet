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
                this._port = 12000;
            else
                this._port = 11000;

            CargarClientes();
        }

		public Pathfinder(int port)
		{
            this._port = port;

			CargarClientes();
		}

        public void ProcessMsg(string content)
        {
            //parseamos el mensaje para pasar los parámetros de conexion
			XmlDocument xDoc = new XmlDocument();
			xDoc.LoadXml(content);

            String clienteDestino = xDoc.GetElementsByTagName("destination")[0].InnerText;
			if (this._clientes.ContainsKey(clienteDestino))
			{
				BindSocket(this._clientes[clienteDestino], content, clienteDestino);
			}
			else
			{
                String clienteOrigen = xDoc.GetElementsByTagName("source")[0].InnerText;
				BindSocket(this._clientes[clienteOrigen], "No se encuentra ese vecino en tu OV", clienteOrigen);
			}
        }

        private void BindSocket(IPAddress ip, string msg, string hostname)
        {
			// Establish the remote endpoint for the socket.
			// This example uses port 12000 on the local computer.
			//IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            IPEndPoint remoteEP = new IPEndPoint(ip, 13000 /*this._port*/);

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
        public static Dictionary<string,IPAddress> CargarClientesD()
		{
			//Archivo a leer
			StreamReader conFile = File.OpenText("clients.conf");
			string line = conFile.ReadLine();
            Dictionary<string,IPAddress> ipdict = new Dictionary<string, IPAddress>();

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
