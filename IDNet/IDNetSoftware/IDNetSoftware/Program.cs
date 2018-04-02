using Gtk;
using System.Diagnostics;
using ConstantsLibraryS;

namespace IDNetSoftware
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            //CheckIDNetDaemon();
            SplashWindow splash = new SplashWindow();
            splash.Show();
            Application.Run();
        }

        private static void CheckIDNetDaemon()
        {
            Process[] pname = Process.GetProcessesByName(Constants.IDNETDAEMON);
            if (pname.Length == 0)
            {
                RunIDNetDaemon();
            }
        }

        private static void RunIDNetDaemon()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C ./IDNetDaemonScript.sh";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
