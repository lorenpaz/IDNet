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
        // plugin path 
        private string myPythonPlugin = @"../../../PluginsLibrary/resources/conexionMongo.py";

        //python.exe path
        private string pythonpath = @"C:\Python27\python.exe";

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

        public string PythonPath
        {
            get
            {
                return pythonpath;
            }
            set
            {
                pythonpath = value;
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
            string args = this.myPythonPlugin;
            Run_cmd(args);
        }
        private void Run_cmd(string args)
        {
            //Información del proceso
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = this.pythonpath,
                Arguments = args + " " + "selectall",
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
