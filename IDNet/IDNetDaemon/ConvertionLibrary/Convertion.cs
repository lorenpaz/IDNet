using System;
using System.Linq;
using System.Xml;
using System.Json;

namespace ConvertionLibrary
{
    public class Convertion
    {
        public static void xmlToJson(XmlNode xml)
        {
            
        }

        public static XmlDocument JsonToXml(string k)
        {
            XmlDocument xmldocument = new XmlDocument();

            return xmldocument;
        }

        public static XmlDocument stringToXml(string texto)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(texto);
            return document;
        }
    }
}
