using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoServer
{
    internal class Program
    {
        private static async Task Main()
        {
            IPEndPoint localAddress = new(IPAddress.Parse("127.0.0.1"), 9050);
            TcpListener server = new(localAddress);

            server.Start();

            var client = await server.AcceptTcpClientAsync();
            var clientIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;

            Console.WriteLine($"Connected from {clientIpEndPoint.Address}:{clientIpEndPoint.Port}");

            using NetworkStream networkStream = client.GetStream();
            using StreamReader reader = new(networkStream, Encoding.UTF8);
            using StreamWriter writer = new(networkStream, Encoding.UTF8);

            string message;

            try
            {
                await writer.WriteLineAsync("Echo서버에 접속하셨습니다.");
                await writer.FlushAsync();

                while (true)
                {
                    message = await reader.ReadLineAsync();
                    Console.WriteLine($"{clientIpEndPoint.Address}:{clientIpEndPoint.Port}: {message}");

                    await writer.WriteLineAsync(message);
                    await writer.FlushAsync();
                }
            }
            catch (IOException)
            {
                Console.WriteLine("클라이언트 연결이 끊김.");
            }

            Console.WriteLine($"Disconnected from {clientIpEndPoint.Address}:{clientIpEndPoint.Port}");
            server.Stop();
        }
    }
}
