using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GiftOpenServer
{
    internal class Server
    {
        private const int Port = 9050;
        private static TcpListener _server;

        private static void Main()
        {
            _server = new TcpListener(IPAddress.Any, Port);
            _server.Start();
            Console.WriteLine("서버가 시작되었습니다. 클라이언트를 기다리는 중...");

            ClientHandler handler = new(3);

            while (true)
            {
                TcpClient client = _server.AcceptTcpClient();

                Thread clientThread = new(handler.HandleConnection);
                clientThread.Start(client);
            }
        }
    }

    public class ClientHandler
    {
        private readonly Semaphore _giftPool;
        private int _count = 0;

        public ClientHandler(int max)
        {
            // 처음 할당가능한 수와 최대로 가능한 수를 지정하고 Semaphore 초기화
            _giftPool = new Semaphore(max, max);
        }

        public void HandleConnection(object obj)
        {
            if (obj is not TcpClient client)
            {
                Console.WriteLine("Handle parameter is wrong!");
                return;
            }

            IPEndPoint clientIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            string clientAddress = $"[{clientIpEndPoint.Address}:{clientIpEndPoint.Port}]";

            Console.WriteLine($"{clientAddress}와 연결되었습니다.");

            // Close를 직접 사용하는 대신 using을 써서 메소드가 끝날 때 자동으로 호출되도록 함
            NetworkStream stream = client.GetStream();

            try
            {
                // Semaphore 내부 카운트가 내려감
                // 만약 가능한 카운트가 없어서 대기를 하다가 타임아웃되면 false 리턴
                bool canGive = _giftPool.WaitOne(500);

                // BitConverter: 숫자 자료형을 byte array로 변경해주거나 그 반대의 기능을 제공
                stream.Write(BitConverter.GetBytes(canGive), 0, sizeof(bool));

                if (!canGive) return;

                _count++;
                int userNumber = _count;

                // 데이터베이스 처리등의 시간지연을 대신해서 Sleep을 사용함
                Thread.Sleep(1000 * 10);

                stream.Write(BitConverter.GetBytes(userNumber), 0, sizeof(int));
                Console.WriteLine($"{clientAddress}가 {userNumber}번째로 아이템을 받았습니다.");

                // 할 일을 다하고 다른 스레드가 작업할 수 있도록 Release를 해서 Semaphore 카운트를 다시 올림
                _giftPool.Release();
            }
            catch (IOException)
            {
                // Read나 Write 실패 시 처리할 코드
            }
            finally
            {
                // 모든 작업을 수행하고 NetworkStream을 닫은 뒤 콘솔에 로그를 남김
                // NetworkStream을 닫으면 TcpClient도 함께 닫힘
                stream.Close();
                Console.WriteLine($"{clientAddress}와의 연결이 끊겼습니다.");
            }
        }
    }
}
