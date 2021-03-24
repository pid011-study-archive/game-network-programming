using System;
using System.Threading;

namespace ThreadSync
{
    internal class Program
    {
        private int number = 0;

        private static void Main()
        {
            Program p = new Program();

            Thread t1 = new Thread(ThreadFunc);
            Thread t2 = new Thread(ThreadFunc);

            t1.Start(p);
            t2.Start(p);

            t1.Join();
            t2.Join();

            Console.WriteLine(p.number);
            // 값이 실행할 때마다 바뀜
        }

        private static void ThreadFunc(object obj)
        {
            Program p = obj as Program;

            for (int i = 0; i < 10000; i++)
            {
                p.number = p.number + 1;
            }
        }
    }
}
