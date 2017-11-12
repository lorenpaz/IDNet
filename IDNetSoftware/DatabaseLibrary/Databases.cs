using System;
using System.Collections.Generic;
using System.IO;
using ConstantsLibrary;

namespace DatabaseLibrary
{

    public class Databases
    {

		//Diccionario tipoBBDD -> [nombreBBDD1,nombreBBDD2]
		private Dictionary<string, List<string>> _databasesPropias;

		public Databases()
		{
			ParseConf();
		}

		public Dictionary<string, List<string>> databasesPropias
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
    }
}
