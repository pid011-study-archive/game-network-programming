using System;
using System.Net;
using System.Net.Sockets;

IPAddress ia = IPAddress.Parse("127.0.0.1");
IPEndPoint ie = new IPEndPoint(ia, 8000);

Socket test = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Handle

Console.WriteLine($"AddressFamily: {test.AddressFamily}");
Console.WriteLine($"SocketType: {test.SocketType}");
Console.WriteLine($"ProtocolType: {test.ProtocolType}");

Console.WriteLine($"Blocking: {test.Blocking}");

test.Blocking = false;
Console.WriteLine($"new Blocking: {test.Blocking}");
Console.WriteLine($"Connected: {test.Connected}");

test.Bind(ie);
IPEndPoint iep = (IPEndPoint)test.LocalEndPoint;
Console.WriteLine($"Local EndPoint: {iep}");

test.Close();