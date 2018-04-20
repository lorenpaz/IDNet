using System;
using System.Collections.Generic;

using DatabaseLibraryS;
using ConstantsLibraryS;

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

        //Atributo para saber cómo ha cerrado el diálogo
        private string _typeOutPut;

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

        public String TypeOutPut
        {
            get
            {
                return this._typeOutPut;
            }
            set
            {
                this._typeOutPut = value;
            }
        }

        public AddDatabaseDialog(Databases databases)
        {
            this.Build();

            this._databases = databases;
            this._bbdd = new List<string>();
            this._success = false;
            this._typeOutPut = Constants.CANCEL;
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
            this._typeOutPut = "Adiccion";
			this.Destroy();
        }

        private void Adiccion()
        {
            this._bbdd.Clear();
            this._bbdd.Add(comboboxTipos.ActiveText);
            this._bbdd.Add(entryNombreBBDD.Text);
            if (entryUsername.Text != "")
            {
                this._bbdd.Add(entryUsername.Text);
                this._bbdd.Add(entryContrasenia.Text);
            }
        }
    }
}
