using System;
using DatabaseLibraryS;

namespace IDNetSoftware
{
    public partial class AddDatabaseDialog : Gtk.Dialog
    {
		//Atributo de las bases de datos propias
		Databases _databases;

        //Nombre base de datos a añadir
        String _bbdd;

        //Tarea con exito
        bool _success;

        public String BBDD{

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
            this._bbdd = null;
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
                this._bbdd = entryNombreBBDD.Text;

                if (entryUsername.Text == "")
                {
                    this._success = this._databases.addDatabase(comboboxTipos.ActiveText, this._bbdd, null, null);
                }
                else
                {
                    this._success = this._databases.addDatabase(comboboxTipos.ActiveText, this._bbdd,
                                                     entryUsername.Text, entryContrasenia.Text);
                }
            }       
			this.Destroy();
        }
    }
}
