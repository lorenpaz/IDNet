using System;
using Gtk;
using System.IO;
using System.Net;
using System.Xml;

using DatabaseLibraryS;
using MessageLibraryS;
using ConstantsLibraryS;
using CriptoLibraryS;

using System.Collections.Generic;

using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;
using PostBoxLibraryS;
using ConnectionLibraryS;

namespace IDNetSoftware
{
    /*
     * Estructua que nos sirve para guardar el intercambio de mensajes en cada accion
     * */
    public class PipeMessage
    {
        Message _messageRequestConexion;
        Message _messageResponseConexion;
        Message _messageRequest;
        Message _messageResponse;
        RsaKeyParameters _publicKey;
        SymmetricAlgorithm _symmetricKey;

        public PipeMessage() { }
        public PipeMessage(Message mrequest, Message mresponse, RsaKeyParameters publicKey, SymmetricAlgorithm key)
        {
            this._messageRequest = mrequest;
            this._messageResponse = mresponse;
            this._publicKey = publicKey;
            this._symmetricKey = key;
        }
        public PipeMessage(Message mrequestConexion, Message mresponseConexion, Message mrequest, Message mresponse, RsaKeyParameters publicKey, SymmetricAlgorithm key)
        {
            this._messageRequestConexion = mrequestConexion;
            this._messageResponseConexion = mresponseConexion;
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
        public Message MessageRequestConexion
        {
            get
            {
                return this._messageRequestConexion;
            }
            set
            {
                this._messageRequestConexion = value;
            }
        }
        public Message MessageResponseConexion
        {
            get
            {
                return this._messageResponseConexion;
            }
            set
            {
                this._messageResponseConexion = value;
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

    public partial class MainWindow : Gtk.Window
    {
        //Usuario actual
        Usuario _user;

        //Lista que se muestra
        ListStore _infoBBDDView;

        //Lista que se muestra de las BBDD propias
        ListStore _infoBBDDownView;

        //Lista que muestra los vecinos
        ListStore _infoNeighboursView;

        //Atributo de las bases de datos propias
        Databases _databases;

        //Atributo con los vecinos
        Neighbours _neighbours;

        //Dialogo de añadir BBDD
        AddDatabaseDialog _addDatabaseDialog;

        //Dialogo de borrado BBDD
        DeleteDatabaseDialog _deleteDatabaseDialog;

        //Dialogo de modificar BBDD
        ModifyDatabaseDialog _modifyDatabaseDialog;

        //Dialogo de error en algun servidor
        ErrorServersDialog _errorServerDialog;

        //Dialogo de conexion
        ConnectionDialog _connectionDialog;

        //Dialogo alternativo de conexion
        ConnectionNeighboursDialog _connectionNeighboursDialog;

        //Diálogo usuarios O.V.
        UsuariosOVDialog _usuariosOVDialog;

        //Diálogo de Simbologia
        SimbologiaDialog _simbologiaDialog;

        //Diálogo de AcercaDe
        AcercadeDialog _acercaDeDialog;

        //Diálogo de MostrarMensajes
        ShowMessagesSentWindow _showMessagesSentWindow;

        //Numero conexiones activas
        int _conexionesActivas;

        //Diccionario con todos los mensajes (neighbour -> [(tipobbdd,nombrebbdd,(connection->Pipe,schema->Pipe,select->Pipe))])
        Dictionary<string, List<Tuple<string, string, Dictionary<string, PipeMessage>>>> _messages;

        //Clave pública y privada
        Cripto _claves;

        //Diccionario con claves públicas y simétricas de los clientes
        Dictionary<string, Tuple<RsaKeyParameters, SymmetricAlgorithm>> _keyPairClients;

        /**
         * Constructor de la ventana
         * */
        public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();

            this._user = new Usuario();

            infoview.ModifyFont(new Pango.FontDescription());

            this._infoBBDDView = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));
            this._infoBBDDownView = new ListStore(typeof(string), typeof(string), typeof(bool));
            this._infoNeighboursView = new ListStore(typeof(string));

            this._databases = new Databases();
            this._keyPairClients = new Dictionary<string, Tuple<RsaKeyParameters, SymmetricAlgorithm>>();

            this._messages = new Dictionary<string, List<Tuple<string, string, Dictionary<string, PipeMessage>>>>();

            CargoBasesDeDatosDeLaOV();

            GenerarParDeClaves();

            DesactivadoBotones();

            ComprobacionServidoresBaseDeDatos();

            CargoBasesDeDatosPropias();

            MensajeBienvenida();
        }

        /**
         * Método privado para cargar las bases de datos de la OV
         * */
        private void CargoBasesDeDatosDeLaOV()
        {
            if (SolicitarVecinos())
            {
                CargoVecinosVO();

                //Añado al treeview la información
                treeviewDatabases.Model = this._infoBBDDView;
                treeviewNeighbours.Model = this._infoNeighboursView;


                //Añado las columnas
                treeviewNeighbours.AppendColumn(Constants.TABLA_COLUMNA_USUARIO, new CellRendererText(), "text", 0);
                treeviewDatabases.AppendColumn(Constants.TABLA_COLUMNA_USUARIO, new CellRendererText(), "text", 0);
                treeviewDatabases.AppendColumn(Constants.TABLA_COLUMNA_TIPOBBDD, new CellRendererText(), "text", 1);
                treeviewDatabases.AppendColumn(Constants.TABLA_COLUMNA_NOMBREBBDD, new CellRendererText(), "text", 2);
            }
            else
            {

            }
        }

        /*
         * Método privado para cargar los vecinos de la OV en la vista
         * */
        private void CargoVecinosVO()
        {
            this._neighbours = new Neighbours();
            foreach (string entry in this._neighbours.VecinosVO)
            {
                this._infoNeighboursView.AppendValues(entry);
            }
        }

        /*
         * Método privado para solicitar los vecinos
         * */
        private bool SolicitarVecinos()
        {
              string msg, response;

                //Proceso el envio
                PostBoxGK post = new PostBoxGK(this._user, Constants.GATEKEEPER,
                                           Constants.MENSAJE_CONSULTA_BBDD_VECINOS);
                msg = post.ProcesarEnvio(this._user.IP.ToString());

                //Creo el cliente y le envio el mensaje
                Client c = new Client();
                bool conexion = c.comprobarConexion(Constants.GATEKEEPER);
                if (conexion)
                {
                    response = c.StartClient(msg, Constants.GATEKEEPER);
                    post.ProcesarRespuesta(response);
                    return true;
                }

              return false;
          //  return true;
        }


        /**
         * Método privado para cargar las bases de datos propias
         * */
        private void CargoBasesDeDatosPropias()
        {

            //Añado valores a la lista
            AddValuesOwn();

            //Añado al treeview la información
            treeviewDatabasesPropias.Model = this._infoBBDDownView;

            //Añado las columnas
            treeviewDatabasesPropias.AppendColumn(Constants.TABLA_COLUMNA_NOMBREBBDD, new CellRendererText(), "text", 0);
            treeviewDatabasesPropias.AppendColumn(Constants.TABLA_COLUMNA_TIPOBBDD, new CellRendererText(), "text", 1);
            treeviewDatabasesPropias.AppendColumn(Constants.TABLA_COLUMNA_FUNCIONA, new CellRendererToggle(), "active", 2);
        }

        /*
         * Método privado para comprobar que los servidores de bases de datos funcionan
         * */
        private void ComprobacionServidoresBaseDeDatos()
        {
            bool ok = false;
            string messageError = "";
            Dictionary<string, string> errors = new Dictionary<string, string>();

            foreach (KeyValuePair<string, List<Tuple<string, string, string>>> entry in this._databases.DatabasesPropias)
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

        /**
         * Método privado para la generación del par de claves (pública y privada)
         * */
        private void GenerarParDeClaves()
        {
            this._claves = new Cripto();
            this._databases.Symmetric = this._claves.SymmetricBBDD;
        }

        /*
         * Método borrado de ventana
         * */
        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            Constants.BorrarRecursos();
            Application.Quit();
            a.RetVal = true;
        }

        /*
         * Método destrucción de la ventana
         * */
        protected void OnDestroyEvent(object o, DestroyEventArgs args)
        {
            Constants.BorrarRecursos();
            Application.Quit();
            args.RetVal = true;
        }

        /*
         * Método de acción del icono añadirBasededatos
         * */
        protected void OnAddDatabasePngActionActivated(object sender, EventArgs e)
        {
            try
            {
                this._addDatabaseDialog = new AddDatabaseDialog(this._databases);
                this._addDatabaseDialog.Run();

                UpdateOwnDatabases();
                if (this._addDatabaseDialog.TypeOutPut != Constants.CANCEL)
                    AdiccionBBDD(this._addDatabaseDialog.BBDD, this._addDatabaseDialog.Success);
            }
            catch (Exception ee)
            {
                string error = ee.StackTrace; 
                ErrorAdiccionBBDD(this._addDatabaseDialog.BBDD);
            }
        }

        /*
         * Método de acción del icono deleteBasededatos
         * */
        protected void OnDeleteDatabasePngActionActivated(object sender, EventArgs e)
        {
            try
            {
                this._deleteDatabaseDialog = new DeleteDatabaseDialog(this._databases);
                this._deleteDatabaseDialog.Run();

                UpdateOwnDatabases();
                if (this._deleteDatabaseDialog.TypeOutPut != Constants.CANCEL)
                    BorradoBBDD(this._deleteDatabaseDialog.BBDD, this._deleteDatabaseDialog.Success);
            }
            catch (Exception)
            {
                ErrorBorradoBBDD(this._deleteDatabaseDialog.BBDD);
            }
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

        /*
         * Menú 'Base de datos' opción 'Añadir'
         * */
        protected void OnAddActionActivated(object sender, EventArgs e)
        {
            try
            {
                this._addDatabaseDialog = new AddDatabaseDialog(this._databases);
                this._addDatabaseDialog.Run();

                UpdateOwnDatabases();
                if (this._addDatabaseDialog.TypeOutPut != Constants.CANCEL)
                    AdiccionBBDD(this._addDatabaseDialog.BBDD, this._addDatabaseDialog.Success);
            }
            catch (Exception)
            {
                ErrorAdiccionBBDD(this._addDatabaseDialog.BBDD);
            }
        }

        /*
         * Menú 'Base de datos' opción 'Borrar'
         * */
        protected void OnRemoveActionActivated(object sender, EventArgs e)
        {
            try
            {
                this._deleteDatabaseDialog = new DeleteDatabaseDialog(this._databases);
                this._deleteDatabaseDialog.Run();

                UpdateOwnDatabases();
                if (this._deleteDatabaseDialog.TypeOutPut != Constants.CANCEL)
                    BorradoBBDD(this._deleteDatabaseDialog.BBDD, this._deleteDatabaseDialog.Success);
            }
            catch (Exception)
            {
                ErrorBorradoBBDD(this._deleteDatabaseDialog.BBDD);
            }
        }

        /*
         * Método de la acción del icono de actualizarBaseDeDatos
         **/
        protected void OnUpdateDatabasePngActionActivated(object sender, EventArgs e)
        {
            UpdateOwnDatabases();
            Actualizacion();
        }

        //Añadir las bases de datos de los vecinos
        private void AddValuesDatabasesNeighbours()
        {
            this._neighbours.LoadNeighbourDatabases();

            foreach (var vecino in this._neighbours.MiembrosOV)
            {
                foreach (var type in vecino.Value)
                {
                    foreach (var bbdd in type.Value)
                    {
                        this._infoBBDDView.AppendValues(vecino.Key, type.Key, bbdd);
                    }
                }
            }
        }


        /*
         * Método privado para añadir información
         * de las bases de datos PROPIAS a la vista
         * */
        private void AddValuesOwn()
        {
            //Recorremos las bases de datos para mostrarlas
            foreach (KeyValuePair<string, List<Tuple<string, string, string>>> entry in this._databases.DatabasesPropias)
            {
                foreach (Tuple<string, string, string> bbdd in entry.Value)
                {
                    bool works = false;
                    if (this._databases.ComprobacionServidor(entry.Key, bbdd.Item1, bbdd.Item2, bbdd.Item3))
                    {
                        works = true;
                    }
                    else
                    {
                        works = false;
                    }
                    this._infoBBDDownView.AppendValues(bbdd.Item1, entry.Key, works);
                }
                // do something with entry.Value or entry.Key
            }
        }

        /*
         * Menú opción salir
         * */
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
            if (this._modifyDatabaseDialog.TypeTask == Constants.TYPE_MODIFY)
            {
                ModifyBBDD(this._modifyDatabaseDialog.BBDD, this._modifyDatabaseDialog.Success);
            }
            else if (this._modifyDatabaseDialog.TypeTask == Constants.TYPE_DELETE)
            {
                DeleteBBDD(this._modifyDatabaseDialog.BBDD, this._modifyDatabaseDialog.Success);
            }

        }

        /*
         * Método activado cuando se pulsa en la lista de vecinos
        */
        protected void OnTreeviewNeighboursRowActivated(object o, RowActivatedArgs args)
        {
            TreeIter t;
            TreePath p = null;
            if (args != null)
            {
                p = args.Path;
            }

            this._infoNeighboursView.GetIter(out t, p);

            string vecino = (string)this._infoNeighboursView.GetValue(t, 0);

            //Comprobamos que está el usuario y si la tupla que tiene es del mismo (tipoBBDD,nombreBBDD)
            if (!this._messages.ContainsKey(vecino))
            {
                this._connectionDialog = new ConnectionDialog(this._user.Nombre, vecino, null, null, this._claves);

                this._connectionDialog.Run();


                switch (this._connectionDialog.TypeOutPut)
                {
                    case Constants.CANCEL:

                        break;
                    case Constants.MENSAJE_CONEXION:
                        Dictionary<string, PipeMessage> messageC = new Dictionary<string, PipeMessage>();
                        messageC.Add(Constants.CONNECTION, this._connectionDialog.Connection);
                        Tuple<string, string, Dictionary<string, PipeMessage>> tupl =
                            new Tuple<string, string, Dictionary<string, PipeMessage>>(null, null, messageC);

                        if (!this._messages.ContainsKey(vecino))
                        {
                            List<Tuple<string, string, Dictionary<string, PipeMessage>>> lista = new List<Tuple<string, string, Dictionary<string, PipeMessage>>>();
                            lista.Add(tupl);

                            this._messages.Add(vecino, lista);
                        }
                        else
                        {
                            this._messages[vecino].Add(tupl);
                        }

                        if (!this._keyPairClients.ContainsKey(vecino))
                        {
                            Tuple<RsaKeyParameters, SymmetricAlgorithm> tup = new Tuple<RsaKeyParameters, SymmetricAlgorithm>(this._connectionDialog.Connection.PublicKey, this._connectionDialog.Connection.SymmetricKey);
                            this._keyPairClients.Add(vecino, tup);
                        }

                        this._neighbours.SaveNeighbourDatabases(this._connectionDialog.Connection.MessageResponse);

                        AddValuesDatabasesNeighbours();

                        MostrarSolicitudConexion(this._connectionDialog.Connection.MessageRequest);
                        MostrarConexion(this._connectionDialog.Connection.MessageResponse);
                        IncrementarConexionesActivas();

                        break;
                    case Constants.ERROR_CONNECTION:
                        MostrarError(this._connectionDialog.TypeOutPut);
                        break;
                }
            }
        }

        /*
         * Método activado cuando se pulsa en la lista de BBDD de vecinos
         * */
        protected void OnTreeviewDatabasesRowActivated(object o, RowActivatedArgs args)
        {
            TreeIter t;
            TreePath p = null;
            if (args != null)
            {
                p = args.Path;
            }

            this._infoBBDDView.GetIter(out t, p);

            string usuarioDestino = (string)this._infoBBDDView.GetValue(t, 0);
            string tipoBBDD = (string)this._infoBBDDView.GetValue(t, 1);
            string nombreBBDD = (string)this._infoBBDDView.GetValue(t, 2);

            //Comprobamos que está el usuario y si la tupla que tiene es del mismo (tipoBBDD,nombreBBDD)
            if (this._messages.ContainsKey(usuarioDestino))
            {
                Tuple<string, string, Dictionary<string, PipeMessage>> tupla = DevuelveTupla(usuarioDestino, tipoBBDD, nombreBBDD);

                if (tupla == null)
                {
                    Tuple<string, string, Dictionary<string, PipeMessage>> firstTupla = DevuelvePrimeraTupla(usuarioDestino);
                    int index = this._messages[usuarioDestino].IndexOf(firstTupla);

                    PipeMessage pipeConexion = this._messages[usuarioDestino][index].Item3[Constants.CONNECTION];

                    this._connectionDialog = new ConnectionDialog(this._user.Nombre, usuarioDestino, tipoBBDD, nombreBBDD, pipeConexion, this._claves);
                }
                else
                {
                    int index = this._messages[usuarioDestino].IndexOf(tupla);

                    if (ComprobarTuplaConSchema(usuarioDestino, tipoBBDD, nombreBBDD))
                    {
                        PipeMessage pipeConexion = null;
                        if (this._messages[usuarioDestino][index].Item3.ContainsKey(Constants.CONNECTION))
                        {
                            pipeConexion = this._messages[usuarioDestino][index].Item3[Constants.CONNECTION];

                        }
                        else
                        {
                            Tuple<string, string, Dictionary<string, PipeMessage>> tuplaAuxiliar = DevuelveTupla(usuarioDestino, null, null);

                            int indexAuxiliar = this._messages[usuarioDestino].IndexOf(tuplaAuxiliar);
                            pipeConexion = this._messages[usuarioDestino][indexAuxiliar].Item3[Constants.CONNECTION];

                        }
                        PipeMessage pipeSchema = this._messages[usuarioDestino][index].Item3[Constants.SCHEMA];

                        this._connectionDialog = new ConnectionDialog(this._user.Nombre,
                            usuarioDestino, tipoBBDD, nombreBBDD, pipeConexion, pipeSchema, this._claves);
                    }
                    else
                    {
                        PipeMessage pipeConexion = this._messages[usuarioDestino][index].Item3[Constants.CONNECTION];

                        this._connectionDialog = new ConnectionDialog(this._user.Nombre, usuarioDestino, tipoBBDD, nombreBBDD, pipeConexion, this._claves);
                    }
                }
            }

            this._connectionDialog.Run();

            switch (this._connectionDialog.TypeOutPut)
            {
                case Constants.CANCEL:

                    break;
                case Constants.MENSAJE_ESQUEMA:

                    if (!ComprobarTuplaConSchema(usuarioDestino, tipoBBDD, nombreBBDD))
                    {
                        Tuple<string, string, Dictionary<string, PipeMessage>> tuplaa = DevuelveTupla(usuarioDestino, tipoBBDD, nombreBBDD);

                        if (tuplaa != null)
                        {
                            //Añadimos un nuevo pipeMessage a la tupla existente
                            int indexx = this._messages[usuarioDestino].IndexOf(tuplaa);
                            this._messages[usuarioDestino][indexx].Item3.Add(Constants.SCHEMA, this._connectionDialog.Schema);
                        }
                        else
                        {

                            Tuple<string, string, Dictionary<string, PipeMessage>> tuplaDiferente = DevuelvePrimeraTupla(usuarioDestino);

                            //Añadimos una nueva tupla
                            Dictionary<string, PipeMessage> diccionarioNuevo = new Dictionary<string, PipeMessage>();
                            //diccionarioNuevo.Add(Constants.CONNECTION, tuplaDiferente.Item3[Constants.CONNECTION]);
                            diccionarioNuevo.Add(Constants.SCHEMA, this._connectionDialog.Schema);
                            Tuple<string, string, Dictionary<string, PipeMessage>> tuplaNueva = new Tuple<string, string, Dictionary<string, PipeMessage>>(tipoBBDD, nombreBBDD, diccionarioNuevo);
                            this._messages[usuarioDestino].Add(tuplaNueva);
                        }
                    }

                    MostrarSolicitudEsquema(this._connectionDialog.Schema.MessageRequest);
                    MostrarEsquema(this._connectionDialog.Schema.MessageResponse);
                    break;

                case Constants.MENSAJE_CONSULTA:
                    Tuple<string, string, Dictionary<string, PipeMessage>> tupla = DevuelveTupla(usuarioDestino, tipoBBDD, nombreBBDD);

                    int index = this._messages[usuarioDestino].IndexOf(tupla);
                    if (this._messages[usuarioDestino][index].Item3.ContainsKey(Constants.SELECT))
                    {
                        this._messages[usuarioDestino][index].Item3[Constants.SELECT] = this._connectionDialog.Select;
                    }
                    else
                    {
                        this._messages[usuarioDestino][index].Item3.Add(Constants.SELECT, this._connectionDialog.Select);
                    }

                    MostrarSolicitudConsulta(this._connectionDialog.Select.MessageRequest);
                    MostrarResultadoConsulta(this._connectionDialog.Select.MessageRequest, this._connectionDialog.Select.MessageResponse);

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
        private bool ComprobarTuplaConSchema(string usuario, string tipoBBDD, string nombreBBDD)
        {
            Tuple<string, string, Dictionary<string, PipeMessage>> tupl = DevuelveTupla(usuario, tipoBBDD, nombreBBDD);

            if (tupl != null && tupl.Item3.ContainsKey(Constants.SCHEMA))
            {
                return true;
            }

            return false;
        }

        protected void OnAcercaDeActionActivated(object sender, EventArgs e)
        {
            this._acercaDeDialog = new AcercadeDialog();
            this._acercaDeDialog.Run();
        }

        protected void OnMostrarUsuariosActionActivated(object sender, EventArgs e)
        {
            this._usuariosOVDialog = new UsuariosOVDialog(this._neighbours);
            this._usuariosOVDialog.Run();
        }

        /*
         * Método que incorpora la función de OnTreeviewDatabasesRowActivated
         * */
        protected void OnDatabaseConnectionPngActionActivated(object sender, EventArgs e)
        {
            this._connectionNeighboursDialog = new ConnectionNeighboursDialog(this._infoBBDDView, this._neighbours);
            this._connectionNeighboursDialog.Run();

            switch (this._connectionNeighboursDialog.TypeOutPut)
            {
                case Constants.CANCEL:
                    break;

                default:


                    break;
            }

        }

        /*
         * Evento producido cuando pulsas en los vecinos
         * */
        protected void OnTreeviewNeighboursButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
        {
            TreeIter t;
            if (treeviewNeighbours.Selection.GetSelectedRows().Length > 0)
            {
                TreePath p = treeviewNeighbours.Selection.GetSelectedRows()[0];

                this._infoNeighboursView.GetIter(out t, p);

                string vecino = (string)this._infoNeighboursView.GetValue(t, 0);


                if (!this._messages.ContainsKey(vecino))
                    this.connectionPng.Sensitive = true;
                else
                    this.connectionPng.Sensitive = false;

                BotoneraConexiones(false);
                connectionPng.Sensitive = true;
            }
        }

        /*
         * Método que realiza la misma función que el método OnTreeviewNeighboursRowActivated. BOTON CONEXIÓN
         * */
        protected void OnConnectionPngAction1Activated(object sender, EventArgs e)
        {
            TreeIter t;
            TreePath p = treeviewNeighbours.Selection.GetSelectedRows()[0];

            this._infoNeighboursView.GetIter(out t, p);

            string vecino = (string)this._infoNeighboursView.GetValue(t, 0);

            //Comprobamos que está el usuario y si la tupla que tiene es del mismo (tipoBBDD,nombreBBDD)
            if (!this._messages.ContainsKey(vecino))
            {
                this._connectionDialog = new ConnectionDialog(this._user.Nombre, vecino, null, null, this._claves);

                this._connectionDialog.Run();


                switch (this._connectionDialog.TypeOutPut)
                {
                    case Constants.CANCEL:

                        break;
                    case Constants.MENSAJE_CONEXION:
                        Dictionary<string, PipeMessage> messageC = new Dictionary<string, PipeMessage>();
                        messageC.Add(Constants.CONNECTION, this._connectionDialog.Connection);
                        Tuple<string, string, Dictionary<string, PipeMessage>> tupl =
                            new Tuple<string, string, Dictionary<string, PipeMessage>>(null, null, messageC);

                        if (!this._messages.ContainsKey(vecino))
                        {
                            List<Tuple<string, string, Dictionary<string, PipeMessage>>> lista = new List<Tuple<string, string, Dictionary<string, PipeMessage>>>();
                            lista.Add(tupl);

                            this._messages.Add(vecino, lista);
                        }
                        else
                        {
                            this._messages[vecino].Add(tupl);
                        }

                        if (!this._keyPairClients.ContainsKey(vecino))
                        {
                            Tuple<RsaKeyParameters, SymmetricAlgorithm> tup = new Tuple<RsaKeyParameters, SymmetricAlgorithm>(this._connectionDialog.Connection.PublicKey, this._connectionDialog.Connection.SymmetricKey);
                            this._keyPairClients.Add(vecino, tup);
                        }

                        this._neighbours.SaveNeighbourDatabases(this._connectionDialog.Connection.MessageResponse);

                        AddValuesDatabasesNeighbours();

                        MostrarSolicitudConexion(this._connectionDialog.Connection.MessageRequest);
                        MostrarConexion(this._connectionDialog.Connection.MessageResponse);
                        IncrementarConexionesActivas();

                        break;
                    case Constants.ERROR_CONNECTION:
                        MostrarError(this._connectionDialog.TypeOutPut);
                        break;
                }
            }
        }

        /*
         * Método evento para solicitar esquema BBDD. BOTON ESQUEMA
         * */
        protected void OnSchemaPngActionActivated(object sender, EventArgs e)
        {
            TreeIter t;
            TreePath p = treeviewDatabases.Selection.GetSelectedRows()[0];

            this._infoBBDDView.GetIter(out t, p);

            string usuarioDestino = (string)this._infoBBDDView.GetValue(t, 0);
            string tipoBBDD = (string)this._infoBBDDView.GetValue(t, 1);
            string nombreBBDD = (string)this._infoBBDDView.GetValue(t, 2);

            //Comprobamos que está el usuario y si la tupla que tiene es del mismo (tipoBBDD,nombreBBDD)
            if (this._messages.ContainsKey(usuarioDestino))
            {
                Tuple<string, string, Dictionary<string, PipeMessage>> tupla = DevuelveTupla(usuarioDestino, tipoBBDD, nombreBBDD);

                if (tupla == null)
                {
                    Tuple<string, string, Dictionary<string, PipeMessage>> firstTupla = DevuelvePrimeraTupla(usuarioDestino);
                    int index = this._messages[usuarioDestino].IndexOf(firstTupla);

                    PipeMessage pipeConexion = this._messages[usuarioDestino][index].Item3[Constants.CONNECTION];

                    this._connectionDialog = new ConnectionDialog(this._user.Nombre, usuarioDestino, tipoBBDD, nombreBBDD, pipeConexion, this._claves);
                }
                else
                {
                    int index = this._messages[usuarioDestino].IndexOf(tupla);

                    if (!ComprobarTuplaConSchema(usuarioDestino, tipoBBDD, nombreBBDD))
                    {
                        PipeMessage pipeConexion = this._messages[usuarioDestino][index].Item3[Constants.CONNECTION];

                        this._connectionDialog = new ConnectionDialog(this._user.Nombre, usuarioDestino, tipoBBDD, nombreBBDD, pipeConexion, this._claves);
                    }
                }
            }

            this._connectionDialog.Run();

            switch (this._connectionDialog.TypeOutPut)
            {
                case Constants.CANCEL:

                    break;
                case Constants.MENSAJE_ESQUEMA:

                    if (!ComprobarTuplaConSchema(usuarioDestino, tipoBBDD, nombreBBDD))
                    {
                        Tuple<string, string, Dictionary<string, PipeMessage>> tuplaa = DevuelveTupla(usuarioDestino, tipoBBDD, nombreBBDD);

                        if (tuplaa != null)
                        {
                            //Añadimos un nuevo pipeMessage a la tupla existente
                            int indexx = this._messages[usuarioDestino].IndexOf(tuplaa);
                            this._messages[usuarioDestino][indexx].Item3.Add(Constants.SCHEMA, this._connectionDialog.Schema);
                        }
                        else
                        {

                            Tuple<string, string, Dictionary<string, PipeMessage>> tuplaDiferente = DevuelvePrimeraTupla(usuarioDestino);

                            //Añadimos una nueva tupla
                            Dictionary<string, PipeMessage> diccionarioNuevo = new Dictionary<string, PipeMessage>();
                            //diccionarioNuevo.Add(Constants.CONNECTION, tuplaDiferente.Item3[Constants.CONNECTION]);
                            diccionarioNuevo.Add(Constants.SCHEMA, this._connectionDialog.Schema);
                            Tuple<string, string, Dictionary<string, PipeMessage>> tuplaNueva = new Tuple<string, string, Dictionary<string, PipeMessage>>(tipoBBDD, nombreBBDD, diccionarioNuevo);
                            this._messages[usuarioDestino].Add(tuplaNueva);
                        }
                    }

                    MostrarSolicitudEsquema(this._connectionDialog.Schema.MessageRequest);
                    MostrarEsquema(this._connectionDialog.Schema.MessageResponse);
                    break;

                case Constants.ERROR_CONNECTION:
                    MostrarError(this._connectionDialog.TypeOutPut);
                    break;
            }
        }

        /*
         * Método evento para solicitar consulta BBDD. BOTON SELECT
         * */
        protected void OnSelectPngActionActivated(object sender, EventArgs e)
        {
            TreeIter t;
            TreePath p = treeviewDatabases.Selection.GetSelectedRows()[0];

            this._infoBBDDView.GetIter(out t, p);


            string usuarioDestino = (string)this._infoBBDDView.GetValue(t, 0);
            string tipoBBDD = (string)this._infoBBDDView.GetValue(t, 1);
            string nombreBBDD = (string)this._infoBBDDView.GetValue(t, 2);

            //Comprobamos que está el usuario y si la tupla que tiene es del mismo (tipoBBDD,nombreBBDD)
            if (this._messages.ContainsKey(usuarioDestino))
            {
                Tuple<string, string, Dictionary<string, PipeMessage>> tupla = DevuelveTupla(usuarioDestino, tipoBBDD, nombreBBDD);

                if (tupla != null)
                {
                    int index = this._messages[usuarioDestino].IndexOf(tupla);

                    if (ComprobarTuplaConSchema(usuarioDestino, tipoBBDD, nombreBBDD))
                    {
                        PipeMessage pipeConexion = null;
                        if (this._messages[usuarioDestino][index].Item3.ContainsKey(Constants.CONNECTION))
                        {
                            pipeConexion = this._messages[usuarioDestino][index].Item3[Constants.CONNECTION];

                        }
                        else
                        {
                            Tuple<string, string, Dictionary<string, PipeMessage>> tuplaAuxiliar = DevuelveTupla(usuarioDestino, null, null);

                            int indexAuxiliar = this._messages[usuarioDestino].IndexOf(tuplaAuxiliar);
                            pipeConexion = this._messages[usuarioDestino][indexAuxiliar].Item3[Constants.CONNECTION];

                        }
                        PipeMessage pipeSchema = this._messages[usuarioDestino][index].Item3[Constants.SCHEMA];

                        this._connectionDialog = new ConnectionDialog(this._user.Nombre,
                            usuarioDestino, tipoBBDD, nombreBBDD, pipeConexion, pipeSchema, this._claves);
                    }
                }
            }

            this._connectionDialog.Run();

            switch (this._connectionDialog.TypeOutPut)
            {
                case Constants.CANCEL:

                    break;

                case Constants.MENSAJE_CONSULTA:
                    Tuple<string, string, Dictionary<string, PipeMessage>> tupla = DevuelveTupla(usuarioDestino, tipoBBDD, nombreBBDD);

                    int index = this._messages[usuarioDestino].IndexOf(tupla);
                    if (this._messages[usuarioDestino][index].Item3.ContainsKey(Constants.SELECT))
                    {
                        this._messages[usuarioDestino][index].Item3[Constants.SELECT] = this._connectionDialog.Select;
                    }
                    else
                    {
                        this._messages[usuarioDestino][index].Item3.Add(Constants.SELECT, this._connectionDialog.Select);
                    }

                    MostrarSolicitudConsulta(this._connectionDialog.Select.MessageRequest);
                    MostrarResultadoConsulta(this._connectionDialog.Select.MessageRequest, this._connectionDialog.Select.MessageResponse);

                    break;

                case Constants.ERROR_CONNECTION:
                    MostrarError(this._connectionDialog.TypeOutPut);
                    break;
            }

        }

        /*
         * Método evento cuando pulsas en el treeview de BBDDVecinos
         * */
        protected void OnTreeviewDatabasesButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
        {
            TreeIter t;
            if (treeviewDatabases.Selection.GetSelectedRows().Length > 0)
            {
                TreePath p = treeviewDatabases.Selection.GetSelectedRows()[0];

                this._infoBBDDView.GetIter(out t, p);

                this._infoBBDDView.GetIter(out t, p);

                string usuarioDestino = (string)this._infoBBDDView.GetValue(t, 0);
                string tipoBBDD = (string)this._infoBBDDView.GetValue(t, 1);
                string nombreBBDD = (string)this._infoBBDDView.GetValue(t, 2);

                //Comprobamos su existencia: que se haya realizado previamente conexión
                if (this._messages.ContainsKey(usuarioDestino))
                {
                    this.connectionPng.Sensitive = false;

                    //Comprobamos si existe ya un esquema
                    if (ComprobarTuplaConSchema(usuarioDestino, tipoBBDD, nombreBBDD))
                    {
                        this.selectPngAction.Sensitive = true;
                        this.schemaPngAction.Sensitive = true;

                    }
                    else
                    //Si no existe esquema
                    {
                        this.selectPngAction.Sensitive = false;
                        this.schemaPngAction.Sensitive = true;
                    }
                }
            }else{
                BotoneraConexiones(false);
            }
        }

        protected void OnMensajesEnviadosActionActivated(object sender, EventArgs e)
        {
            this._showMessagesSentWindow = new ShowMessagesSentWindow(this._messages);
        }

        /*A PARTIR DE AQUI SON METODOS PARA MOSTRAR RESULTADOS DE LAS ACCIONES*/

        /*
         * Método privado para mostrar la solicitud de conexión de BBDD
         * */
        private void MostrarSolicitudConexion(Message messageRequest)
        {
            infoview.Buffer.Text += "\n" + Constants.LINEA + "\n" + Constants.SolicitudConexion(messageRequest);
        }

        /*
         * Método privado para mostrar la respuesta a la solicitud de esquema de BBDD
         * */
        private void MostrarConexion(Message messageResponse)
        {
            infoview.Buffer.Text += "\n" + Constants.RespuestaConexion(messageResponse) + Constants.LINEA + "\n";
        }

        /*
         * Método privado para mostrar la solicitud de esquema de BBDD
         * */
        private void MostrarSolicitudEsquema(Message messageRequest)
        {
            infoview.Buffer.Text += "\n" + Constants.LINEA + "\n" + Constants.SolicitudEsquema(messageRequest);
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
                infoview.Buffer.Text += "\n" + Constants.RespuestaEsquemaMySQL(messageResponse) + Constants.LINEA + "\n";
            else if (messageResponse.Db_type == Constants.MONGODB)
                infoview.Buffer.Text += "\n" + Constants.RespuestaEsquemaMongoDB(messageResponse) + Constants.LINEA + "\n";
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
        private void MostrarResultadoConsulta(Message messageRequest, Message messageResponse)
        {
            infoview.Buffer.Text += "\n" + Constants.RespuestaConsulta(messageRequest, messageResponse);
        }

        /*
         * Método privado para informar del fallo en la carga de vecinos
         * */
        private void FalloCargaVecinos()
        {
            infoview.Buffer.Text += "\n" + Constants.FalloCargaVecinos();
        }

        /*
         * Método privado para incrementar las conexiones activas
         * */
        private void IncrementarConexionesActivas()
        {
            this._conexionesActivas += 1;
            if (this._conexionesActivas == 1)
            {
                conexionesLabel.Text = this._conexionesActivas + " conexión activa";

            }
            else
            {
                conexionesLabel.Text = this._conexionesActivas + " conexiones activas";
            }
        }

        /*
         * Método privdo para mostrar la actualizacion de los servidores
         * */
        private void Actualizacion()
        {
            infoview.Buffer.Text += "\n" + Constants.Actualizacion();
        }

        private void AdiccionBBDD(List<string> bbdd, bool success)
        {
            infoview.Buffer.Text += "\n" + Constants.AdiccionBBDD(bbdd, success);
        }
        private void ErrorAdiccionBBDD(List<string> bbdd)
        {
            infoview.Buffer.Text += "\n" + Constants.ErrorAdiccionBBDD(bbdd);
        }
        private void BorradoBBDD(List<string> bbdd, bool success)
        {
            infoview.Buffer.Text += "\n" + Constants.BorradoBBDD(bbdd, success);
        }
        private void ErrorBorradoBBDD(List<string> bbdd)
        {
            infoview.Buffer.Text += "\n" + Constants.ErrorBorradoBBDD(bbdd);
        }
        private void ModifyBBDD(List<string> bbdd, bool success)
        {
            infoview.Buffer.Text += "\n" + Constants.ModifyBBDD(bbdd, success);
        }
        private void DeleteBBDD(List<string> bbdd, bool success)
        {
            infoview.Buffer.Text += "\n" + Constants.DeleteBBDD(bbdd, success);
        }
        /*
         * Método privado para mostrar los errores producidos
         * */
        private void MostrarError(string errore)
        {
            infoview.Buffer.Text += "\n" + Constants.LINEA + "\n" + errore + "\n" + Constants.LINEA + "\n";
        }

        /*
         * Método privado para dar la bienvenida al usuario
         * */
        private void MensajeBienvenida()
        {
            infoview.Buffer.Text = "\n" + Constants.Bienvenida(this._user.Nombre);
        }

        /*
         * Limpiado de consola
         * */
        protected void OnClearActionActivated(object sender, EventArgs e)
        {
            infoview.Buffer.Clear();
        }

        protected void OnSimbologaActionActivated(object sender, EventArgs e)
        {
            this._simbologiaDialog = new SimbologiaDialog();
            this._simbologiaDialog.Run();
        }

        private void DesactivadoBotones()
        {
            this._conexionesActivas = 0;
            BotoneraConexiones(false);
        }

        /*
         * Evento ocurrido cuando pulsas en el treeview de BBDD propias
         * */
        protected void OnTreeviewDatabasesPropiasButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
        {
            //Activamos/desactivamos los respectivos botones
            BotoneraConexiones(false);
            BotoneraBBDD(true);
        }

        /*
         * Evento ocurrido cuando pulsas en el infoview
         * */
        protected void OnInfoviewButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
        {
            //Activamos/desactivamos los respectivos botones
            BotoneraConexiones(false);
        }

        private void BotoneraConexiones(bool activacion)
        {
            connectionPng.Sensitive = activacion;
            schemaPngAction.Sensitive = activacion;
            selectPngAction.Sensitive = activacion;
        }
        private void BotoneraBBDD(bool activacion)
        {
            addDatabasePngAction.Sensitive = activacion;
            deleteDatabasePngAction.Sensitive = activacion;
            updateDatabasePngAction.Sensitive = activacion;
        }
    }
}