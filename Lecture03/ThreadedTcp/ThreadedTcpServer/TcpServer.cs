using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ThreadedTcpServer
{
    internal class TcpServer
    {
        private TcpListener client;

        public TcpServer()
        {
            client = new TcpListener(IPAddress.Any, 9050);
            client.Start();

            Console.WriteLine("Waiting for clients...");
            while (true)
            {
                while (!client.Pending())
                {
                    Thread.Sleep(1000);
                }

                ConnectionThread newconnection = new ConnectionThread();
                newconnection.ThreadListener = this.client;
                Thread newthread = new Thread(new ThreadStart(newconnection.HandleConnection));
                newthread.Start();
            }
        }

        private static void Main(string[] args)
        {
            TcpServer server = new TcpServer();
        }
    }

    internal class ConnectionThread
    {
        public TcpListener ThreadListener;
        private static int _connections = 0;

        public void HandleConnection()
        {
            int recv;
            byte[] data = new byte[1024];

            TcpClient client = ThreadListener.AcceptTcpClient();
            NetworkStream ns = client.GetStream();
            _connections++;
            Console.WriteLine($"New client accepted: {_connections} active connections");

            string welcome = "Welcome to my test server";
            data = Encoding.UTF8.GetBytes(welcome);
            ns.Write(data, 0, data.Length);

            while (true)
            {
                data = new byte[1024];
                recv = ns.Read(data, 0, data.Length);
                if (recv == 0)
                {
                    break;
                }

                ns.Write(data, 0, recv);
            }

            ns.Close();
            client.Close();
            _connections--;
            Console.WriteLine($"Client disconected: {_connections} active connections");
        }
    }
}
