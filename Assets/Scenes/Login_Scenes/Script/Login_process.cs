using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using TMPro;
using System.Text;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine.SceneManagement;

public class Login_process : MonoBehaviour
{
    [SerializeField]
    private string ip, cong;
    int recv;
    IPEndPoint ipep;
    Socket server;
    EndPoint Remote;
    byte[] data_lenght = new byte[1024];
    Thread thread;
    JSONObject json = new JSONObject(JSONObject.Type.ARRAY);

    //-----------login input--------------
    [Header("Login input")]
    [SerializeField]
    private TMP_InputField Register_name;
    [SerializeField]
    private TMP_InputField Username;
    [SerializeField]
    private TMP_InputField Password;
    //-----------register input--------------
    [Header("Register input")]
    [SerializeField]
    private TMP_InputField Register_username;
    [SerializeField]
    private TMP_InputField Register_password;
    [SerializeField]
    private TMP_InputField Register_confirm_password;
   
    [SerializeField]
    private TMP_InputField Register_phone;
    //-----------error input--------------
    [Header("Error input")]
    [SerializeField]
    private GameObject error_alert_obj;
    [SerializeField]
    private TextMeshProUGUI error_text;
    [Header("Login and register object ")]
    [SerializeField]
    private GameObject login_object;
    [SerializeField]
    private GameObject register_object;
    //-----------------------
    private static int userid;
    private bool show_error=false;
    private bool register_success = false;
    private bool login_success = false;
    private string error_content;
    private int user_token;
   
   
    // Start is called before the first frame update
    void Start()
    {
      
        server = Socket_property.Server;
        ipep = Socket_property.Ipep;
        Remote = Socket_property.Remote;
        //thread = new Thread(new ThreadStart(server_ctrl));
        //thread.Start();
       
    }
   
    public void login()
    {
        if (Username.text.Equals(""))
        {
            Username.Select();
        }else if (Password.text.Equals(""))
        {
            Password.Select();
        }
        else
        {
            thread = new Thread(new ThreadStart(login_process));
            thread.Start();
        }
    }
    private void login_process()
    { /* bool login_status=true;*/
        json = new JSONObject(JSONObject.Type.ARRAY);
        json.AddField("msg", "login");
        json.AddField("Username", Username.text);
        json.AddField("Password", Password.text);
        server.SendTo(Encoding.UTF8.GetBytes(json.ToString()), SocketFlags.None, ipep);
      

        while (true)
        {
            data_lenght = new byte[4069];
            try {
                recv = server.ReceiveFrom(data_lenght, ref Remote);
            }
            catch (SocketException ex)
            {
                Debug.Log("exception:" + ex.Message);
            }
            string Data_str = Encoding.UTF8.GetString(data_lenght, 0, recv);
           JObject oj = JObject.Parse(Data_str);

            if (oj.GetValue("msg").ToString().Equals("success"))
            {
                //PlayerPrefs.SetString("player_name",oj.GetValue("name").ToString());
                userid = int.Parse(oj.GetValue("id").ToString());
                //Debug.Log("login message: "+ userid+" data lenght: "+recv+ " login token: " + oj.GetValue("token"));
                user_token = Int32.Parse(oj.GetValue("token").ToString());
                sever_config.online_start(userid);
                login_success = true;
                thread.Abort();
                break;
            }else if (oj.GetValue("msg").ToString().Equals("fail"))
            {
                Debug.Log("Login fail");
                error_content="Sai tên đăng nhập hoặc mật khẩu";
                show_error = true;
                thread.Abort();
                break;
            }

           
        }

      
    }
    public void register()
    {
        if (Register_name.text.Equals(""))
        {
            Register_name.Select();
        }
        else if (Register_username.text.Equals(""))
        {
            Register_username.Select();
        }
        else if (Register_password.text.Equals(""))
        {
            Register_password.Select();
        }
        else if (Register_confirm_password.text.Equals(""))
        {
            Register_confirm_password.Select();
        }
        else if (Register_phone.text.Equals(""))
        {
            Register_phone.Select();
        }
        else if(!Register_password.text.Equals(Register_confirm_password.text))
        {
            error_content = "Nhập lại mật khẩu không đúng";
            show_error = true;

        }
        else
        {

            thread = new Thread(new ThreadStart(register_proccess));
            thread.Start();
        }
       

    }
    private void register_proccess()
    {
       
        json = new JSONObject(JSONObject.Type.ARRAY);
        json.AddField("msg", "Register");
        json.AddField("Name", Register_name.text);
        json.AddField("Username", Register_username.text);
        json.AddField("Password", Register_password.text);
        json.AddField("Phone", Register_phone.text);
        server.SendTo(Encoding.UTF8.GetBytes(json.ToString()), SocketFlags.None, ipep);
        while (true)
        {
            data_lenght = new byte[4069];
            recv = server.ReceiveFrom(data_lenght, ref Remote);
            string Data_str = Encoding.UTF8.GetString(data_lenght, 0, recv);
            Debug.Log(Data_str);
            JObject oj = JObject.Parse(Data_str);
            if (oj.GetValue("msg").ToString().Equals("success"))
            {
                error_content = "Đăng ký thành công!";
             
                register_success = true;
                thread.Abort();
                break;
            }
            else if (oj.GetValue("msg").ToString().Equals("fail"))
            {
                if (oj.GetValue("Error").ToString().Equals("Username"))
                {
                    error_content = "Tên đăng nhập đã được người khác sử dụng";
                    show_error = true;
                }
                else
                {
                    error_content = "Tên nhân vật đã được người khác sử dụng";
                    show_error = true;
                }
                thread.Abort();
                break;
            }
        }

    }
         
    // Update is called once per frame
    void Update()
    {
        if (show_error)
        {
            error_alert_obj.SetActive(true);
            error_text.SetText(error_content);
            show_error = false;
        }
        if (register_success)
        {
            login_object.SetActive(true);
            register_object.SetActive(false);
            error_text.SetText(error_content);
            error_alert_obj.SetActive(true);
            Username.text =Register_username.text;
            Password.text = Register_password.text;
            register_success = false;
        }
        if (login_success)
        {
            PlayerPrefs.SetInt("token",user_token);
            login_success = false;
            user_token = 0;
            Debug.Log("Login success");
            SceneManager.LoadScene(1);
        }
        
    }
}
