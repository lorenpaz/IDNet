using System;
using System.Xml;
using System.Collections.Generic;

using ConstantsLibraryS;

namespace IDNetSoftware
{
    public partial class FindDialog : Gtk.Dialog
    {

		//Atributos para la construcción de los mensajes
		private string _destination;
		private string _db_name;
		private XmlDocument _body;

		//Contiene la información del esquema
        private BodyRespuesta002MongoDB _schema;

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

		public XmlDocument Body
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

		/*Constructor del diálogo del FIND
         * */
		public FindDialog(string destination, string db_name,
                          BodyRespuesta002MongoDB schema)
        {
            this.Build();

            this._destination = destination;
            this._db_name = db_name;
            this._schema = schema;
			this._typeOutPut = "Cancel";

            DeshabilitarInicio();

            RellenarComboBox(CargarComboBoxCollections(),null,null,null);
		}


        protected void OnComboboxCollectionChanged(object sender, EventArgs e)
        {
            RellenarComboBox(null,CargarComboBoxFilter(comboboxCollection.ActiveText),
                             CargarComboBoxProjections(comboboxCollection.ActiveText),
                             CargarComboBoxProjections(comboboxCollection.ActiveText));

            comboboxFilter.Sensitive = true;

            comboboxSort.Sensitive = true;

            comboboxProjection.Sensitive = true;
            checkbuttonProjection.Sensitive = true;

            spinbuttonLimit.Sensitive = true;
        }

        protected void OnComboboxFilterChanged(object sender, EventArgs e)
        {
        }

        protected void OnComboboxProjectionChanged(object sender, EventArgs e)
        {
        }

        private void DeshabilitarInicio()
        {
            comboboxFilter.Sensitive = false;
			comboboxFilterSymbols.Sensitive = false;
            entryFilter.Sensitive = false;

			comboboxProjection.Sensitive = false;
            checkbuttonProjection.Sensitive = false;

            comboboxSort.Sensitive = false;

            spinbuttonLimit.Sensitive = false;


        }

		/*
		* Método privado para rellenar los ComboBoxs
		* */
		private void RellenarComboBox(List<string> collections, List<string> filter, List<string> projections, List<string> sort)
		{
			if (collections != null)
			{
				foreach (string field in collections)
				{
                    comboboxCollection.AppendText(field);
				}
			}

            if (filter != null)
			{
				foreach (string field in filter)
				{
                    comboboxFilter.AppendText(field);
				}
			}

            if (projections != null)
			{
				foreach (string field in projections)
				{
                    comboboxProjection.AppendText(field);
				}
			}

            if (sort != null)
			{
				foreach (string field in sort)
				{
                    comboboxSort.AppendText(field);
				}
			}
		}

		/*
         * Método para cargar el ComboBox del Collections
         * */
		private List<string> CargarComboBoxCollections()
		{
			List<string> collections = new List<string>();

            foreach (var collection in this._schema.Collections)
			{
                collections.Add(collection.Name);
			}

			return collections;
		}

		/*
         * Método para cargar el ComboBox del Filter
         * */
		private List<string> CargarComboBoxFilter(string coleccion)
		{
			List<string> filter = new List<string>();

			//Añado el vacio
			filter.Add(" ");

            foreach (var collection in this._schema.Collections)
			{
                if (coleccion == collection.Name)
				{
                    foreach (var field in collection.Fields)
					{
                        filter.Add(field.Name);
					}
				}
			}

			return filter;
		}

		/*
         * Método para cargar el ComboBox del Projections
         * */
		private List<string> CargarComboBoxProjections(string coleccion)
		{
			List<string> projections = new List<string>();

			foreach (var collection in this._schema.Collections)
			{
				if (coleccion == collection.Name)
				{
					foreach (var field in collection.Fields)
					{
						projections.Add(field.Name);
					}
				}
			}

			return projections;
		}

    }
}
