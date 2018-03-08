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
            iconosUsados.Add(Constants.ICONO_ADDATABASE);
            iconosUsados.Add(Constants.ICONO_UPDATEDATABASE);
            iconosUsados.Add(Constants.ICONO_CONNECTIONDATABASE);

            Dictionary<string, Gdk.Pixbuf> imagenes = CargarImagenes(iconosUsados);

            foreach (var a in imagenes)
            {
                switch(a.Key)
                {
                    case Constants.ICONO_ADDATABASE:
                        this._symbolsView.AppendValues(a.Value,Constants.INFORMACION_ICONO_ADDATABASE);
                        break;
                    case Constants.ICONO_UPDATEDATABASE:
                        this._symbolsView.AppendValues(a.Value, Constants.INFORMACION_ICONO_UPDATEDATABASE);
                        break;
                    case Constants.ICONO_CONNECTIONDATABASE:
                        this._symbolsView.AppendValues(a.Value, Constants.INFORMACION_ICONO_CONNECTIONDATABASE);
                        break;
                }

            }
        }

        private Dictionary<string, Gdk.Pixbuf> CargarImagenes(List<string> iconosUsados)
        {

            Dictionary<string, Gdk.Pixbuf> p = new Dictionary<string, Gdk.Pixbuf>();
            foreach(string path in iconosUsados)
            {
                p.Add(path,CargarImagen(path));
            }
            return p;
        }

        private Gdk.Pixbuf CargarImagen(string path)
        {
            var buffer = System.IO.File.ReadAllBytes(path);
           return  new Gdk.Pixbuf(buffer);
        }

        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }
    }   
}
