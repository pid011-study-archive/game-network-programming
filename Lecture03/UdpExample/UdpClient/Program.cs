using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpClient
{
    internal class Program
    {
        private static void Main()
        {
            Socket clientSocket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            byte[] buffer = Encoding.UTF8.GetBytes(DateTime.Now.ToString());

            EndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 10200);

            clientSocket.SendTo(buffer, serverEP);

            byte[] recvBytes = new byte[1024];
            int nrecv = clientSocket.ReceiveFrom(recvBytes, ref serverEP);
            string txt = Encoding.UTF8.GetString(recvBytes, 0, nrecv);

            Console.WriteLine(txt);
        }
    }
}
