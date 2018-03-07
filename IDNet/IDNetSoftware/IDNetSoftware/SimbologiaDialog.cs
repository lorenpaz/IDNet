using System;
using System.Collections.Generic;
using Gtk;
using ConstantsLibraryS;
using System.Resources;

namespace IDNetSoftware
{
    public partial class SimbologiaDialog : Gtk.Dialog
    {
        //Lista que muestra los iconos y su simbologia
        ListStore _symbolsView;


        public SimbologiaDialog()
        {
            this.Build();

            this._symbolsView = new ListStore(typeof(Gdk.Pixbuf),typeof(string));
            treeviewInfoSymbols.Model = this._symbolsView;

            treeviewInfoSymbols.AppendColumn(Constants.TABLA_COLUMNA_ICONOS, new CellRendererPixbuf(), "pixbuf", 0);
            treeviewInfoSymbols.AppendColumn(Constants.TABLA_COLUMNA_EXPLICACION, new CellRendererText(), "text", 1);
            AddValues();
        }

        private void AddValues()
        {
            List<string> iconosUsados = new List<string>();
            iconosUsados.Add("../../resources/icons/addDatabase.png");
            iconosUsados.Add("../../resources/icons/updateDatabase.png");
            iconosUsados.Add("../../resources/icons/databaseConnection.png");

            List<Gdk.Pixbuf> imagenes = CargarImagenes(iconosUsados);

            foreach (var a in imagenes)
            {
                this._symbolsView.AppendValues(a,"prueba");
            }
        }

        private List<Gdk.Pixbuf> CargarImagenes(List<string> iconosUsados)
        {
            List<Gdk.Pixbuf> p = new List<Gdk.Pixbuf>();
            foreach(string path in iconosUsados)
            {
                p.Add(CargarImagen(path));
            }
            return p;
        }

        private Gdk.Pixbuf CargarImagen(string path)
        {
            var buffer = System.IO.File.ReadAllBytes(path);
           return  new Gdk.Pixbuf(buffer);
        }

        protected void OnButtonOkActivated(object sender, EventArgs e)
        {
            this.Destroy();
        }
    }   
}
