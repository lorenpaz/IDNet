
// This file has been generated by the GUI designer. Do not modify.
namespace IDNetSoftware
{
	public partial class MainWindow
	{
		private global::Gtk.UIManager UIManager;

		private global::Gtk.Action ArchivoAction;

		private global::Gtk.Action EdicinAction;

		private global::Gtk.Action BasesDeDatosAction;

		private global::Gtk.Action OVAction;

		private global::Gtk.Action AyudaAction;

		private global::Gtk.Action SalirAction;

		private global::Gtk.Action salirAction;

		private global::Gtk.Action addDatabasePngAction;

		private global::Gtk.Action addAction;

		private global::Gtk.Action clearAction;

		private global::Gtk.Action AcercaDeAction;

		private global::Gtk.Action MostrarUsuariosAction;

		private global::Gtk.Action MensajesAction;

		private global::Gtk.Action updateDatabasePngAction;

		private global::Gtk.Action SimbologiaAction;

		private global::Gtk.Action justifyRightAction;

		private global::Gtk.Action connectionPng;

		private global::Gtk.Action schemaPngAction;

		private global::Gtk.Action selectPngAction;

		private global::Gtk.Action deleteDatabasePngAction;

		private global::Gtk.Action BorrarAction;

		private global::Gtk.Action removeAction;

		private global::Gtk.VBox vbox1;

		private global::Gtk.MenuBar menubar2;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Toolbar toolbar1;

		private global::Gtk.VSeparator vseparator3;

		private global::Gtk.Toolbar toolbar2;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.TextView infoview;

		private global::Gtk.HBox hbox1;

		private global::Gtk.VBox vbox7;

		private global::Gtk.Label labelDatabases;

		private global::Gtk.ScrolledWindow GtkScrolledWindow2;

		private global::Gtk.TreeView treeviewDatabasesPropias;

		private global::Gtk.VBox vbox3;

		private global::Gtk.Label labelVecinosNombres;

		private global::Gtk.ScrolledWindow GtkScrolledWindow3;

		private global::Gtk.TreeView treeviewNeighbours;

		private global::Gtk.VBox vbox8;

		private global::Gtk.Label labelVecinos;

		private global::Gtk.ScrolledWindow GtkScrolledWindow1;

		private global::Gtk.TreeView treeviewDatabases;

		private global::Gtk.Statusbar statusbar;

		private global::Gtk.Label conexionesLabel;

		private global::Gtk.Label brandLabel;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget IDNetSoftware.MainWindow
			this.UIManager = new global::Gtk.UIManager();
			global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup("Default");
			this.ArchivoAction = new global::Gtk.Action("ArchivoAction", global::Mono.Unix.Catalog.GetString("Archivo"), null, null);
			this.ArchivoAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Archivo");
			w1.Add(this.ArchivoAction, null);
			this.EdicinAction = new global::Gtk.Action("EdicinAction", global::Mono.Unix.Catalog.GetString("Edición"), null, null);
			this.EdicinAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Edición");
			w1.Add(this.EdicinAction, null);
			this.BasesDeDatosAction = new global::Gtk.Action("BasesDeDatosAction", global::Mono.Unix.Catalog.GetString("Bases de Datos"), null, null);
			this.BasesDeDatosAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Bases de Datos");
			w1.Add(this.BasesDeDatosAction, null);
			this.OVAction = new global::Gtk.Action("OVAction", global::Mono.Unix.Catalog.GetString("O.V"), null, null);
			this.OVAction.ShortLabel = global::Mono.Unix.Catalog.GetString("O.V");
			w1.Add(this.OVAction, null);
			this.AyudaAction = new global::Gtk.Action("AyudaAction", global::Mono.Unix.Catalog.GetString("Ayuda"), null, null);
			this.AyudaAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Ayuda");
			w1.Add(this.AyudaAction, null);
			this.SalirAction = new global::Gtk.Action("SalirAction", global::Mono.Unix.Catalog.GetString("Salir"), null, null);
			this.SalirAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Salir");
			w1.Add(this.SalirAction, null);
			this.salirAction = new global::Gtk.Action("salirAction", global::Mono.Unix.Catalog.GetString("Salir"), null, "gtk-close");
			this.salirAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Salir");
			w1.Add(this.salirAction, null);
			this.addDatabasePngAction = new global::Gtk.Action("addDatabasePngAction", null, null, "addDatabase.png");
			w1.Add(this.addDatabasePngAction, null);
			this.addAction = new global::Gtk.Action("addAction", global::Mono.Unix.Catalog.GetString("Añadir"), null, "gtk-add");
			this.addAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Añadir");
			w1.Add(this.addAction, null);
			this.clearAction = new global::Gtk.Action("clearAction", null, null, "gtk-clear");
			w1.Add(this.clearAction, null);
			this.AcercaDeAction = new global::Gtk.Action("AcercaDeAction", global::Mono.Unix.Catalog.GetString("Acerca de"), null, null);
			this.AcercaDeAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Acerca de");
			w1.Add(this.AcercaDeAction, null);
			this.MostrarUsuariosAction = new global::Gtk.Action("MostrarUsuariosAction", global::Mono.Unix.Catalog.GetString("Mostrar usuarios"), null, null);
			this.MostrarUsuariosAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Mostrar usuarios");
			w1.Add(this.MostrarUsuariosAction, null);
			this.MensajesAction = new global::Gtk.Action("MensajesAction", global::Mono.Unix.Catalog.GetString("Mensajes "), null, null);
			this.MensajesAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Mensajes ");
			w1.Add(this.MensajesAction, null);
			this.updateDatabasePngAction = new global::Gtk.Action("updateDatabasePngAction", null, null, "updateDatabase.png");
			w1.Add(this.updateDatabasePngAction, null);
			this.SimbologiaAction = new global::Gtk.Action("SimbologiaAction", global::Mono.Unix.Catalog.GetString("Simbología"), null, null);
			this.SimbologiaAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Simbología");
			w1.Add(this.SimbologiaAction, null);
			this.justifyRightAction = new global::Gtk.Action("justifyRightAction", null, null, "gtk-justify-right");
			w1.Add(this.justifyRightAction, null);
			this.connectionPng = new global::Gtk.Action("connectionPng", null, null, "connection.png");
			w1.Add(this.connectionPng, null);
			this.schemaPngAction = new global::Gtk.Action("schemaPngAction", null, null, "schema.png");
			w1.Add(this.schemaPngAction, null);
			this.selectPngAction = new global::Gtk.Action("selectPngAction", null, null, "select.png");
			w1.Add(this.selectPngAction, null);
			this.deleteDatabasePngAction = new global::Gtk.Action("deleteDatabasePngAction", null, null, "deleteDatabase.png");
			w1.Add(this.deleteDatabasePngAction, null);
			this.BorrarAction = new global::Gtk.Action("BorrarAction", global::Mono.Unix.Catalog.GetString("Borrar"), null, null);
			this.BorrarAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Borrar");
			w1.Add(this.BorrarAction, null);
			this.removeAction = new global::Gtk.Action("removeAction", global::Mono.Unix.Catalog.GetString("Borrar"), null, "gtk-remove");
			this.removeAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Borrar");
			w1.Add(this.removeAction, null);
			this.UIManager.InsertActionGroup(w1, 0);
			this.AddAccelGroup(this.UIManager.AccelGroup);
			this.WidthRequest = 700;
			this.HeightRequest = 700;
			this.Name = "IDNetSoftware.MainWindow";
			this.Title = global::Mono.Unix.Catalog.GetString("IDNet");
			this.Icon = global::Gdk.Pixbuf.LoadFromResource("iconoIDNetSoftware");
			this.WindowPosition = ((global::Gtk.WindowPosition)(1));
			this.AllowShrink = true;
			this.DefaultWidth = 910;
			this.DefaultHeight = 650;
			// Container child IDNetSoftware.MainWindow.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString(@"<ui><menubar name='menubar2'><menu name='ArchivoAction' action='ArchivoAction'><menuitem name='salirAction' action='salirAction'/></menu><menu name='BasesDeDatosAction' action='BasesDeDatosAction'><menuitem name='addAction' action='addAction'/><menuitem name='removeAction' action='removeAction'/></menu><menu name='OVAction' action='OVAction'><menuitem name='MostrarUsuariosAction' action='MostrarUsuariosAction'/><menuitem name='MensajesAction' action='MensajesAction'/></menu><menu name='AyudaAction' action='AyudaAction'><menuitem name='AcercaDeAction' action='AcercaDeAction'/><menuitem name='SimbologiaAction' action='SimbologiaAction'/></menu></menubar></ui>");
			this.menubar2 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget("/menubar2")));
			this.menubar2.Name = "menubar2";
			this.vbox1.Add(this.menubar2);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.menubar2]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString(@"<ui><toolbar name='toolbar1'><toolitem name='clearAction' action='clearAction'/><toolitem name='addDatabasePngAction' action='addDatabasePngAction'/><toolitem name='deleteDatabasePngAction' action='deleteDatabasePngAction'/><toolitem name='updateDatabasePngAction' action='updateDatabasePngAction'/></toolbar></ui>");
			this.toolbar1 = ((global::Gtk.Toolbar)(this.UIManager.GetWidget("/toolbar1")));
			this.toolbar1.Name = "toolbar1";
			this.toolbar1.ShowArrow = false;
			this.toolbar1.ToolbarStyle = ((global::Gtk.ToolbarStyle)(0));
			this.toolbar1.IconSize = ((global::Gtk.IconSize)(5));
			this.hbox2.Add(this.toolbar1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.toolbar1]));
			w3.Position = 0;
			// Container child hbox2.Gtk.Box+BoxChild
			this.vseparator3 = new global::Gtk.VSeparator();
			this.vseparator3.Name = "vseparator3";
			this.hbox2.Add(this.vseparator3);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.vseparator3]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.UIManager.AddUiFromString("<ui><toolbar name=\'toolbar2\'><toolitem name=\'connectionPng\' action=\'connectionPng" +
					"\'/><toolitem name=\'schemaPngAction\' action=\'schemaPngAction\'/><toolitem name=\'se" +
					"lectPngAction\' action=\'selectPngAction\'/></toolbar></ui>");
			this.toolbar2 = ((global::Gtk.Toolbar)(this.UIManager.GetWidget("/toolbar2")));
			this.toolbar2.Name = "toolbar2";
			this.toolbar2.ShowArrow = false;
			this.toolbar2.ToolbarStyle = ((global::Gtk.ToolbarStyle)(0));
			this.toolbar2.IconSize = ((global::Gtk.IconSize)(5));
			this.hbox2.Add(this.toolbar2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.toolbar2]));
			w5.Position = 2;
			this.vbox1.Add(this.hbox2);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox2]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.infoview = new global::Gtk.TextView();
			this.infoview.WidthRequest = 200;
			this.infoview.HeightRequest = 300;
			this.infoview.Name = "infoview";
			this.infoview.Editable = false;
			this.infoview.CursorVisible = false;
			this.infoview.AcceptsTab = false;
			this.GtkScrolledWindow.Add(this.infoview);
			this.vbox1.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
			w8.Position = 2;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox7 = new global::Gtk.VBox();
			this.vbox7.Name = "vbox7";
			this.vbox7.Spacing = 6;
			// Container child vbox7.Gtk.Box+BoxChild
			this.labelDatabases = new global::Gtk.Label();
			this.labelDatabases.Name = "labelDatabases";
			this.labelDatabases.LabelProp = global::Mono.Unix.Catalog.GetString("Base de datos propias");
			this.vbox7.Add(this.labelDatabases);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox7[this.labelDatabases]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox7.Gtk.Box+BoxChild
			this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
			this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
			this.treeviewDatabasesPropias = new global::Gtk.TreeView();
			this.treeviewDatabasesPropias.CanFocus = true;
			this.treeviewDatabasesPropias.Name = "treeviewDatabasesPropias";
			this.treeviewDatabasesPropias.EnableSearch = false;
			this.GtkScrolledWindow2.Add(this.treeviewDatabasesPropias);
			this.vbox7.Add(this.GtkScrolledWindow2);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox7[this.GtkScrolledWindow2]));
			w11.Position = 1;
			this.hbox1.Add(this.vbox7);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox7]));
			w12.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox3 = new global::Gtk.VBox();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.labelVecinosNombres = new global::Gtk.Label();
			this.labelVecinosNombres.Name = "labelVecinosNombres";
			this.labelVecinosNombres.LabelProp = global::Mono.Unix.Catalog.GetString("Vecinos VO");
			this.vbox3.Add(this.labelVecinosNombres);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.labelVecinosNombres]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.GtkScrolledWindow3 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow3.Name = "GtkScrolledWindow3";
			this.GtkScrolledWindow3.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow3.Gtk.Container+ContainerChild
			this.treeviewNeighbours = new global::Gtk.TreeView();
			this.treeviewNeighbours.CanFocus = true;
			this.treeviewNeighbours.Name = "treeviewNeighbours";
			this.treeviewNeighbours.EnableSearch = false;
			this.GtkScrolledWindow3.Add(this.treeviewNeighbours);
			this.vbox3.Add(this.GtkScrolledWindow3);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.GtkScrolledWindow3]));
			w15.Position = 1;
			this.hbox1.Add(this.vbox3);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox3]));
			w16.Position = 1;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox8 = new global::Gtk.VBox();
			this.vbox8.Name = "vbox8";
			this.vbox8.Spacing = 6;
			// Container child vbox8.Gtk.Box+BoxChild
			this.labelVecinos = new global::Gtk.Label();
			this.labelVecinos.Name = "labelVecinos";
			this.labelVecinos.LabelProp = global::Mono.Unix.Catalog.GetString("BBDD Vecinos");
			this.vbox8.Add(this.labelVecinos);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox8[this.labelVecinos]));
			w17.Position = 0;
			w17.Expand = false;
			w17.Fill = false;
			// Container child vbox8.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.treeviewDatabases = new global::Gtk.TreeView();
			this.treeviewDatabases.CanFocus = true;
			this.treeviewDatabases.Name = "treeviewDatabases";
			this.treeviewDatabases.EnableSearch = false;
			this.GtkScrolledWindow1.Add(this.treeviewDatabases);
			this.vbox8.Add(this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vbox8[this.GtkScrolledWindow1]));
			w19.Position = 1;
			this.hbox1.Add(this.vbox8);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox8]));
			w20.Position = 2;
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w21.Position = 3;
			// Container child vbox1.Gtk.Box+BoxChild
			this.statusbar = new global::Gtk.Statusbar();
			this.statusbar.Name = "statusbar";
			this.statusbar.Homogeneous = true;
			this.statusbar.Spacing = 6;
			// Container child statusbar.Gtk.Box+BoxChild
			this.conexionesLabel = new global::Gtk.Label();
			this.conexionesLabel.Name = "conexionesLabel";
			this.conexionesLabel.LabelProp = global::Mono.Unix.Catalog.GetString("0 conexiones activas");
			this.statusbar.Add(this.conexionesLabel);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.statusbar[this.conexionesLabel]));
			w22.Position = 1;
			w22.Expand = false;
			w22.Fill = false;
			// Container child statusbar.Gtk.Box+BoxChild
			this.brandLabel = new global::Gtk.Label();
			this.brandLabel.Name = "brandLabel";
			this.brandLabel.LabelProp = global::Mono.Unix.Catalog.GetString("IDNet Software");
			this.statusbar.Add(this.brandLabel);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.statusbar[this.brandLabel]));
			w23.Position = 2;
			w23.Expand = false;
			w23.Fill = false;
			this.vbox1.Add(this.statusbar);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.statusbar]));
			w24.Position = 4;
			w24.Expand = false;
			w24.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Show();
			this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
			this.DestroyEvent += new global::Gtk.DestroyEventHandler(this.OnDestroyEvent);
			this.salirAction.Activated += new global::System.EventHandler(this.OnSalirActionActivated);
			this.addDatabasePngAction.Activated += new global::System.EventHandler(this.OnAddDatabasePngActionActivated);
			this.addAction.Activated += new global::System.EventHandler(this.OnAddActionActivated);
			this.clearAction.Activated += new global::System.EventHandler(this.OnClearActionActivated);
			this.AcercaDeAction.Activated += new global::System.EventHandler(this.OnAcercaDeActionActivated);
			this.MostrarUsuariosAction.Activated += new global::System.EventHandler(this.OnMostrarUsuariosActionActivated);
			this.MensajesAction.Activated += new global::System.EventHandler(this.OnMensajesEnviadosActionActivated);
			this.updateDatabasePngAction.Activated += new global::System.EventHandler(this.OnUpdateDatabasePngActionActivated);
			this.SimbologiaAction.Activated += new global::System.EventHandler(this.OnSimbologaActionActivated);
			this.connectionPng.Activated += new global::System.EventHandler(this.OnConnectionPngAction1Activated);
			this.schemaPngAction.Activated += new global::System.EventHandler(this.OnSchemaPngActionActivated);
			this.selectPngAction.Activated += new global::System.EventHandler(this.OnSelectPngActionActivated);
			this.deleteDatabasePngAction.Activated += new global::System.EventHandler(this.OnDeleteDatabasePngActionActivated);
			this.removeAction.Activated += new global::System.EventHandler(this.OnRemoveActionActivated);
			this.infoview.ButtonReleaseEvent += new global::Gtk.ButtonReleaseEventHandler(this.OnInfoviewButtonReleaseEvent);
			this.treeviewDatabasesPropias.RowActivated += new global::Gtk.RowActivatedHandler(this.OnTreeviewDatabasesPropiasRowActivated);
			this.treeviewDatabasesPropias.ButtonReleaseEvent += new global::Gtk.ButtonReleaseEventHandler(this.OnTreeviewDatabasesPropiasButtonReleaseEvent);
			this.treeviewNeighbours.RowActivated += new global::Gtk.RowActivatedHandler(this.OnTreeviewNeighboursRowActivated);
			this.treeviewNeighbours.ButtonReleaseEvent += new global::Gtk.ButtonReleaseEventHandler(this.OnTreeviewNeighboursButtonReleaseEvent);
			this.treeviewDatabases.RowActivated += new global::Gtk.RowActivatedHandler(this.OnTreeviewDatabasesRowActivated);
			this.treeviewDatabases.ButtonReleaseEvent += new global::Gtk.ButtonReleaseEventHandler(this.OnTreeviewDatabasesButtonReleaseEvent);
		}
	}
}
