using System;
using System.Threading;

namespace ThreadLock
{
    internal class Program
    {
        private int number = 0;

        private static void Main(string[] args)
        {
            Program p = new Program();

            Thread t1 = new Thread(ThreadFunc);
            Thread t2 = new Thread(ThreadFunc);

            t1.Start(p);
            t2.Start(p);

            t1.Join();
            t2.Join();

            Console.WriteLine(p.number);
        }

        private static void ThreadFunc(object obj)
        {
            Program p = obj as Program;
            for (int i = 0; i < 100000; i++)
            {
                lock (p)
                {
                    p.number += 1;
                }
            }
        }
    }
}
