using System;
using System.Threading;

namespace ThreadJoin
{
    internal class Program
    {
        private static void Main()
        {
            Thread t = new Thread(ThreadFunc);
            t.IsBackground = true;
            t.Start();

            t.Join(); // t 스레드 종료될 때까지 대기
            Console.WriteLine("주 스레드 종료");
        }

        private static void ThreadFunc()
        {
            Console.WriteLine("10초 후 프로그램 종료");
            Thread.Sleep(1000 * 10);
            Console.WriteLine("스레드 종료");
        }
    }
}
