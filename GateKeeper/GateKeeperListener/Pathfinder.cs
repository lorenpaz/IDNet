using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using log4net;
using System.Linq;

namespace GateKeeperListener
{
    public class Pathfinder
    {
        private int _port;
        //Key = name, Value = ip
        private Dictionary<String, IPAddress> _clienteDireccion;
        private List<String> _vecinos;
		private Dictionary<String, int> _clienteDistancia;
		ILog log = LogManager.GetLogger(typeof(Pathfinder));

		public Pathfinder(bool cliente)
        {
            if (cliente)
                this._port = Constants.PORT_CLIENT;
            else
                this._port = Constants.PORT_GATEKEEPER;

            CargarClientes();
            CargarVecinos();
        }

		public Pathfinder(int port)
		{
            this._port = port;

			CargarClientes();
            CargarVecinos();
		}

        public String ProcessMsg(string content, String respuesta)
        {
            bool esProtocolo = false;
            //parseamos el mensaje para pasar los parámetros de conexion
			XmlDocument xDoc = new XmlDocument();
			xDoc.LoadXml(content);

            //El cliente destino y origen del mensaje original
            String clienteDestino = xDoc.GetElementsByTagName("destination")[0].InnerText;
            String clienteOrigen = xDoc.GetElementsByTagName("source")[0].InnerText;
			String codigo = xDoc.GetElementsByTagName("code")[0].InnerText;

			respuesta = content;

			// Mis ips
			String strHostName = Dns.GetHostName();
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

			// Enumerate IP addresses
			String  nIP;
			foreach (IPAddress ipaddress in iphostentry.AddressList){
				nIP = ipaddress.ToString();

                //Si recibo el mensaje de un cliente hacia mi mismo
                if (clienteDestino == nIP && this._port == Constants.PORT_CLIENT)
                {
                    RemoteDatabase db = new RemoteDatabase();

                    //Si es una nueva conexion la registramos en la tabla de rutas
                    if (xDoc.GetElementsByTagName("message_type")[0].InnerText == "010")
                    {
                        String code = xDoc.GetElementsByTagName("code")[0].InnerText;
                        String ip_origen = xDoc.GetElementsByTagName("ip")[0].InnerText;
                        RouteXML.PrepararRuta(clienteOrigen, ip_origen, nIP);
                        esProtocolo = true;
                    }
                    else if (xDoc.GetElementsByTagName("message_type")[0].InnerText == "011")
                    {
                        if (db.CheckCode(clienteOrigen, codigo))
                        {
                            respuesta = AnunciarNombresAlCliente(xDoc, content);
                            esProtocolo = true;
                        }
                        else
                        {
                            respuesta = "El cliente que se ha intentado conectar no es legítimo";
                            log.Error(respuesta);
                        }
                    }

                }
                //Si recibo un mensaje de un GK hacia mi mismo
                else if (clienteDestino == nIP && this._port == Constants.PORT_GATEKEEPER)
                {
                    RouteXML.merge(content);
                    esProtocolo = true;
                }

			}

            if (!esProtocolo){

                if (this._clienteDireccion.ContainsKey(clienteDestino)){
                    
                    IPAddress ip_dest = _clienteDireccion[clienteDestino];
                    if (this._clienteDistancia[clienteDestino] == 0)
                        ip_dest = RouteXML.CargarIP(clienteDestino);

                    BindSocket(this._clienteDireccion[clienteDestino], content, clienteDestino);
                }
                else if (this._vecinos.Contains(clienteDestino)){
                    
                    var match = this._vecinos
                                    .FirstOrDefault(stringToCheck => stringToCheck.Contains(clienteDestino));

                    if (match != null)
                        BindSocket(IPAddress.Parse(match), content, clienteOrigen);
                    else
                        log.Warn("Aviso de incoherencia con neighbours.xml: El " +
                                  "vecino" + clienteDestino + "aparece en routes.xml" +
                                  "pero no en neihbours.xml.");
                }
                else{
                    log.Error("Destino no encontrado");
                }
                respuesta = "";
            }

            return respuesta;
        }

        public void BindSocket(IPAddress ip, string msg, string hostname)
        {
			// Establish the remote endpoint for the socket.
			// This example uses port 12000 on the local computer.
			//IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            IPEndPoint remoteEP = new IPEndPoint(ip, this._port);

			// Create a TCP/IP  socket.
			Socket sender = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

            Sender s = new Sender(sender);

            s.SendEP(msg, hostname, remoteEP);
        }

        private void CargarVecinos()
        {
            this._vecinos = new List<String>();

            XmlDocument neighbours = new XmlDocument();

            FileStream stream = File.OpenRead(Constants.XMLNEIGHBOURS);
			neighbours.Load(stream);
			stream.Close();

			XmlNodeList nodes = neighbours.GetElementsByTagName("node");

			//Por cada nodo de nuestra tabla
			foreach (XmlNode n in nodes)
                this._vecinos.Add(n.InnerText);
		}

		private void CargarClientes()
		{
			this._clienteDireccion = new Dictionary<string, IPAddress>();
			this._clienteDistancia = new Dictionary<string, int>();
			//Cargar la tabla de rutas porque recibo el nombre y no la ip
			XmlDocument routes = new XmlDocument();

            FileStream stream = File.OpenRead(Constants.XMLROUTES);
            routes.Load(stream);
            stream.Close();

			XmlNodeList r = routes.GetElementsByTagName("route");


			//Por cada nodo de nuestra tabla
			foreach (XmlNode route in r)
			{
				//Cargamos los datos de ls direcciones y los nombres
				String dir_dest = route.ChildNodes[1].InnerText;
				String nombre = route.ChildNodes[2].InnerText;
				String distancia = route.ChildNodes[3].InnerText;
				this._clienteDireccion.Add(nombre, IPAddress.Parse(dir_dest));
				this._clienteDistancia.Add(nombre, Int32.Parse(distancia));
			}
		}

		internal String AnunciarNombresAlCliente(XmlDocument xDoc, String content)
		{
			XmlDocument doc = new XmlDocument();

			FileStream stream = File.OpenRead(Constants.XMLROUTES);
			doc.Load(stream);
			stream.Close();

			String nombre = xDoc.GetElementsByTagName("source")[0].InnerText;
			String ip_dest = xDoc.GetElementsByTagName("ip")[0].InnerText;

			//Quitamos el propio nodo
			XmlNodeList existe = doc.SelectNodes("//route[d_node = '" + ip_dest + "']");
            doc.DocumentElement.RemoveChild(existe[0]);

			//Quitamos los hops
			XmlNodeList routes = doc.GetElementsByTagName("route");
            foreach (XmlNode r in routes)
            {
                doc.DocumentElement.RemoveChild(doc.GetElementsByTagName("d_hop")[0]);
                doc.DocumentElement.RemoveChild(doc.GetElementsByTagName("d_node")[0]);
                doc.DocumentElement.RemoveChild(doc.GetElementsByTagName("distance")[0]);
            }

            return doc.InnerXml;
		}
	}
}
