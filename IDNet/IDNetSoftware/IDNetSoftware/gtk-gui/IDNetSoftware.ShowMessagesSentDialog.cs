
// This file has been generated by the GUI designer. Do not modify.
namespace IDNetSoftware
{
	public partial class ShowMessagesSentDialog
	{
		private global::Gtk.VBox vbox3;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TreeView treeviewMessage;

		private global::Gtk.Frame frame1;

		private global::Gtk.Alignment GtkAlignment1;

		private global::Gtk.Label labelCuerpo;

		private global::Gtk.Label labelBody;

		private global::Gtk.Button buttonOk;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget IDNetSoftware.ShowMessagesSentDialog
			this.WidthRequest = 400;
			this.Name = "IDNetSoftware.ShowMessagesSentDialog";
			this.Icon = global::Gdk.Pixbuf.LoadFromResource("iconoIDNetSoftware");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			this.DefaultWidth = 500;
			// Internal child IDNetSoftware.ShowMessagesSentDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox3 = new global::Gtk.VBox();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeviewMessage = new global::Gtk.TreeView();
			this.treeviewMessage.WidthRequest = 500;
			this.treeviewMessage.HeightRequest = 250;
			this.treeviewMessage.CanFocus = true;
			this.treeviewMessage.Name = "treeviewMessage";
			this.GtkScrolledWindow.Add(this.treeviewMessage);
			this.vbox3.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.GtkScrolledWindow]));
			w3.Position = 0;
			// Container child vbox3.Gtk.Box+BoxChild
			this.frame1 = new global::Gtk.Frame();
			this.frame1.WidthRequest = 300;
			this.frame1.Name = "frame1";
			this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment1 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
			this.GtkAlignment1.Name = "GtkAlignment1";
			this.GtkAlignment1.LeftPadding = ((uint)(12));
			// Container child GtkAlignment1.Gtk.Container+ContainerChild
			this.labelCuerpo = new global::Gtk.Label();
			this.labelCuerpo.Name = "labelCuerpo";
			this.labelCuerpo.Wrap = true;
			this.GtkAlignment1.Add(this.labelCuerpo);
			this.frame1.Add(this.GtkAlignment1);
			this.labelBody = new global::Gtk.Label();
			this.labelBody.Name = "labelBody";
			this.labelBody.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Cuerpo del mensaje</b>");
			this.labelBody.UseMarkup = true;
			this.labelBody.Justify = ((global::Gtk.Justification)(2));
			this.frame1.LabelWidget = this.labelBody;
			this.vbox3.Add(this.frame1);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.frame1]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			w1.Add(this.vbox3);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(w1[this.vbox3]));
			w7.Position = 0;
			// Internal child IDNetSoftware.ShowMessagesSentDialog.ActionArea
			global::Gtk.HButtonBox w8 = this.ActionArea;
			w8.Name = "dialog1_ActionArea";
			w8.Spacing = 10;
			w8.BorderWidth = ((uint)(5));
			w8.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget(this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w9 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w8[this.buttonOk]));
			w9.Expand = false;
			w9.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultHeight = 355;
			this.Show();
			this.buttonOk.Clicked += new global::System.EventHandler(this.OnButtonOkClicked);
		}
	}
}
