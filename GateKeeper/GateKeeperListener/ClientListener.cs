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
		private static readonly ILog log = LogManager.GetLogger(typeof(ClientListener));
		private ManualResetEvent allDone = new ManualResetEvent(false);

		public ClientListener():base(){}

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
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
    }
}
