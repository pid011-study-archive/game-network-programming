using System;
using System.Net;

internal class AddressSample
{
    private static void Main()
    {
        IPAddress test1 = IPAddress.Parse("192.168.35.19");
        IPAddress test2 = IPAddress.Loopback; // 127.0.0.1
        IPAddress test3 = IPAddress.Broadcast; // 255.255.255.255
        IPAddress test4 = IPAddress.Any; // 0.0.0.0
        IPAddress test5 = IPAddress.None; // 255.255.255.255
        IPHostEntry ihe = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress myself = ihe.AddressList[0];

        if (IPAddress.IsLoopback(test2))
        {
            Console.WriteLine($"The Loopback address is {test2}");
        }
        else
        {
            Console.WriteLine("Error obtaining the loopback address");
        }

        Console.WriteLine("The Local IP Address is {0}\n", myself);

        if (myself == test2)
        {
            Console.WriteLine("The loopback address is the same as local address.\n");
        }
        else
        {
            Console.WriteLine("The loopback address is not the local address.\n");
        }

        Console.WriteLine($"The test address is {test1}");
        Console.WriteLine($"Broadcast address is {test3}");
        Console.WriteLine($"The ANY address is {test4}");
        Console.WriteLine($"The NONE address is {test5}");
    }
}