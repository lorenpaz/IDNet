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
        private XmlDocument _body;

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

        /*
         * Constructor del diálogo de SELECT
         * */
        public SelectDialog(string destination, string db_name,
                            BodyRespuesta002MySQL schema)
        {
            this.Build();

            this._destination = destination;
            this._db_name = db_name;
			this._body = new XmlDocument();
			this._typeOutPut = "Cancel";

            this._schema = schema;

            comboboxSelect.Sensitive = false;
            entryWhere.Sensitive = false;
            comboboxWhere.Sensitive = false;
            comboboxOrderBy.Sensitive = false;
            comboboxWhereSymbols.Sensitive = false;
            buttonOk.Sensitive = true;

            RellenarComboBox(null,CargarComboBoxFrom(),null,null);
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

			CrearBody();
            this._typeOutPut = "003";

			this.Destroy();
		}

		protected void OnComboboxSelectChanged(object sender, EventArgs e)
		{
            if(comboboxSelect.ActiveText == "*")
            {
                RellenarComboBox(null,null,null,CargarComboBoxOrderBy(comboboxFrom.ActiveText));
            }else{
                comboboxOrderBy.Data.Clear();
                comboboxOrderBy.AppendText(comboboxSelect.ActiveText);
            }

			comboboxOrderBy.Sensitive = true;

            //Control para que solo pulse el botón 'OK' cuando mande el from y el where
            if(comboboxFrom.ActiveText != "")
            {
                buttonOk.Sensitive = true;
            }
		}

		protected void OnComboboxFromChanged(object sender, EventArgs e)
		{
			//Relleno el select,where y orderBy
            RellenarComboBox(CargarComboBoxSelect(comboboxFrom.ActiveText), null, CargarComboBoxWhere(comboboxFrom.ActiveText), null);
			comboboxSelect.Sensitive = true;
			comboboxWhere.Sensitive = true;
			comboboxWhereSymbols.Sensitive = true;
			entryWhere.Sensitive = true;

			//Control para que solo pulse el botón 'OK' cuando mande el from y el where
            if (comboboxSelect.ActiveText != "")
			{
				buttonOk.Sensitive = true;
			}
		}

		protected void OnComboboxWhereChanged(object sender, EventArgs e)
		{
			entryWhere.Sensitive = true;
			comboboxWhereSymbols.Sensitive = true;
            if (comboboxWhere.ActiveText != " ")
            {
                entryWhere.Sensitive = true;
                comboboxWhereSymbols.Sensitive = true;
                RellenarComboBoxSymbols(CargarComboBoxWhereSymbols(comboboxFrom.ActiveText));
            }else{
                RellenarComboBoxSymbols(null);
                entryWhere.Text = "";
                entryWhere.Sensitive = false;
                comboboxWhereSymbols.Sensitive = false;
            }
        }

        /*
         * Método privado para rellenar los ComboBoxs
         * */
        private void RellenarComboBox(List<string> select, List<string> from,List<string> where, List<string> orderBy)
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

            if(where != null)
            {
				foreach (string field in where)
                {
                    comboboxWhere.AppendText(field);
                }
            }

            if (orderBy != null)
			{
				foreach (string field in orderBy)
				{
                    comboboxOrderBy.AppendText(field);
				}
            }

        }

        private void RellenarComboBoxSymbols(List<string> whereSymbols)
        {
            if (whereSymbols != null)
			{
				foreach (string field in whereSymbols)
				{
                    comboboxWhereSymbols.AppendText(field);
				}
            }else{
                comboboxWhereSymbols.Data.Clear();
			}
        }

		/*
         * Método para cargar el ComboBox del Select
         * */
		private List<string> CargarComboBoxSelect(string tabla)
		{
			List<string> select = new List<string>();

            select.Add("*");

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
         * Método para cargar el ComboBox del From
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
         * Método para cargar el ComboBox del Where
         * */
		private List<string> CargarComboBoxWhere(string tabla)
		{
			List<string> select = new List<string>();

            //Añado el vacio
            select.Add(" ");

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
         * Método para cargar el ComboBox del WhereSymbols
         * */
		private List<string> CargarComboBoxWhereSymbols(string tabla)
		{
			List<string> whereSymbols = new List<string>();

			foreach (var table in this._schema.Tables)
			{
				if (tabla == table.Name)
				{
					foreach (var col in table.Cols)
					{
                        if(col.Name == comboboxWhere.ActiveText)
                        {
                            if (col.Type == "int" || col.Type=="smallint" || 
                                col.Type == "decimal" || col.Type=="double" ||
                                col.Type=="float")
							{
								whereSymbols.Add("=");
								whereSymbols.Add("<");
								whereSymbols.Add(">");
								whereSymbols.Add("<>");

							}
                            else if (col.Type == "varchar" || col.Type=="char"
                                     || col.Type=="text" || col.Type=="mediumtext"
                                     || col.Type=="longtext")
							{
								whereSymbols.Add("=");
								whereSymbols.Add("<>");

							}
                            return whereSymbols;
                        }
					}
				}
			}


			return whereSymbols;
		}

		/*
         * Método para cargar el ComboBox del OrderBy
         * */
		private List<string> CargarComboBoxOrderBy(string tabla)
		{
			List<string> orderby = new List<string>();

			foreach (var table in this._schema.Tables)
			{
				if (tabla == table.Name)
				{
					foreach (var col in table.Cols)
					{
                        orderby.Add(col.Name);
					}
				}
			}

            return orderby;
		}

        private void CrearBody()
        {
            XmlDocument bodyDoc = new XmlDocument();
            XmlElement root = bodyDoc.DocumentElement;

			//Creamos elemento root
            XmlElement elementRoot = bodyDoc.CreateElement("body");
            bodyDoc.AppendChild(elementRoot);

			//Creamos elemento select
            XmlNode select = bodyDoc.CreateElement("select");
            select.InnerText = comboboxSelect.ActiveText;
            elementRoot.AppendChild(select);

			//Creamos elemento from
			XmlNode from = bodyDoc.CreateElement("from");
            from.InnerText = comboboxFrom.ActiveText;
            elementRoot.AppendChild(from);

			//Creamos elemento where
			XmlNode where = bodyDoc.CreateElement("where");
            if (comboboxWhere.ActiveText != null && comboboxWhere.ActiveText != "" && comboboxWhere.ActiveText != " ")
            {
                where.InnerText = comboboxWhere.ActiveText + comboboxWhereSymbols.ActiveText + "'" + entryWhere.Text + "'";
            }
            elementRoot.AppendChild(where);

			//Creamos elemento orderBy
			XmlNode orderby = bodyDoc.CreateElement("orderby");
            if (comboboxWhere.ActiveText != null && comboboxOrderBy.ActiveText == "")
                orderby.InnerText = "ASC";
            else
                orderby.InnerText = comboboxOrderBy.ActiveText;
            elementRoot.AppendChild(orderby);

            this._body = bodyDoc;
        }


    }
}
