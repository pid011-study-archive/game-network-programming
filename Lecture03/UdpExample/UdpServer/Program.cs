using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UdpServer
{
    internal class Program
    {
        private static void Main()
        {
            var serverThread = new Thread(ServerFunc)
            {
                IsBackground = true
            };

            serverThread.Start();

            Thread.Sleep(500);

            Console.WriteLine("종료하려면 아무 키나 누르세요...");
            Console.ReadLine();
        }

        private static void ServerFunc(object obj)
        {
            Socket serverSocket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint endPoint = new(IPAddress.Any, 10200);

            serverSocket.Bind(endPoint);

            byte[] recvBytes = new byte[1024];
            EndPoint clientEP = new IPEndPoint(IPAddress.None, 0);

            while (true)
            {
                int nrecv = serverSocket.ReceiveFrom(recvBytes, ref clientEP);
                string txt = Encoding.UTF8.GetString(recvBytes, 0, nrecv);

                byte[] sendBytes = Encoding.UTF8.GetBytes($"Hello: {txt}");
                serverSocket.SendTo(sendBytes, clientEP);
            }
        }
    }
}
