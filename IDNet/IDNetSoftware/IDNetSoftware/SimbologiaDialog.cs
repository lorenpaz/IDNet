using System;
using System.Collections.Generic;
using Gtk;
using System.IO;
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

        public void AddValues()
        {
            List<string> iconosUsados = new List<string>();

            iconosUsados.Add(Constants.ICONO_ADDDATABASE);
            iconosUsados.Add(Constants.ICONO_DELETEDATABASE);
            iconosUsados.Add(Constants.ICONO_WARNINGDATABASE);
            iconosUsados.Add(Constants.ICONO_UPDATEDATABASE);
            iconosUsados.Add(Constants.ICONO_CONNECTIONDATABASE);
            iconosUsados.Add(Constants.ICONO_SCHEMADATABASE);
            iconosUsados.Add(Constants.ICONO_SELECTDATABASE);

            Dictionary<string, Gdk.Pixbuf> imagenes = CargarImagenes(iconosUsados);

            foreach (var a in imagenes)
            {
                switch(a.Key)
                {
                    case Constants.ADDDATABASE:
                        this._symbolsView.AppendValues(a.Value,Constants.INFORMACION_ICONO_ADDDATABASE);
                        break;
                    case Constants.DELETEDATABASE:
                        this._symbolsView.AppendValues(a.Value,Constants.INFORMACION_ICONO_DELETEDATABASE);
                        break;
                    case Constants.UPDATEDATABASE:
                        this._symbolsView.AppendValues(a.Value, Constants.INFORMACION_ICONO_UPDATEDATABASE);
                        break;
                    case Constants.WARNINGDATABASE:
                        this._symbolsView.AppendValues(a.Value,Constants.INFORMACION_ICONO_WARNINGDATABASE);
                        break;
                    case Constants.CONNECTIONDATABASE:
                        this._symbolsView.AppendValues(a.Value, Constants.INFORMACION_ICONO_CONNECTIONDATABASE);
                        break;
                    case Constants.SCHEMADATABASE:
                        this._symbolsView.AppendValues(a.Value, Constants.INFORMACION_ICONO_SCHEMADATABASE);
                        break;
                    case Constants.SELECTDATABASE:
                        this._symbolsView.AppendValues(a.Value, Constants.INFORMACION_ICONO_SELECTDATABASE);
                        break;
                    default:
                        break;
                }

            }
        }

        private Dictionary<string, Gdk.Pixbuf> CargarImagenes(List<string> iconosUsados)
        {

            Dictionary<string, Gdk.Pixbuf> p = new Dictionary<string, Gdk.Pixbuf>();
            foreach(string path in iconosUsados)
            {
                string val = "";
                if(path.Contains(Constants.ADDDATABASE)){
                    val = Constants.ADDDATABASE;
                }else if (path.Contains(Constants.DELETEDATABASE))
                {
                    val = Constants.DELETEDATABASE;
                }else if (path.Contains(Constants.UPDATEDATABASE))
                {
                    val = Constants.UPDATEDATABASE;
                }else if (path.Contains(Constants.WARNINGDATABASE))
                {
                    val = Constants.WARNINGDATABASE;
                }
                else if (path.Contains(Constants.CONNECTIONDATABASE))
                {
                    val = Constants.CONNECTIONDATABASE;
                }else if (path.Contains(Constants.SCHEMADATABASE))
                {
                    val = Constants.SCHEMADATABASE;
                }else if (path.Contains(Constants.SELECTDATABASE))
                {
                    val = Constants.SELECTDATABASE;
                }
                p.Add(val,CargarImagen(path));
            }
            return p;
        }

        private Gdk.Pixbuf CargarImagen(string path)
        {
            var buffer = System.IO.File.ReadAllBytes(path);
            Gdk.Pixbuf image = new Gdk.Pixbuf(buffer);
            image = image.ScaleSimple(32, 32,Gdk.InterpType.Tiles);
            return  image;
        }

        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }
    }   
}
