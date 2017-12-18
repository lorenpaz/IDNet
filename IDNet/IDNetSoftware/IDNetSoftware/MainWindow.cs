using System;
using Gtk;

using DatabaseLibraryS;
using MessageLibraryS;
using ConstantsLibraryS;

using System.Collections.Generic;

namespace IDNetSoftware
{
    /*
     * Estructua que nos sirve para guardar el intercambio de mensajes en cada accion
     * */
    public class PipeMessage
    {
        Message _messageRequest;
        Message _messageResponse;

        public PipeMessage(){}
        public PipeMessage(Message mrequest, Message mresponse)
        {
            this._messageRequest = mrequest;
            this._messageResponse = mresponse;
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
    }
    public partial class MainWindow : Gtk.Window
    {
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

        //Diccionario con todos los mensajes (neighbour -> [(tipobbdd,nombrebbdd,(connection->Pipe,schema->Pipe,select->Pipe))])
        Dictionary<string, List<Tuple<string,string,Dictionary<string, PipeMessage>>>> _messages;

        public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
           
            infoview.ModifyFont(new Pango.FontDescription());

            this._infoBBDDView = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));
            this._infoBBDDownView = new ListStore(typeof(string), typeof(string));

            this._databases = new Databases();
            this._neighbours = new Neighbours();

            this._messages = new Dictionary<string, List<Tuple<string,string,Dictionary<string, PipeMessage>>>>();

            cargoBasesDeDatosDeLaOV();

            cargoBasesDeDatosPropias();

            comprobacionServidoresBaseDeDatos();
        }

        private void cargoBasesDeDatosDeLaOV()
        {
            //AQUI HACE FALTA OBTENER BBDD de OTROS (conexion cliente con el GK para que nos dé tal información)

            //Añado valores a la lista
            addValues();

            //Añado al treeview la información
            treeviewDatabases.Model = this._infoBBDDView;

            //Añado las columnas
            treeviewDatabases.AppendColumn("Usuario", new CellRendererText(), "text", 0);
            treeviewDatabases.AppendColumn("Tipo BBDD", new CellRendererText(), "text", 1);
            treeviewDatabases.AppendColumn("Nombre BBDD", new CellRendererText(), "text", 2);
        }

        private void cargoBasesDeDatosPropias()
        {

            //Añado valores a la lista
            addValuesOwn();

            //Añado al treeview la información
            treeviewDatabasesPropias.Model = this._infoBBDDownView;

            //Añado las columnas
            treeviewDatabasesPropias.AppendColumn("Nombre BBDD", new CellRendererText(), "text", 0);
            treeviewDatabasesPropias.AppendColumn("Tipo BBDD", new CellRendererText(), "text", 1);
        }

        private void comprobacionServidoresBaseDeDatos()
        {
            bool ok = false;
            string messageError = "";
            Dictionary<string, string> errors = new Dictionary<string, string>();

            foreach (KeyValuePair<string, List<string>> entry in this._databases.DatabasesPropias)
            {
                switch (entry.Key)
                {

                    case "mysql":
                        try
                        {
                            ok = this._databases.ComprobacionMysql();
                        }
                        catch (Exception e)
                        {
                            messageError = e.Message;
                            errors.Add("mysql", messageError);
                        }

                        break;
                    case "mongodb":
                        ok = this._databases.ComprobacionMongodb();
                        if (!ok)
                        {
                            errors.Add("mongodb", Constants.UNABLE_CONNECT_MONGODB);
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

            updateOwnDatabases();
        }

        /*
         * Método privado para la actualización de las bases de datos propias
         * */
        private void updateOwnDatabases()
        {
            this._databases.update();

            //Limpiamos
            this._infoBBDDownView.Clear();

            addValuesOwn();
            treeviewDatabasesPropias.Model = this._infoBBDDownView;
        }

        //Menú 'Base de datos' opción 'Añadir'
        protected void OnAddActionActivated(object sender, EventArgs e)
        {
            this._addDatabaseDialog = new AddDatabaseDialog(this._databases);
            this._addDatabaseDialog.Run();

            updateOwnDatabases();
        }

        //Añadir info de las bases de datos DE OTROS
        private void addValues()
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
        private void addValuesOwn()
        {
            //Recorremos las bases de datos para mostrarlas
            foreach (KeyValuePair<string, List<string>> entry in this._databases.DatabasesPropias)
            {
                foreach (string bbdd in entry.Value)
                {
                    this._infoBBDDownView.AppendValues(bbdd, entry.Key);
                }
                // do something with entry.Value or entry.Key
            }
        }

        //Menú opción salir
        protected void OnSalirActionActivated(object sender, EventArgs e)
        {
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
            List<string> bbdd = new List<string>();
            bbdd.Add(tipoBBDD);
            bbdd.Add(nombreBBDD);
            this._modifyDatabaseDialog = new ModifyDatabaseDialog(this._databases, bbdd);
            this._modifyDatabaseDialog.Run();

            updateOwnDatabases();
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
            if (this._messages.ContainsKey(usuario) && devuelveTupla(usuario, tipoBBDD, nombreBBDD) != null)
            {
                Tuple<string, string, Dictionary<string, PipeMessage>> tupla = devuelveTupla(usuario, tipoBBDD, nombreBBDD);
               
				int index = this._messages[usuario].IndexOf(tupla); 

				if (comprobarTuplaConSchema(usuario, tipoBBDD, nombreBBDD))
				{
                    PipeMessage pipeConexion = this._messages[usuario][index].Item3["connection"];
                    PipeMessage pipeSchema = this._messages[usuario][index].Item3["schema"];

                    this._connectionDialog = new ConnectionDialog(
                        usuario, tipoBBDD, nombreBBDD, pipeConexion, pipeSchema);
                }
                else
                {
                    PipeMessage pipeConexion = this._messages[usuario][index].Item3["connection"];

                    this._connectionDialog = new ConnectionDialog(usuario, tipoBBDD, nombreBBDD, pipeConexion);
                }
            }
            else
            {
                this._connectionDialog = new ConnectionDialog(usuario, tipoBBDD, nombreBBDD);
            }

            this._connectionDialog.Run();

            switch (this._connectionDialog.TypeOutPut)
            {
                case "Cancel":

                    break;
                case "001":
                    Dictionary<string, PipeMessage> messageC = new Dictionary<string, PipeMessage>();
                    messageC.Add("connection", this._connectionDialog.Connection);
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
                    MostrarSolicitudConexion(this._connectionDialog.Connection.MessageRequest);
                    MostrarConexion(this._connectionDialog.Connection.MessageResponse);

                    break;

                case "002":
                    
                    if (!comprobarTuplaConSchema(usuario, tipoBBDD, nombreBBDD))
                    {
						Tuple<string, string, Dictionary<string, PipeMessage>> tuplaa = devuelveTupla(usuario, tipoBBDD, nombreBBDD);

						int indexx = this._messages[usuario].IndexOf(tuplaa);

                        this._messages[usuario][indexx].Item3.Add("schema", this._connectionDialog.Schema);
                    }

					MostrarSolicitudEsquema(this._connectionDialog.Schema.MessageRequest);
                    MostrarEsquema(this._connectionDialog.Schema.MessageResponse);
                    break;

                case "003":
					Tuple<string, string, Dictionary<string, PipeMessage>> tupla = devuelveTupla(usuario, tipoBBDD, nombreBBDD);

					int index = this._messages[usuario].IndexOf(tupla);
                    if(this._messages[usuario][index].Item3.ContainsKey("select"))
                    {
                        this._messages[usuario][index].Item3["select"] = this._connectionDialog.Select;
                    }else{
                        this._messages[usuario][index].Item3.Add("select", this._connectionDialog.Select);
					}

					break;
            }

        }

        private Tuple<string, string, Dictionary<string, PipeMessage>> devuelveTupla(string usuario, string tipoBBDD, string nombreBBDD)
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

        private bool comprobarTuplaConSchema(string usuario,string tipoBBDD,string nombreBBDD)
        {
			Tuple<string, string, Dictionary<string, PipeMessage>> tupl = devuelveTupla(usuario, tipoBBDD, nombreBBDD);

            if(tupl != null && tupl.Item3.ContainsKey("schema"))
            {
                return true;
            }

            return false;
        }

        /*A PARTIR DE AQUI SON METODOS PARA MOSTRAR RESULTADOS DE LAS ACCIONES*/

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

            if (messageResponse.Db_type == "mysql")
            //    infoview.Buffer.InsertWithTags(ref insertIter,
              //                                 "\n"+Constants.RespuestaEsquemaMySQL(messageResponse),
                //                              tag);
                infoview.Buffer.Text += "\n" + Constants.RespuestaEsquemaMySQL(messageResponse);
            else
                infoview.Buffer.Text += "\n" + Constants.RespuestaEsquemaMongoDB(messageResponse);
        }

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
         * Limpiado de consola
         * */
        protected void OnClearActionActivated(object sender, EventArgs e)
        {
            infoview.Buffer.Clear();
        }
    }
}