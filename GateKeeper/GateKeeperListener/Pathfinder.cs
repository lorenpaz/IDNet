﻿using System;
using System.Net.Sockets;
using System.Net;

namespace GateKeeperListener
{
    public class Pathfinder
    {
        public Pathfinder()
        {
        }

        private void PathDiscovery()
        {
            
        }

        private void BindSocket(IPAddress ip, int port, string msg, string hostname)
        {
			// Establish the remote endpoint for the socket.
			// This example uses port 11000 on the local computer.
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

			IPEndPoint remoteEP = new IPEndPoint(ip, 11000);

			// Create a TCP/IP  socket.
			Socket sender = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);

            Sender s = new Sender(sender);

            s.Send(msg, hostname, remoteEP);
        }
    }
}