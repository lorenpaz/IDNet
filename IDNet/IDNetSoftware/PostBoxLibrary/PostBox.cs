using System;
using System.IO;
using System.Text;

using MessageLibraryS;
using ProcessLibraryS;
using ConvertionLibraryS;
using CriptoLibraryS;
using ConstantsLibraryS;
using DatabaseLibraryS;

using System.Xml;
using System.Security.Cryptography;
using System.Collections.Generic;

using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;

namespace PostBoxLibraryS
{
    public class PostBox
    {
        //Mensaje de solitud
        Message _messageRequest;

        //Proceso
        Process _process;

        //Mensaje de respuesta
        Message _messageResponse;

        //Clave pública-privada
        Cripto _keyPair;

        //CLave pública del cliente
        RsaKeyParameters _publicKeyClient;

        //Clave simétrica para el cifrado de los mensajes
        SymmetricAlgorithm _symmetricKey;

		public Message MessageRequest
		{
			get
			{
                return this._messageRequest;
			}
		}

        public Message MessageResponse
        {
            get
            {
                return this._messageResponse;
            }
        }
		public RsaKeyParameters PublicKeyClient
		{
			get
			{
                return this._publicKeyClient;
			}
		}
		public SymmetricAlgorithm SymmetricKey
		{
			get
			{
				return this._symmetricKey;
			}
			set
			{
				this._symmetricKey = value;
			}
		}

        //Constructor para mensajes (no conexion)
		public PostBox(string source, string destination, string tipoMensaje, string db_name,
                       string db_type, XmlNode body,SymmetricAlgorithm symmetricKey)
		{
			this._process = new Process();
			this._messageRequest = new Message(source, destination, tipoMensaje, db_name, db_type, body);
			this._messageResponse = new Message();
            this._symmetricKey = symmetricKey;
		}

        //Constructor para conexión 001a
        public PostBox(string source, string destination, string tipoMensaje, Cripto keyPair)
		{
            this._process = new Process();
			this._messageRequest = new Message(source, destination, tipoMensaje);
			this._messageResponse = new Message();
            this._keyPair = keyPair;
		}
		//Constructor para conexión 001b
        public PostBox(string source, string destination, string tipoMensaje, Cripto keyPair,RsaKeyParameters publicKeyClient,SymmetricAlgorithm key)
		{
            this._process = new Process();
			this._messageRequest = new Message(source, destination, tipoMensaje);
			this._messageResponse = new Message();
			this._keyPair = keyPair;
            this._publicKeyClient = publicKeyClient;
            this._symmetricKey = key;
		}

		/*
        * Realizamos el envio.NO conexion
        */
		public string ProcesarEnvio()
		{
			XmlDocument doc = this._messageRequest.createMessage();

            doc = encriptarParteDelDocumentoSimetrico(doc);
            string respuesta = doc.InnerXml;

			return respuesta;
		}

		/*
        * Realizamos el envio de Conexión
        */
		public string ProcesarEnvioConexion()
		{
            //Si es 001a
            if(this._symmetricKey == null)
            {
				XmlDocument doc = this._messageRequest.createMessageConnection(this._keyPair.PublicKeyString());
				return doc.InnerXml;

            } //Sino 001b
            else{
                XmlDocument doc = this._messageRequest.createMessageConnection(this._keyPair.PublicKeyString(),this._symmetricKey);
                encriptarParteDelDocumentoAsimetrico(doc);
                return doc.InnerXml;
            }
		}

		/**
         * Procesamos la respuesta
         * */
		public void ProcesarRespuesta(string response)
		{
			//Convertimos el string a xml
			XmlDocument xmlDoc = Convertion.stringToXml(response);

            //Desencriptamos el mensaje
            DesencriptarParteDelDocumentoSimetrico(xmlDoc);

			//Parseamos el mensaje
			this._messageResponse.parserMessageRecieve(xmlDoc);
		}

		/**
     * Procesamos la respuesta de conexión
     * */
		public bool ProcesarRespuestaConexion(string response)
		{
			//Convertimos el string a xml
			XmlDocument xmlDoc = Convertion.stringToXml(response);

            //Parseamos el mensaje
            this._messageResponse.parserMessageRecieve(xmlDoc);
            if (this._messageResponse.MessageType == Constants.MENSAJE_RESPUESTA_CONEXION_A)
            {
                AlmacenarClavePublica(xmlDoc);
                return true;
            }
            else
            {
                XmlDocument docDesencriptado = DesencriptarParteDelDocumentoAsimetrico(xmlDoc);

                //Parseamos el mensaje de nuevo
                this._messageResponse.parserMessageRecieve(docDesencriptado);
            }

            return true;
		}

        /*
         * Método privado para almacenar la clave pública en un fichero local
         * */
        private void AlmacenarClavePublica(XmlDocument xmlDoc)
        {
            string path = Constants.PathClavePublica(this._messageResponse.Source);
			string publicKey = xmlDoc.DocumentElement.GetElementsByTagName("key")[0].InnerText;
            if (File.Exists(path))
                File.Delete(path);
            File.WriteAllText(path, publicKey);
			this._publicKeyClient = Cripto.ImportPublicKey(path);
		}

        /*
         * Método privado para comprobar la clave simétrica
         * */
       /* private bool ComprobarClaveSimetrica(XmlDocument xmlDoc)
        {
            XmlDocument docDesencriptado = DesencriptarParteDelDocumentoAsimetrico(xmlDoc);
            string keyRecibida, keyMia,ivRecibida,ivMia;
            keyRecibida = docDesencriptado.DocumentElement.GetElementsByTagName("key")[0].InnerText;
            keyMia = Convert.ToBase64String(this._symmetricKey.Key);
            ivRecibida = docDesencriptado.DocumentElement.GetElementsByTagName("IV")[0].InnerText;
            ivMia = Convert.ToBase64String(this._symmetricKey.IV);

            if(keyRecibida == keyMia && ivRecibida == ivMia)
            {
                return true;
            }else{
                return false;
            }
        }*/

        /*
         * Método privado que encripta parte del documento con encriptación asimétrica
         * */
        private XmlDocument encriptarParteDelDocumentoAsimetrico(XmlDocument doc)
        {
            string xmlAEncriptar = doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml;

            string xmlEncriptado = Cripto.Encryption(xmlAEncriptar,this._publicKeyClient);
            Console.WriteLine("Key:"+Encoding.ASCII.GetString(this._symmetricKey.Key));
            Console.Write("IV:"+this._symmetricKey.IV);

            doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml = xmlEncriptado;

            return doc;
        }

        /*
         * Método privado que desencripta parte del documento con encriptación asimétrica
         * */
		private XmlDocument DesencriptarParteDelDocumentoAsimetrico(XmlDocument doc)
		{
			string xmlADesencriptar = doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml;

            string xmlDesencriptado = Cripto.Decryption(Convert.FromBase64String(xmlADesencriptar), this._keyPair.PrivateKey);

			doc.DocumentElement.GetElementsByTagName("encripted")[0].InnerXml = xmlDesencriptado;

			return doc;
		}

        /*
         * Método privado que desencripta parte del documento con encriptación simétrica
         * */
		private XmlDocument DesencriptarParteDelDocumentoSimetrico(XmlDocument doc)
		{
			Cripto.DecryptSymmetric(doc, this._symmetricKey);
			return doc;
		}

        /*
         * Método privado que encripta parte del documento con encriptación simétrica
         * */
		private XmlDocument encriptarParteDelDocumentoSimetrico(XmlDocument doc)
		{
			Cripto.EncryptSymmetric(doc, "encripted", this._symmetricKey);
			return doc;
		}

	}
}