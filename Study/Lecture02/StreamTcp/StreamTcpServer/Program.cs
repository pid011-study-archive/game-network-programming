using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace StreamTcpServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string data;
            IPEndPoint ipep = new(IPAddress.Any, 9050);
            Socket newsock = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newsock.Bind(ipep);
            newsock.Listen(10);
            Console.WriteLine("Waiting for a client...");

            Socket client = newsock.Accept();
            IPEndPoint newclient = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine($"Connected with {newclient.Address} at port {newclient.Port}");

            using NetworkStream ns = new(client);
            using StreamReader sr = new(ns);
            using StreamWriter sw = new(ns);

            string welcome = "Welcome to my test server";
            sw.WriteLine(welcome);
            sw.Flush();

            while (true)
            {
                try
                {
                    data = sr.ReadLine();
                }
                catch (IOException)
                {
                    break;
                }

                Console.WriteLine(data);
                sw.WriteLine(data);
                sw.Flush();
            }

            Console.WriteLine($"Disconnected from {newclient.Address}");
        }
    }
}
