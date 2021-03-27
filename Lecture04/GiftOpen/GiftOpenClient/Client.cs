using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace GiftOpenClient
{
    internal class Client
    {
        private static void Main()
        {
            IPEndPoint address = new(IPAddress.Any, 0);
            TcpClient client = new(address);

            try
            {
                client.Connect(IPAddress.Parse("127.0.0.1"), 9050);
            }
            catch (SocketException)
            {
                Console.WriteLine("서버에 연결하지 못했습니다.");
                return;
            }

            NetworkStream stream = client.GetStream();

            try
            {
                byte[] buffer = new byte[512];

                Console.WriteLine("교환을 요청 중입니다...");

                stream.Read(buffer, 0, sizeof(bool));
                bool canGive = BitConverter.ToBoolean(buffer, 0);

                if (!canGive)
                {
                    Console.WriteLine("지금은 사용자가 많아 상자를 열 수 없습니다. 잠시 후 다시 시도해 주세요.");
                    return;
                }

                Console.WriteLine("요청을 처리 중입니다...");

                stream.Read(buffer, 0, sizeof(int));
                int num = BitConverter.ToInt32(buffer, 0);

                Console.WriteLine($"{num}번째로 아이템을 받았습니다!");
            }
            catch (IOException)
            {
                Console.WriteLine("서버와의 연결이 끊겼습니다.");
            }
            finally
            {
                stream.Close();
            }
        }
    }
}
