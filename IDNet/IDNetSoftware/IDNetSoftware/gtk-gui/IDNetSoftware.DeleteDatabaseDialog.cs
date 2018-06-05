
// This file has been generated by the GUI designer. Do not modify.
namespace IDNetSoftware
{
	public partial class DeleteDatabaseDialog
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.Frame frame1;

		private global::Gtk.Alignment GtkAlignment2;

		private global::Gtk.ComboBox comboboxTipos;

		private global::Gtk.Label labelNombreBBDD;

		private global::Gtk.Frame frame2;

		private global::Gtk.Alignment GtkAlignment3;

		private global::Gtk.Entry entryNombreBBDD;

		private global::Gtk.Label labelTipoBBDD;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Button buttonOk;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget IDNetSoftware.DeleteDatabaseDialog
			this.Name = "IDNetSoftware.DeleteDatabaseDialog";
			this.Icon = global::Gdk.Pixbuf.LoadFromResource("iconoIDNetSoftware");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			// Internal child IDNetSoftware.DeleteDatabaseDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.frame1 = new global::Gtk.Frame();
			this.frame1.Name = "frame1";
			this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment2 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
			this.GtkAlignment2.Name = "GtkAlignment2";
			this.GtkAlignment2.LeftPadding = ((uint)(12));
			// Container child GtkAlignment2.Gtk.Container+ContainerChild
			this.comboboxTipos = global::Gtk.ComboBox.NewText();
			this.comboboxTipos.AppendText(global::Mono.Unix.Catalog.GetString("mysql"));
			this.comboboxTipos.AppendText(global::Mono.Unix.Catalog.GetString("mongodb"));
			this.comboboxTipos.Name = "comboboxTipos";
			this.comboboxTipos.Active = 0;
			this.GtkAlignment2.Add(this.comboboxTipos);
			this.frame1.Add(this.GtkAlignment2);
			this.labelNombreBBDD = new global::Gtk.Label();
			this.labelNombreBBDD.Name = "labelNombreBBDD";
			this.labelNombreBBDD.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Tipo de base de datos</b>");
			this.labelNombreBBDD.UseMarkup = true;
			this.frame1.LabelWidget = this.labelNombreBBDD;
			this.vbox2.Add(this.frame1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.frame1]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.frame2 = new global::Gtk.Frame();
			this.frame2.Name = "frame2";
			this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame2.Gtk.Container+ContainerChild
			this.GtkAlignment3 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
			this.GtkAlignment3.Name = "GtkAlignment3";
			this.GtkAlignment3.LeftPadding = ((uint)(12));
			// Container child GtkAlignment3.Gtk.Container+ContainerChild
			this.entryNombreBBDD = new global::Gtk.Entry();
			this.entryNombreBBDD.CanFocus = true;
			this.entryNombreBBDD.Name = "entryNombreBBDD";
			this.entryNombreBBDD.IsEditable = true;
			this.entryNombreBBDD.InvisibleChar = '•';
			this.GtkAlignment3.Add(this.entryNombreBBDD);
			this.frame2.Add(this.GtkAlignment3);
			this.labelTipoBBDD = new global::Gtk.Label();
			this.labelTipoBBDD.Name = "labelTipoBBDD";
			this.labelTipoBBDD.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Nombre de base de datos</b>");
			this.labelTipoBBDD.UseMarkup = true;
			this.frame2.LabelWidget = this.labelTipoBBDD;
			this.vbox2.Add(this.frame2);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.frame2]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			w1.Add(this.vbox2);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(w1[this.vbox2]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Internal child IDNetSoftware.DeleteDatabaseDialog.ActionArea
			global::Gtk.HButtonBox w9 = this.ActionArea;
			w9.Name = "dialog1_ActionArea";
			w9.Spacing = 10;
			w9.BorderWidth = ((uint)(5));
			w9.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget(this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w10 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w9[this.buttonCancel]));
			w10.Expand = false;
			w10.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget(this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w11 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w9[this.buttonOk]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 320;
			this.DefaultHeight = 147;
			this.Show();
			this.buttonCancel.Clicked += new global::System.EventHandler(this.OnButtonCancelClicked);
			this.buttonOk.Clicked += new global::System.EventHandler(this.OnButtonOkClicked);
		}
	}
}