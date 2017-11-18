﻿using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace MessageLibrary
{
	public class Message
	{
		private string _destination;
		private string _source;
		private string _messageType;
		private string _file_path;
		private string _db_name;
		private string _db_type;
		private string _body;

		public string Destination
		{
			get
			{
				return this._destination;
			}
			set
			{
				this._destination = value;
			}
		}
		public string Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}
		public string MessageType
		{
			get
			{
				return this._messageType;
			}
			set
			{
				this._messageType = value;
			}
		}
		public string Db_name
		{
			get
			{
				return this._db_name;
			}
			set
			{
				this._db_name = value;
			}
		}
		public string Db_type
		{
			get
			{
				return this._db_type;
			}
			set
			{
				this._db_type = value;
			}
		}
		public string Body
		{
			get
			{
				return this._body;
			}
			set
			{
				this._body = value;
			}
		}


		/**
         * Método para parsear un mensaje recibido como un XmlDocument
         * */
		public void parserMessageRecieve(XmlDocument doc)
		{
			//Cogemos el destinatario del mensaje
			this._destination = doc.DocumentElement.GetElementsByTagName("destination")[0].InnerText;

			//Cogemos el origen del mensaje
			this._source = doc.DocumentElement.GetElementsByTagName("source")[0].InnerText;

			//Cogemos el path donde guardamos temporalmente el xml
			//this._file_path = Constants.TEMPORAL_FILE_PATH + doc.DocumentElement.GetElementsByTagName("seq_id")[0].InnerText;

			//Cogemos el código del mensaje
			this._messageType = doc.DocumentElement.GetElementsByTagName("message_type")[0].InnerText;

			//Si existe, guardamos el tipo de la base de datos
			if (doc.DocumentElement.GetElementsByTagName("db_type").Count > 0)
				this._db_type = doc.DocumentElement.GetElementsByTagName("db_type")[0].InnerText;

			//Si existe, guardamos el nombre de la base de datos
			if (doc.DocumentElement.GetElementsByTagName("db_name").Count > 0)
				this._db_name = doc.DocumentElement.GetElementsByTagName("db_name")[0].InnerText;

			this._body = doc.DocumentElement.GetElementsByTagName("body")[0].InnerXml;
		}
		/*
         * Método para al creación de un XmlDocument a partir del mensaje
         * */
		public XmlDocument createMessage()
		{
			XmlDocument xmlDoc = new XmlDocument();
			XmlElement root = xmlDoc.DocumentElement;

			//Creamos elemento root
			XmlElement elementRoot = xmlDoc.CreateElement("root");
			xmlDoc.AppendChild(elementRoot);

			//Creamos elemento origen
			XmlNode source = xmlDoc.CreateElement("source");
			source.InnerText = this._source;
			elementRoot.AppendChild(source);

			//Creamos el elemento destino
			XmlNode destination = xmlDoc.CreateElement("destination");
			destination.InnerText = this._destination;
			elementRoot.AppendChild(destination);

			//Creamos el elemento tipoDeMensaje
			XmlNode message_type = xmlDoc.CreateElement("message_type");
			message_type.InnerText = this._messageType;
			elementRoot.AppendChild(message_type);

			//Creamos el elemento nombreBBDD
			XmlNode db_name = xmlDoc.CreateElement("db_name");
			db_name.InnerText = this._db_name;
			elementRoot.AppendChild(db_name);

			//Creamos el elemento tipoDeBBDD
			XmlNode db_type = xmlDoc.CreateElement("db_type");
			db_type.InnerText = this._db_type;
			elementRoot.AppendChild(db_type);

			//Creamos el elemento Cuerpo
			XmlNode body = xmlDoc.CreateElement("body");
			body.InnerText = this._body;
			elementRoot.AppendChild(body);

			return xmlDoc;
		}
	}
}
