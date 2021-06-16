using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CatEscape.Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // 서버 소켓이 동작하는 스레드
            var serverThread = new Thread(serverFunc);
            serverThread.IsBackground = true;
            serverThread.Start();
            Thread.Sleep(500); // 소켓 서버용 스레드가 실행될 시간을 주기 위해

            Console.WriteLine("종료하려면 아무 키나 누르세요...");
            Console.ReadLine();
        }

        private static void serverFunc(object obj)
        {
            var server = new UdpClient(10200);

            var clientEP = new IPEndPoint(IPAddress.None, 0);

            while (true)
            {
                var buffer = server.Receive(ref clientEP);
                var txt = Encoding.UTF8.GetString(buffer);

                Console.WriteLine("수신 데이터: " + txt);

                var sendBytes = Encoding.UTF8.GetBytes(txt);
                server.Send(sendBytes, sendBytes.Length, clientEP);
            }
        }
    }
}
