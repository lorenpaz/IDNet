﻿using System;
using Gtk;

using DatabaseLibraryS;
using MessageLibraryS;
using ConstantsLibraryS;

using System.Collections.Generic;

namespace IDNetSoftware
{
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

        public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();

            this._infoBBDDView = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));
            this._infoBBDDownView = new ListStore(typeof(string), typeof(string));

            this._databases = new Databases();
            this._neighbours = new Neighbours();

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

            foreach(KeyValuePair<string, List<string>> entry in this._databases.DatabasesPropias)
            {
                switch(entry.Key){

                    case "mysql":
                        try
                        {
                            ok = this._databases.ComprobacionMysql();
                        } catch(Exception e)
                        {
                            messageError = e.Message;
                            errors.Add("mysql",messageError);
                        }

                        break;
                    case "mongodb":
                        ok = this._databases.ComprobacionMongodb();
                        if(!ok)
                        {
                            errors.Add("mongodb", Constants.UNABLE_CONNECT_MONGODB);
                        }
                        break;
                }
            }
            if (!ok && errors.Count != 0)
			{
                this._errorServerDialog = new ErrorServersDialog(this._databases,errors);
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
                        this._infoBBDDView.AppendValues(entry.Key,entryTwo.Key, bbdd);
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
         * Método privado para mostrar la solicitud de esquema de BBDD
         * */
        private void MostrarSolicitudEsquema(Message messageRequest)
        {
            infoview.Buffer.Text += "\n"+ Constants.SolicitudEsquema(messageRequest);
        }

		/*
         * Método privado para mostrar la respuesta a la solicitud de esquema de BBDD
         * */
		private void MostrarEsquema(Message messageResponse)
        {
            infoview.Buffer.Text += "\n" + Constants.RespuestaEsquema(messageResponse);
        }

        protected void OnTreeviewDatabasesRowActivated(object o, RowActivatedArgs args)
        {
			TreeIter t;
			TreePath p = args.Path;
			this._infoBBDDView.GetIter(out t, p);

            string usuario = (string)this._infoBBDDView.GetValue(t, 0);
			string tipoBBDD = (string)this._infoBBDDView.GetValue(t, 1);
            string nombreBBDD = (string)this._infoBBDDView.GetValue(t, 2);

            this._connectionDialog = new ConnectionDialog(usuario,tipoBBDD,nombreBBDD);
            this._connectionDialog.Run();

            MostrarSolicitudEsquema(this._connectionDialog.MessageRequest);
            MostrarEsquema(this._connectionDialog.MessageResponse);
		}
    }
}