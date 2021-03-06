
// This file has been generated by the GUI designer. Do not modify.
namespace IDNetSoftware
{
	public partial class ModifyDatabaseDialog
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.Frame frame1;

		private global::Gtk.Alignment GtkAlignment2;

		private global::Gtk.ComboBox comboboxTipos;

		private global::Gtk.Label GtkLabel2;

		private global::Gtk.Frame frame2;

		private global::Gtk.Alignment GtkAlignment3;

		private global::Gtk.Entry entryBBDD;

		private global::Gtk.Label GtkLabel3;

		private global::Gtk.Frame frame4;

		private global::Gtk.Alignment GtkAlignment5;

		private global::Gtk.Entry entryUsuario;

		private global::Gtk.Label labelUsuario;

		private global::Gtk.Frame frame3;

		private global::Gtk.Alignment GtkAlignment4;

		private global::Gtk.Entry entryPassword;

		private global::Gtk.Label GtkLabel4;

		private global::Gtk.Button buttonBorrar;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Button buttonOk;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget IDNetSoftware.ModifyDatabaseDialog
			this.Name = "IDNetSoftware.ModifyDatabaseDialog";
			this.Icon = global::Gdk.Pixbuf.LoadFromResource("iconoIDNetSoftware");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			// Internal child IDNetSoftware.ModifyDatabaseDialog.VBox
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
			this.GtkAlignment2.Add(this.comboboxTipos);
			this.frame1.Add(this.GtkAlignment2);
			this.GtkLabel2 = new global::Gtk.Label();
			this.GtkLabel2.Name = "GtkLabel2";
			this.GtkLabel2.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Tipo de base de datos</b>");
			this.GtkLabel2.UseMarkup = true;
			this.frame1.LabelWidget = this.GtkLabel2;
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
			this.entryBBDD = new global::Gtk.Entry();
			this.entryBBDD.CanFocus = true;
			this.entryBBDD.Name = "entryBBDD";
			this.entryBBDD.IsEditable = true;
			this.entryBBDD.InvisibleChar = '•';
			this.GtkAlignment3.Add(this.entryBBDD);
			this.frame2.Add(this.GtkAlignment3);
			this.GtkLabel3 = new global::Gtk.Label();
			this.GtkLabel3.Name = "GtkLabel3";
			this.GtkLabel3.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Nombre de base de datos</b>");
			this.GtkLabel3.UseMarkup = true;
			this.frame2.LabelWidget = this.GtkLabel3;
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
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.frame4 = new global::Gtk.Frame();
			this.frame4.Name = "frame4";
			this.frame4.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame4.Gtk.Container+ContainerChild
			this.GtkAlignment5 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
			this.GtkAlignment5.Name = "GtkAlignment5";
			this.GtkAlignment5.LeftPadding = ((uint)(12));
			// Container child GtkAlignment5.Gtk.Container+ContainerChild
			this.entryUsuario = new global::Gtk.Entry();
			this.entryUsuario.CanFocus = true;
			this.entryUsuario.Name = "entryUsuario";
			this.entryUsuario.IsEditable = true;
			this.entryUsuario.InvisibleChar = '•';
			this.GtkAlignment5.Add(this.entryUsuario);
			this.frame4.Add(this.GtkAlignment5);
			this.labelUsuario = new global::Gtk.Label();
			this.labelUsuario.Name = "labelUsuario";
			this.labelUsuario.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Usuario</b>   (opcional)");
			this.labelUsuario.UseMarkup = true;
			this.frame4.LabelWidget = this.labelUsuario;
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
			this.entryPassword = new global::Gtk.Entry();
			this.entryPassword.CanFocus = true;
			this.entryPassword.Name = "entryPassword";
			this.entryPassword.IsEditable = true;
			this.entryPassword.Visibility = false;
			this.entryPassword.InvisibleChar = '•';
			this.GtkAlignment4.Add(this.entryPassword);
			this.frame3.Add(this.GtkAlignment4);
			this.GtkLabel4 = new global::Gtk.Label();
			this.GtkLabel4.Name = "GtkLabel4";
			this.GtkLabel4.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Contraseña</b>   (opcional)");
			this.GtkLabel4.UseMarkup = true;
			this.frame3.LabelWidget = this.GtkLabel4;
			w1.Add(this.frame3);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(w1[this.frame3]));
			w14.Position = 2;
			w14.Expand = false;
			w14.Fill = false;
			// Internal child IDNetSoftware.ModifyDatabaseDialog.ActionArea
			global::Gtk.HButtonBox w15 = this.ActionArea;
			w15.Name = "dialog1_ActionArea";
			w15.Spacing = 10;
			w15.BorderWidth = ((uint)(5));
			w15.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonBorrar = new global::Gtk.Button();
			this.buttonBorrar.CanFocus = true;
			this.buttonBorrar.Name = "buttonBorrar";
			this.buttonBorrar.UseUnderline = true;
			this.buttonBorrar.Label = global::Mono.Unix.Catalog.GetString("Borrar");
			this.AddActionWidget(this.buttonBorrar, 0);
			global::Gtk.ButtonBox.ButtonBoxChild w16 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w15[this.buttonBorrar]));
			w16.Expand = false;
			w16.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget(this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w17 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w15[this.buttonCancel]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget(this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w18 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w15[this.buttonOk]));
			w18.Position = 2;
			w18.Expand = false;
			w18.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show();
			this.buttonBorrar.Clicked += new global::System.EventHandler(this.OnButtonBorrarClicked);
			this.buttonCancel.Activated += new global::System.EventHandler(this.OnButtonCancelActivated);
			this.buttonCancel.Clicked += new global::System.EventHandler(this.OnButtonCancelClicked);
			this.buttonOk.Clicked += new global::System.EventHandler(this.OnButtonOkClicked);
		}
	}
}
