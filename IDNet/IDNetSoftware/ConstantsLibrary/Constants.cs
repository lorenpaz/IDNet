﻿using System;
using MessageLibraryS;
using System.Xml;
using System.Collections.Generic;

namespace ConstantsLibraryS
{
    //Clase con las constantes
    public static class Constants
    {
        public const string CONFIG = @"../../../../configuration/";
        public const string ConfigFileDatabases = CONFIG + @"databases.conf";
        public const string ConfigFileNeighbours = CONFIG + @"neighbours.conf";

        public const string SOLICITUD_CONEXION = @"Solicitud de conexion de base de datos";
		public const string RESPUESTA_CONEXION = @"Respuesta a la solicitud de conexion de base de datos";

		public const string SOLICITUD_ESQUEMA = @"Solicitud de esquema de base de datos";
        public const string RESPUESTA_ESQUEMA = @"Respuesta a la solicitud de esquema de la base de datos";

        public const string USUARIO_SOLICITADO = @"Solicitado al usuario: ";
        public const string USUARIO_RESPUESTA = "Respuesta del usuario: ";

        public const string UNABLE_CONNECT_MYSQL_HOSTS = @"Unable to connect to any of the specified MYSQL hosts";
        public const string ACCESS_DENIED_MYSQL = "Access Denied: Check DB name, username, password";
        public const string NO_ERROR_MYSQL = @"There is not error in MYSQL Server";

        public const string UNABLE_CONNECT_MONGODB = @"Check your MongoDB Server";
        public const string NO_ERROR_MONGODB = @"There is not error in MongoDB Server";

        /*
         * A partir de aquí vienen método y estructuras para mostrar los mensajes por pantalla 
         * */

        public const int LENGTH_TABLE_VIEW = 130;
        public const string NOMBRE_BASE_DE_DATOS = @"Nombre de Base de Datos: ";
        public const string TIPO_BASE_DE_DATOS = @"Tipo de Base de Datos: ";
        public const string NOMBRE_TABLA = @"Nombre Tabla: ";
        public const string NOMBRE_COLUMNA = @"Nombre de la columna: ";
        public const string TIPO_COLUMNA = @"Tipo de columna: ";

        public const string NOMBRE_COLECCION = @"Nombre colección: ";
        public const string NOMBRE_CAMPO = @"Nombre del campo: ";


        private static string Columna(string word, int NSpaces)
        {
            return "|" + word.PadRight(NSpaces) + "|";
        }

        private static string Columna(string word, int NSpaces, char letter)
        {
            return "|" + word.PadRight(NSpaces, letter) + "|";
        }

        private static string TABLA_COLUMNAS = "+-----------+-------------+";
        private static string TABLA_COLUMNAS_CAMPOS = TABLA_COLUMNAS+ "\n" +
            "| Campo     | Tipo        |";
        
        private static string ColumnaTabla(string campo,string tipoCampo)
        {
            return "| " + campo +" | "+tipoCampo+" |";
        }

        public static string SolicitudEsquema(Message messageRequest)
        {
            return "Status: " + messageRequest.MessageType + " " + Constants.SOLICITUD_ESQUEMA + "\n" +
                Constants.USUARIO_SOLICITADO + messageRequest.Destination + "\n";
        }

        public static string RespuestaEsquemaMySQL(Message messageResponse)
        {
            BodyRespuesta002MySQL body = new BodyRespuesta002MySQL(messageResponse.Body.InnerXml);
            string linea = Columna("-", LENGTH_TABLE_VIEW, '-');

            string stado = "Status: " + messageResponse.MessageType + " " + Constants.RESPUESTA_ESQUEMA + "\n" +
            Constants.USUARIO_RESPUESTA + messageResponse.Destination + "\n" +
            linea + "\n" + linea + "\n" +
            Columna(NOMBRE_BASE_DE_DATOS + messageResponse.Db_name, linea.Length) + "\n" +
            linea + "\n" +
           Columna(TIPO_BASE_DE_DATOS + messageResponse.Db_type, linea.Length) + "\n";

            string tables="";
            foreach(Table t in body.Tables)
            {
                tables+="--"+ NOMBRE_TABLA + t.Name + "\n";
                tables +=TABLA_COLUMNAS_CAMPOS + "\n";
                foreach(Col c in t.Cols)
                {
                    //tables += NOMBRE_COLUMNA + c.Name + "-"+TIPO_COLUMNA +c.Type + "\n";
                    tables += ColumnaTabla(c.Name, c.Type) + "\n";
                }
                tables +=TABLA_COLUMNAS + "\n";
            }

            return stado+tables;    
        }
		public static string RespuestaEsquemaMongoDB(Message messageResponse)
		{
			BodyRespuesta002MongoDB body = new BodyRespuesta002MongoDB(messageResponse.Body.InnerXml);
			
            string linea = Columna("-", LENGTH_TABLE_VIEW, '-');
			string status = "Status: " + messageResponse.MessageType + " " + Constants.RESPUESTA_ESQUEMA + "\n" +
			Constants.USUARIO_RESPUESTA + messageResponse.Destination + "\n" +
			linea + "\n" +
			 Columna(" ", LENGTH_TABLE_VIEW, ' ') + "\n" +
			Columna(NOMBRE_BASE_DE_DATOS + messageResponse.Db_name, linea.Length) + "\n" +
			linea + "\n" +
		   Columna(TIPO_BASE_DE_DATOS + messageResponse.Db_type, linea.Length) + "\n";

            string collections = "";
            foreach(Collection c in body.Collections)
            {
                collections += "--" + NOMBRE_COLECCION + c.Name + "\n";
				collections += TABLA_COLUMNAS_CAMPOS + "\n";
                foreach (Field co in c.Fields)
				{
					//tables += NOMBRE_COLUMNA + c.Name + "-"+TIPO_COLUMNA +c.Type + "\n";
					collections += ColumnaTabla(co.Name,"") + "\n";
				}
				collections += TABLA_COLUMNAS + "\n";
            }
            return status + collections;
		}

        public static string SolicitudConexion(Message messageRequest)
        {
			return "Status: " + messageRequest.MessageType + " " + Constants.SOLICITUD_CONEXION + "\n" +
			Constants.USUARIO_SOLICITADO + messageRequest.Destination + "\n";
		}

        public static string RespuestaConexion(Message messageResponse)
        {
            string linea = Columna("-", LENGTH_TABLE_VIEW, '-');
            return "Status: " + messageResponse.MessageType + " " + Constants.RESPUESTA_CONEXION +"\n" +
           Constants.USUARIO_RESPUESTA + messageResponse.Destination + "\n";

		}
    }
	public struct Col
	{
		private string _name;
		private string _type;

		public Col(XmlElement infoCol)
		{
            this._name = infoCol.GetAttribute("name");
            this._type = infoCol.GetAttribute("type");
		}
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}
	}

	public struct Field
	{
		private string _name;
		//string _type;

        public Field(XmlNode infoField)
		{
            this._name = infoField.Name;
			//this._type = infoCol.GetElementsByTagName("type")[0].InnerText;
		}
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		/*public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}*/
	}

	public struct Table
	{
		private string _name;
		private List<Col> _cols;

		public Table(XmlElement infoTable)
		{

			this._cols = new List<Col>();
            this._name = infoTable.GetAttribute("name");
			foreach (XmlElement infoCol in infoTable.GetElementsByTagName("col"))
			{
				this._cols.Add(new Col(infoCol));

			}
		}
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public List<Col> Cols
		{
			get
			{
				return this._cols;
			}
			set
			{
				this._cols = value;
			}
		}
	}

	public struct Collection
	{
		private string _name;
		private List<Field> _fields;

		public Collection(XmlElement infoCollection)
		{

			this._fields = new List<Field>();
			this._name = infoCollection.GetElementsByTagName("name")[0].InnerText;
            /*foreach (XmlElement infoField in infoCollection.GetElementsByTagName("field"))
			{
				this._fields.Add(new Field(infoField));

			}*/
            XmlNode ejemploColeccion = infoCollection.GetElementsByTagName("ejemploColeccion")[0];
            foreach (XmlNode node in ejemploColeccion.ChildNodes)
            {
                this._fields.Add(new Field(node));
            }
		}
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public List<Field> Fields
		{
			get
			{
				return this._fields;
			}
			set
			{
				this._fields = value;
			}
		}
	}

	public struct BodyRespuesta002MySQL
	{
		private string _db_name;
		private List<Table> _tables;
		public BodyRespuesta002MySQL(string body)
		{

			this._tables = new List<Table>();
			XmlDocument x = new XmlDocument();
			x.LoadXml(body);
            this._db_name = x.DocumentElement.GetAttribute("name");
			foreach (XmlElement infoTable in x.DocumentElement.GetElementsByTagName("table"))
			{
				this._tables.Add(new Table(infoTable));
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
		public List<Table> Tables
		{
			get
			{
				return this._tables;
			}
			set
			{
				this._tables = value;
			}
		}
	}

	public struct BodyRespuesta002MongoDB
	{
		private string _db_name;
		private List<Collection> _collections;

		public BodyRespuesta002MongoDB(string body)
		{
			_collections = new List<Collection>();

			XmlDocument x = new XmlDocument();
			x.LoadXml(body);
			this._db_name = x.DocumentElement.GetElementsByTagName("name")[0].InnerText;
			foreach (XmlElement coleccion in x.DocumentElement.GetElementsByTagName("colecciones"))
			{
				this._collections.Add(new Collection(coleccion));
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
        public List<Collection> Collections
		{
			get
			{
                return this._collections;
			}
			set
			{
                this._collections = value;
			}
		}
	}
}
