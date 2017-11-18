﻿using System;
using Gtk;
using DatabaseLibrary;
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

        public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();

            this._infoBBDDView = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));
            this._infoBBDDownView = new ListStore(typeof(string), typeof(string));

            this._databases = new Databases();
            this._neighbours = new Neighbours();

            cargoBasesDeDatosDeLaOV();

            cargoBasesDeDatosPropias();
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
            treeviewDatabases.AppendColumn("Conexión", new CellRendererToggle(), "activatable", 3);
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
            foreach(KeyValuePair<string, Dictionary<string,List<string>>> entry in this._neighbours.MiembrosOV)
            {
                foreach(KeyValuePair<string, List<string>> entryTwo in entry.Value)
                {
                    foreach (string bbdd in entryTwo.Value)
                    {
                        this._infoBBDDView.AppendValues(entry.Key,bbdd,entryTwo.Key,"No"); 
                    }
                }

            }
			//Ejemplo dar valores
			//this._infoBBDDView.AppendValues("empleados", "mysql", "Lorenzo", "No");
			//this._infoBBDDView.AppendValues("compañia", "mongodb", "Juan", "Si");
		}

        //Añadir info de las bases de datos PROPIAS
        private void addValuesOwn()
        {
			//Recorremos las bases de datos para mostrarlas
			foreach (KeyValuePair<string, List<string>> entry in this._databases.databasesPropias)
			{
				foreach (string bbdd in entry.Value)
				{
                    this._infoBBDDownView.AppendValues(bbdd,entry.Key);
				}
				// do something with entry.Value or entry.Key
			}
        }

        //Menú opción salir
        protected void OnSalirActionActivated(object sender, EventArgs e)
        {
			Application.Quit();
        }

    }
}