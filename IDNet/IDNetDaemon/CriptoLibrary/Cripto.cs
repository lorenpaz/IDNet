using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using ConstantsLibrary;
using System.IO;


namespace CriptoLibrary
{
    public class Cripto
    {
        private Dictionary<string, SymmetricAlgorithm> _keyMap;

        public Cripto(){}

		/*static void Main(string[] args)
        {
            RijndaelManaged key = null;

            try
            {
                // Load an XML document.
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.Load("test.xml");

                // Encrypt the "creditcard" element.
                Encrypt(xmlDoc, "creditcard", key);
                //EncryptMultiple(xmlDoc, "creditcard", key);

                Console.WriteLine("The element was encrypted");

                Console.WriteLine(xmlDoc.InnerXml);

                Decrypt(xmlDoc, key);
                //DecryptMultiple(xmlDoc, key);

                Console.WriteLine("The element was decrypted");

                Console.WriteLine(xmlDoc.InnerXml);
                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Clear the key.
                if (key != null)
                {
                    key.Clear();
                }
            }
        }*/

		public static void Encrypt(XmlDocument Doc, string ElementName, SymmetricAlgorithm Key)
        {
			// Create a new Rijndael key.
			RijndaelManaged key = new RijndaelManaged();

            // Check the arguments.  
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (ElementName == null)
                throw new ArgumentNullException("ElementToEncrypt");
            if (Key == null)
                throw new ArgumentNullException("Alg");

            ////////////////////////////////////////////////
            // Find the specified element in the XmlDocument
            // object and create a new XmlElemnt object.
            ////////////////////////////////////////////////
            XmlElement elementToEncrypt = Doc.GetElementsByTagName(ElementName)[0] as XmlElement;
            XmlNodeList elementsToEncrypt = Doc.GetElementsByTagName(ElementName);
            // Throw an XmlException if the element was not found.
            if (elementsToEncrypt == null)
            {
                throw new XmlException("The specified elements were not found");

            }

            //////////////////////////////////////////////////
            // Create a new instance of the EncryptedXml class 
            // and use it to encrypt the XmlElement with the 
            // symmetric key.
            //////////////////////////////////////////////////

            EncryptedXml eXml = new EncryptedXml();

            byte[] encryptedElement = eXml.EncryptData(elementToEncrypt, Key, false);

            ////////////////////////////////////////////////
            // Construct an EncryptedData object and populate
            // it with the desired encryption information.
            ////////////////////////////////////////////////

            EncryptedData edElement = new EncryptedData();
            edElement.Type = EncryptedXml.XmlEncElementUrl;

            // Create an EncryptionMethod element so that the 
            // receiver knows which algorithm to use for decryption.
            // Determine what kind of algorithm is being used and
            // supply the appropriate URL to the EncryptionMethod element.

            string encryptionMethod = null;

            if (Key is TripleDES)
            {
                encryptionMethod = EncryptedXml.XmlEncTripleDESUrl;
            }
            else if (Key is DES)
            {
                encryptionMethod = EncryptedXml.XmlEncDESUrl;
            }
            if (Key is Rijndael)
            {
                switch (Key.KeySize)
                {
                    case 128:
                        encryptionMethod = EncryptedXml.XmlEncAES128Url;
                        break;
                    case 192:
                        encryptionMethod = EncryptedXml.XmlEncAES192Url;
                        break;
                    case 256:
                        encryptionMethod = EncryptedXml.XmlEncAES256Url;
                        break;
                }
            }
            else
            {
                // Throw an exception if the transform is not in the previous categories
                throw new CryptographicException("The specified algorithm is not supported for XML Encryption.");
            }

            edElement.EncryptionMethod = new EncryptionMethod(encryptionMethod);

            // Add the encrypted element data to the 
            // EncryptedData object.
            edElement.CipherData.CipherValue = encryptedElement;

            ////////////////////////////////////////////////////
            // Replace the element from the original XmlDocument
            // object with the EncryptedData element.
            ////////////////////////////////////////////////////

            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
        }

        public static void Decrypt(XmlDocument Doc, SymmetricAlgorithm Alg)
        {
            // Check the arguments.  
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (Alg == null)
                throw new ArgumentNullException("Alg");

            // Find the EncryptedData element in the XmlDocument.
            XmlElement encryptedElement = Doc.GetElementsByTagName("EncryptedData")[0] as XmlElement;

            // If the EncryptedData element was not found, throw an exception.
            if (encryptedElement == null)
            {
                throw new XmlException("The EncryptedData element was not found.");
            }

            // Create an EncryptedData object and populate it.
            EncryptedData edElement = new EncryptedData();
            edElement.LoadXml(encryptedElement);

            // Create a new EncryptedXml object.
            EncryptedXml exml = new EncryptedXml();


            // Decrypt the element using the symmetric key.
            byte[] rgbOutput = exml.DecryptData(edElement, Alg);

            // Replace the encryptedData element with the plaintext XML element.
            exml.ReplaceData(encryptedElement, rgbOutput);
        }

        //Comprueba el fichero de claves para el mensaje 001
        public void CheckKey(string ip, SymmetricAlgorithm key)
		{
			ParseConf();

            if (this._keyMap.ContainsKey(ip))
            {
                if (this._keyMap.Remove(ip))
                    this._keyMap.Add(ip, key);
            }
            else
				this._keyMap.Add(ip, key);
		}

        /*
         * Dados un tipo de mesaje y una ip, devuelve si existe una clave
         * ya pactada para esa conexión.
         * 
         */
        public SymmetricAlgorithm CheckKey(string ip)
		{
            ParseConf();

            return this._keyMap.ContainsKey(ip)? this._keyMap[ip]:null;
		}

		//Lee del fichero de configuración
		private void ParseConf()
		{
			//Archivo a leer
			StreamReader conFile = File.OpenText(Constants.CONF_KEYS);
			string line = conFile.ReadLine();
            this._keyMap = new Dictionary<string, SymmetricAlgorithm>();

			//Voy leyendo línea por línea
			while (line != null)
			{
				int i = 0;
				bool param = true;
				string parameter = "", valor = "";
				/*
                 * 
                 * ip=key;
                 * 
                 * Ejemplo:
                 * 192.168.9.7=sauBIUBFuncd223;
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
                if (!this._keyMap.ContainsKey(parameter))
                {
                    this._keyMap.Add(parameter, SymmetricAlgorithm.Create(valor));
                }
                else
                {
                    this._keyMap[parameter] = SymmetricAlgorithm.Create(valor);
                }
				line = conFile.ReadLine();
			}
		}

	}
}

