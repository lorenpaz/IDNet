using System;
using System.IO;
using ConstantsLibraryS;

namespace IDNetSoftware
{
    public partial class LaunchIDNetDaemonWindow : Gtk.Window
    {

        private bool _success;

        public Boolean Success
        {
            get
            {
                return this._success;
            }
            set
            {
                this._success = value;
            }
        }

        public LaunchIDNetDaemonWindow() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this._success = false;
            this.entryFirst.Text = "cd "+System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"../");
            this.entrySecond.Text = "./IDNetDaemonScript.sh start";
        }

        /*
         * Evento producido por pulsar el botón Salir
         * */
        protected void OnButtonSalirClicked(object sender, EventArgs e)
        {
            this._success = false;
            Constants.BorrarRecursos();
            this.Destroy();
        }

        /*
         * Evento producido por pulsar el botón OK
         * */
        protected void OnButtonOKClicked(object sender, EventArgs e)
        {
            if(LoginWindow.CheckIDNetDaemon()){
                this._success = true;
                MainWindow main = new MainWindow();
                main.Show();
                this.Destroy();
            }
        }

    }
}
