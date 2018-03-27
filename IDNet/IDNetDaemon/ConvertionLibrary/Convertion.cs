using System.Xml;
using MongoDB.Bson;
using log4net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ConvertionLibrary
{
    public class Convertion
    {
        //Objeto para mostrar información
        static readonly ILog log = LogManager.GetLogger(typeof(Convertion));

        /*
         * Método estático para la conversión de un documento JSON a XML
         * */
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
                doc = JsonConvert.DeserializeXmlNode(k,"result");
            }   

            return doc;
        }

        /*
         * Método estático para la conversión de String a XMLDocument
         * */
        public static XmlDocument stringToXml(string texto)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(texto);
            return document;
        }
    }
}
