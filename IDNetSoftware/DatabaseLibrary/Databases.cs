using System;
using System.Collections.Generic;
using System.IO;

namespace DatabaseLibrary
{
	//Clase constante con el archivo de configuración de las bases de datos
	static class Constants
	{
		public const string ConfigFile = @"databases.conf";
	}

    public class Databases
    {

		//Diccionario tipoBBDD -> [nombreBBDD1,nombreBBDD2]
		private Dictionary<string, List<string>> _databasesPropias;

        private Dictionary<string, Dictionary<string, List<string>>> _miembrosOV;

		public Databases()
		{
			ParseConf();
            ObtenerMiembrosOV();
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

		public Dictionary<string, Dictionary<string, List<string>>> MiembrosOV
		{
			get
			{
				return this._miembrosOV;
			}
			set
			{
                
				this._miembrosOV = value;
			}
		}

		//Lee del fichero de configuración
		private void ParseConf()
		{
			//Archivo a leer
			StreamReader conFile = File.OpenText(Constants.ConfigFile);
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

        private void ObtenerMiembrosOV()
        {
          //AQUI SE DEBERÍA REALIZAR LA CONEXIÓN CON EL GK PARA OBTENER INFORMACIÓN DE LOS MIEMBROS DE LA OV

        }
    }
}
