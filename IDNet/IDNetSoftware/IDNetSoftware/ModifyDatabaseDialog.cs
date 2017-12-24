using System;
using DatabaseLibraryS;
using System.Collections.Generic;
using Gtk;

namespace IDNetSoftware
{
    public partial class ModifyDatabaseDialog : Gtk.Dialog
    {
		//Atributo de las bases de datos propias
		Databases _databases;
        List<string> _bbdd;

        public ModifyDatabaseDialog(Databases databases,List<string> bbdd)
        {
            this.Build();

			this._databases = databases;
            this._bbdd = bbdd;

            if (this._bbdd[0] == "mysql")
            {
                comboboxTipos.Active = 0;
            }else{
                comboboxTipos.Active = 1;
            }
            entryBBDD.Text = this._bbdd[1];
            if (this._bbdd[2] != null)
            {
                entryUsuario.Text = this._bbdd[2];
                entryPassword.Text = this._bbdd[3];
            }

        }

		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			this.Destroy();
		}

		//Cuando pulsas en el botón 'OK' para modificar BBDD
		protected void OnButtonOkClicked(object sender, EventArgs e)
		{
			bool add = false;
            if(entryUsuario.Text == "")
			    add = this._databases.ModifyDatabase(_bbdd, comboboxTipos.ActiveText, entryBBDD.Text,null,null);
            else
				add = this._databases.ModifyDatabase(_bbdd, comboboxTipos.ActiveText, entryBBDD.Text,
                                                     entryUsuario.Text, entryPassword.Text);
			this.Destroy();
		}

    }
}
