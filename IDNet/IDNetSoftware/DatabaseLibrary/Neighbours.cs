using System;
using System.Collections.Generic;
using System.IO;
using ConstantsLibraryS;

namespace DatabaseLibraryS
{

    public class Neighbours
    {
        //Diccionario usuario -> (tipoBBDD -> [nombreBBDD1,nombreBBDD2])
        private Dictionary<string, Dictionary<string, List<string>>> _miembrosOV;

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

        public Neighbours()
        {
            ObtenerMiembrosOV();
            ParseConf();
        }

        //Lee del fichero de configuración
        private void ParseConf()
        {
            //Archivo a leer
            StreamReader conFile = File.OpenText(Constants.ConfigFileNeighbours);
            string line = conFile.ReadLine();

            //Inicializamos el atributo
            this._miembrosOV = new Dictionary<string, Dictionary<string, List<string>>>();

            //Voy leyendo línea por línea
            while (line != null)
            {
                int i = 0;
                bool param = true, param2 = true;
                string name = "", tipoBBDD = "", nombreBBDD = "";
                /*
                 * 
                 * nombreUsuario=database_type,database_name;
                 * 
                 * Ejemplo:
                 * lorenzo=mongodb,empleados;
                 * 
                 */

                //Leemos el parámetro
                while (line[i] != ';')
                {
                    //Parseo para la obtención
                    if (line[i] == '=')
                        param = false;
                    else if (line[i] == ',')
                        param2 = false;
                    else if (param)
                        name += line[i];
                    else if (!param && param2)
                    {
                        tipoBBDD += line[i];
                    }
                    else if (!param && !param2)
                    {
                        nombreBBDD += line[i];
                    }

                    i++;
                }

                if (this._miembrosOV.ContainsKey(name))
                {
                    if (this._miembrosOV[name].ContainsKey(tipoBBDD))
                    {
                        this._miembrosOV[name][tipoBBDD].Add(nombreBBDD);
                    }
                    else
                    {
                        List<string> list = new List<string>();
                        list.Add(nombreBBDD);
                        this._miembrosOV[name].Add(tipoBBDD, list);
                    }
                }
                else
                {
                    Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
                    this._miembrosOV.Add(name, dict);

                    List<string> list = new List<string>();
                    list.Add(nombreBBDD);
                    this._miembrosOV[name].Add(tipoBBDD, list);
                }

                line = conFile.ReadLine();
            }
        }

        private void ObtenerMiembrosOV()
        {
            /*AQUI SE DEBERÍA REALIZAR LA CONEXIÓN CON EL GK PARA OBTENER INFORMACIÓN DE LOS MIEMBROS DE LA OV
			 * Y GUARDARLA EN UN ARCHIVO DE CONFIGURACIÓN LLAMADO neighbours.conf. MIRAR EL PARSERCONF
            */
        }
    }
}
