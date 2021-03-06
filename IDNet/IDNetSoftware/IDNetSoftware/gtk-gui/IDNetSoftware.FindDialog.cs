
// This file has been generated by the GUI designer. Do not modify.
namespace IDNetSoftware
{
	public partial class FindDialog
	{
		private global::Gtk.Label labelTitulo;

		private global::Gtk.VBox vbox2;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Label labelCollection;

		private global::Gtk.ComboBox comboboxCollection;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Label labelFilter;

		private global::Gtk.ComboBox comboboxFilter;

		private global::Gtk.ComboBox comboboxFilterSymbols;

		private global::Gtk.Entry entryFilter;

		private global::Gtk.HBox hbox3;

		private global::Gtk.Label labelProjection;

		private global::Gtk.ComboBox comboboxProjection;

		private global::Gtk.CheckButton checkbuttonProjection;

		private global::Gtk.HBox hbox4;

		private global::Gtk.Label labelSort;

		private global::Gtk.ComboBox comboboxSort;

		private global::Gtk.HBox hbox5;

		private global::Gtk.Label labelLimit;

		private global::Gtk.ComboBox comboboxLimit;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Button buttonOk;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget IDNetSoftware.FindDialog
			this.Name = "IDNetSoftware.FindDialog";
			this.Icon = global::Gdk.Pixbuf.LoadFromResource("iconoIDNetSoftware");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			// Internal child IDNetSoftware.FindDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.labelTitulo = new global::Gtk.Label();
			this.labelTitulo.Name = "labelTitulo";
			this.labelTitulo.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Solicitud de consulta a una base de datos MongoDB</b>");
			this.labelTitulo.UseMarkup = true;
			this.labelTitulo.Justify = ((global::Gtk.Justification)(2));
			w1.Add(this.labelTitulo);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(w1[this.labelTitulo]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.labelCollection = new global::Gtk.Label();
			this.labelCollection.Name = "labelCollection";
			this.labelCollection.LabelProp = global::Mono.Unix.Catalog.GetString("Collection");
			this.hbox1.Add(this.labelCollection);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.labelCollection]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.comboboxCollection = global::Gtk.ComboBox.NewText();
			this.comboboxCollection.Name = "comboboxCollection";
			this.hbox1.Add(this.comboboxCollection);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.comboboxCollection]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox2.Add(this.hbox1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.labelFilter = new global::Gtk.Label();
			this.labelFilter.Name = "labelFilter";
			this.labelFilter.LabelProp = global::Mono.Unix.Catalog.GetString("Filter");
			this.hbox2.Add(this.labelFilter);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.labelFilter]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.comboboxFilter = global::Gtk.ComboBox.NewText();
			this.comboboxFilter.Name = "comboboxFilter";
			this.hbox2.Add(this.comboboxFilter);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.comboboxFilter]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.comboboxFilterSymbols = global::Gtk.ComboBox.NewText();
			this.comboboxFilterSymbols.Name = "comboboxFilterSymbols";
			this.hbox2.Add(this.comboboxFilterSymbols);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.comboboxFilterSymbols]));
			w8.Position = 2;
			w8.Expand = false;
			w8.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.entryFilter = new global::Gtk.Entry();
			this.entryFilter.CanFocus = true;
			this.entryFilter.Name = "entryFilter";
			this.entryFilter.IsEditable = true;
			this.entryFilter.InvisibleChar = '•';
			this.hbox2.Add(this.entryFilter);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.entryFilter]));
			w9.Position = 3;
			this.vbox2.Add(this.hbox2);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox2]));
			w10.Position = 1;
			w10.Expand = false;
			w10.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.labelProjection = new global::Gtk.Label();
			this.labelProjection.Name = "labelProjection";
			this.labelProjection.LabelProp = global::Mono.Unix.Catalog.GetString("Projection");
			this.hbox3.Add(this.labelProjection);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.labelProjection]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.comboboxProjection = global::Gtk.ComboBox.NewText();
			this.comboboxProjection.Name = "comboboxProjection";
			this.hbox3.Add(this.comboboxProjection);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.comboboxProjection]));
			w12.Position = 1;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.checkbuttonProjection = new global::Gtk.CheckButton();
			this.checkbuttonProjection.CanFocus = true;
			this.checkbuttonProjection.Name = "checkbuttonProjection";
			this.checkbuttonProjection.Label = global::Mono.Unix.Catalog.GetString("Included in results");
			this.checkbuttonProjection.Active = true;
			this.checkbuttonProjection.DrawIndicator = true;
			this.checkbuttonProjection.UseUnderline = true;
			this.hbox3.Add(this.checkbuttonProjection);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.checkbuttonProjection]));
			w13.PackType = ((global::Gtk.PackType)(1));
			w13.Position = 2;
			this.vbox2.Add(this.hbox3);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox3]));
			w14.Position = 2;
			w14.Expand = false;
			w14.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.labelSort = new global::Gtk.Label();
			this.labelSort.Name = "labelSort";
			this.labelSort.LabelProp = global::Mono.Unix.Catalog.GetString("Sort");
			this.hbox4.Add(this.labelSort);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.labelSort]));
			w15.Position = 0;
			w15.Expand = false;
			w15.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.comboboxSort = global::Gtk.ComboBox.NewText();
			this.comboboxSort.Name = "comboboxSort";
			this.hbox4.Add(this.comboboxSort);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.comboboxSort]));
			w16.Position = 1;
			w16.Expand = false;
			w16.Fill = false;
			this.vbox2.Add(this.hbox4);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox4]));
			w17.Position = 3;
			w17.Expand = false;
			w17.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.labelLimit = new global::Gtk.Label();
			this.labelLimit.Name = "labelLimit";
			this.labelLimit.LabelProp = global::Mono.Unix.Catalog.GetString("Limit");
			this.hbox5.Add(this.labelLimit);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.labelLimit]));
			w18.Position = 0;
			w18.Expand = false;
			w18.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.comboboxLimit = global::Gtk.ComboBox.NewText();
			this.comboboxLimit.AppendText(global::Mono.Unix.Catalog.GetString("10"));
			this.comboboxLimit.AppendText(global::Mono.Unix.Catalog.GetString("20"));
			this.comboboxLimit.AppendText(global::Mono.Unix.Catalog.GetString("30"));
			this.comboboxLimit.AppendText(global::Mono.Unix.Catalog.GetString("40"));
			this.comboboxLimit.AppendText(global::Mono.Unix.Catalog.GetString("50"));
			this.comboboxLimit.Name = "comboboxLimit";
			this.comboboxLimit.Active = 0;
			this.hbox5.Add(this.comboboxLimit);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.comboboxLimit]));
			w19.Position = 1;
			w19.Expand = false;
			w19.Fill = false;
			this.vbox2.Add(this.hbox5);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox5]));
			w20.Position = 4;
			w20.Expand = false;
			w20.Fill = false;
			w1.Add(this.vbox2);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(w1[this.vbox2]));
			w21.Position = 1;
			w21.Expand = false;
			w21.Fill = false;
			// Internal child IDNetSoftware.FindDialog.ActionArea
			global::Gtk.HButtonBox w22 = this.ActionArea;
			w22.Name = "dialog1_ActionArea";
			w22.Spacing = 10;
			w22.BorderWidth = ((uint)(5));
			w22.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget(this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w23 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w22[this.buttonCancel]));
			w23.Expand = false;
			w23.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget(this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w24 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w22[this.buttonOk]));
			w24.Position = 1;
			w24.Expand = false;
			w24.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show();
			this.comboboxCollection.Changed += new global::System.EventHandler(this.OnComboboxCollectionChanged);
			this.comboboxFilter.Changed += new global::System.EventHandler(this.OnComboboxFilterChanged);
			this.comboboxProjection.Changed += new global::System.EventHandler(this.OnComboboxProjectionChanged);
			this.checkbuttonProjection.Toggled += new global::System.EventHandler(this.OnCheckbuttonProjectionToggled);
			this.buttonCancel.Clicked += new global::System.EventHandler(this.OnButtonCancelClicked);
			this.buttonOk.Clicked += new global::System.EventHandler(this.OnButtonOkClicked);
		}
	}
}
