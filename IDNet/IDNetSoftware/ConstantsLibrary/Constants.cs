using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections.Generic;

using MessageLibraryS;


namespace ConstantsLibraryS
{
    //Clase con las constantes
    public static class Constants
    {
        public const string IDNETDAEMON = @"IDNetDaemon";
        public static string CONFIG = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"./configuration");
        public static string ConfigFileDatabases = Path.Combine(CONFIG, @"databases.conf");
        public static string ConfigFileNeighbours = Path.Combine(CONFIG, @"neighbours.conf");
        public static string ConfigFileNeighboursDatabases = Path.Combine(CONFIG, @"neighboursDatabases.conf");
        public static string ConfigFileInfoUser = Path.Combine(CONFIG, @"info.conf");

        public static string CONF_PUBLIC_KEY = Path.Combine(CONFIG, @"publicKeyIDNet.pem");
        public static string CONF_PRIVATE_KEY = Path.Combine(CONFIG, @"privateKeyIDNet.pem");

        public static byte[] SYMMETRIC_KEY = Convert.FromBase64String("AAECAwQFBgcICQoLDA0ODw==");
        public static byte[] SYMMETRIC_IV = Convert.FromBase64String("AAECAwQFBgcICQoLDA0ODw==");

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

        public const string ACTUALIZACION = @"Actualizada la disponibilidad de los servidores";

        public const string TYPE_MODIFY = @"type";
        public const string TYPE_DELETE = @"delete";
        public const string MYSQL_REMOTE_NAMEBBDD = @"IDNet";
        public const string MYSQL_REMOTE_SERVERBBDD = @"mysqlinstance.crfd5ylvvpz8.eu-west-2.rds.amazonaws.com";
        public const string MYSQL_REMOTE_NAMETABLE = @"usuariosIDNet";
        public const string MYSQL_REMOTE = @"Server=" + MYSQL_REMOTE_SERVERBBDD + ";Database=" + MYSQL_REMOTE_NAMEBBDD + ";User ID=root;Password=admin1234;Pooling=false;";
        public const string MYSQL_REMOTE_ERROR_INICIO_SESION = @"Usuario y/o contraseña no válidos.";
        public const string MYSQL_REMOTE_ERROR_REGISTRARSE_CONTRASEÑA = @"Fallo en la repetición de la contraseña.";
        public const string MYSQL_REMOTE_LOGIN_SUCCESS = @"Se ha iniciado correctamente.";
        public const string MYSQL_REMOTE_REGISTER_SUCCESS = @"Se ha registrado correctamente.";
        public const string MYSQL_REMOTE_REGISTER_EXISTS = @"Ya existe ese nombre de usuario.";

        public static string MysqlRemoteSelect()
        {
            return "SELECT * FROM " + MYSQL_REMOTE_NAMETABLE + " WHERE username=@username";
        }
        public static string MysqlRemoteInsert()
        {
            return "INSERT INTO " + MYSQL_REMOTE_NAMETABLE + "(username,password,code) VALUES(@username, @password,@code)";
        }
        public static string MysqlRemoteUpdate()
        {
            return "UPDATE " + MYSQL_REMOTE_NAMETABLE + " SET code=@code WHERE username=@username";
        }

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

        public static string GATEKEEPER = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();

        //IP del GK en AWS
        //public const string GATEKEEPER = @"172.16.1.49";
        public const int GATEKEEPER_PORT = 11000;

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
        public const string MENSAJE_CONSULTA_BBDD_VECINOS = "011";

        public const string MENSAJE_RESPUESTA_CONEXION_A = "004a";
        public const string MENSAJE_RESPUESTA_CONEXION_B = "004b";

        public const string INFORMACION_ICONO_ADDATABASE = @"Icono para añadir una base de datos con su información, con el fin de que los vecinos puedan realizar consultas a su base de datos."+
            "Durante la inserción de la base de datos, se le informará de la información que debe de adquirir para añadir la base de datos (nombre,tipo de base de datos,usuario y contraseña)";
        public const string INFORMACION_ICONO_UPDATEDATABASE = @"Icono para actualizar el estado de las bases de datos."+"\n"+"Los estados en los que se puede encontrar una base de datos propia son los siguientes: Disponible y No disponible."+"\n"+
            "Si le notifica que no está disponible su base de datos, consulta el estado de su servidor de bases de datos junto con la información proporcionada para conectarse a aquella base de datos.";
        public const string INFORMACION_ICONO_CONNECTIONDATABASE = @"Icono para realizar la conexión con un vecino de la Organización Virtual. También, se puede realizar la conexión pulsando en una base de datos en el cuadro de la derecha del menu principal."+"\n"
            +"Una vez se haya conectado a un vecino, podrá realizar consultas a sus bases de datos.";
        public const string INFORMACION_ICONO_SELECTDATABASE = @"Icono para realizar la consulta de la base de datos de un vecino de la Organización Virtual.";
        public const string INFORMACION_ICONO_SCHEMADATABASE = @"Icono para solicitar el esquema de la base de datos de un vecino de la Organización Vitual.";
        public const string ICONO_ADDATABASE = @"../../resources/icons/addDatabase.png";
        public const string ICONO_UPDATEDATABASE = @"../../resources/icons/updateDatabase.png";
        public const string ICONO_CONNECTIONDATABASE = @"../../resources/icons/connection.png";
        public const string ICONO_SELECTDATABASE = @"../../resources/icons/select.png";
        public const string ICONO_SCHEMADATABASE = @"../../resources/icons/schema.png";

        public static string PathClavePublica(string source)
        {
            return Path.Combine(CONFIG, "publicKey" + source + ".pem");
        }

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

        public static string Actualizacion()
        {
            return LINEA + "\n" +ACTUALIZACION +"\n" + LINEA + "\n";
        }

        public static string AdiccionBBDD(List<string> bbdd, bool success)
        {
            return success?LINEA + "\n" + "Se ha añadido satisfactoriamente la base de datos '"+bbdd[1]+"' de tipo '"+bbdd[0]+"' " + "\n" + LINEA + "\n":
                LINEA + "\n" + "No se ha podido añadir la base de datos. Revise los campos requeridos para la base de datos." +"\n" + LINEA + "\n";
        }
        public static string ErrorAdiccionBBDD(List<string> bbdd)
        {
            return  LINEA + "\n" + "No se ha podido añadir la base de datos '" + bbdd[1] + "' de tipo '" + bbdd[0] + "' " + "\n" +
                "Revise los permisos de creación de ficheros en su carpeta," + "\n" + "debido a que IDNet necesita poder crear/modificar/borrar sus archivos de configuración."+LINEA + "\n";
        }
        public static string BorradoBBDD(List<string> bbdd, bool success)
        {
            string borrado = "";
            if(success){
              borrado+= LINEA + "\n" + "Se ha borrado satisfactoriamente la base de datos '" + bbdd[1] + "' de tipo '" + bbdd[0] + "' " + "\n" + LINEA + "\n"; 
            }
            else{
                borrado += bbdd.Count == 0 ? "No hay bases de datos guardadas en IDNet." + "\n" + LINEA + "\n" : LINEA + "\n" + "No se ha podido borrar la base de datos de nuestra aplicación." + "\n" + "Revise los campos requeridos para el borrado de la base de datos." + "\n" + LINEA + "\n";
            }
            return borrado;
        }
        public static string ErrorBorradoBBDD(List<string> bbdd)
        {
            string borrado = "";
            if (bbdd.Count != 0)
            {
                borrado = LINEA + "\n" + "No se ha podido borrar la base de datos '" + bbdd[1] + "' de tipo '" + bbdd[0] + "' de nuestra aplicación." + "\n"+ "Revise los permisos de creación de ficheros en su carpeta," + "\n" + "debido a que IDNet necesita poder crear/modificar/borrar sus archivos de configuración." + LINEA + "\n";  
            }
            else
            {
                borrado += LINEA + "\n" + "No se ha podido borrar la base de datos" + "\n" + "No hay bases de datos guardadas en IDNet." + "\n" + LINEA + "\n";
            }

            return borrado;
        }
        public static string ModifyBBDD(List<string> bbdd, bool success)
        {
            return success ? LINEA + "\n" + "Se ha modificado satisfactoriamente la base de datos '" + bbdd[1] + "' de tipo '"+ bbdd[0] +"'. " + "\n" + LINEA + "\n" :
                LINEA + "\n" + "No se ha podido modificar la base de datos. Revise los campos de la la base de datos." + "\n" + LINEA + "\n";
        }

        public static string DeleteBBDD(List<string> bbdd, bool success)
        {
            return success ? LINEA + "\n" + "Se ha borrado satisfactoriamente la base de datos '" + bbdd[1] + "' de tipo '"+ bbdd[0] +"'. " + "\n" + LINEA + "\n" :
                LINEA + "\n" + "No se ha podido borrar la base de datos. Revise los campos de la base de datos." + "\n" + LINEA + "\n";
        }

        public static string FalloCargaVecinos()
        {
            return LINEA + "\n" + "Se ha producido un fallo a la hora de cargar los vecinos de la Organización Virtual."+"\n" + "Consulte con el administrador" +"\n" + LINEA + "\n";
        }

        public const int LENGTH_TABLE_VIEW = 130;
        public const string NOMBRE_BASE_DE_DATOS = @"Nombre de Base de Datos: ";
        public const string TIPO_BASE_DE_DATOS = @"Tipo de Base de Datos: ";
        public const string NOMBRE_TABLA = @"Nombre Tabla: ";
        public const string NOMBRE_COLUMNA = @"Nombre de la columna: ";
        public const string TIPO_COLUMNA = @"Tipo de columna: ";

        public const string NOMBRE_COLECCION = @"Nombre colección: ";
        public const string NOMBRE_CAMPO = @"Nombre del campo: ";

        /*
         * Método estático para devolver la solicitud de conexión en String
         * */
        public static string SolicitudConexion(Message messageRequest)
        {
            return "Status: 001 "+ Constants.SOLICITUD_CONEXION + "\n" +
            Constants.USUARIO_SOLICITADO + messageRequest.Destination + "\n";
        }

        /*
         * Método estático para devolver la respuesta de conexión en String
         * */
        public static string RespuestaConexion(Message messageResponse)
        {
            string linea = Columna("-", LENGTH_TABLE_VIEW, '-');
            return "Status: 004 " + Constants.RESPUESTA_CONEXION + "\n" +
           Constants.USUARIO_RESPUESTA + messageResponse.Source + "\n" +
           "Se ha realizado un intercambio de claves públicas con una posterior encriptación de claves simétricas." + "\n" + 
          "A partir de ahora los mensajes con "+messageResponse.Destination+" estarán encriptados con clave simétrica." + "\n"; 

        }

        /*
         * Método estático para devolver la solicitud de esquema en String
         * */
        public static string SolicitudEsquema(Message messageRequest)
        {
            return "Status: " + messageRequest.MessageType + " " + Constants.SOLICITUD_ESQUEMA + "\n" +
                Constants.USUARIO_SOLICITADO + messageRequest.Destination + "\n";
        }

        /*
         * Método estático para devolver la solicitud de consulta en String
         * */
        public static string SolicitudConsulta(Message messageRequest)
        {
            return "Status: " + messageRequest.MessageType + " " + Constants.SOLICITUD_CONSULTA + "\n" +
                Constants.USUARIO_SOLICITADO + messageRequest.Destination + "\n";
        }

        /*
         * Método estático para devolver la respuesta de consulta en String
         * */
        public static String RespuestaConsulta(Message messageRequest,Message messageResponse)
        {
            BodyRespuesta003 body = new BodyRespuesta003(messageRequest.Body.InnerXml,messageResponse.Body.InnerXml);
            string linea = Columna("-", LENGTH_TABLE_VIEW, '-');

            string stado = "Status: " + messageResponse.MessageType + " " + Constants.RESPUESTA_ESQUEMA + "\n" +
           Constants.USUARIO_RESPUESTA + messageResponse.Source + "\n" +
            linea + "\n" + linea + "\n" +
            Columna(NOMBRE_BASE_DE_DATOS + messageResponse.Db_name, linea.Length) + "\n" +
            linea + "\n" +
           Columna(TIPO_BASE_DE_DATOS + messageResponse.Db_type, linea.Length) + "\n" +
            messageResponse.Db_type== MYSQL?Columna(NOMBRE_TABLA + body.TableCollection, linea.Length):Columna(NOMBRE_COLECCION + body.TableCollection, linea.Length) + "\n";

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

        /*
         * Método estático para devolver la respuesta de esquema MySQL en String
         * */
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

        /*
         * Método estático para devolver la respuesta de esquema MongoDB en String
         * */
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

        private static string MostrarValorFirst(string db_type, object value)
        {
            KeyValuePair<string, object> attr = (KeyValuePair<string, object>)value;
            string key = attr.Key;
            object valor = attr.Value;

            return MostrarValor(key,db_type, valor);
        }

        private static string MostrarValor(string key,string db_type, object value)
        {
           string resultado = null;
            if(db_type == MYSQL)
            {
                return key+ ":"+value.ToString();
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

        /*
         * Método estático público para el borrado de recursos
         * */
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
        private string _tableCollection;

        public BodyRespuesta003(string bodyRequest,string bodyResponse)
        {
            XmlDocument x = new XmlDocument();
            x.LoadXml(bodyResponse);

            XmlDocument z = new XmlDocument();
            z.LoadXml(bodyRequest);

            //MYSQL
            if(z.DocumentElement.SelectSingleNode("from")!=null){
                this._tableCollection = z.DocumentElement.GetElementsByTagName("from")[0].InnerText;
            }
            //MONGODB
            else if(z.DocumentElement.SelectSingleNode("collection") != null){
                this._tableCollection = z.DocumentElement.GetElementsByTagName("collection")[0].InnerText;
            }else{
                this._tableCollection = null;
            }

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
        public string TableCollection
        {
            get
            {
                return this._tableCollection;
            }
            set
            {
                this._tableCollection = value;
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

            //MYSQL
            if (infoRow.HasAttributes)
            {
                  XmlAttributeCollection attributes = infoRow.Attributes;
                  foreach (XmlAttribute attr in attributes)
                  {
                      this._attributes.Add(attr.Name, attr.Value);
                  }
            }
            else //MONGODB
            {
                XmlNodeList a = infoRow.ChildNodes;
                foreach (XmlNode r in a)
                {
                    if (r.HasChildNodes)
                        this._attributes.Add(r.Name, r);
                    else
                        this._attributes.Add(r.Name, r.InnerText);
                }
            }
        }
    }
}
