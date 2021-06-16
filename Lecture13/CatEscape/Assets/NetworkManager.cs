using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private readonly ConcurrentQueue<string> _receivedPackets = new ConcurrentQueue<string>();
    private readonly ConcurrentQueue<string> _packetsWaitList = new ConcurrentQueue<string>();
    private UdpClient _client;
    private IPEndPoint _serverEndPoint;

    private void Start()
    {
        _client = new UdpClient();
        _serverEndPoint = new IPEndPoint(IPAddress.Loopback, 10200);
        _client.Connect(_serverEndPoint);

        var listeningThread = new Thread(ListeningPackets)
        {
            IsBackground = true
        };
        listeningThread.Start();

        var sendingThread = new Thread(SendingPackets)
        {
            IsBackground = true
        };
        sendingThread.Start();
    }

    private void ListeningPackets()
    {
        while (true)
        {
            if (_client is null)
            {
                continue;
            }

            try
            {
                var clientEp = new IPEndPoint(IPAddress.Any, 0);
                var buffer = _client.Receive(ref clientEp);
                var txt = Encoding.UTF8.GetString(buffer);
                _receivedPackets.Enqueue(txt);
            }
            catch (SocketException e)
            {
                Debug.LogError($"eror code: {e.SocketErrorCode}");
            }
        }
    }

    private void SendingPackets()
    {
        while (true)
        {
            if (_client is null)
            {
                continue;
            }

            if (!_packetsWaitList.TryDequeue(out var next))
            {
                continue;
            }
            var buffer = Encoding.UTF8.GetBytes(next);
            _client.Send(buffer, buffer.Length);
        }
    }

    public void SendData(int direction)
    {
        _packetsWaitList.Enqueue(direction.ToString());
    }

    public bool TryGetNextData(out string data)
    {
        return _receivedPackets.TryDequeue(out data);
    }
}
