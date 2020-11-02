using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FriendPanel : PanelBase
{
    private Button back;
    private Button menu;
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "FriendPanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {
        Transform skinTrans = skin.transform;
        back = skinTrans.Find("Return").GetComponent<Button>();
        menu = skinTrans.Find("Meun").GetComponent<Button>();
        back.onClick.AddListener(() => { Close(); });
        menu.onClick.AddListener(() => { Close(); });
        skinTrans.DOScaleY(1, 0.1f);
       // FriendQuery();

    }

    void FriendQuery()
    {
        if (NetMgr.srvConn.status != Connection.Status.Connected)
        {
            string host = NetworkAddress.ip;
            int port = NetworkAddress.port;
            NetMgr.srvConn.proto = new ProtocolBytes();
            if (!NetMgr.srvConn.Connect(host, port))
                PanelMgr.instance.OpenPanel<Disconnect>("");
        }
        //发送
        ProtocolBytes protocol = new ProtocolBytes();
        protocol.AddString("Friend");
        NetMgr.srvConn.Send(protocol, FriendQueryBack);
    }

    void FriendQueryBack(ProtocolBase protocol)
    {
        Debug.Log("OnFriendQueryBack");
        ProtocolBytes proto = (ProtocolBytes)protocol;
        int start = 0;
        string protoName = proto.GetString(start, ref start);
        Debug.Log("服务器返回协议：" + protoName);
        try
        {
            byte[] buff = proto.Getbyte(start, ref start);
            string str = "";
            if (buff == null)
                return;
            for (int i = 0; i < buff.Length; i++)
            {
                int b = (int)buff[i];
                str += b.ToString() + " ";
            }
            Debug.Log(str);


        }
        catch
        {

        }

    }
}
