using System;
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

        public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this._infoBBDDView = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));
            this._infoBBDDownView = new ListStore(typeof(string), typeof(string));
            this._databases = new Databases();

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
            treeviewDatabases.AppendColumn("Nombre BBDD", new CellRendererText(), "text", 0);
            treeviewDatabases.AppendColumn("Tipo BBDD", new CellRendererText(), "text", 1);
            treeviewDatabases.AppendColumn("Usuario", new CellRendererText(), "text", 2);
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
            AddDatabaseDialog dialog = new AddDatabaseDialog();
            dialog.Show();
        }

        //Menú 'Base de datos' opción 'Añadir'
        protected void OnAddActionActivated(object sender, EventArgs e)
        {
			AddDatabaseDialog dialog = new AddDatabaseDialog();
			dialog.Show();
        }

        //Añadir info de las bases de datos DE OTROS
        private void addValues()
        {
			//Ejemplo dar valores
			this._infoBBDDView.AppendValues("empleados", "mysql", "Lorenzo", "No");
			this._infoBBDDView.AppendValues("compañia", "mongodb", "Juan", "Si");
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