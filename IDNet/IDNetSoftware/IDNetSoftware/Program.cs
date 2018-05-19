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
            Console.WriteLine("Directorio configuration:"+Constants.CONFIG);
            SplashWindow splash = new SplashWindow();
            splash.Show();
            Application.Run();
        }
    }
}
