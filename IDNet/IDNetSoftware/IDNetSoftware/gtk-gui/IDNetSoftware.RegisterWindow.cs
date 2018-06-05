
// This file has been generated by the GUI designer. Do not modify.
namespace IDNetSoftware
{
	public partial class RegisterWindow
	{
		private global::Gtk.VBox vbox4;

		private global::Gtk.Label labelRegistrarse;

		private global::Gtk.Image imageIDNet;

		private global::Gtk.HBox hbox4;

		private global::Gtk.Label labelUsuario;

		private global::Gtk.Entry entryUser;

		private global::Gtk.HBox hbox5;

		private global::Gtk.Label labelContrasenia;

		private global::Gtk.Entry entryPassword;

		private global::Gtk.HBox hbox7;

		private global::Gtk.Label labelRContrasenia;

		private global::Gtk.Entry entryPasswordR;

		private global::Gtk.HBox hbox6;

		private global::Gtk.Button buttonIniciarSesion;

		private global::Gtk.Label labelState;

		private global::Gtk.Button buttonRegistrarse;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget IDNetSoftware.RegisterWindow
			this.Name = "IDNetSoftware.RegisterWindow";
			this.Title = global::Mono.Unix.Catalog.GetString("IDNet");
			this.Icon = global::Gdk.Pixbuf.LoadFromResource("iconoIDNetSoftware");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			this.Resizable = false;
			// Container child IDNetSoftware.RegisterWindow.Gtk.Container+ContainerChild
			this.vbox4 = new global::Gtk.VBox();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			// Container child vbox4.Gtk.Box+BoxChild
			this.labelRegistrarse = new global::Gtk.Label();
			this.labelRegistrarse.Name = "labelRegistrarse";
			this.labelRegistrarse.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Registro</b>");
			this.labelRegistrarse.UseMarkup = true;
			this.labelRegistrarse.Justify = ((global::Gtk.Justification)(2));
			this.vbox4.Add(this.labelRegistrarse);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.labelRegistrarse]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.imageIDNet = new global::Gtk.Image();
			this.imageIDNet.Name = "imageIDNet";
			this.imageIDNet.Pixbuf = global::Gdk.Pixbuf.LoadFromResource("iconoSoftwareSmall.png");
			this.vbox4.Add(this.imageIDNet);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.imageIDNet]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			this.hbox4.Homogeneous = true;
			this.hbox4.Spacing = -57;
			this.hbox4.BorderWidth = ((uint)(3));
			// Container child hbox4.Gtk.Box+BoxChild
			this.labelUsuario = new global::Gtk.Label();
			this.labelUsuario.Name = "labelUsuario";
			this.labelUsuario.LabelProp = global::Mono.Unix.Catalog.GetString("Usuario:");
			this.labelUsuario.Justify = ((global::Gtk.Justification)(2));
			this.hbox4.Add(this.labelUsuario);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.labelUsuario]));
			w3.Position = 0;
			// Container child hbox4.Gtk.Box+BoxChild
			this.entryUser = new global::Gtk.Entry();
			this.entryUser.CanFocus = true;
			this.entryUser.Name = "entryUser";
			this.entryUser.IsEditable = true;
			this.entryUser.InvisibleChar = '•';
			this.hbox4.Add(this.entryUser);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.entryUser]));
			w4.Position = 1;
			w4.Fill = false;
			this.vbox4.Add(this.hbox4);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.hbox4]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Homogeneous = true;
			this.hbox5.Spacing = -57;
			this.hbox5.BorderWidth = ((uint)(3));
			// Container child hbox5.Gtk.Box+BoxChild
			this.labelContrasenia = new global::Gtk.Label();
			this.labelContrasenia.Name = "labelContrasenia";
			this.labelContrasenia.LabelProp = global::Mono.Unix.Catalog.GetString("Contraseña:");
			this.labelContrasenia.Justify = ((global::Gtk.Justification)(2));
			this.hbox5.Add(this.labelContrasenia);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.labelContrasenia]));
			w6.Position = 0;
			// Container child hbox5.Gtk.Box+BoxChild
			this.entryPassword = new global::Gtk.Entry();
			this.entryPassword.CanFocus = true;
			this.entryPassword.Name = "entryPassword";
			this.entryPassword.IsEditable = true;
			this.entryPassword.Visibility = false;
			this.entryPassword.InvisibleChar = '•';
			this.hbox5.Add(this.entryPassword);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.entryPassword]));
			w7.Position = 1;
			w7.Fill = false;
			this.vbox4.Add(this.hbox5);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.hbox5]));
			w8.Position = 3;
			// Container child vbox4.Gtk.Box+BoxChild
			this.hbox7 = new global::Gtk.HBox();
			this.hbox7.Name = "hbox7";
			this.hbox7.Homogeneous = true;
			this.hbox7.Spacing = -57;
			this.hbox7.BorderWidth = ((uint)(3));
			// Container child hbox7.Gtk.Box+BoxChild
			this.labelRContrasenia = new global::Gtk.Label();
			this.labelRContrasenia.Name = "labelRContrasenia";
			this.labelRContrasenia.LabelProp = global::Mono.Unix.Catalog.GetString("Repetir contraseña:");
			this.labelRContrasenia.Justify = ((global::Gtk.Justification)(2));
			this.hbox7.Add(this.labelRContrasenia);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.labelRContrasenia]));
			w9.Position = 0;
			// Container child hbox7.Gtk.Box+BoxChild
			this.entryPasswordR = new global::Gtk.Entry();
			this.entryPasswordR.CanFocus = true;
			this.entryPasswordR.Name = "entryPasswordR";
			this.entryPasswordR.IsEditable = true;
			this.entryPasswordR.Visibility = false;
			this.entryPasswordR.InvisibleChar = '•';
			this.hbox7.Add(this.entryPasswordR);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.entryPasswordR]));
			w10.Position = 1;
			w10.Fill = false;
			this.vbox4.Add(this.hbox7);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.hbox7]));
			w11.Position = 4;
			w11.Expand = false;
			w11.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			this.hbox6.BorderWidth = ((uint)(11));
			// Container child hbox6.Gtk.Box+BoxChild
			this.buttonIniciarSesion = new global::Gtk.Button();
			this.buttonIniciarSesion.CanFocus = true;
			this.buttonIniciarSesion.Name = "buttonIniciarSesion";
			this.buttonIniciarSesion.Label = global::Mono.Unix.Catalog.GetString("Iniciar sesión");
			this.hbox6.Add(this.buttonIniciarSesion);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.buttonIniciarSesion]));
			w12.Position = 0;
			w12.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.labelState = new global::Gtk.Label();
			this.labelState.Name = "labelState";
			this.labelState.Justify = ((global::Gtk.Justification)(2));
			this.hbox6.Add(this.labelState);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.labelState]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.buttonRegistrarse = new global::Gtk.Button();
			this.buttonRegistrarse.CanFocus = true;
			this.buttonRegistrarse.Name = "buttonRegistrarse";
			this.buttonRegistrarse.UseUnderline = true;
			this.buttonRegistrarse.Label = global::Mono.Unix.Catalog.GetString("Registrarse");
			this.hbox6.Add(this.buttonRegistrarse);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.buttonRegistrarse]));
			w14.Position = 2;
			w14.Fill = false;
			this.vbox4.Add(this.hbox6);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.hbox6]));
			w15.Position = 5;
			w15.Expand = false;
			w15.Fill = false;
			this.Add(this.vbox4);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 425;
			this.DefaultHeight = 397;
			this.Show();
			this.buttonIniciarSesion.Clicked += new global::System.EventHandler(this.OnButtonIniciarSesionClicked);
			this.buttonRegistrarse.Clicked += new global::System.EventHandler(this.OnButtonRegistrarseClicked);
		}
	}
}