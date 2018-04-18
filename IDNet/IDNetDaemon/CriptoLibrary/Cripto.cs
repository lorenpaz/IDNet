using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using ConstantsLibrary;
using System.IO;
using System.IO.Compression;
using log4net;

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;

namespace CriptoLibrary
{
    public class Cripto
    {
        static readonly ILog log = LogManager.GetLogger(typeof(Cripto));

		RsaKeyParameters _publicKey;
		RsaKeyParameters _privateKey;

        /*
         * Constructor
         * */
		public Cripto()
		{
			RsaKeyPairGenerator rsa = new RsaKeyPairGenerator();
			rsa.Init(new KeyGenerationParameters(new SecureRandom(), 2048));
			AsymmetricCipherKeyPair asymetric = rsa.GenerateKeyPair();

			//Extracting the public/private key from the pair
			this._privateKey = (RsaKeyParameters)asymetric.Private;
			this._publicKey = (RsaKeyParameters)asymetric.Public;

			ExportPublicKey(this._publicKey);
			ExportPrivateKey(this._privateKey);

		}

		public RsaKeyParameters PublicKey
		{
			get
			{
                return this._publicKey;
			}
			set
			{
                this._publicKey = value;
			}
		}

		public RsaKeyParameters PrivateKey
		{
			get
			{
                return this._privateKey;
			}
			set
			{
                this._privateKey = value;
			}
		}

        /*
         * Método privado estático para exportar la clave pública
         * */
		private static void ExportPublicKey(RsaKeyParameters publicKey)
		{
			//To print the public key in pem format
            TextWriter textWriter1 = new StreamWriter(Constants.CONF_PUBLIC_KEY);
			PemWriter pemWriter1 = new PemWriter(textWriter1);
			pemWriter1.WriteObject(publicKey);
			pemWriter1.Writer.Flush();
			pemWriter1.Writer.Close();
		}

        /*
         * Método privado estático para exportar la clave privada
         * */
		private static void ExportPrivateKey(RsaKeyParameters privateKey)
		{
			//To print the public key in pem format
            TextWriter textWriter1 = new StreamWriter(Constants.CONF_PRIVATE_KEY);
			PemWriter pemWriter1 = new PemWriter(textWriter1);
			pemWriter1.WriteObject(privateKey);
			pemWriter1.Writer.Flush();
			pemWriter1.Writer.Close();
		}

        /*
         * Método público estático para importar la clave pública
         * */
		public static RsaKeyParameters ImportPublicKey(string pem)
		{
			PemReader pr = new PemReader(new StreamReader(pem));
			AsymmetricKeyParameter publicKey = (AsymmetricKeyParameter)pr.ReadObject();
			RsaKeyParameters rsaParams = (RsaKeyParameters)publicKey;
			return rsaParams;
		}

        /*
         * Método público estático para importar la clave privada
         * */
		public static RsaKeyParameters ImportPrivateKey(string pem)
		{
			PemReader pr = new PemReader(new StreamReader(pem));
			AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)pr.ReadObject();
			RsaKeyParameters rsaParams = (RsaPrivateCrtKeyParameters)keyPair.Private;
			return rsaParams;
		}

        /*
         * Método para conversión de una clave pública a String
         * */
        public string PublicKeyString(){
			StringWriter str = new StringWriter();
			PemWriter pemWriter = new PemWriter(str);
            pemWriter.WriteObject(this._publicKey);
            pemWriter.Writer.Close();

            return str.ToString();
        }

        /*
         * Método estático para la encriptaicón
         * */
		public static string Encryption(string text, RsaKeyParameters PublicKey)
		{
            RsaEngine cipher = new RsaEngine();
            cipher.Init(true,PublicKey);

			byte[] ct = Encoding.ASCII.GetBytes(text);

            byte[] cipherText = cipher.ProcessBlock(ct, 0, ct.Length);

            string cifrado = Convert.ToBase64String(cipherText);

            return cifrado;
		}

        /*
         * Método estático para desencriptar
         * */
		public static string Decryption(byte[] ct, RsaKeyParameters Pvtkey)
		{
			RsaEngine cipher = new RsaEngine();
            cipher.Init(false,Pvtkey);

			byte[] cipherText = cipher.ProcessBlock(ct, 0, ct.Length);
            string descifrado = Encoding.ASCII.GetString(cipherText);
			return descifrado;
		}

        /*
         * Método estático para encriptar simétricamente un XMLDocument
         * */
        public static void EncryptSymmetric(XmlDocument Doc, string ElementName, SymmetricAlgorithm Key)
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

        /*
         * Método estático para desencriptar simétricamente un XMLDocument
         * */
		public static void DecryptSymmetric(XmlDocument Doc, SymmetricAlgorithm Alg)
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
	}
}

