﻿using Gtk;
using System.Threading;

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
