using System.Xml;
using MongoDB.Bson;
using log4net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ConvertionLibrary
{
    public class Convertion
    {
        static readonly ILog log = LogManager.GetLogger(typeof(Convertion));
        public static void xmlToJson(XmlNode xml)
        {
            
        }

        public static XmlDocument JsonToXml(string k)
        {
            log.Info(k);
       /*    XmlDocument doc = new XmlDocument();
			XmlElement database = doc.CreateElement("database");
			doc.AppendChild(database);

            log.Info(k);
          //  JObject json = JObject.Parse(k);
            dynamic x = Newtonsoft.Json.JsonConvert.DeserializeObject(k);


			XmlNode nombreBBDD = doc.CreateElement("name");
            nombreBBDD.InnerText = x.name;
            database.AppendChild(nombreBBDD);

             
            foreach(var coleccion in x.colecciones)
            {
                XmlNode collection = doc.CreateElement("collection");

                foreach (var field in coleccion)
                {
                    XmlNode nombreColeccion = doc.CreateElement("name");
                    nombreColeccion.InnerText = field.name;
					//XmlNode campo = doc.CreateElement("field");

                    //XmlNode campoNombre = doc.

					collection.AppendChild(nombreColeccion);
				}

                database.AppendChild(collection);
            }
*/
            XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(k);
            log.Info(doc.InnerXml);
            return doc;
        }

        public static XmlDocument stringToXml(string texto)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(texto);
            return document;
        }
    }
}
