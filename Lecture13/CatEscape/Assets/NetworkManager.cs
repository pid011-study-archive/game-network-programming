using System.Net;
using System.Net.Sockets;
using System.Text;

using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private Socket _clientSocket;
    private EndPoint _serverEndPoint;
    private PlayerController _playerController;

    private void Start()
    {
        _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        _serverEndPoint = new IPEndPoint(IPAddress.Loopback, 10200);
        _playerController = GameObject.Find("player").GetComponent<PlayerController>();
    }

    public void SendData(int direction)
    {
        var buffer = new byte[16];
        var recvBytes = new byte[16];

        if (direction == -1)
            buffer = Encoding.UTF8.GetBytes("-1");
        else if (direction == 1)
            buffer = Encoding.UTF8.GetBytes("1");
        else
            buffer = Encoding.UTF8.GetBytes("0");

        _clientSocket.SendTo(buffer, _serverEndPoint);

        var nrecv = _clientSocket.ReceiveFrom(recvBytes, ref _serverEndPoint);
        var txt = Encoding.UTF8.GetString(recvBytes, 0, nrecv);

        Debug.Log(txt);
        _playerController.TranslateCat(txt);
    }
}
