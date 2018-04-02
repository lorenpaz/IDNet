using System;
using DatabaseLibraryS;
using System.Collections.Generic;
using Gtk;

using ConstantsLibraryS;

namespace IDNetSoftware
{
    public partial class ModifyDatabaseDialog : Gtk.Dialog
    {
		//Atributo de las bases de datos propias
		private Databases _databases;

        //BBDD
        private List<string> _bbdd;

        private string _type;

        //Modificado
        private bool _modified;

        //Tarea con exito
        private bool _success;

        public List<string> BBDD
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

        public String TypeTask
        {
            get
            {
                return this._type;
            }
            set {
                this._type = value;
            }
        }

        public bool Success
        {
            get
            {
                return this._success;
            }
        }

        public ModifyDatabaseDialog(Databases databases,List<string> bbdd)
        {
            this.Build();

			this._databases = databases;
            this._bbdd = bbdd;

            if (this._bbdd[0] == Constants.MYSQL)
            {
                comboboxTipos.Active = 0;
            }else if (this._bbdd[0] == Constants.MONGODB){
                comboboxTipos.Active = 1;
            }
            entryBBDD.Text = this._bbdd[1];
            if (this._bbdd[2] != null)
            {
                entryUsuario.Text = this._bbdd[2];
                entryPassword.Text = this._bbdd[3];
            }
            this._modified = false;
            this._success = false;
            this._type = null;
        }

		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			this.Destroy();
		}

		//Cuando pulsas en el botón 'OK' para modificar BBDD
		protected void OnButtonOkClicked(object sender, EventArgs e)
		{
            this._type = Constants.TYPE_MODIFY;

            Adiccion();

            if(entryUsuario.Text == "")
                this._success = this._databases.ModifyDatabase(this._bbdd, comboboxTipos.ActiveText, entryBBDD.Text,null,null);
            else
                this._success = this._databases.ModifyDatabase(this._bbdd, comboboxTipos.ActiveText, entryBBDD.Text,
                                                     entryUsuario.Text, entryPassword.Text);
			this.Destroy();
		}

        /*
         * Evento para salir del menú de modificar la base de datos
         * */
        protected void OnButtonCancelActivated(object sender, EventArgs e)
        {
            this.Destroy();
        }

        private void Adiccion()
        {
            this._bbdd.Clear();
            this._bbdd[0] = comboboxTipos.ActiveText;
            this._bbdd[1] = entryBBDD.Text;
            if (entryUsuario.Text != "")
            {
                this._bbdd[2] = entryUsuario.Text;
                this._bbdd[3] = entryPassword.Text;
            }
        }

        /*
         * Evento para el borrado de la base de datos
         * */
        protected void OnButtonBorrarClicked(object sender, EventArgs e)
        {
            this._type = Constants.TYPE_DELETE;

            //Comprobacion borrar una bbdd que no se haya modificado
            this._modified = this._bbdd[0] != comboboxTipos.ActiveText || this._bbdd[1] != entryBBDD.Text ||
                (this._bbdd[2] != null && (this._bbdd[2] != entryUsuario.Text || this._bbdd[3] != entryPassword.Text));

            if (!this._modified)
            {
                this._success = this._databases.DeleteDatabase(this._bbdd, comboboxTipos.ActiveText, entryBBDD.Text,
                                                     entryUsuario.Text, entryPassword.Text);
            }
            this.Destroy();
        }
    }
}
