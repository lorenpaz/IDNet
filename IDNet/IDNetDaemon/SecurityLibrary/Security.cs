using System;
using System.IO;
using ConstantsLibrary;

namespace SecurityLibrary
{
	public class Security
	{

		public Security()
		{
		}

		public bool checkBlackList(string ip)
		{
			bool encontrado = false;

			try
			{
				//Leemos el archivo
				StreamReader conFile = File.OpenText(Constants.CONF_BLACK_LIST);
                string lines = conFile.ReadToEnd();

				//Lo recorremos y buscamos si la ip figura en la lista
				int i = 0;
				while (ip.Equals(lines[i]))
					i++;

				//SI hemos terminado la lista y no lo hemos encontrado
                if (i == lines.Length)
					encontrado = true;
			}
			catch (Exception e)
			{
				//Si ha habido una excepcion al abrir el archivo lo notificamos e ignoramos la comprobacion
				encontrado = true;
                throw new Exception("BlackList cannot be read. Security check will be ignored."+e.Message);
			}
				return encontrado;
		}


	}
}
