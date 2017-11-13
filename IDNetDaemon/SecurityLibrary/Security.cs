using System;
using System.IO.File;
using System.Windows.Forms;

namespace SecurityLibrary
{
	public class Security
	{
		const string CONFIG_FILE = "./config/blackList.conf";
		public Security ()
		{
		}

		public bool checkBlackList(string ip)
		{
			bool encontrado = false;

			try
			{
				//Leemos el archivo
				string[] lines = ReadAllLines(CONFIG_FILE);

				//Lo recorremos y buscamos si la ip figura en la lista
				int i = 0;
				while(ip.Equals(lines[i]))
					i++;

				//SI hemos terminado la lista y no lo hemos encontrado
				if(i = lines.GetLength)
					encontrado = true;
			}
			catch(Exception ex){
				//Si ha habido una excepcion al abrir el archivo lo notificamos e ignoramos la comprobacion
				encontrado = true;
				MessageBox.Show("BlackList cannot be read. Security check will be ignored.");
			}
			finally{
				return encontrado;
			}
		}

	}
}

