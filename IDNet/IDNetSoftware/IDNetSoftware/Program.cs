using Gtk;
using System;
using ConstantsLibraryS;
namespace IDNetSoftware
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            SplashWindow splash = new SplashWindow();
            splash.Show();
            Application.Run();
        }
    }
}
