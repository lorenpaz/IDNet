using System;
using Gtk;

namespace IDNetSoftware
{
    public partial class MainWindow : Gtk.Window
    {
        ListStore _infoBBDDView; 
        public MainWindow() : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this._infoBBDDView = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));

            addValues();

            //Añado al treeviw la información
            treeviewDatabases.Model = this._infoBBDDView;

            //Añado las columnas
            treeviewDatabases.AppendColumn("Nombre BBDD",new CellRendererText(),"text",0);
			treeviewDatabases.AppendColumn("Tipo BBDD", new CellRendererText(),"text", 1);
			treeviewDatabases.AppendColumn("Usuario", new CellRendererText(),"text", 2);
            treeviewDatabases.AppendColumn("Conexión", new CellRendererToggle(),"activatable",3);
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

        private void addValues()
        {
			//Ejemplo dar valores
			this._infoBBDDView.AppendValues("empleados", "mysql", "Lorenzo", "No");
			this._infoBBDDView.AppendValues("compañia", "mongodb", "Juan", "Si");
		}

        //Menú opción salir
        protected void OnSalirActionActivated(object sender, EventArgs e)
        {
			Application.Quit();
        }
    }
}