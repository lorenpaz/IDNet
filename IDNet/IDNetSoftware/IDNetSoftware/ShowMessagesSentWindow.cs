using System;
using Gtk;
using System.Collections.Generic;

using MessageLibraryS;
using ConstantsLibraryS;

namespace IDNetSoftware
{
    public partial class ShowMessagesSentWindow : Gtk.Window
    {
        //Diccionario con todos los mensajes (neighbour -> [(tipobbdd,nombrebbdd,(connection->Pipe,schema->Pipe,select->Pipe))])
        Dictionary<string, List<Tuple<string, string, Dictionary<string, PipeMessage>>>> _messages;

        //Lista para mostrar los mensajes
        ListStore _messagesView;

        ShowMessagesSentDialog _messageViewDialog;

        public ShowMessagesSentWindow(Dictionary<string, List<Tuple<string, string, Dictionary<string, PipeMessage>>>> messages) :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this._messages = messages;
            this._messagesView = new ListStore(typeof(string), typeof(string), typeof(string),typeof(string));
            CargarTreeView();
        }

        private void CargarTreeView()
        {
            treeviewMessages.Model = this._messagesView;

            AddValues();

            //Añado las columnas
            treeviewMessages.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_ORIGEN, new CellRendererText(), "text", 0);
            treeviewMessages.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_DESTINO, new CellRendererText(), "text", 1);
            treeviewMessages.AppendColumn(Constants.TABLA_COLUMNA_TIPO_MENSAJE, new CellRendererText(), "text", 2);
            treeviewMessages.AppendColumn(Constants.TABLA_COLUMNA_TIPOBBDD, new CellRendererText(), "text", 3);
            treeviewMessages.AppendColumn(Constants.TABLA_COLUMNA_NOMBREBBDD, new CellRendererText(), "text", 4);
        }
        private void AddValues()
        {
            foreach (var neigh in this._messages)
            {
                foreach(var tupla in neigh.Value)
                {
                    foreach(var dictMensaje in tupla.Item3)
                    {
                        PipeMessage p = dictMensaje.Value;
                        Message enviado = p.MessageRequest;
                        Message recibido = p.MessageResponse;
                        this._messagesView.AppendValues(enviado.Source,enviado.Destination,enviado.MessageType,enviado.Db_type,enviado.Db_name);
                        this._messagesView.AppendValues(recibido.Source, recibido.Destination, recibido.MessageType, recibido.Db_type, recibido.Db_name);

                        if(p.MessageRequestConexion != null)
                        {
                            Message enviadoConexion = p.MessageRequestConexion;
                            Message recibidoConexion = p.MessageResponseConexion;
                            this._messagesView.AppendValues(enviadoConexion.Source, enviadoConexion.Destination, enviadoConexion.MessageType, enviadoConexion.Db_type, enviadoConexion.Db_name);
                            this._messagesView.AppendValues(recibidoConexion.Source, recibidoConexion.Destination, recibidoConexion.MessageType, recibidoConexion.Db_type, recibidoConexion.Db_name);
                        }
                    }
                }
            }
        }

        protected void OnTreeviewMessagesRowActivated(object o, RowActivatedArgs args)
        {
            TreeIter t;
            TreePath p = args.Path;
            this._messagesView.GetIter(out t, p);

            string source = (string)this._messagesView.GetValue(t, 0);
            string destination = (string)this._messagesView.GetValue(t, 1);
            string messageType = (string)this._messagesView.GetValue(t, 2);
            string tipoBBDD = this._messagesView.GetValue(t, 3) == null? null:(string)this._messagesView.GetValue(t, 3);
            string nombreBBDD = this._messagesView.GetValue(t, 4) == null? null:(string)this._messagesView.GetValue(t, 4);

            Tuple<string, string, string, string, string> tupla = new Tuple<string, string, string, string, string>(source, destination, messageType, tipoBBDD, nombreBBDD);

            Message m = BuscarMensaje(tupla);
            if (m != null)
            {
                this._messageViewDialog = new ShowMessagesSentDialog(m);
                this._messageViewDialog.Run();
            }
        }

        private Message BuscarMensaje(Tuple<string, string, string, string, string> tuplaBuscar)
        {
            Message m = null;
            foreach (var neigh in this._messages)
            {
                foreach (var tupla in neigh.Value)
                {
                    foreach (var dictMensaje in tupla.Item3)
                    {
                        PipeMessage p = dictMensaje.Value;
                        Message enviado = p.MessageRequest;
                        Message recibido = p.MessageResponse;
                        if(ComprobacionMensaje(tuplaBuscar,enviado))
                        {
                            return enviado;
                        }else if(ComprobacionMensaje(tuplaBuscar,recibido))
                        {
                            return recibido;
                        }
                        if(p.MessageRequestConexion != null)
                        {
                            enviado = p.MessageRequestConexion;
                            recibido = p.MessageResponseConexion;
                            if (ComprobacionMensaje(tuplaBuscar, enviado))
                            {
                                return enviado;
                            }
                            else if (ComprobacionMensaje(tuplaBuscar, recibido))
                            {
                                return recibido;
                            }
                        }
                    }
                }
            }

            return m;
        }

        private bool ComprobacionMensaje(Tuple<string, string, string, string, string> tuplaBuscar,Message mensaje)
        {
            if( tuplaBuscar.Item1 == mensaje.Source && 
                   tuplaBuscar.Item2 == mensaje.Destination &&
                    tuplaBuscar.Item3 == mensaje.MessageType &&
               ((mensaje.Db_type !=null && tuplaBuscar.Item4 == mensaje.Db_type) || mensaje.Db_type==null) &&
               ((mensaje.Db_name != null && tuplaBuscar.Item5 == mensaje.Db_name) || mensaje.Db_name == null))
            {
                return true;   
            }else{
                return false;
            }
        }

        protected void OnButtonOKClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }
    }
}
