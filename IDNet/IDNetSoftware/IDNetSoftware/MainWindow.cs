using System;
using Gtk;
using System.IO;


using DatabaseLibraryS;
using MessageLibraryS;
using ConstantsLibraryS;
using CriptoLibraryS;

using System.Collections.Generic;

using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;


namespace IDNetSoftware
{
    /*
     * Estructua que nos sirve para guardar el intercambio de mensajes en cada accion
     * */
    public class PipeMessage
    {
        Message _messageRequest;
        Message _messageResponse;
        RsaKeyParameters _publicKey;
        SymmetricAlgorithm _symmetricKey;

        public PipeMessage(){}
        public PipeMessage(Message mrequest, Message mresponse, RsaKeyParameters publicKey,SymmetricAlgorithm key)
        {
            this._messageRequest = mrequest;
            this._messageResponse = mresponse;
            this._publicKey = publicKey;
            this._symmetricKey = key;
        }
        public Message MessageRequest
        {
            get
            {
                return this._messageRequest;
            }
            set
            {
                this._messageRequest = value;
            }
        }
        public Message MessageResponse
        {
            get
            {
                return this._messageResponse;
            }
            set
            {
                this._messageResponse = value;
            }
        }
		public RsaKeyParameters PublicKey
		{
			get
			{
                return this._publicKey;
			}
			set
			{
                this._publicKey = value;
			}
		}
        public SymmetricAlgorithm SymmetricKey
		{
			get
			{
                return this._symmetricKey;
			}
			set
			{
                this._symmetricKey = value;
			}
		}
    }
    public class Usuario
    {
        private string _nombre; 
        public string Nombre
        {
            get
            {
                return this._nombre;
            }
            set
            {
                this._nombre = value;
            }
        }

        public Usuario()
        {
            ParseConf();
        }

        //Lee del fichero de configuración
        private void ParseConf()
        {
            //Archivo a leer
            StreamReader conFile = File.OpenText(Constants.ConfigFileInfoUser);
            string line = conFile.ReadLine();

            //Voy leyendo línea por línea
            while (line != null)
            {
                int i = 0;
                bool firstParam = true;
                string user = "";
                /*
                 * 
                 * nombre=userName;
                 * 
                 * Ejemplo:
                 * nombre=lorenzo;
                 * 
                 */
                //Leemos el parámetro
                while (line[i] != ';')
                {
                    if (line[i] == '=')
                    {
                        firstParam = false;
                    }else if(!firstParam)
                    {
                        user += line[i];
                    }
                    i++;
                }
                this._nombre = user;
                line = conFile.ReadLine();
            }
            conFile.Close();
        }

    }

    public partial class MainWindow : Gtk.Window
    {
        //Usuario actual
        Usuario _user;

        //Lista que se muestra
        ListStore _infoBBDDView;

        //Lista que se muestra de las BBDD propias
        ListStore _infoBBDDownView;

        //Atributo de las bases de datos propias
        Databases _databases;

        //Atributo con los vecinos
        Neighbours _neighbours;

        //Dialogo de añadir BBDD
        AddDatabaseDialog _addDatabaseDialog;

        //Dialogo de modificar BBDD
        ModifyDatabaseDialog _modifyDatabaseDialog;

        //Dialogo de error en algun servidor
        ErrorServersDialog _errorServerDialog;

        //Dialogo de conexion
        ConnectionDialog _connectionDialog;

        //Numero conexiones activas
        int _conexionesActivas;

        //Diccionario con todos los mensajes (neighbour -> [(tipobbdd,nombrebbdd,(connection->Pipe,schema->Pipe,select->Pipe))])
        Dictionary<string, List<Tuple<string,string,Dictionary<string, PipeMessage>>>> _messages;

        //Clave pública y privada
        Cripto _claves;

        //Diccionario con claves públicas y simétricas de los clientes
        Dictionary<string, Tuple<RsaKeyParameters,SymmetricAlgorithm>> _keyPairClients;

		public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();

            this._user = new Usuario();
                
            infoview.ModifyFont(new Pango.FontDescription());

            this._infoBBDDView = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));
            this._infoBBDDownView = new ListStore(typeof(string), typeof(string),typeof(bool));

            this._databases = new Databases();
            this._neighbours = new Neighbours();
            this._keyPairClients = new Dictionary<string, Tuple<RsaKeyParameters, SymmetricAlgorithm>>();

			this._messages = new Dictionary<string, List<Tuple<string,string,Dictionary<string, PipeMessage>>>>();

            this._conexionesActivas = 0;

            CargoBasesDeDatosDeLaOV();

			ComprobacionServidoresBaseDeDatos();

			CargoBasesDeDatosPropias();

            GenerarParDeClaves();

            MensajeBienvenida();
        }

        private void CargoBasesDeDatosDeLaOV()
        {
            //AQUI HACE FALTA OBTENER BBDD de OTROS (conexion cliente con el GK para que nos dé tal información)

            //Añado valores a la lista
            AddValues();

            //Añado al treeview la información
            treeviewDatabases.Model = this._infoBBDDView;

            //Añado las columnas
            treeviewDatabases.AppendColumn(Constants.TABLA_COLUMNA_USUARIO, new CellRendererText(), "text", 0);
            treeviewDatabases.AppendColumn(Constants.TABLA_COLUMNA_TIPOBBDD, new CellRendererText(), "text", 1);
            treeviewDatabases.AppendColumn(Constants.TABLA_COLUMNA_NOMBREBBDD, new CellRendererText(), "text", 2);
        }

        private void CargoBasesDeDatosPropias()
        {

            //Añado valores a la lista
            AddValuesOwn();

            //Añado al treeview la información
            treeviewDatabasesPropias.Model = this._infoBBDDownView;

            //Añado las columnas
            treeviewDatabasesPropias.AppendColumn(Constants.TABLA_COLUMNA_NOMBREBBDD, new CellRendererText(), "text", 0);
            treeviewDatabasesPropias.AppendColumn(Constants.TABLA_COLUMNA_TIPOBBDD, new CellRendererText(), "text", 1);
            treeviewDatabasesPropias.AppendColumn(Constants.TABLA_COLUMNA_FUNCIONA,new CellRendererToggle(), "active", 2);
        }

        /*
         * Método privado para comprobar que los servidores de bases de datos funcionan
         * */
        private void ComprobacionServidoresBaseDeDatos()
        {
            bool ok = false;
            string messageError = "";
            Dictionary<string, string> errors = new Dictionary<string, string>();

            foreach (KeyValuePair<string, List<Tuple<string,string,string>>> entry in this._databases.DatabasesPropias)
            {
                switch (entry.Key)
                {

                    case Constants.MYSQL:
                        try
                        {
                            ok = this._databases.ComprobacionMysql();
                        }
                        catch (Exception e)
                        {
                            messageError = e.Message;
                            errors.Add(Constants.MYSQL, messageError);
                        }

                        break;
                    case Constants.MONGODB:
                        ok = this._databases.ComprobacionMongodb();
                        if (!ok)
                        {
                            errors.Add(Constants.MONGODB, Constants.UNABLE_CONNECT_MONGODB);
                        }
                        break;
                }
            }
            if (!ok && errors.Count != 0)
            {
                this._errorServerDialog = new ErrorServersDialog(this._databases, errors);
                this._errorServerDialog.Run();
            }
        }

        private void GenerarParDeClaves()
        {
            this._claves = new Cripto();
        }

        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
            a.RetVal = true;
        }

        //Icono añadirBasededatos
        protected void OnAddDatabasePngActionActivated(object sender, EventArgs e)
        {
            this._addDatabaseDialog = new AddDatabaseDialog(this._databases);
            this._addDatabaseDialog.Run();

            UpdateOwnDatabases();
        }

        /*
         * Método privado para la actualización de las bases de datos propias
         * */
        private void UpdateOwnDatabases()
        {
            this._databases.update();

            //Limpiamos
            this._infoBBDDownView.Clear();

            AddValuesOwn();
            treeviewDatabasesPropias.Model = this._infoBBDDownView;
        }

        //Menú 'Base de datos' opción 'Añadir'
        protected void OnAddActionActivated(object sender, EventArgs e)
        {
            this._addDatabaseDialog = new AddDatabaseDialog(this._databases);
            this._addDatabaseDialog.Run();

            UpdateOwnDatabases();
        }

        //Añadir info de las bases de datos DE OTROS
        private void AddValues()
        {
            foreach (KeyValuePair<string, Dictionary<string, List<string>>> entry in this._neighbours.MiembrosOV)
            {
                foreach (KeyValuePair<string, List<string>> entryTwo in entry.Value)
                {
                    foreach (string bbdd in entryTwo.Value)
                    {
                        this._infoBBDDView.AppendValues(entry.Key, entryTwo.Key, bbdd);
                    }
                }

            }
        }

        //Añadir info de las bases de datos PROPIAS
        private void AddValuesOwn()
        {
            //Recorremos las bases de datos para mostrarlas
            foreach (KeyValuePair<string, List<Tuple<string,string,string>>> entry in this._databases.DatabasesPropias)
            {
                foreach (Tuple<string, string, string> bbdd in entry.Value)
                {
                    bool works = false;
                    if (this._databases.ComprobacionServidor(entry.Key,bbdd.Item1,bbdd.Item2,bbdd.Item3))
                    {
                        works = true;
                    }else{
                        works = false;
                    }
                    this._infoBBDDownView.AppendValues(bbdd.Item1, entry.Key,works);
                }
                // do something with entry.Value or entry.Key
            }
        }

        //Menú opción salir
        protected void OnSalirActionActivated(object sender, EventArgs e)
        {
            Constants.BorrarRecursos();
            Application.Quit();
        }

        /*
         * Evento cuando cliqueas en una base de datos propias
         * */
        protected void OnTreeviewDatabasesPropiasRowActivated(object o, RowActivatedArgs args)
        {
            TreeIter t;
            TreePath p = args.Path;
            this._infoBBDDownView.GetIter(out t, p);

            string nombreBBDD = (string)this._infoBBDDownView.GetValue(t, 0);
            string tipoBBDD = (string)this._infoBBDDownView.GetValue(t, 1);
            string usuarioBBDD = GetUserDatabase(tipoBBDD, nombreBBDD);
            string passwordBBDD = GetPasswordDatabase(tipoBBDD, nombreBBDD);
            List<string> bbdd = new List<string>();
            bbdd.Add(tipoBBDD);
            bbdd.Add(nombreBBDD);
            bbdd.Add(usuarioBBDD);
            bbdd.Add(passwordBBDD);

            this._modifyDatabaseDialog = new ModifyDatabaseDialog(this._databases, bbdd);
            this._modifyDatabaseDialog.Run();

            UpdateOwnDatabases();
        }

        /*
         * Método activado cuando se pulsa en la lista de BBDD de vecinos
         * */
        protected void OnTreeviewDatabasesRowActivated(object o, RowActivatedArgs args)
        {
            TreeIter t;
            TreePath p = args.Path;
            this._infoBBDDView.GetIter(out t, p);

            string usuario = (string)this._infoBBDDView.GetValue(t, 0);
            string tipoBBDD = (string)this._infoBBDDView.GetValue(t, 1);
            string nombreBBDD = (string)this._infoBBDDView.GetValue(t, 2);

            //Comprobamos que está el usuario y si la tupla que tiene es del mismo (tipoBBDD,nombreBBDD)
            if (this._messages.ContainsKey(usuario))
            {
                Tuple<string, string, Dictionary<string, PipeMessage>> tupla = DevuelveTupla(usuario, tipoBBDD, nombreBBDD);

                if (tupla == null)
                {
                    Tuple<string, string, Dictionary<string, PipeMessage>> firstTupla = DevuelvePrimeraTupla(usuario);
                    int index = this._messages[usuario].IndexOf(firstTupla);

                    PipeMessage pipeConexion = this._messages[usuario][index].Item3[Constants.CONNECTION];

					this._connectionDialog = new ConnectionDialog(usuario, tipoBBDD, nombreBBDD, pipeConexion, this._claves);
                }
                else
                {
                    int index = this._messages[usuario].IndexOf(tupla);

                    if (ComprobarTuplaConSchema(usuario, tipoBBDD, nombreBBDD))
                    {
                        PipeMessage pipeConexion = this._messages[usuario][index].Item3[Constants.CONNECTION];
                        PipeMessage pipeSchema = this._messages[usuario][index].Item3[Constants.SCHEMA];

                        this._connectionDialog = new ConnectionDialog(
                            usuario, tipoBBDD, nombreBBDD, pipeConexion, pipeSchema, this._claves);
                    }
                    else
                    {
                        PipeMessage pipeConexion = this._messages[usuario][index].Item3[Constants.CONNECTION];

                        this._connectionDialog = new ConnectionDialog(usuario, tipoBBDD, nombreBBDD, pipeConexion, this._claves);
                    }
                }
            }
            else
            {
                this._connectionDialog = new ConnectionDialog(usuario, tipoBBDD, nombreBBDD,this._claves);
            }

            this._connectionDialog.Run();

            switch (this._connectionDialog.TypeOutPut)
            {
                case Constants.CANCEL:

                    break;
                case Constants.MENSAJE_CONEXION:
                    Dictionary<string, PipeMessage> messageC = new Dictionary<string, PipeMessage>();
                    messageC.Add(Constants.CONNECTION, this._connectionDialog.Connection);
                    Tuple<string, string, Dictionary<string, PipeMessage>> tupl =
                        new Tuple<string, string, Dictionary<string, PipeMessage>>(tipoBBDD,nombreBBDD,messageC);

                    if (!this._messages.ContainsKey(usuario))
                    {
                        List<Tuple<string, string, Dictionary<string, PipeMessage>>> lista = new List<Tuple<string, string, Dictionary<string, PipeMessage>>>();
                        lista.Add(tupl);

                        this._messages.Add(usuario, lista);
                    }else{
                        this._messages[usuario].Add(tupl);
                    }

                    if(!this._keyPairClients.ContainsKey(usuario))
                    {
                        Tuple<RsaKeyParameters, SymmetricAlgorithm> tup = new Tuple<RsaKeyParameters, SymmetricAlgorithm>(this._connectionDialog.Connection.PublicKey,this._connectionDialog.Connection.SymmetricKey);
						this._keyPairClients.Add(usuario,tup);
					}

                    MostrarSolicitudConexion(this._connectionDialog.Connection.MessageRequest);
                    MostrarConexion(this._connectionDialog.Connection.MessageResponse);
                    IncrementarConexionesActivas();

                    break;

                case Constants.MENSAJE_ESQUEMA:
                    
                    if (!ComprobarTuplaConSchema(usuario, tipoBBDD, nombreBBDD))
                    {
						Tuple<string, string, Dictionary<string, PipeMessage>> tuplaa = DevuelveTupla(usuario, tipoBBDD, nombreBBDD);

						int indexx = this._messages[usuario].IndexOf(tuplaa);

                        this._messages[usuario][indexx].Item3.Add(Constants.SCHEMA, this._connectionDialog.Schema);
                    }

					MostrarSolicitudEsquema(this._connectionDialog.Schema.MessageRequest);
                    MostrarEsquema(this._connectionDialog.Schema.MessageResponse);
                    break;

                case Constants.MENSAJE_CONSULTA:
					Tuple<string, string, Dictionary<string, PipeMessage>> tupla = DevuelveTupla(usuario, tipoBBDD, nombreBBDD);

					int index = this._messages[usuario].IndexOf(tupla);
                    if(this._messages[usuario][index].Item3.ContainsKey(Constants.SELECT))
                    {
                        this._messages[usuario][index].Item3[Constants.SELECT] = this._connectionDialog.Select;
                    }else{
                        this._messages[usuario][index].Item3.Add(Constants.SELECT, this._connectionDialog.Select);
					}

                    MostrarSolicitudConsulta(this._connectionDialog.Select.MessageRequest);
                    MostrarResultadoConsulta(this._connectionDialog.Select.MessageResponse);

					break;

                case Constants.ERROR_CONNECTION:
                    MostrarError(this._connectionDialog.TypeOutPut);
                    break;
            }

        }

        private Tuple<string, string, Dictionary<string, PipeMessage>> DevuelveTupla(string usuario, string tipoBBDD, string nombreBBDD)
        {
			foreach (var tupla in this._messages[usuario])
			{
				if (tupla.Item1 == tipoBBDD && tupla.Item2 == nombreBBDD)
				{
					 return tupla;
				}
			}
            return null;
		}
		private Tuple<string, string, Dictionary<string, PipeMessage>> DevuelvePrimeraTupla(string usuario)
		{
			foreach (var tupla in this._messages[usuario])
			{
                return tupla;
			}
			return null;
		}

		private Tuple<string, string, string> devuelveTuplaDatabase(string tipoBBDD, string nombreBBDD)
		{
            foreach (var tupla in this._databases.DatabasesPropias[tipoBBDD])
			{
				if (tupla.Item1 == nombreBBDD)
				{
					return tupla;
				}
			}
			return null;
		}

        /*
         * Método privado para conseguir un usuario a partir del tipo de base de datos y del nombre de la base de datos
         * */
		private string GetUserDatabase(string databaseType, string databaseName)
		{

            int index = this._databases.DatabasesPropias[databaseType].IndexOf(devuelveTuplaDatabase(databaseType, databaseName));
            return this._databases.DatabasesPropias[databaseType][index].Item2 == null ? null : this._databases.DatabasesPropias[databaseType][index].Item2;
		}

        /*
         * Método privado para conseguir la contraseña a partir del tipo de base de datos y del nombre de la base de datos
         * */
		private string GetPasswordDatabase(string databaseType, string databaseName)
		{
			int index = this._databases.DatabasesPropias[databaseType].IndexOf(devuelveTuplaDatabase(databaseType, databaseName));
			return this._databases.DatabasesPropias[databaseType][index].Item3 == null ? null : this._databases.DatabasesPropias[databaseType][index].Item3;
		}

        /*
         * Método privado para comprobar que existe la tupla de esquema
         * */
        private bool ComprobarTuplaConSchema(string usuario,string tipoBBDD,string nombreBBDD)
        {
			Tuple<string, string, Dictionary<string, PipeMessage>> tupl = DevuelveTupla(usuario, tipoBBDD, nombreBBDD);

            if(tupl != null && tupl.Item3.ContainsKey(Constants.SCHEMA))
            {
                return true;
            }

            return false;
        }

		/*A PARTIR DE AQUI SON METODOS PARA MOSTRAR RESULTADOS DE LAS ACCIONES*/

		/*
         * Método privado para mostrar la solicitud de conexión de BBDD
         * */
		private void MostrarSolicitudConexion(Message messageRequest)
		{
			infoview.Buffer.Text += "\n" + Constants.SolicitudConexion(messageRequest);
		}

		/*
         * Método privado para mostrar la respuesta a la solicitud de esquema de BBDD
         * */
		private void MostrarConexion(Message messageResponse)
		{
			infoview.Buffer.Text += "\n" + Constants.RespuestaConexion(messageResponse);
		}

        /*
         * Método privado para mostrar la solicitud de esquema de BBDD
         * */
        private void MostrarSolicitudEsquema(Message messageRequest)
        {
            infoview.Buffer.Text += "\n" + Constants.SolicitudEsquema(messageRequest);
        }

        /*
         * Método privado para mostrar la respuesta a la solicitud de esquema de BBDD
         * */
        private void MostrarEsquema(Message messageResponse)
        {
		/*	TextTag tag = new TextTag("xx-small");
            tag.Size = (int) Pango.Scale.PangoScale * 9;
			infoview.Buffer.TagTable.Add(tag);
			TextIter insertIter = infoview.Buffer.EndIter;*/

            if (messageResponse.Db_type == Constants.MYSQL)
            //    infoview.Buffer.InsertWithTags(ref insertIter,
              //                                 "\n"+Constants.RespuestaEsquemaMySQL(messageResponse),
                //                              tag);
                infoview.Buffer.Text += "\n" + Constants.RespuestaEsquemaMySQL(messageResponse);
            else if(messageResponse.Db_type == Constants.MONGODB)
                infoview.Buffer.Text += "\n" + Constants.RespuestaEsquemaMongoDB(messageResponse);
        }

		/*
         * Método privado para mostrar la solicitud de consulta
         * */
		private void MostrarSolicitudConsulta(Message messageRequest)
		{
			infoview.Buffer.Text += "\n" + Constants.SolicitudConsulta(messageRequest);
		}

		/*
         * Método privado para mostrar la respuesta a la solicitud de esquema de BBDD
         * */
		private void MostrarResultadoConsulta(Message messageResponse)
		{
			infoview.Buffer.Text += "\n" + Constants.RespuestaConsulta(messageResponse);
		}

        /*
         * Método privado para incrementar las conexiones activas
         * */
        private void IncrementarConexionesActivas()
        {
            this._conexionesActivas += 1;
            if(this._conexionesActivas == 1)
            {
                conexionesLabel.Text = this._conexionesActivas+" conexión activa";

			}else{
				conexionesLabel.Text = this._conexionesActivas + " conexiones activas";
			}
        }

        /*
         * Método privado para mostrar los errores producidos
         * */
        private void MostrarError(string error)
        {
            infoview.Buffer.Text += "\n" + error;
        }

        /*
         * Método privado para dar la bienvenida al usuario
         * */
        private void MensajeBienvenida()
        {
            infoview.Buffer.Text =  Constants.Bienvenida(this._user.Nombre);
        }

        /*
         * Limpiado de consola
         * */
        protected void OnClearActionActivated(object sender, EventArgs e)
        {
            infoview.Buffer.Clear();
        }
    }
}