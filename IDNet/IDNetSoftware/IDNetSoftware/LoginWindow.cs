using System;
using System.IO;
using System.Diagnostics;

using DatabaseLibrary;
using ConstantsLibraryS;


namespace IDNetSoftware
{
    public partial class LoginWindow : Gtk.Window
    {
        private RemoteDatabase _remoteDatabase;

        public LoginWindow() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this._remoteDatabase = new RemoteDatabase();
        }

        /*
         * Evento cuando inicias sesión
         * */
        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            string username = entryUser.Text, password = entryPassword.Text;

            //Que no esten vacios
            if(username != "" && password != "" && this._remoteDatabase.CheckUser(username, password))
            {
                MostrarMensaje(Constants.MYSQL_REMOTE_LOGIN_SUCCESS);
                Usuario.SaveConf(this._remoteDatabase.SaveUserToFile(username));
                if (!CheckIDNetDaemon())
                {
                    RunIDNetDaemon();
                }
                MainWindow main = new MainWindow();
                main.Show();

                this.Destroy();
            }else{
                MostrarMensaje(Constants.MYSQL_REMOTE_ERROR_INICIO_SESION);
            }
        }

        /*
         * Evento cuando quieres ir a registrarse
         * */
        protected void OnButtonRegisterClicked(object sender, EventArgs e)
        {

            RegisterWindow register = new RegisterWindow();
            register.Show();
            this.Destroy();
        }

        /*
         * Metodo privado para mostrar mensajes
         * */
        private void MostrarMensaje(string mensaje)
        {
            labelState.Text = mensaje;
        }

        private static void RunIDNetDaemon()
        {
            Console.WriteLine("Arrancamos INetDaemon");
            Console.WriteLine("Ruta:"+System.AppDomain.CurrentDomain.BaseDirectory + "../");
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WorkingDirectory = System.AppDomain.CurrentDomain.BaseDirectory+ "../";
            startInfo.FileName = "./IDNetDaemonScript.sh start";
            startInfo.UseShellExecute = false;
            process = Process.Start(startInfo);
            process.WaitForExit();
        }

        private static bool CheckIDNetDaemon()
        {
            Process[] p = Process.GetProcesses();
            Process[] pname = Process.GetProcessesByName("mono-sgen");

            return pname.Length != 0;
        }
    }
}
