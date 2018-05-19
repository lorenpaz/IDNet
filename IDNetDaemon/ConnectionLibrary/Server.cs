using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using log4net;
using System.Collections.Generic;

using PostBoxLibrary;
using CriptoLibrary;
using MessageLibrary;
using ConstantsLibrary;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;

namespace ConnectionLibrary
{

	// State object for reading client data asynchronously
	public class StateObject
	{
		// Client  socket.
		public Socket workSocket = null;
		// Size of receive buffer.
		public const int BufferSize = 4096;
		// Receive buffer.
		public byte[] buffer = new byte[BufferSize];
		// Received data string.
		public StringBuilder sb = new StringBuilder();
	}

	public class Server
	{
		// Thread signal.
		public ManualResetEvent allDone = new ManualResetEvent(false);
		static readonly ILog log = LogManager.GetLogger(typeof(Server));

         Cripto _keyPair;
        Dictionary<string, Tuple<RsaKeyParameters,SymmetricAlgorithm>> _keyPairClients;

		public Server()
		{
			this._keyPair = new Cripto();
            this._keyPairClients = new Dictionary<string, Tuple<RsaKeyParameters, SymmetricAlgorithm>>();
		}

		public void StartListening()
		{
			// Data buffer for incoming data.
            byte[] bytes = new Byte[4096];

			// Establish the local endpoint for the socket.
			// The DNS name of the computer
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ipAddress = ipHostInfo.AddressList[0];
           // IPAddress ipLocal
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Constants.GATEKEEPER_PORT);

			// Create a TCP/IP socket.
			Socket listener = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

			// Bind the socket to the local endpoint and listen for incoming connections.
			try
			{
				listener.Bind(localEndPoint);
				listener.Listen(100);

				while (true)
				{
					// Set the event to nonsignaled state.
					allDone.Reset();

					// Start an asynchronous socket to listen for connections.
					log.Info("Waiting for a connection...");
					listener.BeginAccept(
						new AsyncCallback(AcceptCallback),
						listener);

					// Wait until a connection is made before continuing.
				    	allDone.WaitOne();
				}
			}
			catch (Exception e) 
			{
				Console.WriteLine(e.ToString());
			}
		}

		public void AcceptCallback(IAsyncResult ar)
		{
			// Signal the main thread to continue.
			allDone.Set();

			// Get the socket that handles the client request.
			Socket listener = (Socket)ar.AsyncState;
			Socket handler = listener.EndAccept(ar);

			// Create the state object.
			StateObject state = new StateObject();
			state.workSocket = handler;
			handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
				new AsyncCallback(ReadCallback), state);
		}

		public void ReadCallback(IAsyncResult ar)
		{
			String content = String.Empty;

			// Retrieve the state object and the handler socket
			// from the asynchronous state object.
			StateObject state = (StateObject)ar.AsyncState;
			Socket handler = state.workSocket;

			// Read data from the client socket. 
			int bytesRead = handler.EndReceive(ar);

			if (bytesRead > 0)
			{
				// There  might be more data, so store the data received so far.
				state.sb.Append(Encoding.ASCII.GetString(
					state.buffer, 0, bytesRead));

				// Check for end-of-file tag. If it is not there, read 
				// more data.
				content = state.sb.ToString();
				if (content.IndexOf("</root>") > -1)
				{
					// client. Display it on the console.
					log.Info("Read " + content.Length + " bytes from socket. \n");
				}
				else
				{
					// Not all data received. Get more.
					handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
					new AsyncCallback(ReadCallback), state);
				}

                PostBox post = new PostBox(_keyPair);

                string respuesta = post.procesar(content,_keyPairClients);

                if(!this._keyPairClients.ContainsKey(post.MessageRecieve.Source))
                {

                    Tuple<RsaKeyParameters, SymmetricAlgorithm> tupla = new Tuple<RsaKeyParameters, SymmetricAlgorithm>(post.PublicKeyClient, post.SymmetricKey);
                    _keyPairClients.Add(post.MessageRecieve.Source,tupla);
				}else if(this._keyPairClients[post.MessageRecieve.Source].Item2 == null)
                {
					Tuple<RsaKeyParameters, SymmetricAlgorithm> tupla = new Tuple<RsaKeyParameters, SymmetricAlgorithm>(post.PublicKeyClient, post.SymmetricKey);
					_keyPairClients[post.MessageRecieve.Source] = tupla;
				}

                Send(handler,respuesta);
    			}
		}

		private void Send(Socket handler, String data)
		{
			// Convert the string data to byte data using ASCII encoding.
			byte[] byteData = Encoding.ASCII.GetBytes(data);

			// Begin sending the data to the remote device.
			handler.BeginSend(byteData, 0, byteData.Length, 0,
				new AsyncCallback(SendCallback), handler);
		}

		private void SendCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.
				Socket handler = (Socket)ar.AsyncState;

				// Complete sending the data to the remote device.
				int bytesSent = handler.EndSend(ar);
				log.Info("Sent " + bytesSent + " bytes to client.");

				handler.Shutdown(SocketShutdown.Both);
				handler.Close();

			}   
			catch (Exception)
			{
			}
		}
	}
}