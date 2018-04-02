using System;
using System.Xml;
using System.Net;
using System.IO;

namespace GateKeeperListener
{
	public class RouteXML
	{
		public RouteXML()
		{
		}

		public static void merge(string content)
		{
			//Loads both, our routes.xml file and the routing information
			//received in the message onto a XmlDocument object.
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(content);

			XmlDocument routes = new XmlDocument();

			FileStream stream = File.OpenRead("../../../Config/routes.xml");
			routes.Load(stream);
			stream.Close();

			XmlNodeList d = doc.GetElementsByTagName("route");

			foreach (XmlNode node in d)
			{
				//Load the routing information from the message received
				String dir_dest = node.ChildNodes[0].InnerText;
				String dir_hop = node.ChildNodes[1].InnerText;
				String nombre = node.ChildNodes[2].InnerText;
				int distance = Int32.Parse(node.ChildNodes[3].InnerText);

				XmlNodeList r = routes.GetElementsByTagName("route");

				/* If the distance to dir_dest through dir_hop is less than
                 * the distance to dir_dest which is already store in our file.
                 * We switch them onto our routes.xml file and update the value.
                 * */
				foreach (XmlNode route in r)
				{
					XmlNodeList existe = routes.SelectNodes("//route[d_node='" + dir_dest + "']");

					if (existe.Count > 0)
					{

						if (route.ChildNodes[0].InnerText == dir_dest)
						{

							if (Int32.Parse(route.LastChild.InnerText) > distance)
							{
								route.ChildNodes[0].InnerText = dir_dest;
								route.ChildNodes[1].InnerText = dir_hop;
								route.ChildNodes[2].InnerText = nombre;
								route.ChildNodes[3].InnerText = distance.ToString();
							}
						}
					}
					else
					{
						AñadirATablaRutas(routes, dir_dest, dir_hop, distance, nombre, routes.DocumentElement);
					}
				}
			}

			//Saves the file
			routes.Save("../../../Config/routes.xml");
		}


		public static void SendRoutingTables(Object source, System.Timers.ElapsedEventArgs e)
		{
			//Siempre mandará un mensaje por el puerto 12000
			Pathfinder p = new Pathfinder(true);

			XmlDocument neighbours = new XmlDocument();

			FileStream stream = File.OpenRead("../../../Config/neighbours.xml");
			neighbours.Load(stream);
			stream.Close();

			XmlDocument routes = new XmlDocument();

			FileStream streamR = File.OpenRead("../../../Config/routes.xml");
			routes.Load(streamR);
			streamR.Close();

			XmlNodeList n = neighbours.GetElementsByTagName("node");
			XmlNodeList r = routes.GetElementsByTagName("route");

			XmlDocument table = new XmlDocument();

			//Creamos el nodo routes
			XmlElement elementRoot = table.CreateElement("routes");
			table.AppendChild(elementRoot);

			//Para cada gatekeeper vecino
			foreach (XmlNode neigh in n)
			{
				//Por cada nodo de nuestra tabla
				String dir_neighbour = neigh.InnerText;
				foreach (XmlNode route in r)
				{
					//Cargamos los datos de la ruta
					String dir_dest = route.ChildNodes[0].InnerText;
					String dir_hop = route.ChildNodes[1].InnerText;
					String nombre = route.ChildNodes[2].InnerText;
					int distance = Int32.Parse(route.ChildNodes[3].InnerText) + 1;

					//Si la ruta pasa por el vecino al que se lo vamos a mandar, no lo hacemos
					if (dir_hop != dir_neighbour)
					{
						AñadirATablaRutas(table, dir_dest, dir_hop, distance, nombre, elementRoot);
					}
				}
				p.ProcessMsg(table.ToString(), "");
			}
		}

		public static void AñadirATablaRutas(XmlDocument table, string dir_dest, string dir_hop, int distance, String nombre, XmlElement root)
		{
			//Creamos el elemento route
			XmlNode route = table.CreateElement("route");
			root.AppendChild(route);

			//Creamos el nodo con la dirección destino
			XmlNode dest = table.CreateElement("d_node");
			dest.InnerText = dir_dest;
			route.AppendChild(dest);

			//Creamos el nodo con la dirección de salto
			XmlNode hop = table.CreateElement("d_hop");
			hop.InnerText = dir_hop;
			route.AppendChild(hop);

			//Creamos el nodo con la dirección de salto
			XmlNode name = table.CreateElement("name");
			name.InnerText = nombre;
			route.AppendChild(name);

			//Creamos el nodo con la distancia
			XmlNode distancia = table.CreateElement("distance");
			distancia.InnerText = distance.ToString();
			route.AppendChild(distancia);
		}

		public static IPAddress CargarIP(string clienteDestino)
		{
			XmlDocument doc = new XmlDocument();

			FileStream stream = File.OpenRead("../../../Config/routes.xml");
			doc.Load(stream);
			stream.Close();

			XmlNodeList existe = doc.SelectNodes("//route[name='" + clienteDestino + "']");

			return IPAddress.Parse(existe[0].ChildNodes[0].InnerText);
		}

		public static void PrepararRuta(string nom_cliente, string ip_cliente)
		{
			XmlDocument doc = new XmlDocument();

			FileStream stream = File.OpenRead("../../../Config/routes.xml");
			doc.Load(stream);
			stream.Close();

			XmlElement root = doc.DocumentElement;

            XmlNodeList existe = doc.SelectNodes("//route[name='" + nom_cliente + "']");
			if (existe[0] == null)
				AñadirATablaRutas(doc, ip_cliente, "127.0.0.1", 0, nom_cliente, root);

			doc.Save("../../../Config/routes.xml");
		}
	}
}
