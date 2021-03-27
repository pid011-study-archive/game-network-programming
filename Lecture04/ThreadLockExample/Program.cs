using System;
using System.Threading.Tasks;

// lock
// https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/lock-statement
namespace ThreadLockExample
{
    internal class Program
    {
        private static async Task Main()
        {
            var account = new Account(1000);

            var tasks = new Task[100];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => Update(account));
            }

            await Task.WhenAll(tasks);
            Console.WriteLine($"계좌 잔고는 {account.Balance}원 입니다.");
            // result: 2000
        }

        private static void Update(Account account)
        {
            // 음수: 출금액, 양수: 입금액
            decimal[] amounts = { 0, 2, -3, 6, -2, -1, 8, -5, 11, -6 };
            foreach (var amount in amounts)
            {
                if (amount >= 0)
                {
                    account.Credit(amount);
                }
                else
                {
                    account.Debit(Math.Abs(amount));
                }
            }
        }
    }

    public class Account
    {
        // 교착 상태 등 문제가 발생할 수 있으므로 lock 전용 객체를 만들어서 사용해야 함
        // _balance같은 객체에 사용하면 안됨
        private readonly object _balanceLock = new();

        private decimal _balance;

        public decimal Balance
        {
            get
            {
                lock (_balanceLock)
                {
                    return _balance;
                }
            }
        }

        public Account(decimal initialBalance) => _balance = initialBalance;

        public decimal Debit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "출금액은 음수가 될 수 없습니다.");
            }

            decimal appliedAmount = 0;
            lock (_balanceLock)
            {
                if (_balance >= amount)
                {
                    _balance -= amount;
                    appliedAmount = amount;
                }
            }

            return appliedAmount;
        }

        public void Credit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "입금액은 음수가 될 수 없습니다.");
            }

            lock (_balanceLock)
            {
                _balance += amount;
            }
        }
    }
}
