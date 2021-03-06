﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using log4net;


namespace GateKeeperListener
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

    public class Listener
    {
        private ManualResetEvent allDone = new ManualResetEvent(false);
		ILog log = LogManager.GetLogger(typeof(Listener));

		private  Queue<string> _msgQueue;
        private int _port;

		public Listener(bool cliente)
		{
			_msgQueue = new Queue<string>();
            if (cliente)
				this._port = Constants.PORT_LISTENING_FROM_CLIENT;
            else
				this._port = Constants.PORT_LISTENING_FROM_GATEKEEPER;
		}

#pragma warning disable RECS0135 // Function does not reach its end or a 'return' statement by any of possible execution paths
        public void StartListening()
#pragma warning restore RECS0135 // Function does not reach its end or a 'return' statement by any of possible execution paths
        {
			// Data buffer for incoming data.
			byte[] bytes = new Byte[4096];

			// Establish the local endpoint for the socket.
			// The DNS name of the computer
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ipAddress = ipHostInfo.AddressList[0];
			// IPAddress ipLocal
			IPEndPoint localEndPoint = new IPEndPoint(ipAddress, _port);

			// Create a TCP/IP socket.
			Socket listener = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

			// Bind the socket to the local endpoint and listen for incoming connections.
			try
			{
				listener.Bind(localEndPoint);
				listener.Listen(this._port);

				while (true)
				{
					// Set the event to nonsignaled state.
					allDone.Reset();
					// Start an asynchronous socket to listen for connections.

                    if (this._port == Constants.PORT_LISTENING_FROM_CLIENT)
						log.Info("Esperando a un cliente para conectar...");
                    else
                        log.Info("Esperando a un GateKeeper para conectar...");
                    
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
            String respuesta;

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
                respuesta = content;
                if (content.IndexOf("</root>", StringComparison.Ordinal) > -1)
                {
                    // All the data has been read from the 
                    // client. Display it on the console.
                    log.Info("Read " + content.Length + " bytes from socket. \n Data :" + content);

                    // Echo the data back to the client.
                    this._msgQueue.Enqueue(content);

                    if (this._msgQueue.Count == 1)
                        respuesta = TratarMensaje(respuesta);
                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }

                if(respuesta != "")
                    Sender.Send(handler, respuesta);
            }
        }

		private String TratarMensaje(String respuesta)
		{
            Pathfinder p;
			if(this._port == Constants.PORT_LISTENING_FROM_CLIENT)
			    p = new Pathfinder(true);
            else
				p = new Pathfinder(false);

			while (_msgQueue.Count > 0)
				respuesta = p.ProcessMsg(_msgQueue.Dequeue(), respuesta);

            return respuesta;
		}
    }
}
