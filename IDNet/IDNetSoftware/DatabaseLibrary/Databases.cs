using System;
using System.Collections.Generic;
using System.IO;
using ConstantsLibraryS;

namespace DatabaseLibraryS
{

    public class Databases
    {

		//Diccionario tipoBBDD -> [nombreBBDD1,nombreBBDD2]
		private Dictionary<string, List<string>> _databasesPropias;

		public Databases()
		{
			ParseConf();
		}

		public Dictionary<string, List<string>> DatabasesPropias
		{
			get
			{
				return this._databasesPropias;
			}
			set
			{
				this._databasesPropias = value;
			}
		}

        public void update()
        {
            this._databasesPropias.Clear();
            ParseConf();    
        }

		//Lee del fichero de configuración
		private void ParseConf()
		{
            //Archivo a leer
            if(!File.Exists(Constants.ConfigFileDatabases))
            {
                throw new Exception("No hay archivo de configuración");
            }
            using (StreamReader conFile = File.OpenText(Constants.ConfigFileDatabases))
            {

                string line = conFile.ReadLine();

                //Inicializamos el atributo
                this._databasesPropias = new Dictionary<string, List<string>>();

                //Voy leyendo línea por línea
                while (line != null)
                {
                    int i = 0;
                    bool param = true;
                    string parameter = "", valor = "";
                    /*
                     * 
                     * database_type=database_name;
                     * 
                     * Ejemplo:
                     * mongodb=empleados;
                     * 
                     */

                    //Leemos el parámetro
                    while (line[i] != ';')
                    {
                        //Ignoramos el igual y lo usamos como marca que separa el parámetro de su valor
                        if (line[i] == '=')
                            param = false;
                        else if (param)
                            parameter += line[i];
                        else if (!param)
                            valor += line[i];

                        i++;
                    }

                    if (this._databasesPropias.ContainsKey(parameter))
                    {
                        this._databasesPropias[parameter].Add(valor);
                    }
                    else
                    {
                        List<string> aux = new List<string>();
                        aux.Add(valor);
                        this._databasesPropias.Add(parameter, aux);
                    }

                    line = conFile.ReadLine();
                }
            }
		}

        public bool addDatabase(string tipoBBDD,string nombreBBDD)
        {
            if(this._databasesPropias.ContainsKey(tipoBBDD) &&
               this._databasesPropias[tipoBBDD].Contains(nombreBBDD) )
            {
                return false;
            }

			if (!File.Exists(Constants.ConfigFileDatabases))
			{
				throw new Exception("No hay archivo de configuración");
			}

            using (StreamWriter w = File.AppendText(Constants.ConfigFileDatabases))
            {
                w.WriteLine(tipoBBDD+"="+nombreBBDD+";");

                if (this._databasesPropias.ContainsKey(tipoBBDD))
				{
					this._databasesPropias[tipoBBDD].Add(nombreBBDD);
				}
				else
				{
					List<string> aux = new List<string>();
					aux.Add(nombreBBDD);
					this._databasesPropias.Add(tipoBBDD, aux);
				}

                return true;
            }
        }

        public bool ModifyDatabase(List<string> bbdd,string nuevoTipoBBDD, string nuevoNombreBBDD)
        {
			if (!this._databasesPropias.ContainsKey(bbdd[0]) ||
             !this._databasesPropias[bbdd[0]].Contains(bbdd[1]))
			{
				return false;
			}

			if (!File.Exists(Constants.ConfigFileDatabases))
			{
				throw new Exception("No hay archivo de configuración");
			}

            string tempFile = Constants.CONFIG+"temp.txt";

            string[] lines = File.ReadAllLines(Constants.ConfigFileDatabases);
            string lineExactly = null;

            foreach(string linea in lines)
            {
                if(linea.Contains(bbdd[0]) && linea.Contains(bbdd[1]))
                {
                    lineExactly = linea;
                }    
            }

            string line = null;
            string lineToWrite = nuevoTipoBBDD + "=" + nuevoNombreBBDD + ";";

            //Escribo en un archivo temporal mientras que leo
            using (StreamReader reader = new StreamReader(Constants.ConfigFileDatabases))
			using (StreamWriter writer = new StreamWriter(tempFile))
			{
				while ((line = reader.ReadLine()) != null)
				{
                    //Sustituyo la linea que he modificado
                    if (line==lineExactly)
					{
						writer.WriteLine(lineToWrite);
					}
					else
					{
						writer.WriteLine(line);
					}
				}
			}

            //Borro el original,copio creando el original y borro el temporal
            File.Delete(Constants.ConfigFileDatabases);
            File.Copy(tempFile,Constants.ConfigFileDatabases);
            File.Delete(tempFile);

            return true;
        }
    }
}
