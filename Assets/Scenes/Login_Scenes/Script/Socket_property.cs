using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class Socket_property
{
    private static string ip = "127.10.0.0", cong = "3000";
    private static IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ip), int.Parse(cong));
    private static Socket server= new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    private static EndPoint remote = (EndPoint)ipep;



    Socket_property()
    {

    }

    public static string Ip { get => ip; set => ip = value; }
    public static string Cong { get => cong; set => cong = value; }
    public static IPEndPoint Ipep { get => ipep; set => ipep = value; }
    public static Socket Server { get => server; set => server = value; }
    public static EndPoint Remote { get => remote; set => remote = value; }
}
