using System;
using DatabaseLibraryS;

namespace IDNetSoftware
{
    public partial class AddDatabaseDialog : Gtk.Dialog
    {
		//Atributo de las bases de datos propias
		Databases _databases;

        public AddDatabaseDialog(Databases databases)
        {
            this.Build();

            this._databases = databases;
        }

        protected void OnButtonCancelClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }

        //Cuando pulsas en el botón 'OK' para agregar BBDD
        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            bool add = false;
            add = this._databases.addDatabase(comboboxTipos.ActiveText,entryNombreBBDD.Text);
            this.Destroy();
        }
    }
}
