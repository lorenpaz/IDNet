using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
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

                LaunchIDNetDaemonWindow launchDaemon = new LaunchIDNetDaemonWindow();
                launchDaemon.Show();
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

        public static bool CheckIDNetDaemon()
        {
            Process[]  pname = Process.GetProcessesByName("mono-sgen");
            return pname.Length != 0;
        }
    }
}
