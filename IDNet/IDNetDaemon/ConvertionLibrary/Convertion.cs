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
            XmlDocument doc = null;
            if (k.Contains("error"))
            {
                doc = new XmlDocument();
				XmlElement root = doc.CreateElement("result");
				doc.AppendChild(root);
            }
            else
            {
                 doc = JsonConvert.DeserializeXmlNode(k);
            }

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
