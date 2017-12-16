﻿using System;
using System.Net;
using System.Xml;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography;

using CriptoLibrary;
using MessageLibrary;
using ProcessLibrary;
using ConvertionLibrary;

using log4net;

namespace PostBoxLibrary
{
    public class PostBox
    {
        static readonly ILog log = LogManager.GetLogger(typeof(PostBox));

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

            //Recogemos la información de inicio
            SymmetricAlgorithm simKey;
			Cripto cript = new Cripto();

            this._messageRecieve.parserStartRecievedMessage(xmlDoc);

            if (this._messageRecieve.MessageType == "001")
            {
                //Aquí iria una funcion para quitar el cifrado asimetrico
               // cript.CheckKey(this._messageRecieve.Source, this._messageRecieve.Key);
			}
            else
            {
                //  simKey = cript.CheckKey(this._messageRecieve.Source);

                //Desencriptamos
                // Cripto.Decrypt(xmlDoc, simKey);

                //Parseamos el mensaje
				this._messageRecieve.parserMessageRecieve(xmlDoc);

            }

            String respuesta="";

			if (!(this._messageRecieve.MessageType == "001"))
            {
				//Ejecutamos el proceso
				XmlDocument xmlDocResponse = this._process.ejecutar(this._messageRecieve);

                //Creamos la respuesta
                respuesta = responder(xmlDocResponse);

            }else{
				log.Info("here3");

				respuesta = responderConexion();
                log.Info(respuesta);
            }

            return respuesta;
		}
        private string responder(XmlDocument doc)
        {
            string respuesta="";

            //Empezamos a realizar el mensaje de respuesta
            this._messageResponse = Message.respuestaAotro(this._messageRecieve);

            //Si hay cuerpo de la respuesta
            if(doc != null){
                this._messageResponse.Body = doc as XmlNode;
            }
            //Creamos un XMLDocument con el mensaje de respuesta
            XmlDocument xmlDocRespuesta = this._messageResponse.createMessage();

            //Devolvemos la respesta en forma de string
            respuesta = xmlDocRespuesta.InnerXml;

            return respuesta;
        }

        private string responderConexion()
        {
            this._messageResponse.Source = this._messageRecieve.Destination;
            this._messageResponse.Destination = this._messageRecieve.Source;
            this._messageResponse.MessageType = "004";
            XmlDocument xmlDocRespuesta = this._messageResponse.createMessageConexion();

            return xmlDocRespuesta.InnerXml;
        }
	}
}
