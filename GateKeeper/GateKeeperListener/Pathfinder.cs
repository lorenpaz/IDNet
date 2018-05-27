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
		private string _origen;
        //Key = name, Value = ip
        private Dictionary<String, IPAddress> _clienteDireccion;
        private List<String> _vecinos;
		private Dictionary<String, int> _clienteDistancia;
		ILog log = LogManager.GetLogger(typeof(Pathfinder));
        
		public Pathfinder(bool cliente)
		{
			if (cliente)
				this._origen = Constants.CLIENTE;
			else
				this._origen = Constants.GATEKEEPER;

			this._port = Constants.PORT_SENDING_TO_CLIENT;
			CargarClientes();
			CargarVecinos();
		}

        public String ProcessMsg(string content, String respuesta)
        {
            bool esProtocolo = false;
			XmlDocument xDoc = new XmlDocument();

			//parseamos el mensaje para pasar los parámetros de conexion

			try{
				xDoc.LoadXml(content);
			}
			catch(Exception e){
				log.Fatal("Message XML structure malformed: " + e.Message + "--> " + e.StackTrace, e);
				return "";
			}

			String clienteDestino = "", clienteOrigen = "", codigo = "", ip_origen = "";
			//El cliente destino y origen del mensaje original
			try{
				clienteDestino = xDoc.GetElementsByTagName("destination")[0].InnerText;
				clienteOrigen = xDoc.GetElementsByTagName("source")[0].InnerText;
				codigo = xDoc.GetElementsByTagName("code")[0].InnerText;
				ip_origen = xDoc.GetElementsByTagName("ip")[0].InnerText;            
			}
			catch(Exception e){
				log.Fatal("Message Fields malformed: " + e.Message + ": " + e.StackTrace, e);
				return "";
			}

			respuesta = content;

            //Mis ips
            IPHostEntry iphostentry = Dns.GetHostEntry(Dns.GetHostName());

			// Enumerate IP addresses
			String  nIP;         
			foreach (IPAddress ipaddress in iphostentry.AddressList){
                nIP = Constants.ipPublica.ToString();

                //Si recibo el mensaje de un cliente hacia mi mismo
				if (clienteDestino == nIP && this._origen == Constants.CLIENTE)
                {
                    RemoteDatabase db = new RemoteDatabase();

					//Si es una nueva conexion la registramos en la tabla de rutas
					if (xDoc.GetElementsByTagName("message_type")[0].InnerText == "010")
					{
						log.Info("Actualizando estado en la tabla de rutas del cliente '"+
						         clienteOrigen +"'");
						
						RouteXML.PrepararRuta(clienteOrigen, ip_origen, nIP);

						log.Info(clienteOrigen + "ha realizado la conexión con exito.");
						esProtocolo = true;
					}               
                    else if (xDoc.GetElementsByTagName("message_type")[0].InnerText == "011")                    {
						if (db.CheckCode(clienteOrigen, codigo)){
							log.Info("Anunciando nombre al cliente '"+ clienteOrigen + "'");
							respuesta = AnunciarNombresAlCliente(xDoc, content);
						}
                        else{
                            respuesta = "El cliente que se ha intentado conectar no es legítimo";
                            log.Error(respuesta);
                        }

						esProtocolo = true;
                    }               
                }
                //Si recibo un mensaje de un GK hacia mi mismo
				else if (clienteDestino == nIP && this._origen == Constants.GATEKEEPER){
					log.Info("Recibida tabla de rutas...actualizando rutas.");
                    RouteXML.merge(content);
					esProtocolo = true;
                }

			}
            
            if (!esProtocolo){
                //Si tenemos la direccion de destino en nuestra tabla de rutas
                if (this._clienteDireccion.ContainsKey(clienteDestino)){
                    
                    //Por defecto usamos d_hop para reenviarlo al siguiente GK
                    IPAddress ip_dest = _clienteDireccion[clienteDestino];

                    //Si resulta que la distancia es 0 usamos la d_node
					if (this._clienteDistancia[clienteDestino] == 0)
						ip_dest = RouteXML.CargarIP(clienteDestino);

                    BindSocket(ip_dest, content, clienteDestino);
                }
                else if (this._vecinos.Contains(clienteDestino)){
                    //Si resulta que la dirección de destino es un GK vecino
                    //enviamos usando el puerto para GK
                    var match = this._vecinos
                                    .FirstOrDefault(stringToCheck => stringToCheck.Contains(clienteDestino));
					this._port = Constants.PORT_SENDING_TO_GATEKEEPER;
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
                //Cargamos d_hop que será la direccion de destino
				String dir_dest = route.ChildNodes[1].InnerText;
                //Cargamos el nombre
				String nombre = route.ChildNodes[2].InnerText;
                //Cargamos la distancia a ese nodo
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
				r.RemoveChild(r.ChildNodes[1]);
				r.RemoveChild(r.FirstChild);
				r.RemoveChild(r.LastChild);            
            }
            
            return doc.InnerXml;
		}
	}
}
