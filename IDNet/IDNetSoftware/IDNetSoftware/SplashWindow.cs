using System;
using System.Threading;
using Gtk;
using System.Diagnostics;
using ConstantsLibraryS;

namespace IDNetSoftware
{
    public partial class SplashWindow : Gtk.Window
    {
        MainWindow _main;
        LoginWindow _login;

        public SplashWindow() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();

            labelInfoTFG.ModifyFg(StateType.Normal, new Gdk.Color(1, 1, 1));
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
            if (!CheckIDNetDaemon())
            {
                this._login = new LoginWindow();
                this._login.Show();
            }
            this.Destroy();
        }


        private static bool CheckIDNetDaemon()
        {
            Process[] pname = Process.GetProcessesByName(Constants.IDNETDAEMON);
            return pname.Length != 0;
        }
    }
}
