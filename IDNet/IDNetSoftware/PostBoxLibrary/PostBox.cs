using System;
using MessageLibraryS;
using ProcessLibraryS;
using ConvertionLibraryS;

using System.Xml;

namespace PostBoxLibraryS
{
    public class PostBox
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

		public PostBox(string source, string destination, string tipoMensaje, string db_name,
					  string db_type, string body)
		{
			this._process = new Process();
			this._messageRequest = new Message(source, destination, tipoMensaje, db_name, db_type, body);
			this._messageResponse = new Message();
		}


		/*
        * Realizamos el envio
        */
		public string ProcesarEnvio()
		{
			XmlDocument doc = this._messageRequest.createMessage();
			return doc.InnerXml;
		}

		/**
         * Procesamos la respuesta
         * */
		public void ProcesarRespuesta(string response)
		{
			//Convertimos el string a xml
			XmlDocument xmlDoc = Convertion.stringToXml(response);

			//Clave para desencriptar
			//RijndaelManaged key = new RijndaelManaged();

			//Desencriptamos el mensaje
			//Security.Decrypt(xmlDoc, key);

			//Parseamos el mensaje
			this._messageResponse.parserMessageRecieve(xmlDoc);
		}
	}
}