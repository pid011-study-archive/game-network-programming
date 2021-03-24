using System;
using System.Threading;

namespace ThreadSample
{
    internal class Program
    {
        private static void Main()
        {
            _ = new Program();
        }

        public Program()
        {
            Thread newCounter = new(new ThreadStart(Counter));
            Thread newCounter2 = new(new ThreadStart(Counter2));

            newCounter.Start();
            newCounter2.Start();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"main: {i}");
                Thread.Sleep(1000);
            }
        }

        private void Counter()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"thread: {i}");
                Thread.Sleep(2000);
            }
        }

        private void Counter2()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"thread2: {i}");
                Thread.Sleep(3000);
            }
        }
    }
}
