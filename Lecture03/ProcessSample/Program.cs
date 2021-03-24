using System;
using System.Diagnostics;

namespace ProcessSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Process thisProc = Process.GetCurrentProcess();

            string procName = thisProc.ProcessName;
            DateTime started = thisProc.StartTime;
            int procID = thisProc.Id;
            long memory = thisProc.VirtualMemorySize64;
            long priMemory = thisProc.PrivateMemorySize64;
            long physMemory = thisProc.WorkingSet64;
            int priority = thisProc.BasePriority;
            ProcessPriorityClass priClass = thisProc.PriorityClass;
            TimeSpan cpuTime = thisProc.TotalProcessorTime;

            Console.WriteLine("Process:{0},ID:{1}", procName, procID);
            Console.WriteLine("started:{0}", started.ToString());
            Console.WriteLine("CPUtime:{0}", cpuTime.ToString());
            Console.WriteLine("priorityclass:{0}priority:{1}", priClass, priority);
            Console.WriteLine("virtualmemory:{0}", memory);
            Console.WriteLine("privatememory:{0}", priMemory);
            Console.WriteLine("physicalmemory:{0}", physMemory);
            Console.WriteLine("'ntryingtochangepriority...");
            thisProc.PriorityClass = ProcessPriorityClass.High;
            priClass = thisProc.PriorityClass;
            Console.WriteLine("newpriorityclass:{0}", priClass);
        }
    }
}
