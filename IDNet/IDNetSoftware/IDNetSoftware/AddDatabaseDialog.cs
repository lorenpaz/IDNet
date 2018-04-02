using System;
using System.Collections.Generic;
using DatabaseLibraryS;

namespace IDNetSoftware
{
    public partial class AddDatabaseDialog : Gtk.Dialog
    {
		//Atributo de las bases de datos propias
		private Databases _databases;

        //Nombre base de datos a añadir
        private List<String> _bbdd;

        //Tarea con exito
        private bool _success;

        public List<String> BBDD{

            get{
                return this._bbdd;
            }
            set{
                this._bbdd = value;
            }
        }

        public bool Success
        {
            get{
                return this._success;
            }
            set{
                this._success = value;
            }
        }

        public AddDatabaseDialog(Databases databases)
        {
            this.Build();

            this._databases = databases;
            this._bbdd = new List<string>();
            this._success = false;
        }

        protected void OnButtonCancelClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }

        //Cuando pulsas en el botón 'OK' para agregar BBDD
        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            if (entryNombreBBDD.Text != "")
            {
                Adiccion();
                if (entryUsername.Text == "")
                {
                    this._success = this._databases.addDatabase(comboboxTipos.ActiveText, entryNombreBBDD.Text, null, null);
                }
                else
                {
                    this._success = this._databases.addDatabase(comboboxTipos.ActiveText, entryNombreBBDD.Text,
                                                     entryUsername.Text, entryContrasenia.Text);
                }
            }       
			this.Destroy();
        }

        private void Adiccion()
        {
            this._bbdd.Clear();
            this._bbdd[0] = comboboxTipos.ActiveText;
            this._bbdd[1] = entryNombreBBDD.Text;
            if (entryUsername.Text != "")
            {
                this._bbdd[2] = entryUsername.Text;
                this._bbdd[3] = entryContrasenia.Text;
            }
        }
    }
}
