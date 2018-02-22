using System;
using System.Collections.Generic;
using Gtk;
using DatabaseLibraryS;
using ConstantsLibraryS;

namespace IDNetSoftware
{
    public partial class UsuariosOVDialog : Gtk.Dialog
    {
        //Lista que muestra los usuarios
        ListStore _infoUsersView;

        //Vecinos O.V.
        Neighbours _neighbours;

        public UsuariosOVDialog(Neighbours vecinos)
        {
            this.Build();
            this._neighbours = vecinos;

            this._infoUsersView = new ListStore(typeof(string));
            AddValues();

            treeviewUsuariosOV.Model = this._infoUsersView;
            treeviewUsuariosOV.AppendColumn(Constants.TABLA_COLUMNA_VECINOS_VO, new CellRendererText(), "text", 0);

        }

        private void AddValues()
        {
            foreach(string vecino in this._neighbours.MiembrosOV.Keys)
            {
                this._infoUsersView.AppendValues(vecino);
            }
        }

        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }
    }
}
