using System;
using System.Threading;

namespace ThreadArgs
{
    internal class ThreadParam
    {
        public int Value1;
        public int Value2;
    }

    internal class Program
    {
        private static void Main()
        {
            Thread t = new Thread(ThreadFunc);

            ThreadParam param = new ThreadParam();
            param.Value1 = 10;
            param.Value2 = 20;

            t.Start(param);
        }

        private static void ThreadFunc(object obj)
        {
            ThreadParam args = (ThreadParam)obj;
            Console.WriteLine($"{args.Value1}, {args.Value2}");
        }
    }
}
