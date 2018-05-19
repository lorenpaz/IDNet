
// This file has been generated by the GUI designer. Do not modify.
namespace IDNetSoftware
{
	public partial class AddDatabaseDialog
	{
		private global::Gtk.VBox vbox9;

		private global::Gtk.Frame frame1;

		private global::Gtk.Alignment GtkAlignment2;

		private global::Gtk.ComboBox comboboxTipos;

		private global::Gtk.Label labelTipoBaseDeDatos;

		private global::Gtk.Frame frame2;

		private global::Gtk.Alignment GtkAlignment3;

		private global::Gtk.Entry entryNombreBBDD;

		private global::Gtk.Label labelNombreBaseDeDatos;

		private global::Gtk.Frame frame4;

		private global::Gtk.Alignment GtkAlignment5;

		private global::Gtk.Entry entryUsername;

		private global::Gtk.Label labelUsername;

		private global::Gtk.Frame frame3;

		private global::Gtk.Alignment GtkAlignment4;

		private global::Gtk.Entry entryContrasenia;

		private global::Gtk.Label labelContrasenia;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Button buttonOk;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget IDNetSoftware.AddDatabaseDialog
			this.Name = "IDNetSoftware.AddDatabaseDialog";
			this.Icon = global::Gdk.Pixbuf.LoadFromResource("iconoIDNetSoftware");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			// Internal child IDNetSoftware.AddDatabaseDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox9 = new global::Gtk.VBox();
			this.vbox9.Name = "vbox9";
			this.vbox9.Spacing = 6;
			// Container child vbox9.Gtk.Box+BoxChild
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
			this.labelTipoBaseDeDatos = new global::Gtk.Label();
			this.labelTipoBaseDeDatos.Name = "labelTipoBaseDeDatos";
			this.labelTipoBaseDeDatos.LabelProp = global::Mono.Unix.Catalog.GetString("<b>TIpo de base de datos</b>");
			this.labelTipoBaseDeDatos.UseMarkup = true;
			this.frame1.LabelWidget = this.labelTipoBaseDeDatos;
			this.vbox9.Add(this.frame1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox9[this.frame1]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox9.Gtk.Box+BoxChild
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
			this.labelNombreBaseDeDatos = new global::Gtk.Label();
			this.labelNombreBaseDeDatos.Name = "labelNombreBaseDeDatos";
			this.labelNombreBaseDeDatos.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Nombre de base de datos</b>");
			this.labelNombreBaseDeDatos.UseMarkup = true;
			this.frame2.LabelWidget = this.labelNombreBaseDeDatos;
			this.vbox9.Add(this.frame2);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox9[this.frame2]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			w1.Add(this.vbox9);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(w1[this.vbox9]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.frame4 = new global::Gtk.Frame();
			this.frame4.Name = "frame4";
			this.frame4.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame4.Gtk.Container+ContainerChild
			this.GtkAlignment5 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
			this.GtkAlignment5.Name = "GtkAlignment5";
			this.GtkAlignment5.LeftPadding = ((uint)(12));
			// Container child GtkAlignment5.Gtk.Container+ContainerChild
			this.entryUsername = new global::Gtk.Entry();
			this.entryUsername.CanFocus = true;
			this.entryUsername.Name = "entryUsername";
			this.entryUsername.IsEditable = true;
			this.entryUsername.InvisibleChar = '•';
			this.GtkAlignment5.Add(this.entryUsername);
			this.frame4.Add(this.GtkAlignment5);
			this.labelUsername = new global::Gtk.Label();
			this.labelUsername.Name = "labelUsername";
			this.labelUsername.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Usuario</b>   (opcional)");
			this.labelUsername.UseMarkup = true;
			this.frame4.LabelWidget = this.labelUsername;
			w1.Add(this.frame4);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(w1[this.frame4]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.frame3 = new global::Gtk.Frame();
			this.frame3.Name = "frame3";
			this.frame3.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame3.Gtk.Container+ContainerChild
			this.GtkAlignment4 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
			this.GtkAlignment4.Name = "GtkAlignment4";
			this.GtkAlignment4.LeftPadding = ((uint)(12));
			// Container child GtkAlignment4.Gtk.Container+ContainerChild
			this.entryContrasenia = new global::Gtk.Entry();
			this.entryContrasenia.CanFocus = true;
			this.entryContrasenia.Name = "entryContrasenia";
			this.entryContrasenia.IsEditable = true;
			this.entryContrasenia.Visibility = false;
			this.entryContrasenia.InvisibleChar = '•';
			this.GtkAlignment4.Add(this.entryContrasenia);
			this.frame3.Add(this.GtkAlignment4);
			this.labelContrasenia = new global::Gtk.Label();
			this.labelContrasenia.Name = "labelContrasenia";
			this.labelContrasenia.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Contraseña</b>   (opcional)");
			this.labelContrasenia.UseMarkup = true;
			this.frame3.LabelWidget = this.labelContrasenia;
			w1.Add(this.frame3);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(w1[this.frame3]));
			w14.Position = 2;
			w14.Expand = false;
			w14.Fill = false;
			// Internal child IDNetSoftware.AddDatabaseDialog.ActionArea
			global::Gtk.HButtonBox w15 = this.ActionArea;
			w15.Name = "dialog1_ActionArea";
			w15.Spacing = 10;
			w15.BorderWidth = ((uint)(5));
			w15.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget(this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w16 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w15[this.buttonCancel]));
			w16.Expand = false;
			w16.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget(this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w17 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w15[this.buttonOk]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show();
			this.buttonCancel.Clicked += new global::System.EventHandler(this.OnButtonCancelClicked);
			this.buttonOk.Clicked += new global::System.EventHandler(this.OnButtonOkClicked);
		}
	}
}
