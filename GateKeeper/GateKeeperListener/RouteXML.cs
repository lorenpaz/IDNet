using System;
using System.Xml;
using System.Net;
using System.IO;

namespace GateKeeperListener
{
    public class RouteXML
    {
        public static void merge(string content)
        {
            //Loads both, our routes.xml file and the routing information
            //received in the message onto a XmlDocument object.
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);

            XmlDocument routes = new XmlDocument();

            FileStream stream = File.OpenRead(Constants.XMLROUTES);
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

                XmlNodeList existe = routes.SelectNodes("//route[d_node='" + dir_dest + "']");

                if (existe.Count > 0)
                {
                    /* If the distance to dir_dest through dir_hop is less than
                     * the distance to dir_dest which is already store in our file.
                     * We switch them onto our routes.xml file and update the value.
                     * */
                    foreach (XmlNode route in r)
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
                }
                else
                    AñadirATablaRutas(routes, dir_dest, dir_hop, distance, nombre, routes.DocumentElement);
            }
            //Console.WriteLine(routes.InnerXml);
            //Saves the file
            routes.Save(Constants.XMLROUTES);
        }

        internal static void actualizarRuta(string clienteOrigen, object ip_origen, string nIP)
        {
            throw new NotImplementedException();
        }

        public static void SendRoutingTables()
        {
            //Siempre mandará un mensaje por el puerto 12000
            Pathfinder p = new Pathfinder(false);

            XmlDocument neighbours = new XmlDocument();

            FileStream stream = File.OpenRead(Constants.XMLNEIGHBOURS);
            neighbours.Load(stream);
            stream.Close();

            XmlDocument routes = new XmlDocument();

            FileStream streamR = File.OpenRead(Constants.XMLROUTES);
            routes.Load(streamR);
            streamR.Close();

            XmlNodeList n = neighbours.GetElementsByTagName("node");
            XmlNodeList r = routes.GetElementsByTagName("route");

            //Para cada gatekeeper vecino
            foreach (XmlNode neigh in n)
            {
                XmlDocument table = new XmlDocument();

                XmlElement root = table.CreateElement("root");
                table.AppendChild(root);

                IPAddress mi_ip = Constants.ipPublica;

                XmlElement source = table.CreateElement("source");
                source.InnerText = mi_ip.ToString();
                root.AppendChild(source);

                //Por cada nodo de nuestra tabla
                String dir_neighbour = neigh.InnerText;

                XmlElement destination = table.CreateElement("destination");
                destination.InnerText = dir_neighbour;
                root.AppendChild(destination);

                //Creamos el nodo routes
                XmlElement routes_node = table.CreateElement("routes");
                root.AppendChild(routes_node);

                foreach (XmlNode route in r)
                {
                    //Cargamos los datos de la ruta
                    String dir_dest = route.ChildNodes[0].InnerText;
                    String dir_hop = mi_ip.ToString();
                    String nombre = route.ChildNodes[2].InnerText;
                    int distance = Int32.Parse(route.ChildNodes[3].InnerText) + 1;

                    //Si la ruta pasa por el vecino al que se lo vamos a mandar, no lo hacemos
                    //if (dir_hop != dir_neighbour)
                    //{
                    AñadirATablaRutas(table, dir_dest, dir_hop, distance, nombre, routes_node);
                    //}
                }

                p.ProcessMsg(table.InnerXml, "");
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

            FileStream stream = File.OpenRead(Constants.XMLROUTES);
            doc.Load(stream);
            stream.Close();

            XmlNodeList existe = doc.SelectNodes("//route[name='" + clienteDestino + "']");

            return IPAddress.Parse(existe[0].ChildNodes[0].InnerText);
        }

        public static void PrepararRuta(string nom_cliente, string ip_cliente, string mi_ip)
        {
            XmlDocument doc = new XmlDocument();

            FileStream stream = File.OpenRead(Constants.XMLROUTES);
            doc.Load(stream);
            stream.Close();

            XmlElement root = doc.DocumentElement;
            
            XmlNodeList existe = doc.SelectNodes("//route[name='" + nom_cliente + "']");
			if (existe[0] == null)
				AñadirATablaRutas(doc, ip_cliente, mi_ip, 0, nom_cliente, root);
			else
			{
				XmlNode p = existe[0].ParentNode;
				p.RemoveChild(0);
				AñadirATablaRutas(doc, ip_cliente, mi_ip, 0, nom_cliente, root);
			}

            doc.Save(Constants.XMLROUTES);
        }
    }
}
