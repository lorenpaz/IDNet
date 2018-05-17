using System;
using ConstantsLibraryS;
using DatabaseLibrary;
using CriptoLibraryS;
using System.Threading;

using Gdk;

namespace IDNetSoftware
{
    public partial class RegisterWindow : Gtk.Window
    {
        private RemoteDatabase _remoteDatabase;

        public RegisterWindow() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();

            this._remoteDatabase = new RemoteDatabase();
        }

        protected void OnButtonRegistrarseClicked(object sender, EventArgs e)
        {
            string username = entryUser.Text, password = entryPassword.Text, passwordR = entryPasswordR.Text;

            //Que no esten vacios
            if (username != "" && password != "" && password != "")
            {
                if(password == passwordR)
                {
                    if(this._remoteDatabase.InsertUser(username,password))
                    {
                        MostrarMensaje(Constants.MYSQL_REMOTE_REGISTER_SUCCESS);
                        Thread.Sleep(500);
                        LoginWindowShow();
                    }else{
                        MostrarMensaje(Constants.MYSQL_REMOTE_REGISTER_EXISTS);
                    }
                }else{
                    MostrarMensaje(Constants.MYSQL_REMOTE_ERROR_REGISTRARSE_CONTRASEÑA);
                }
            }
        }

        /*
         * Evento para retornar al inicio de sesion
         * */
        protected void OnButtonIniciarSesionClicked(object sender, EventArgs e)
        {
            LoginWindowShow();
        }

        private void LoginWindowShow()
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Destroy(); 
        }
        /*
         * Metodo privado para mostrar mensajes
         * */
        private void MostrarMensaje(string mensaje)
        {
            labelState.Text = mensaje;
        }
    }
}
