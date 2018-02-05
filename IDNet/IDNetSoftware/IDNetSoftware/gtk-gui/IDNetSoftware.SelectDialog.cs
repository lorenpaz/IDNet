
// This file has been generated by the GUI designer. Do not modify.
namespace IDNetSoftware
{
	public partial class SelectDialog
	{
		private global::Gtk.HBox hbox1;

		private global::Gtk.VBox vbox2;

		private global::Gtk.HBox hbox3;

		private global::Gtk.Label labelSelect;

		private global::Gtk.ComboBox comboboxSelect;

		private global::Gtk.Label labelFrom;

		private global::Gtk.ComboBox comboboxFrom;

		private global::Gtk.HBox hbox5;

		private global::Gtk.HBox hbox8;

		private global::Gtk.Label labelWhere;

		private global::Gtk.ComboBox comboboxWhere;

		private global::Gtk.HBox hbox7;

		private global::Gtk.ComboBox comboboxWhereSymbols;

		private global::Gtk.Entry entryWhere;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Label labelOrderBy;

		private global::Gtk.ComboBox comboboxOrderBy;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Button buttonOk;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget IDNetSoftware.SelectDialog
			this.Name = "IDNetSoftware.SelectDialog";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child IDNetSoftware.SelectDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.labelSelect = new global::Gtk.Label();
			this.labelSelect.Name = "labelSelect";
			this.labelSelect.LabelProp = global::Mono.Unix.Catalog.GetString("SELECT");
			this.hbox3.Add(this.labelSelect);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.labelSelect]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.comboboxSelect = global::Gtk.ComboBox.NewText();
			this.comboboxSelect.Name = "comboboxSelect";
			this.hbox3.Add(this.comboboxSelect);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.comboboxSelect]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.labelFrom = new global::Gtk.Label();
			this.labelFrom.Name = "labelFrom";
			this.labelFrom.LabelProp = global::Mono.Unix.Catalog.GetString("FROM");
			this.hbox3.Add(this.labelFrom);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.labelFrom]));
			w4.Position = 2;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.comboboxFrom = global::Gtk.ComboBox.NewText();
			this.comboboxFrom.Name = "comboboxFrom";
			this.hbox3.Add(this.comboboxFrom);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.comboboxFrom]));
			w5.Position = 3;
			w5.Expand = false;
			w5.Fill = false;
			this.vbox2.Add(this.hbox3);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox3]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.hbox8 = new global::Gtk.HBox();
			this.hbox8.Name = "hbox8";
			this.hbox8.Spacing = 6;
			// Container child hbox8.Gtk.Box+BoxChild
			this.labelWhere = new global::Gtk.Label();
			this.labelWhere.Name = "labelWhere";
			this.labelWhere.LabelProp = global::Mono.Unix.Catalog.GetString("WHERE");
			this.hbox8.Add(this.labelWhere);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.labelWhere]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox8.Gtk.Box+BoxChild
			this.comboboxWhere = global::Gtk.ComboBox.NewText();
			this.comboboxWhere.Name = "comboboxWhere";
			this.hbox8.Add(this.comboboxWhere);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.comboboxWhere]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			this.hbox5.Add(this.hbox8);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.hbox8]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.hbox7 = new global::Gtk.HBox();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			// Container child hbox7.Gtk.Box+BoxChild
			this.comboboxWhereSymbols = global::Gtk.ComboBox.NewText();
			this.comboboxWhereSymbols.Name = "comboboxWhereSymbols";
			this.hbox7.Add(this.comboboxWhereSymbols);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.comboboxWhereSymbols]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.entryWhere = new global::Gtk.Entry();
			this.entryWhere.CanFocus = true;
			this.entryWhere.Name = "entryWhere";
			this.entryWhere.IsEditable = true;
			this.entryWhere.InvisibleChar = '•';
			this.hbox7.Add(this.entryWhere);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.entryWhere]));
			w11.Position = 1;
			this.hbox5.Add(this.hbox7);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.hbox7]));
			w12.Position = 1;
			this.vbox2.Add(this.hbox5);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox5]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.labelOrderBy = new global::Gtk.Label();
			this.labelOrderBy.Name = "labelOrderBy";
			this.labelOrderBy.LabelProp = global::Mono.Unix.Catalog.GetString("ORDER BY");
			this.hbox2.Add(this.labelOrderBy);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.labelOrderBy]));
			w14.Position = 0;
			w14.Expand = false;
			w14.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.comboboxOrderBy = global::Gtk.ComboBox.NewText();
			this.comboboxOrderBy.Name = "comboboxOrderBy";
			this.hbox2.Add(this.comboboxOrderBy);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.comboboxOrderBy]));
			w15.Position = 1;
			w15.Expand = false;
			w15.Fill = false;
			this.vbox2.Add(this.hbox2);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox2]));
			w16.Position = 2;
			w16.Expand = false;
			w16.Fill = false;
			this.hbox1.Add(this.vbox2);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox2]));
			w17.Position = 0;
			w17.Expand = false;
			w17.Fill = false;
			w1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(w1[this.hbox1]));
			w18.Position = 0;
			w18.Expand = false;
			w18.Fill = false;
			// Internal child IDNetSoftware.SelectDialog.ActionArea
			global::Gtk.HButtonBox w19 = this.ActionArea;
			w19.Name = "dialog1_ActionArea";
			w19.Spacing = 10;
			w19.BorderWidth = ((uint)(5));
			w19.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget(this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w20 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w19[this.buttonCancel]));
			w20.Expand = false;
			w20.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget(this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w21 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w19[this.buttonOk]));
			w21.Position = 1;
			w21.Expand = false;
			w21.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 605;
			this.DefaultHeight = 300;
			this.Show();
			this.comboboxSelect.Changed += new global::System.EventHandler(this.OnComboboxSelectChanged);
			this.comboboxFrom.Changed += new global::System.EventHandler(this.OnComboboxFromChanged);
			this.comboboxWhere.Changed += new global::System.EventHandler(this.OnComboboxWhereChanged);
			this.buttonCancel.Clicked += new global::System.EventHandler(this.OnButtonCancelClicked);
			this.buttonOk.Clicked += new global::System.EventHandler(this.OnButtonOkClicked);
		}
	}
}