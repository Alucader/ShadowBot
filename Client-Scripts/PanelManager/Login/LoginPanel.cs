using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginPanel : PanelBase
{
    private InputField idInput;
    private InputField pwInput;
    private Button loginBtn;
    private Button regBtn;
    private Button closeBtn;
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "LoginPanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {

        Transform skinTrans = skin.transform;
        idInput = skinTrans.Find("UserInputField").GetComponent<InputField>();
        pwInput = skinTrans.Find("PwdInputField").GetComponent<InputField>();
        //
        loginBtn = skinTrans.Find("LoginBtn").GetComponent<Button>();
        regBtn = skinTrans.Find("RegBtn").GetComponent<Button>();
        closeBtn = skinTrans.Find("Return").GetComponent<Button>();
        //
        loginBtn.onClick.AddListener(OnLoginClick); 
        regBtn.onClick.AddListener(OnRegClick);
        closeBtn.onClick.AddListener(OnCloseClick);
    }

    // Update is called once per frame
    public void OnRegClick()
    {
        PanelMgr.instance.OpenPanel<RegPanel>("");
        //Close();
    }

    public void OnLoginClick()
    {
       
        //判断用户输入用户名密码是否为空
        if (idInput.text == "" || pwInput.text == "")
        {
            PanelMgr.instance.OpenPanel<TipPanel>("", "用户名密码为空");
           
            return;
        }
        //连接服务器
        if (NetMgr.srvConn.status != Connection.Status.Connected)
        {
            string host = NetworkAddress.ip;
            int port = NetworkAddress.port;
            NetMgr.srvConn.proto = new ProtocolBytes();
            if (!NetMgr.srvConn.Connect(host, port))
                PanelMgr.instance.OpenPanel<TipPanel>("", "连接服务器失败!");
        }
        //发送
        ProtocolBytes protocol = new ProtocolBytes();
        protocol.AddString("Login");
        protocol.AddString(idInput.text.Trim());
        protocol.AddString(pwInput.text.Trim());
       // Debug.Log("发送 " + protocol.GetDesc());
        NetMgr.srvConn.Send(protocol, OnLoginBack);



    }

    public void OnLoginBack(ProtocolBase protocol)
    {
        Debug.Log("OnLoginBack");
        ProtocolBytes proto = (ProtocolBytes)protocol;
        int start = 0;
        string protoName = proto.GetString(start, ref start);
        int ret = proto.GetInt(start, ref start);
        if (ret == 0)
        {
            PanelMgr.instance.OpenPanel<TipPanel>("", "登录成功!");
            //开始游戏         
            GameMgr.instance.id = idInput.text.Trim();
            SceneManager.LoadScene("Room");                  
            Close();           
        }
        else
        {
            PanelMgr.instance.OpenPanel<TipPanel>("", "登录失败，请检查用户名密码!");
        }
    }

    public void OnCloseClick()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}

