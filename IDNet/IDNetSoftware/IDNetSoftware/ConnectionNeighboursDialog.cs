using System;
using System.Collections.Generic;
using ConstantsLibraryS;
using DatabaseLibraryS;
using Gtk;
using MessageLibraryS;

namespace IDNetSoftware
{
    public partial class ConnectionNeighboursDialog : Gtk.Dialog
    {
        //Atributo con los vecinos
        Neighbours _neighbours;

        //Lista con la informacion de las bases de datos de los vecinos
        ListStore _infoBBDDView;

        //Atributo para saber cómo ha cerrado el diálogo
        private string _typeOutPut;

        private TreePath _rowActivated;

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
        public TreePath RowActivated
        {
            get
            {
                return this._rowActivated;
            }
            set
            {
                this._rowActivated = value;
            }
        }

        public ConnectionNeighboursDialog(ListStore infoBBDDView, Neighbours neighbours)
        {
            this.Build();
            this._infoBBDDView = infoBBDDView;
            this._neighbours = neighbours;
            CargoBasesDeDatosDeLaOV();
            this._rowActivated = null;
        }

        private void CargoBasesDeDatosDeLaOV()
        {
            //AQUI HACE FALTA OBTENER BBDD de OTROS (conexion cliente con el GK para que nos dé tal información)

            //Añado valores a la lista
          //  AddValues();

            //Añado al treeview la información
            treeviewDatabases.Model = this._infoBBDDView;

            //Añado las columnas
            treeviewDatabases.AppendColumn(Constants.TABLA_COLUMNA_USUARIO, new CellRendererText(), "text", 0);
            treeviewDatabases.AppendColumn(Constants.TABLA_COLUMNA_TIPOBBDD, new CellRendererText(), "text", 1);
            treeviewDatabases.AppendColumn(Constants.TABLA_COLUMNA_NOMBREBBDD, new CellRendererText(), "text", 2);
        }

        //Añadir info de las bases de datos de los vecinos
      /*  private void AddValues()
        {
            foreach (KeyValuePair<string, Dictionary<string, List<string>>> entry in this._neighbours.MiembrosOV)
            {
                foreach (KeyValuePair<string, List<string>> entryTwo in entry.Value)
                {
                    foreach (string bbdd in entryTwo.Value)
                    {
                        this._infoBBDDView.AppendValues(entry.Key, entryTwo.Key, bbdd);
                    }
                }

            }
        }*/

        /*
         * Método activado cuando se pulsa en la lista de BBDD de vecinos
         * */
        protected void OnTreeviewDatabasesRowActivated(object o, RowActivatedArgs args)
        {
            TreeIter it;
            // this._infoBBDDView.GetIter(out it, args.Path);
            this._rowActivated = args.Path;

        }

        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            if (this._rowActivated != null)
            {
                this.Destroy();
                this._typeOutPut = "OK";
            }
        }

        protected void OnButtonCancelClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }
    }
}
