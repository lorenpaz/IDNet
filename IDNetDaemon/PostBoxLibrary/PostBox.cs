using System;
using System.Xml;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;

using CriptoLibrary;
using MessageLibrary;
using ProcessLibrary;
using ConvertionLibrary;

namespace PostBoxLibrary
{
    public class PostBox
    {
        //Mensaje recibido
        Message _messageRecieve;

        //Proceso
        Process _process;

        //Mensaje de respuesta
        Message _messageResponse;

        public PostBox()
        {
            this._process = new Process();
			this._messageRecieve = new Message();
            this._messageResponse = new Message();
        }

        public string procesar(string document)
		{
            //Convertimos el string a xml
            XmlDocument xmlDoc = Convertion.stringToXml(document);

            //Clave para desencriptar
            //RijndaelManaged key = new RijndaelManaged();

            //Desencriptamos el mensaje
            //Security.Decrypt(xmlDoc, key);

            //Parseamos el mensaje
            this._messageRecieve.parserMessageRecieve(xmlDoc);

            //Ejecutamos el proceso

            XmlDocument xmlDocResponse = this._process.ejecutar(this._messageRecieve);
            //Creamos la respuesta
            String respuesta = responder(xmlDocResponse);
            return respuesta;
		}
        private string responder(XmlDocument doc)
        {
            string respuesta="";

            //Empezamos a realizar el mensaje de respuesta
            this._messageResponse = Message.respuestaAotro(this._messageRecieve);

            //Si hay cuerpo de la respuesta
            if(doc != null){
                this._messageResponse.Body = doc.InnerXml;
            }

            //Creamos un XMLDocument con el mensaje de respuesta
            XmlDocument xmlDocRespuesta = this._messageResponse.createMessage();

            //Devolvemos la respesta en forma de string
            respuesta = xmlDocRespuesta.InnerXml;

            return respuesta;
        }
	}
}
