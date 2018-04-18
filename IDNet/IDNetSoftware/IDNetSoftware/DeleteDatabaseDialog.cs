using System;
using System.Collections.Generic;
using DatabaseLibraryS;


namespace IDNetSoftware
{
    public partial class DeleteDatabaseDialog : Gtk.Dialog
    {
        //Atributo de las bases de datos propias
        private Databases _databases;

        //Nombre base de datos a borrar
        private List<String> _bbdd;

        //Tarea con exito
        private bool _success;

        public List<String> BBDD
        {

            get
            {
                return this._bbdd;
            }
            set
            {
                this._bbdd = value;
            }
        }

        public bool Success
        {
            get
            {
                return this._success;
            }
            set
            {
                this._success = value;
            }
        }

        public DeleteDatabaseDialog(Databases databases)
        {
            this.Build();

            this._databases = databases;
            this._bbdd = new List<string>();
            this._success = false;
        }

        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            if (entryNombreBBDD.Text != "")
            {
                Adiccion();
                this._success = this._databases.DeleteDatabase(this._bbdd,comboboxTipos.ActiveText, entryNombreBBDD.Text, "","");
            }
            this.Destroy();
        }

        protected void OnButtonCancelClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }

        private void Adiccion()
        {
            this._bbdd.Clear();
            this._bbdd.Add(comboboxTipos.ActiveText);
            this._bbdd.Add(entryNombreBBDD.Text);
        }
    }
}
