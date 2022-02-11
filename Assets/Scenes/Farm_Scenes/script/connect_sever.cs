using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;


public class connect_sever : MonoBehaviour
{
    public string ip = "127.10.0.0", cong = "3000";
    int recv;
    IPEndPoint ipep;
    Socket server;
    EndPoint Remote;
    byte[] data_lenght = new byte[1024];
    Thread thread;    
    JSONObject json = new JSONObject(JSONObject.Type.ARRAY);
    void Start()
    {

        ipep = new IPEndPoint(IPAddress.Parse(ip), int.Parse(cong));
        server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        Remote = (EndPoint)ipep;
        thread = new Thread(new ThreadStart(server_ctrl));
        thread.Start();


    }
    

    
    void Update()
    {
        
    }
    private void server_ctrl()
    {
        json.AddField("msg", "connect");

        server.SendTo(Encoding.UTF8.GetBytes("hello"), SocketFlags.None, ipep);
        Debug.Log("Data sended");
        while (true)
        {

            data_lenght = new byte[4069];
            recv = server.ReceiveFrom(data_lenght, ref Remote);
           
            string Data = Encoding.ASCII.GetString(data_lenght, 0, recv);
           // JSONObject oj = new JSONObject(Data);

            Debug.Log("Receive from server: " + Data);
  

        }
    }
}
