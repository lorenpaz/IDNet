
// This file has been generated by the GUI designer. Do not modify.
namespace IDNetSoftware
{
	public partial class ShowMessagesSentWindow
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TreeView treeviewMessages;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Fixed fixed2;

		private global::Gtk.Fixed fixed1;

		private global::Gtk.Button buttonOK;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget IDNetSoftware.ShowMessagesSentWindow
			this.Name = "IDNetSoftware.ShowMessagesSentWindow";
			this.Title = global::Mono.Unix.Catalog.GetString("IDNetSoftware");
			this.Icon = global::Gdk.Pixbuf.LoadFromResource("iconoIDNetSoftware");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			// Container child IDNetSoftware.ShowMessagesSentWindow.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeviewMessages = new global::Gtk.TreeView();
			this.treeviewMessages.CanFocus = true;
			this.treeviewMessages.Name = "treeviewMessages";
			this.GtkScrolledWindow.Add(this.treeviewMessages);
			this.vbox2.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow]));
			w2.Position = 0;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.fixed2 = new global::Gtk.Fixed();
			this.fixed2.Name = "fixed2";
			this.fixed2.HasWindow = false;
			this.hbox1.Add(this.fixed2);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.fixed2]));
			w3.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.fixed1 = new global::Gtk.Fixed();
			this.fixed1.Name = "fixed1";
			this.fixed1.HasWindow = false;
			this.hbox1.Add(this.fixed1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.fixed1]));
			w4.Position = 1;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonOK = new global::Gtk.Button();
			this.buttonOK.CanFocus = true;
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.UseUnderline = true;
			this.buttonOK.Label = global::Mono.Unix.Catalog.GetString("OK");
			this.hbox1.Add(this.buttonOK);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.buttonOK]));
			w5.Position = 2;
			this.vbox2.Add(this.hbox1);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			this.Add(this.vbox2);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 522;
			this.DefaultHeight = 383;
			this.Show();
			this.treeviewMessages.RowActivated += new global::Gtk.RowActivatedHandler(this.OnTreeviewMessagesRowActivated);
			this.buttonOK.Clicked += new global::System.EventHandler(this.OnButtonOKClicked);
		}
	}
}
