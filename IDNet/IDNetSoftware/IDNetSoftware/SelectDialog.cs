using System;
using System.Xml;
using System.Collections.Generic;

namespace IDNetSoftware
{
    public partial class SelectDialog : Gtk.Dialog
    {
		
        //Atributos para la construcción de los mensajes
		private string _destination;
		private string _db_name;
		private string _db_type;
		private XmlNode _body;

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
                           List<string> select, List<string> from, List<string> where)
        {
            this.Build();

            this._destination = destination;
            this._db_type = db_type;
            this._db_name = db_name;
			this._body = new XmlDocument();
			this._typeOutPut = "Cancel";

            RellenarComboBox(select, from,where);
        }

        /*
         * Método privado para rellenar los ComboBoxs
         * */
        private void RellenarComboBox(List<string> select, List<string> from, List<string> where)
        {
            foreach (string field in select)
            {
                comboboxSelect.AppendText(field);
            }
            foreach (string field in from)
            {
				comboboxFrom.AppendText(field);
			}
            foreach (string field in where)
            {
				comboboxWhere.AppendText(field);
			}
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
    }
}
