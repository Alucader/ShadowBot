using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class RegPanel : PanelBase
{
    private InputField idInput;
    private InputField pwInput;
    private InputField againPw;
    private Button regBtn;
    private Button backBtn;

    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "Login/RegPanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {

        Transform skinTrans = skin.transform;
        idInput = skinTrans.Find("User").GetComponent<InputField>();
        pwInput = skinTrans.Find("Pwd").GetComponent<InputField>();
        againPw = skinTrans.Find("AgainPwd").GetComponent<InputField>();
        regBtn = skinTrans.Find("RegBtn").GetComponent<Button>();
        backBtn = skinTrans.Find("Return").GetComponent<Button>();
       
        regBtn.onClick.AddListener(OnRegClick);
        backBtn.onClick.AddListener(OnBackClick);

    }

    public void OnRegClick()
    {
        string user = idInput.text.Trim();
        string pw = pwInput.text.Trim();
        string apw = againPw.text.Trim();

        if (user=="" || pw=="" || apw=="")
        {
            PanelMgr.instance.OpenPanel<TipPanel>("", "账号与密码不能为空");
        }

        else if (user != "" && pw != "" && pw != apw)
        {
            PanelMgr.instance.OpenPanel<TipPanel>("", "两次密码不一样");
        }

       else if (user!= "" && pw!= "" && pw == apw)
        {
            if(!IsNatural_Number(user) || !IsNatural_Number(pw))
            {
                PanelMgr.instance.OpenPanel<TipPanel>("", "输入不合法");
                return;
            }
            OnRegist();
        }      
    }

    public void OnBackClick()
    {
        Close();
    }

    #region 正则表达式
    public bool IsNatural_Number(string str)
    {
        Regex regex1 = new Regex(@"^[A-Za-z0-9]{5,10}$");
        return regex1.IsMatch(str);
    }
    #endregion 

    public void OnRegist()
    {
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
        protocol.AddString("Register");
        protocol.AddString(idInput.text);
        protocol.AddString(pwInput.text);
        Debug.Log("发送 " + protocol.GetDesc());
        NetMgr.srvConn.Send(protocol, OnRegBack);
    }

    public void OnRegBack(ProtocolBase protocol)
    {
        ProtocolBytes proto = (ProtocolBytes)protocol;
        int start = 0;
        string protoName = proto.GetString(start, ref start);
        int ret = proto.GetInt(start, ref start);
        if (ret == 0)
        {
            PanelMgr.instance.OpenPanel<TipPanel>("", "注册成功!");
            Close();
        }
        else
        {
            PanelMgr.instance.OpenPanel<TipPanel>("", "注册失败，请更换用户名!");
        }
    }
}
