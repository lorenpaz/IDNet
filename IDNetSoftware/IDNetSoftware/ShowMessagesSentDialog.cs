using System;
using Gtk;
using System.Collections.Generic;

using MessageLibraryS;
using ConstantsLibraryS;

namespace IDNetSoftware
{
    public partial class ShowMessagesSentDialog : Gtk.Dialog
    {
        //Diccionario con todos los mensajes (neighbour -> [(tipobbdd,nombrebbdd,(connection->Pipe,schema->Pipe,select->Pipe))])
        Message _message;

        //Lista para mostrar el mensaje
        ListStore _messageView;

        /**
         * Constructor para la construcción del diálogo
         * */
        public ShowMessagesSentDialog(Message message)
        {
            this.Build();
            this._message = message;
            ShowMessage();
        }

        /**
         * Método privado para mostrar el mensaje
         * */
        private void ShowMessage()
        {
            if (this._message.MessageType != Constants.MENSAJE_CONEXION_A && this._message.MessageType != Constants.MENSAJE_CONEXION_B &&
                this._message.MessageType != Constants.MENSAJE_RESPUESTA_CONEXION_A && this._message.MessageType != Constants.MENSAJE_RESPUESTA_CONEXION_B)
            {
                this._messageView = new ListStore(typeof(string), typeof(string), typeof(string),typeof(string), typeof(string));

                //Añado las columnas
                treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_ORIGEN, new CellRendererText(), "text", 0);
                treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_DESTINO, new CellRendererText(), "text", 1);
                treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_TIPO_MENSAJE, new CellRendererText(), "text", 2);
                treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_TIPOBBDD, new CellRendererText(), "text", 3);
                treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_NOMBREBBDD, new CellRendererText(), "text", 4);

                this._messageView.AppendValues(this._message.Source, this._message.Destination, this._message.MessageType, this._message.Db_type, this._message.Db_name);
                labelCuerpo.LabelProp = this._message.Body == null || this._message.Body.InnerXml == "" ? "No hay cuerpo del mensaje" : this._message.Body.InnerXml;

            }else{
                this._messageView = new ListStore(typeof(string), typeof(string), typeof(string));

                switch(this._message.MessageType)
                {
                    case Constants.MENSAJE_CONEXION_A:

                        //Añado las columnas
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_ORIGEN, new CellRendererText(), "text", 0);
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_DESTINO, new CellRendererText(), "text", 1);
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_TIPO_MENSAJE, new CellRendererText(), "text", 2);
                        this._messageView.AppendValues(this._message.Source, this._message.Destination, this._message.MessageType);
                        this.labelCuerpo.LabelProp = "En este mensaje "+this._message.Source +" envió la siguiente clave pública a "+ this._message.Destination +" : "+"\n"+this._message.KeyPair;
                        break;

                    case Constants.MENSAJE_CONEXION_B:
                        //Añado las columnas
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_ORIGEN, new CellRendererText(), "text", 0);
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_DESTINO, new CellRendererText(), "text", 1);
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_TIPO_MENSAJE, new CellRendererText(), "text", 2);
                        this._messageView.AppendValues(this._message.Source, this._message.Destination, this._message.MessageType);
                        labelCuerpo.LabelProp = "En este mensaje "+this._message.Source +" envió la clave simétrica encriptada con la clave pública de "+ this._message.Destination;
                        labelCuerpo.LabelProp += "\n"+"Por motivos de seguridad, no vamos a mostrar la clave simétrica \n";
                        break;
                    case Constants.MENSAJE_RESPUESTA_CONEXION_A:
                        //Añado las columnas
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_ORIGEN, new CellRendererText(), "text", 0);
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_DESTINO, new CellRendererText(), "text", 1);
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_TIPO_MENSAJE, new CellRendererText(), "text", 2);
                        this._messageView.AppendValues(this._message.Source, this._message.Destination, this._message.MessageType);
                        this.labelCuerpo.LabelProp = "En este mensaje "+this._message.Source +" envió la siguiente clave pública a "+ this._message.Destination +" :"+"\n"+this._message.KeyPair;
   
                        break;
                    case Constants.MENSAJE_RESPUESTA_CONEXION_B:
                        //Añado las columnas
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_ORIGEN, new CellRendererText(), "text", 0);
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_USUARIO_DESTINO, new CellRendererText(), "text", 1);
                        treeviewMessage.AppendColumn(Constants.TABLA_COLUMNA_TIPO_MENSAJE, new CellRendererText(), "text", 2);
                        this._messageView.AppendValues(this._message.Source, this._message.Destination, this._message.MessageType);
                        labelCuerpo.LabelProp = "En este mensaje "+this._message.Source +" envió la clave simétrica encriptada con la clave pública de "+ this._message.Destination;
                        labelCuerpo.LabelProp += "\n"+"Por motivos de seguridad, no vamos a mostrar la clave simétrica \n";
                 
                        break;
                }
            }
            treeviewMessage.Model = this._messageView;

        }

        /**
         * Método de evento de pulsación en el botón Ok
         * */
        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }
    }
}
