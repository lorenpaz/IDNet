﻿using System;
using System.IO;

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
        public const string ConfigFileInfoUser = CONFIG + @"info.conf";

        public const string CONF_PUBLIC_KEY = CONFIG + @"publicKeyIDNet.pem";
        public const string CONF_PRIVATE_KEY = CONFIG + @"privateKeyIDNet.pem";

        public const string MONGODB = @"mongodb";
        public const string MYSQL = @"mysql";

        public const string SOLICITUD_CONEXION = @"Solicitud de conexion de base de datos";
        public const string RESPUESTA_CONEXION = @"Respuesta a la solicitud de conexion de base de datos";

        public const string SOLICITUD_ESQUEMA = @"Solicitud de esquema de base de datos";
        public const string RESPUESTA_ESQUEMA = @"Respuesta a la solicitud de esquema de la base de datos";

        public const string SOLICITUD_CONSULTA = @"Solicitud de consulta de base de datos";
        public const string RESPUESTA_CONSULTA = @"Respuesta a la solicitud de consulta de la base de datos";


        public const string USUARIO_SOLICITADO = @"Solicitado al usuario: ";
        public const string USUARIO_RESPUESTA = @"Respuesta del usuario: ";

        public const string UNABLE_CONNECT_MYSQL_HOSTS = @"Unable to connect to any of the specified MYSQL hosts";
        public const string ACCESS_DENIED_MYSQL = @"Access Denied: Check DB name, username, password";
        public const string NO_ERROR_MYSQL = @"There is no error in MYSQL Server";

        public const string UNABLE_CONNECT_MONGODB = @"Check your MongoDB Server";
        public const string NO_ERROR_MONGODB = @"There is not error in MongoDB Server";
        public const string LOCALHOST_MONGODB = @"mongodb://localhost";

        public const string ERROR_CONNECTION = @"No se ha podido realizar la conexión con el vecino";

        public const string SCHEMA = @"schema";
        public const string CONNECTION = @"connection";
        public const string SELECT = @"select";

        public const string GATEKEEPER = @"127.0.0.1";

        public const string TABLA_COLUMNA_VECINOS_VO = @"Vecinos";
        public const string TABLA_COLUMNA_USUARIO = @"Usuario";
        public const string TABLA_COLUMNA_USUARIO_ORIGEN = @"Usuario origen";
        public const string TABLA_COLUMNA_USUARIO_DESTINO = @"Usuario destino";
        public const string TABLA_COLUMNA_TIPOBBDD = @"Tipo BBDD";
        public const string TABLA_COLUMNA_TIPO_MENSAJE = @"Tipo Mensaje";
        public const string TABLA_COLUMNA_NOMBREBBDD = @"Nombre BBDD";
        public const string TABLA_COLUMNA_FUNCIONA = @"Funciona";
        public const string TABLA_COLUMNA_ICONOS = @"Símbolos";
        public const string TABLA_COLUMNA_EXPLICACION = @"Significado";

        public const string CANCEL = @"Cancel";

        public const string MENSAJE_CONEXION = "001";
        public const string MENSAJE_CONEXION_A = "001a";
        public const string MENSAJE_CONEXION_B = "001b";
        public const string MENSAJE_ESQUEMA = "002";
        public const string MENSAJE_CONSULTA = "003";

        public const string MENSAJE_RESPUESTA_CONEXION_A = "004a";
        public const string MENSAJE_RESPUESTA_CONEXION_B = "004b";

        public const string INFORMACION_ICONO_ADDATABASE = @"Icono para añadir una base de datos con su información, con el fin de que los vecinos puedan realizar consultas a su base de datos."+
            "Durante la inserción de la base de datos, se le informará de la información que debe de adquirir para añadir la base de datos (nombre,tipo de base de datos,usuario y contraseña)";
        public const string INFORMACION_ICONO_UPDATEDATABASE = @"Icono para actualizar el estado de las bases de datos."+"\n"+"Los estados en los que se puede encontrar una base de datos propia son los siguientes: Disponible y No disponible."+"\n"+
            "Si le notifica que no está disponible su base de datos, consulta el estado de su servidor de bases de datos junto con la información proporcionada para conectarse a aquella base de datos.";
        public const string INFORMACION_ICONO_CONNECTIONDATABASE = @"Icono para realizar la conexión con un vecino de la Organización Virtual. También, se puede realizar la conexión pulsando en una base de datos en el cuadro de la derecha del menu principal."+"\n"
            +"Una vez se haya conectado a un vecino, podrá realizar consultas a sus bases de datos.";
        public const string ICONO_ADDATABASE = @"../../resources/icons/addDatabase.png";
        public const string ICONO_UPDATEDATABASE = @"../../resources/icons/updateDatabase.png";
        public const string ICONO_CONNECTIONDATABASE = @"../../resources/icons/databaseConnection.png";


        public const string LINEA = @"-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
        /*
         * A partir de aquí vienen método y estructuras para mostrar los mensajes por pantalla 
         * */

        public static string Bienvenida(string nombreUsuario)
        {
            return LINEA+"\n"+
                "Bienvenido a la aplicación "+nombreUsuario+ ". \n"+
                "Hemos creado una clave privada y una clave pública para todas las comunicaciones"+
                "\n"+" que se realizarán con los vecinos." +
                "\n"+LINEA+"\n";
        }

        public const int LENGTH_TABLE_VIEW = 130;
        public const string NOMBRE_BASE_DE_DATOS = @"Nombre de Base de Datos: ";
        public const string TIPO_BASE_DE_DATOS = @"Tipo de Base de Datos: ";
        public const string NOMBRE_TABLA = @"Nombre Tabla: ";
        public const string NOMBRE_COLUMNA = @"Nombre de la columna: ";
        public const string TIPO_COLUMNA = @"Tipo de columna: ";

        public const string NOMBRE_COLECCION = @"Nombre colección: ";
        public const string NOMBRE_CAMPO = @"Nombre del campo: ";

        public static string SolicitudConexion(Message messageRequest)
        {
            return "Status: 001 "+ Constants.SOLICITUD_CONEXION + "\n" +
            Constants.USUARIO_SOLICITADO + messageRequest.Destination + "\n";
        }

        public static string RespuestaConexion(Message messageResponse)
        {
            string linea = Columna("-", LENGTH_TABLE_VIEW, '-');
            return "Status: 004 " + Constants.RESPUESTA_CONEXION + "\n" +
           Constants.USUARIO_RESPUESTA + messageResponse.Source + "\n" +
           "Se ha realizado un intercambio de claves públicas con una posterior encriptación de claves simétricas." + "\n" + 
          "A partir de ahora los mensajes con "+messageResponse.Destination+" estarán encriptados con clave simétrica." + "\n"; 

        }

        public static string SolicitudEsquema(Message messageRequest)
        {
            return "Status: " + messageRequest.MessageType + " " + Constants.SOLICITUD_ESQUEMA + "\n" +
                Constants.USUARIO_SOLICITADO + messageRequest.Destination + "\n";
        }

        public static string SolicitudConsulta(Message messageRequest)
        {
            return "Status: " + messageRequest.MessageType + " " + Constants.SOLICITUD_CONSULTA + "\n" +
                Constants.USUARIO_SOLICITADO + messageRequest.Destination + "\n";
        }

        public static String RespuestaConsulta(Message messageResponse)
        {
            BodyRespuesta003 body = new BodyRespuesta003(messageResponse.Body.InnerXml);
            string linea = Columna("-", LENGTH_TABLE_VIEW, '-');

            string stado = "Status: " + messageResponse.MessageType + " " + Constants.RESPUESTA_ESQUEMA + "\n" +
           Constants.USUARIO_RESPUESTA + messageResponse.Source + "\n" +
            linea + "\n" + linea + "\n" +
            Columna(NOMBRE_BASE_DE_DATOS + messageResponse.Db_name, linea.Length) + "\n" +
            linea + "\n" +
           Columna(TIPO_BASE_DE_DATOS + messageResponse.Db_type, linea.Length) + "\n";

            string rows = "";
            int cont=0;
            if (body.Rows.Count == 0)
            {
                rows += "No hay resultados disponibles para su consulta.";
            }
            else
            {
                foreach (Row r in body.Rows)
                {
                    rows += "ROW " + cont + "\n";
                    foreach (KeyValuePair<string, object> attr in r.Attributes)
                    {
                        rows += MostrarValorFirst(messageResponse.Db_type,attr) + "\n";
                    }

                    cont += 1;
                }
            }
            rows += "\n";

                return stado + rows;
        }

        public static string RespuestaEsquemaMySQL(Message messageResponse)
        {
            BodyRespuesta002MySQL body = new BodyRespuesta002MySQL(messageResponse.Body.InnerXml);
            string linea = Columna("-", LENGTH_TABLE_VIEW, '-');

            string stado = "Status: " + messageResponse.MessageType + " " + Constants.RESPUESTA_ESQUEMA + "\n" +
            Constants.USUARIO_RESPUESTA + messageResponse.Source + "\n" +
            linea + "\n" + linea + "\n" +
            Columna(NOMBRE_BASE_DE_DATOS + messageResponse.Db_name, linea.Length) + "\n" +
            linea + "\n" +
           Columna(TIPO_BASE_DE_DATOS + messageResponse.Db_type, linea.Length) + "\n";

            if (body.Error)
            {
                return stado + "\n" + body + "\n";
            }
            else
            {
                string tables = "";
                foreach (Table t in body.Tables)
                {
                    tables += "--" + NOMBRE_TABLA + t.Name + "\n";
                    tables += TABLA_COLUMNAS_CAMPOS + "\n";
                    foreach (Col c in t.Cols)
                    {
                        tables += ColumnaTabla(c.Name, c.Type) + "\n";
                    }
                    tables += TABLA_COLUMNAS + "\n";
                }
                return stado + tables;
            }
        }
        public static string RespuestaEsquemaMongoDB(Message messageResponse)
        {
            BodyRespuesta002MongoDB body = new BodyRespuesta002MongoDB(messageResponse.Body.InnerXml);

                string linea = Columna("-", LENGTH_TABLE_VIEW, '-');
                    string status = "Status: " + messageResponse.MessageType + " " + Constants.RESPUESTA_ESQUEMA + "\n" +
                Constants.USUARIO_RESPUESTA + messageResponse.Source + "\n" +
                linea + "\n" +
                 Columna(" ", LENGTH_TABLE_VIEW, ' ') + "\n" +
                Columna(NOMBRE_BASE_DE_DATOS + messageResponse.Db_name, linea.Length) + "\n" +
                linea + "\n" +
               Columna(TIPO_BASE_DE_DATOS + messageResponse.Db_type, linea.Length) + "\n";

            if (body.Error)
            {
                return status + "\n"+body+"\n";
            }
            else
            {
                string collections = "";
                foreach (Collection c in body.Collections)
                {
                    collections += "--" + NOMBRE_COLECCION + c.Name + "\n";
                    collections += TABLA_COLUMNAS_CAMPOS + "\n";
                    foreach (Field co in c.Fields)
                    {
                        //tables += NOMBRE_COLUMNA + c.Name + "-"+TIPO_COLUMNA +c.Type + "\n";
                        collections += ColumnaTabla(co.Name, co.Type) + "\n";
                    }
                    collections += TABLA_COLUMNAS + "\n";
                }
                return status + collections;
            }
            
        }

        public static string MostrarValorFirst(string db_type, object value)
        {
            KeyValuePair<string, object> attr = (KeyValuePair<string, object>)value;
            string key = attr.Key;
            object valor = attr.Value;

            return MostrarValor(key,db_type, valor);
        }

        public static string MostrarValor(string key,string db_type, object value)
        {
           string resultado = null;
            if(db_type == MYSQL)
            {
                return value.ToString();
            }
            else if(db_type == MONGODB){
                XmlNode c = (XmlNode)value;
                if(!c.HasChildNodes)
                {
                    if(c.ParentNode.Name != key)
                        return "Campo: " + key +"."+ c.ParentNode.Name +" Valor:" + c.Value+"\n";
                    else       
                        return "Campo: " + key + " Valor:" +c.Value+"\n";
                }
                resultado = "";
                XmlNodeList nodes = c.ChildNodes;
                foreach(XmlNode node in nodes)
                {
                    resultado += MostrarValor(key,db_type, node);
                }
                return resultado;
            }
            return resultado;
        }

        private static string Columna(string word, int NSpaces)
        {
            return "|" + word.PadRight(NSpaces) + "|";
        }

        private static string Columna(string word, int NSpaces, char letter)
        {
            return "|" + word.PadRight(NSpaces, letter) + "|";
        }

        private static string TABLA_COLUMNAS = "+-----------+-------------+";
        private static string TABLA_COLUMNAS_CAMPOS = TABLA_COLUMNAS + "\n" +
            "| Campo     | Tipo        |";

        private static string ColumnaTabla(string campo, string tipoCampo)
        {
            return "| " + campo + " | " + tipoCampo + " |";
        }

        public static void BorrarRecursos()
        {
            File.Delete(Constants.CONF_PUBLIC_KEY);
            File.Delete(Constants.CONF_PRIVATE_KEY);
        }

    }

    /* Estructura para recoger la respuesta de 002 MySQL
    * */
    public struct BodyRespuesta002MySQL
    {
        private string _db_name;
        private List<Table> _tables;
        private bool _error;
        private String _errorTrace;

        public BodyRespuesta002MySQL(string body)
        {

            this._tables = new List<Table>();
            XmlDocument x = new XmlDocument();
            x.LoadXml(body);
            this._error = x.SelectSingleNode("/error") == null ? false : true;
            if (!this._error)
            {
                this._db_name = x.DocumentElement.GetAttribute("name");
                foreach (XmlElement infoTable in x.DocumentElement.GetElementsByTagName("table"))
                {
                    this._tables.Add(new Table(infoTable));
                }
                this._errorTrace = null;
            }else{
                this._db_name = null;
                this._errorTrace = x.DocumentElement.GetElementsByTagName("error")[0].InnerText;
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
        public bool Error
        {
            get
            {
                return this._error;
            }
            set
            {
                this._error = value;
            }
        }
        public String ErrorTrace
        {
            get
            {
                return this._errorTrace;
            }
            set
            {
                this._errorTrace = value;
            }
        }
    }

    /* Estructura para recoger la respuesta de 002 MongoDB
    * */
    public struct BodyRespuesta002MongoDB
    {
        private string _db_name;
        private List<Collection> _collections;
        private bool _error;
        private string _errorTrace;

        public BodyRespuesta002MongoDB(string body)
        {
            _collections = new List<Collection>();

            XmlDocument x = new XmlDocument();
            x.LoadXml(body);
            this._error = x.SelectSingleNode("/error") == null ? false : true;
            if (!this._error)
            {
                this._db_name = x.DocumentElement.GetElementsByTagName("name")[0].InnerText;
                foreach (XmlElement coleccion in x.DocumentElement.GetElementsByTagName("colecciones"))
                {
                    this._collections.Add(new Collection(coleccion));
                }
                this._errorTrace = null;
            }else{
                this._db_name = null;
                this._errorTrace = x.DocumentElement.GetElementsByTagName("error")[0].InnerText;
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
        public bool Error
        {
            get
            {
                return this._error;
            }
            set
            {
                this._error = value;
            }
        }
        public String ErrorTrace
        {
            get
            {
                return this._errorTrace;
            }
            set
            {
                this._errorTrace = value;
            }
        }
    }

    public struct BodyRespuesta003
    {
        private List<Row> _rows;

        public BodyRespuesta003(string body)
        {
            XmlDocument x = new XmlDocument();
            x.LoadXml(body);

            this._rows = new List<Row>();
            foreach (XmlElement row in x.DocumentElement.GetElementsByTagName("row"))
            {
                this._rows.Add(new Row(row));
            }
        }

        public List<Row> Rows
        {
            get 
            {
                return this._rows;
            }
            set
            {
                this._rows = value;
            }
        }

    }

    /*DE AQUI PARA ABAJO MINI-ESTRUCTURAS QUE NOS AYUDAN
     * EN LAS ESTRUCTURAS ANTERIORES */
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
        string _type;

        public Field(XmlNode infoField)
        {
            this._name = infoField.Name;
            this._type = infoField.InnerText;
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

    /* Estructura para recoger la respuesta de 002 MongoDB
     * */
    public struct Collection
    {
        private string _name;
        private List<Field> _fields;

        public Collection(XmlElement infoCollection)
        {

            this._fields = new List<Field>();
            this._name = infoCollection.GetElementsByTagName("name")[0].InnerText;

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

    /* Estructura para los resultados de 003
     * */
    public struct Row
    {
        //Atributo-> valor. Example: nombre->lorenzo
        private Dictionary<string, Object> _attributes;

        public Dictionary<string,Object> Attributes
		{
			get
			{
                return this._attributes;
			}
			set
			{
                this._attributes = value;
			}
		}

        public Row(XmlElement infoRow)
        {
            this._attributes = new Dictionary<string, Object>();
            /*  XmlAttributeCollection attributes = infoRow.Attributes;
              foreach (XmlAttribute attr in attributes)
              {
                  this._attributes.Add(attr.Name, attr.Value);
              }*/
            XmlNodeList a = infoRow.ChildNodes;
            foreach(XmlNode r in a)
            {
                if (r.HasChildNodes)
                    this._attributes.Add(r.Name, r);
                else
                    this._attributes.Add(r.Name, r.InnerText);
            }
        }
    }
}
