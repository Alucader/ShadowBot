using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class RoomPanel : PanelBase
{
    private List<Transform> prefabs = new List<Transform>();
    private Button closeBtn;
    private Button startBtn;
    private Button mesBtn;
    private InputField mesInput;
    private Transform mesScroll;
    Text mesText;
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "RoomPanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {
        base.OnShowing();
        Transform skinTrans = skin.transform;
        //组件
        for (int i = 0; i < 4; i++)
        {
            string name = "PlayerPrefab" + i.ToString();
            Transform prefab = skinTrans.Find(name);
            prefabs.Add(prefab);
        }
        closeBtn = skinTrans.Find("Return").GetComponent<Button>();
        startBtn = skinTrans.Find("StartBtn").GetComponent<Button>();
        mesBtn= skinTrans.Find("MesBtn").GetComponent<Button>();
        //聊天
        mesInput= skinTrans.Find("MesInput").GetComponent<InputField>();
        mesScroll= skinTrans.Find("MesScroll").GetComponent<Transform>();
        mesText = mesScroll.GetChild(1).GetChild (0).GetChild(0).GetComponent<Text>();
        //按钮事件
        closeBtn.onClick.AddListener(OnCloseClick);  //关闭
        startBtn.onClick.AddListener(OnStartClick);  //开始
        mesBtn.onClick.AddListener(OnMessageClick);
        //监听
        NetMgr.srvConn.msgDist.AddListener("GetRoomInfo", RecvGetRoomInfo);
        NetMgr.srvConn.msgDist.AddListener("Fight", RecvFight);
        NetMgr.srvConn.msgDist.AddListener("Message", RecvMessage);
        //发送查询
        ProtocolBytes protocol = new ProtocolBytes();
        protocol.AddString("GetRoomInfo");
        NetMgr.srvConn.Send(protocol);


    }

    public override void OnClosing()
    {
        Debug.Log("OnClosing");
        NetMgr.srvConn.msgDist.DelListener("GetRoomInfo", RecvGetRoomInfo);
        NetMgr.srvConn.msgDist.DelListener("Fight", RecvFight);
        NetMgr.srvConn.msgDist.DelListener("Message", RecvMessage);
    }

    #region  发送消息
    public void OnMessageClick()
    {
        string str = mesInput.text;
        if (mesInput.text.Trim()=="")
            return;
        ProtocolBytes protocol = new ProtocolBytes();
        protocol.AddString("Message");
        protocol.AddString(GameMgr.instance.id+":"+mesInput.text);
        NetMgr.srvConn.Send(protocol);
        mesInput.text = "";

    }
    #endregion
    public void RecvMessage(ProtocolBase protocol)
    {
        ProtocolBytes proto = (ProtocolBytes)protocol;
        int start = 0;
        string protoName = proto.GetString(start, ref start);
        string mes= proto.GetString(start, ref start);
        mesText.text = mesText.text + mes +"\r\n";
    }

    public void RecvGetRoomInfo(ProtocolBase protocol)
    {
        //获取总数
        Debug.Log("GetRoomInfo回调函数RecvGetRoomInfo");
        ProtocolBytes proto = (ProtocolBytes)protocol;
        int start = 0;
        string protoName = proto.GetString(start, ref start);
        int count = proto.GetInt(start, ref start);
        //每个处理
        int i = 0;
        for (i = 0; i < count; i++)
        {
            string id = proto.GetString(start, ref start);
            int team = proto.GetInt(start, ref start);
            int win = proto.GetInt(start, ref start);
            int fail = proto.GetInt(start, ref start);
            string achi = proto.GetString(start, ref start);
            int isOwner = proto.GetInt(start, ref start);
            //信息处理
            Transform trans = prefabs[i];
            Text text = trans.Find("Text").GetComponent<Text>();
            string str = "名字：" + id + "\r\n";
            str += "阵营：" + (team == 1 ? "绿" : "红") + "\r\n";
            str += "成就：" + achi + "\r\n";
            str += "胜利：" + win.ToString() + "   ";
            str += "失败：" + fail.ToString() + "\r\n";
            if (id == GameMgr.instance.id)
            {
                str += "【我自己】";
                GameMgr.instance.camp = team;
            }
               
            if (isOwner == 1)
                str += "【房主】";
            text.text = str;

            if (team == 1)
                trans.GetComponent<Image>().color = Color.green;
            else
                trans.GetComponent<Image>().color = Color.red;
        }

        for (; i < 4; i++)
        {
            Transform trans = prefabs[i];
            Text text = trans.Find("Text").GetComponent<Text>();
            text.text = "【等待玩家】";
            trans.GetComponent<Image>().color = Color.gray;
        }
    }

    public void OnCloseClick()
    {
        ProtocolBytes protocol = new ProtocolBytes();
        protocol.AddString("LeaveRoom");
        NetMgr.srvConn.Send(protocol, OnCloseBack);
    }


    public void OnCloseBack(ProtocolBase protocol)
    {
        //获取数值
        ProtocolBytes proto = (ProtocolBytes)protocol;
        int start = 0;
        string protoName = proto.GetString(start, ref start);
        int ret = proto.GetInt(start, ref start);
        //处理
        if (ret == 0)
        {
            PanelMgr.instance.OpenPanel<TipPanel>("", "退出成功!");
            PanelMgr.instance.OpenPanel<RoomListPanel>("");
            Close();
        }
        else
        {
            PanelMgr.instance.OpenPanel<TipPanel>("", "退出失败！");
        }
    }


    public void OnStartClick()
    {
        ProtocolBytes protocol = new ProtocolBytes();
        protocol.AddString("StartFight");
        NetMgr.srvConn.Send(protocol, OnStartBack);
    }

    public void OnStartBack(ProtocolBase protocol)
    {
        //获取数值
        ProtocolBytes proto = (ProtocolBytes)protocol;
        int start = 0;
        string protoName = proto.GetString(start, ref start); //StartFight
        int ret = proto.GetInt(start, ref start);
        //处理
        if (ret != 0)
        {
            PanelMgr.instance.OpenPanel<TipPanel>("", "开始游戏失败！只有房主可以开始战斗！");
        }
    }


    public void RecvFight(ProtocolBase protocol)
    {
        ProtocolBytes proto = (ProtocolBytes)protocol;
        GameMgr.instance.protocol = proto;
        Load.instance.LoadNextScene("Battle");
        Close();
        Debug.LogWarning(" RecvFight close");
        //SceneManager.LoadScene("Level1");
        
       
    }
}
