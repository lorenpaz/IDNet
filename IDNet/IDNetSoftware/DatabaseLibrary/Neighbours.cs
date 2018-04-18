using System.Collections.Generic;
using System.IO;
using System.Xml;
using System;

using ConstantsLibraryS;
using MessageLibraryS;

namespace DatabaseLibraryS
{

    public class Neighbours
    {
        //Diccionario usuario -> (tipoBBDD -> [nombreBBDD1,nombreBBDD2])
        private Dictionary<string, Dictionary<string, List<string>>> _miembrosOV;

        private List<string> _vecinosVO;

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
        public List<string> VecinosVO
        {
            get
            {
                return this._vecinosVO;
            }
            set
            {

                this._vecinosVO = value;
            }
        }

        public Neighbours()
        {
            this._miembrosOV = new Dictionary<string, Dictionary<string, List<string>>>();
            this._vecinosVO = new List<string>();
            ParseConf();
        }

        public void LoadNeighbourDatabases()
        {
            ParseConfAdvanced();    
        }

        public void SaveNeighbourDatabases(Message messageResponse)
        {
            string source = messageResponse.Source;
            XmlDocument aux = new XmlDocument();
            aux = messageResponse.Body.OwnerDocument;
            List<Tuple<string, string>> bbdd = new List<Tuple<string, string>>();
            Dictionary<string, string> lineToWrite = new Dictionary<string, string>();

            foreach(XmlElement bd in aux.DocumentElement.GetElementsByTagName("database"))
            {
                Tuple<string, string> tupl = new Tuple<string, string>(bd.GetAttribute("db_type"),bd.InnerText);
                bbdd.Add(tupl);
            }

            //Si existe se borra
            if(File.Exists(Constants.ConfigFileNeighboursDatabases))
            {
                File.Delete(Constants.ConfigFileNeighboursDatabases);
            }

            List<string> lineExactly = new List<string>();

            foreach (var bd in bbdd)
            {
                //lorenzo = mongodb,empleados;
                File.AppendAllText(Constants.ConfigFileNeighboursDatabases, source + "=" + bd.Item1 + "," + bd.Item2 + ";"+"\n");
            }
        }

        //Lee del fichero de configuración
        private void ParseConf()
        {
            //Archivo a leer
            if (File.Exists(Constants.ConfigFileNeighbours))
            {

                StreamReader conFile = File.OpenText(Constants.ConfigFileNeighbours);
                string line = conFile.ReadLine();

                //Voy leyendo línea por línea
                while (line != null)
                {
                    int i = 0;
                    string name = "";
                    /*
                     * 
                     * nombreUsuario;
                     * 
                     * Ejemplo:
                     * lorenzo;
                     * 
                     */

                    //Leemos el parámetro
                    while (line[i] != ';')
                    {
                        name += line[i];
                        i++;
                    }

                    if (!this._vecinosVO.Contains(name))
                    {
                        this._vecinosVO.Add(name);
                    }

                    line = conFile.ReadLine();
                }
            }
        }

        //Lee del fichero de configuración avanzado
        private void ParseConfAdvanced()
        {
            //Archivo a leer
            if (File.Exists(Constants.ConfigFileNeighboursDatabases))
            {
                StreamReader conFile = File.OpenText(Constants.ConfigFileNeighboursDatabases);
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
        }
    }
}
