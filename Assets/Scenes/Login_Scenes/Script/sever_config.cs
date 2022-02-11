using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sever_config : MonoBehaviour
{
    [SerializeField]
    private static string ip = "127.10.0.0", cong = "3050";
    int recv;
    [SerializeField]
    GameObject tcp_object;
    static Thread thread;
    public static string dirt_array_str;
    public static bool load_dirt_sucess = false;

    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(tcp_object);

    }
    private static void tcp_check_onlinne(object id)
    {
        try
        {
            byte[] data_send = new byte[4069];
            TcpClient client = new TcpClient();
            client.Connect(ip, int.Parse(cong));
            Stream stream = client.GetStream();
            var writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            // 2. send

            int user_id = Int32.Parse(id.ToString());
            JObject jo = new JObject();
            jo.Add("user_id", user_id);
            jo.Add("msg", "get_dirt");
           
            data_send = Encoding.UTF8.GetBytes(jo.ToString());
            stream.Write(data_send, 0, data_send.Length);
           
            while (true)
            {

                byte[] data = new byte[1024];
                int byteRead = stream.Read(data, 0, 1000);
                string Data_str = Encoding.UTF8.GetString(data, 0, byteRead);
                jo = JObject.Parse(Data_str);

                if (jo.GetValue("msg").ToString().Equals("dirt_data"))
                {
                    dirt_array_str = jo.GetValue("data").ToString();
                    Debug.Log("receive dirt data !");
                    load_dirt_sucess = true;
                 
                }


            }
        }
        catch (SystemException ex)
        {
            Debug.LogError("exception:" + ex.Message + " close thread!!!");
            thread.Abort();
        }

    }
  


    private void OnDestroy()
    {
        if (thread != null)
        {
            thread.Abort();
        }
    }

    public static void online_start(int id)
    {
        thread = new Thread(new ParameterizedThreadStart(tcp_check_onlinne));
        thread.Start(id);
    }

    // Update is called once per frame
    void Update()
    {
        if (load_dirt_sucess)
        {
            SceneManager.LoadScene(0);
            load_dirt_sucess = false;
        }
    }
}
