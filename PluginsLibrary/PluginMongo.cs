using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace PluginsLibrary
{
    public class PluginMongo
    {
        // python app to call  
        private string myPythonPlugin = @"C:\Users\loren\Desktop\4Carrera\TFG\mezcla\NodoCliente\conexionMongo.py";
        private string salida;
        public string Name
        {
            get {
                return myPythonPlugin;
            }
            set
            {
                myPythonPlugin = value;
            }
        }

        public string Salida
        {
            get
            {
                return salida;
            }
            set
            {
                salida = value;
            }
        }

        public void CallScript()
        {
            string args = myPythonPlugin;
            Run_cmd(args);

        }
        private void Run_cmd(string args)
        {
            //Información del proceso
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = @"C:\Python27\python.exe",
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true // We don't need new window
            };
            Process cmdProcess = new Process
            {
                StartInfo = start
            };
            cmdProcess.Start();

            // Read the standard output of the app we called.  
            StreamReader myStreamReader = cmdProcess.StandardOutput;
            this.salida = myStreamReader.ReadToEnd();

            // wait exit signal from the app we called 
            cmdProcess.WaitForExit();

            // close the process 
            cmdProcess.Close();
        }
    }
}
