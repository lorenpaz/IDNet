using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using log4net;
using System.Collections.Generic;

namespace GateKeeperListener
{

    public class ClientListener
    {
		public ManualResetEvent allDone = new ManualResetEvent(false);
        static readonly ILog log = LogManager.GetLogger(typeof(ClientListener));
		public Queue<string> _msgQueue;

		public ClientListener()
        {
            _msgQueue = new Queue<string>();
        }

        public void StartListening()
		{
            // Data buffer for incoming data.
			byte[] bytes = new Byte[1024];
            Console.WriteLine("Het¡y");

			// Establish the local endpoint for the socket.
			// The DNS name of the computer
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress ipAddress = ipHostInfo.AddressList[0];
			// IPAddress ipLocal
			IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

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
                    // All the data has been read from the 
                    // client. Display it on the console.
                    log.Info("Read " + content.Length + " bytes from socket. \n Data :" + content);

                    // Echo the data back to the client.
                    this._msgQueue.Enqueue(content);

                    if (this._msgQueue.Count == 1)
                        TratarMensaje();
                    /*
                    Queue<string> init, processed;
					do
					{
                        init = new Queue<string>(_msgQueue);
                        processed = new Queue<string>(_msgQueue);
                        processed.Enqueue(content);

						// Compares q to init. If they are not equal, then another
						// thread has updated the running queue since this loop
						// started. CompareExchange does not update q.
						// CompareExchange returns the contents of q, which do not
						// equal init, so the loop executes again.
                    } while (init == Interlocked.CompareExchange(ref _msgQueue, processed, init));

                    if (init.Count == 0 && processed.Count == 1)
                    {
                        _msgQueue = processed;  
                        p.PathDiscovery(ref _msgQueue);
                    }
                    */

				}
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private void TratarMensaje()
        {
            Pathfinder p = new Pathfinder(true);
            while (_msgQueue.Count > 0)
                p.ProcessMsg(_msgQueue.Dequeue());
        }
    }
}
