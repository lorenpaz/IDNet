using System;
using System.Xml;
using System.Collections.Generic;

using ConstantsLibraryS;

namespace IDNetSoftware
{
    public partial class SelectDialog : Gtk.Dialog
    {
		
        //Atributos para la construcción de los mensajes
		private string _destination;
		private string _db_name;
		private string _db_type;
		private XmlNode _body;

        //Contiene la información del esquema
        private BodyRespuesta002MySQL _schema;

		//Atributo para saber cómo ha cerrado el diálogo
        private string _typeOutPut;

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

        public XmlNode Body
		{
			get
			{
                return this._body;
			}
			set
			{
                this._body = value;
			}
		}

        /*
         * Constructor del diálogo de SELECT
         * */
        public SelectDialog(string destination, string db_type, string db_name,
                            BodyRespuesta002MySQL schema)
        {
            this.Build();

            this._destination = destination;
            this._db_type = db_type;
            this._db_name = db_name;
			this._body = new XmlDocument();
			this._typeOutPut = "Cancel";

            this._schema = schema;

            comboboxSelect.Sensitive = false;
            entryWhere.Sensitive = false;

            RellenarComboBox(null,CargarComboBoxFrom());
        }

        /*
         * Método privado para rellenar los ComboBoxs
         * */
        private void RellenarComboBox(List<string> select, List<string> from)
        {
            if (select != null)
            {
                foreach (string field in select)
                {
                    comboboxSelect.AppendText(field);
                }
            }
            if (from != null)
            {
				foreach (string field in from)
				{
					comboboxFrom.AppendText(field);
				}
            }
        }

		/*
         * Método para cargar el ComboBox del Select
         * */
		private List<string> CargarComboBoxSelect(string tabla)
		{
			List<string> select = new List<string>();

            foreach (var table in this._schema.Tables)
			{
                if (tabla == table.Name)
                {
                    foreach (var col in table.Cols)
                    {
                        select.Add(col.Name);
                    }
                }
			}

			return select;
		}

		/*
         * Método para cargar el ComboBox del Select
         * */
		private List<string> CargarComboBoxFrom()
		{
			List<string> from = new List<string>();

            foreach (var table in this._schema.Tables)
			{
				from.Add(table.Name);
			}

			return from;
		}

        /*
         * Método del botón 'Cancel'
         * */
		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			this.Destroy();
		}

        /*
         * Método del botón 'OK'
         * */
        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }

        protected void OnComboboxFromChanged(object sender, EventArgs e)
        {
            //Relleno el select
            RellenarComboBox(CargarComboBoxSelect(comboboxFrom.ActiveText),null);
            comboboxSelect.Sensitive = true;
        }

        protected void OnComboboxSelectChanged(object sender, EventArgs e)
        {
            entryWhere.Sensitive = true;
        }
    }
}
