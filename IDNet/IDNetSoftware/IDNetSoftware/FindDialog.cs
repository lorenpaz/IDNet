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

        private Dictionary<string, bool> _activeProyections;

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

            RellenarComboBox(CargarComboBoxCollections(), null, null, null);
            this._activeProyections = new Dictionary<string, bool>();
        }


        protected void OnButtonCancelClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }

        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            CrearBody();
            this._typeOutPut = "003";

            this.Destroy();
        }

        protected void OnComboboxCollectionChanged(object sender, EventArgs e)
        {
            RellenarComboBox(null, CargarComboBoxFilter(comboboxCollection.ActiveText),
                             CargarComboBoxProjections(comboboxCollection.ActiveText),
                             CargarComboBoxProjections(comboboxCollection.ActiveText));

            comboboxFilter.Sensitive = true;

            comboboxSort.Sensitive = true;

            comboboxProjection.Sensitive = true;
            checkbuttonProjection.Sensitive = true;

            comboboxLimit.Sensitive = true;

            buttonOk.Sensitive = true;

        }

        protected void OnComboboxFilterChanged(object sender, EventArgs e)
        {
            if (comboboxFilter.ActiveText != " ")
            {
                comboboxFilterSymbols.Sensitive = true;
                entryFilter.Sensitive = true;
                RellenarComboBoxFilterSymbols(CargarComboBoxFilterSymbols(comboboxCollection.ActiveText));
            }
            else
            {
                RellenarComboBoxFilterSymbols(null);
                entryFilter.Text = "";
                comboboxFilterSymbols.Sensitive = false;
                entryFilter.Sensitive = false;
            }
        }

        protected void OnComboboxProjectionChanged(object sender, EventArgs e)
        {
            checkbuttonProjection.Active = this._activeProyections[comboboxProjection.ActiveText];
        }

        protected void OnCheckbuttonProjectionToggled(object sender, EventArgs e)
        {
            this._activeProyections[comboboxProjection.ActiveText] = checkbuttonProjection.Active;
        }

        private void DeshabilitarInicio()
        {
            comboboxFilter.Sensitive = false;
            comboboxFilterSymbols.Sensitive = false;
            entryFilter.Sensitive = false;

            comboboxProjection.Sensitive = false;
            checkbuttonProjection.Sensitive = false;

            comboboxSort.Sensitive = false;

            comboboxLimit.Sensitive = false;

            buttonOk.Sensitive = false;
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
                comboboxFilter.Data.Clear();
                foreach (string field in filter)
                {
                    comboboxFilter.AppendText(field);
                }
            }

            if (projections != null)
            {
                comboboxProjection.Data.Clear();
                this._activeProyections.Clear();
                foreach (string field in projections)
                {
                    comboboxProjection.AppendText(field);
                    this._activeProyections.Add(field, true);
                }
            }

            if (sort != null)
            {
                comboboxSort.Data.Clear();

                foreach (string field in sort)
                {
                    comboboxSort.AppendText(field);
                }
            }
        }

        private void RellenarComboBoxFilterSymbols(List<string> filterSymbols)
        {
            if (filterSymbols != null)
            {
                comboboxFilterSymbols.Data.Clear();
                foreach (string field in filterSymbols)
                {
                    comboboxFilterSymbols.AppendText(field);
                }
            }
            else
            {
                comboboxFilterSymbols.Data.Clear();
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

        /*
         * Método para cargar el ComboBox del FilterSymbols
         * */
        private List<string> CargarComboBoxFilterSymbols(string coleccion)
        {
            List<string> filterSymbols = new List<string>();

            foreach (var collection in this._schema.Collections)
            {
                if (coleccion == collection.Name)
                {
                    foreach (var field in collection.Fields)
                    {
                        if (field.Name == comboboxFilter.ActiveText)
                        {
                            filterSymbols.Add("$eq");
                            filterSymbols.Add("$ne");
                        }
                    }
                }
            }

            return filterSymbols;
        }


        private void CrearBody()
        {
            XmlDocument bodyDoc = new XmlDocument();
            XmlElement root = bodyDoc.DocumentElement;

            //Creamos elemento root
            XmlElement elementRoot = bodyDoc.CreateElement("query");
            bodyDoc.AppendChild(elementRoot);

            //Creamos elemento Collection
            XmlNode collection = bodyDoc.CreateElement("collection");
            collection.InnerText = comboboxCollection.ActiveText;
            elementRoot.AppendChild(collection);

            //Creamos elemento Filter
            XmlNode filter = bodyDoc.CreateElement("filter");
            if (comboboxFilter.ActiveText != null && comboboxFilter.ActiveText != "" && comboboxFilter.ActiveText != " ")
            {
                filter.InnerText = comboboxFilter.ActiveText + " " + comboboxFilterSymbols.ActiveText + " " + entryFilter.Text;
            }
            elementRoot.AppendChild(filter);

            //Creamos elemento Projection
            XmlElement projection = bodyDoc.CreateElement("projection");
            foreach (var field in this._activeProyections)
            {
                string num = field.Value == true ? 1.ToString() : 0.ToString();
                projection.SetAttribute(field.Key, num);
            }
            elementRoot.AppendChild((XmlNode)projection);

            //Creamos elemento Sort
            XmlNode sort = bodyDoc.CreateElement("sort");
            if (comboboxSort.ActiveText == "")
                sort.InnerText = "";
            else
                sort.InnerText = comboboxSort.ActiveText;
            elementRoot.AppendChild(sort);

            //Creamos elemento Limit
            XmlNode limit = bodyDoc.CreateElement("limit");
            limit.InnerText = comboboxLimit.ActiveText;
            elementRoot.AppendChild(limit);


            this._body = bodyDoc;
        }
    }

}