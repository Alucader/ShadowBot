using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendRoot : MonoBehaviour
{
    public Toggle friend;
    public Toggle waitpass;
    public Toggle addfriend;
    public List<GameObject> scroll = new List<GameObject>();

    public int friendNum = 0;
    public GameObject friendobj;
    public List<GameObject> frilist = new List<GameObject>();
    public GameObject obj;
    // Start is called before the first frame update
    int i = 0;

    [Header("AddFriend")]
    public GameObject addFri;
    public  Text idtxt;
    public Button friAdd;
    public InputField input;
    public Button friQuery;
    static string strid;

    void Start()
    {      
        friend.onValueChanged.AddListener(isOn => { if (isOn) { scroll[0].SetActive(true); } else { scroll[0].SetActive(false); } });
        waitpass.onValueChanged.AddListener(isOn => { if (isOn) { scroll[1].SetActive(true); } else { scroll[1].SetActive(false); } });
        addfriend.onValueChanged.AddListener(isOn => { if (isOn) { scroll[2].SetActive(true); } else { scroll[2].SetActive(false); } });
        friQuery.onClick.AddListener(FriendAdd);
        friAdd.onClick.AddListener(FriendAdd1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FriendList()
    {
        Debug.Log(obj.GetComponent<RectTransform>().sizeDelta.y);

        if (i<=4)
        {
            GameObject fri = (GameObject)Instantiate(friendobj);
            fri.transform.position = new Vector3(fri.transform.position.x, fri.transform.position.y + 90 * i);
            fri.transform.SetParent(obj.transform, false);
            i++;
        }
        else
        {
            print("2");
            obj.GetComponent<RectTransform>().sizeDelta=new Vector2 (obj.GetComponent<RectTransform>().sizeDelta.x, obj.GetComponent<RectTransform>().sizeDelta.y+90);
            GameObject fri = (GameObject)Instantiate(friendobj);
            fri.transform.position = new Vector3(fri.transform.position.x, fri.transform.position.y + 90 * i);
            fri.transform.SetParent(obj.transform, false);
            i++;
        }
       
    }



    void FriendAdd()
    {
        if (input.text.Trim ()!="")
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
            protocol.AddInt(0);
            protocol.AddString(input.text);
            strid = input.text;
            NetMgr.srvConn.Send(protocol, FriendAddBack);
        }
        
    }
    void FriendAddBack(ProtocolBase protocol)
    {
        Debug.Log("OnFriendAddBack");
        ProtocolBytes proto = (ProtocolBytes)protocol;
        int start = 0;
        string protoName = proto.GetString(start, ref start);
        Debug.Log("服务器返回协议：" + protoName);
        //提取返回内容
        int ret = proto.GetInt(start, ref start);
        if (ret == 0)
        {
            //找到      
            string id = proto.GetString(start, ref start);
            idtxt.text = strid;
            addFri.SetActive(true);
            
        }
        else
        {           
            PanelMgr.instance.OpenPanel<TipPanel>("", "用户不存在");
        }
    }

    void FriendAdd1()
    {
        ProtocolBytes protocol = new ProtocolBytes();
        protocol.AddString("Friend");
        protocol.AddInt(1);
        protocol.AddString(strid);
        addFri.SetActive(false);
        PanelMgr.instance.OpenPanel<TipPanel>("", "已发送好友邀请");
        NetMgr.srvConn.Send(protocol);
    }
}
