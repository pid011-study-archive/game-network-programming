using System;
using System.Threading;

namespace ThreadRunSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var t = new Thread(ThreadFunc);

            t.Start();
        }

        private static void ThreadFunc()
        {
            Console.WriteLine("60초 후에 프로그램 종료");
            Thread.Sleep(1000 * 60);
            Console.WriteLine("스레드 종료!");
        }
    }
}
