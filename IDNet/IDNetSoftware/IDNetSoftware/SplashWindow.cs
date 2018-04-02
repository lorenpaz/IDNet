using System;
using System.Threading;

namespace IDNetSoftware
{
    public partial class SplashWindow : Gtk.Window
    {
        MainWindow _main;
            
        public SplashWindow() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();

            ThreadStart tStart = new ThreadStart(this.Cargando);
            Thread t = new Thread(tStart);
            t.Start();
        }

        public void Cargando()
        {
            progressbar.Pulse();
            Thread.Sleep(1000);
            progressbar.Pulse();
            Thread.Sleep(1000);
            progressbar.Pulse();
            Thread.Sleep(1000);
            progressbar.Pulse();

            Gtk.Application.Invoke(
                delegate (object sender, EventArgs args)
                {
                NewWindow();
                }
            );
        }

        private void NewWindow()
        {
            this._main = new MainWindow();
            this.Destroy();
        }
    }
}
