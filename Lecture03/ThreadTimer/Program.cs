using System;
using System.Threading;
using System.Timers;

using Timer = System.Timers.Timer;

// https://www.csharpstudy.com/Threads/timer.aspx
namespace ThreadTimer
{
    internal class Program
    {
        private static object _key = new object();

        private static void Main()
        {
            /*
            System.Timers.Timer: 특정 간격으로 이벤트를 멀티스레딩으로 발생시킴

            System.Threading.Timer와 System.Timers.Timer가 있는데 후자는 전자를 Wrapping한 클래스다.

            Elapsed로 이벤트를 등록하면 설정한 Interval 간격으로 이벤트를 발생시킨다.

            각 이벤트마다 쓰레드를 작업스레드를 이용해서 이벤트를 실행하기 때문에
            만약 이벤트가 다음 이벤트 발생 전까지 끝나지 않아도 지정된 시간 후에 다른 작업스레드에서
            이벤트를 실행하게 된다.
            
            따라서 Lock등을 이용해서 Thread-safe하게 구현해야 한다.
            */

            Timer timer = new() { Interval = 100 };
            timer.Elapsed += TimerElapsed;
            timer.Start();

            Console.WriteLine("아무 키나 눌러서 종료할 수 있습니다...");
            Console.ReadLine();
        }

        private static void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            /*
            Timer는 100ms마다 이벤트를 실행하는데 lock을 걸어서 1000ms동안 Sleep시키고 실행하므로
            화면 상에서는 1초마다 순차적으로 시간이 출력된다.
            */
            lock (_key)
            {
                Thread.Sleep(1000);
                Console.WriteLine(DateTime.Now.ToString());
            }
        }
    }
}
