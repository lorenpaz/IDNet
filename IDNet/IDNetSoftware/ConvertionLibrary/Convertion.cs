using System;
using System.Linq;
using System.Xml;

namespace ConvertionLibraryS
{
    public class Convertion
    {
        public Convertion()
        {
        }

		public static XmlDocument stringToXml(string texto)
		{
			XmlDocument document = new XmlDocument();
			document.LoadXml(texto);
			return document;
		}
    }
}
