
// This file has been generated by the GUI designer. Do not modify.
namespace IDNetSoftware
{
	public partial class SimbologiaDialog
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.Label labelSimbologia;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TreeView treeviewInfoSymbols;

		private global::Gtk.Button buttonOk;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget IDNetSoftware.SimbologiaDialog
			this.Name = "IDNetSoftware.SimbologiaDialog";
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child IDNetSoftware.SimbologiaDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.labelSimbologia = new global::Gtk.Label();
			this.labelSimbologia.Name = "labelSimbologia";
			this.labelSimbologia.LabelProp = global::Mono.Unix.Catalog.GetString("Simbología");
			this.vbox2.Add(this.labelSimbologia);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.labelSimbologia]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeviewInfoSymbols = new global::Gtk.TreeView();
			this.treeviewInfoSymbols.CanFocus = true;
			this.treeviewInfoSymbols.Name = "treeviewInfoSymbols";
			this.GtkScrolledWindow.Add(this.treeviewInfoSymbols);
			this.vbox2.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow]));
			w4.Position = 1;
			w1.Add(this.vbox2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(w1[this.vbox2]));
			w5.Position = 0;
			// Internal child IDNetSoftware.SimbologiaDialog.ActionArea
			global::Gtk.HButtonBox w6 = this.ActionArea;
			w6.Name = "dialog1_ActionArea";
			w6.Spacing = 10;
			w6.BorderWidth = ((uint)(5));
			w6.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget(this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w7 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w6[this.buttonOk]));
			w7.Expand = false;
			w7.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.Show();
			this.buttonOk.Activated += new global::System.EventHandler(this.OnButtonOkActivated);
		}
	}
}