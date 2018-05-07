using System;
using System.IO;

using MessageLibraryS;
using ProcessLibraryS;
using ConvertionLibraryS;

using ConstantsLibraryS;

using System.Xml;
using System.Text;

namespace PostBoxLibraryS
{
    public class PostBoxGK
    {
        //Mensaje de solitud
        Message _messageRequest;

        //Proceso
        Process _process;

        //Mensaje de respuesta
        Message _messageResponse;

        public Message MessageRequest
        {
            get
            {
                return this._messageRequest;
            }
        }

        public Message MessageResponse
        {
            get
            {
                return this._messageResponse;
            }
        }


        //Constructor para conexión al GK
        public PostBoxGK(string source, string destination, string tipoMensaje)
        {
            this._process = new Process();
            this._messageRequest = new Message(source, destination,Constants.MENSAJE_CONSULTA_BBDD_VECINOS);
            this._messageResponse = new Message();
        }

        /*
        * Realizamos el envio
        */
        public string ProcesarEnvio(string ip)
        {
            XmlDocument doc = this._messageRequest.createMessageNeighbour(ip);
            return doc.InnerXml;
        }

        /**
         * Procesamos la respuesta
         * */
        public void ProcesarRespuesta(string response)
        {
            //Convertimos el string a xml
            XmlDocument xmlDoc = Convertion.stringToXml(response);

            //Volcamos a un archivo de configuración
            GuardarEnArchivoConfiguracion(xmlDoc);
        }

        /*
         * Método privado para guardar en un archivo de configuración los vecinos
         * */
        private void GuardarEnArchivoConfiguracion(XmlDocument xmlDoc)
        {
            if(File.Exists(Constants.ConfigFileNeighbours))
            {
                File.Delete(Constants.ConfigFileNeighbours);
            }

            // Create the file.
            using (FileStream fs = File.Create(Constants.ConfigFileNeighbours))
            {
                foreach (XmlElement vecino in xmlDoc.GetElementsByTagName("route"))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(vecino.GetElementsByTagName("name")[0].InnerText+";"+"\n");

                    fs.Write(info, 0, info.Length);
                }
            }

        }

    }
}