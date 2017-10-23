using System;
using Gtk;
using PluginsLibrary;
using MessageLibrary;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnMongoPluginClicked(object sender, EventArgs e)
    {
        /*  PluginMongo mongo = new PluginMongo("usuarios");
          mongo.EstructureRequest();
          textContent.Buffer.Text = mongo.Salida;*/
        Message m = new Message();
        m.EstructureRequest("mongodb","usuarios");
    }

    protected void OnMysqlPluginClicked(object sender, EventArgs e)
    {
        //PluginMySQL mysql = new PluginMySQL();
        //mysql.EstructureRequest();
        Message m = new Message();
        m.EstructureRequest("mysql","usuarios");
    }
}
