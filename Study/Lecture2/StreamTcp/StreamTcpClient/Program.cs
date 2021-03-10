using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace StreamTcpClient
{
    internal class Program
    {
        private static void Main()
        {
            string data;
            string input;
            IPEndPoint ipep = new(IPAddress.Parse("127.0.0.1"), 9050);

            Socket server = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ipep);
            }
            catch (SocketException e)
            {
                Console.WriteLine("Unable to connect to server.");
                Console.WriteLine(e.ToString());
                return;
            }

            using NetworkStream ns = new(server);
            using StreamReader sr = new(ns);
            using StreamWriter sw = new(ns);

            data = sr.ReadLine();
            Console.WriteLine(data);

            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit") break;
                sw.WriteLine(input);
                sw.Flush();
                data = sr.ReadLine();
                Console.WriteLine(data);
            }
            Console.WriteLine("Disconnecting from server...");

            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}
