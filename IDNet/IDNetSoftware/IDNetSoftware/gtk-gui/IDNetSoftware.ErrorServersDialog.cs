
// This file has been generated by the GUI designer. Do not modify.
namespace IDNetSoftware
{
	public partial class ErrorServersDialog
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TextView textviewErrorMysql;

		private global::Gtk.ScrolledWindow GtkScrolledWindow1;

		private global::Gtk.TextView textviewErrorMongoDB;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Button buttonOK;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget IDNetSoftware.ErrorServersDialog
			this.Name = "IDNetSoftware.ErrorServersDialog";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child IDNetSoftware.ErrorServersDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.textviewErrorMysql = new global::Gtk.TextView();
			this.textviewErrorMysql.Name = "textviewErrorMysql";
			this.textviewErrorMysql.Editable = false;
			this.textviewErrorMysql.CursorVisible = false;
			this.textviewErrorMysql.AcceptsTab = false;
			this.GtkScrolledWindow.Add(this.textviewErrorMysql);
			this.vbox2.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow]));
			w3.Position = 0;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.textviewErrorMongoDB = new global::Gtk.TextView();
			this.textviewErrorMongoDB.Name = "textviewErrorMongoDB";
			this.textviewErrorMongoDB.Editable = false;
			this.textviewErrorMongoDB.CursorVisible = false;
			this.textviewErrorMongoDB.AcceptsTab = false;
			this.GtkScrolledWindow1.Add(this.textviewErrorMongoDB);
			this.vbox2.Add(this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow1]));
			w5.Position = 1;
			w1.Add(this.vbox2);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(w1[this.vbox2]));
			w6.Position = 0;
			// Internal child IDNetSoftware.ErrorServersDialog.ActionArea
			global::Gtk.HButtonBox w7 = this.ActionArea;
			w7.Name = "dialog1_ActionArea";
			w7.Spacing = 10;
			w7.BorderWidth = ((uint)(5));
			w7.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget(this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w8 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w7[this.buttonCancel]));
			w8.Expand = false;
			w8.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOK = new global::Gtk.Button();
			this.buttonOK.CanDefault = true;
			this.buttonOK.CanFocus = true;
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.UseUnderline = true;
			this.buttonOK.Label = global::Mono.Unix.Catalog.GetString("_OK");
			this.AddActionWidget(this.buttonOK, 0);
			global::Gtk.ButtonBox.ButtonBoxChild w9 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w7[this.buttonOK]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show();
			this.buttonCancel.Clicked += new global::System.EventHandler(this.OnButtonCancelClicked);
			this.buttonOK.Clicked += new global::System.EventHandler(this.OnButtonOKClicked);
		}
	}
}
