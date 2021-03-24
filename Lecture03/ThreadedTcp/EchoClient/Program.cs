using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoClient
{
    internal class Program
    {
        private static async Task Main()
        {
            IPEndPoint clientAddress = new(IPAddress.Parse("127.0.0.1"), 0);
            TcpClient client = new(clientAddress);

            try
            {
                await client.ConnectAsync(IPAddress.Parse("127.0.0.1"), 9050);
            }
            catch (SocketException)
            {
                Console.WriteLine("서버에 연결하지 못했습니다.");
                return;
            }

            using NetworkStream networkStream = client.GetStream();
            using StreamReader reader = new(networkStream, Encoding.UTF8);
            using StreamWriter writer = new(networkStream, Encoding.UTF8);

            string message;

            try
            {
                message = await reader.ReadLineAsync();
                Console.WriteLine(message);

                while (true)
                {
                    Console.Write("Input: ");
                    var input = Console.ReadLine();

                    if (input is "exit") break;

                    await writer.WriteLineAsync(input);
                    await writer.FlushAsync();

                    message = await reader.ReadLineAsync();
                    Console.WriteLine($"Output: {message}");
                }
            }
            catch (SocketException)
            {
                Console.WriteLine("서버와의 연결이 끊겼습니다.");
            }
        }
    }
}
