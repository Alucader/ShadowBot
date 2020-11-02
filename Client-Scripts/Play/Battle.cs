using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Cinemachine;
using DG.Tweening;
public class Battle : MonoBehaviour
{

    //单例
    public static Battle instance;
    //人物预设
    public GameObject[] playPrefabs;
    //战场中的所有玩家
    public Dictionary<string, BattlePlayer> list = new Dictionary<string, BattlePlayer>();

    public CinemachineFreeLook cameraFreeLook;
    
    void Start()
    {
    
        Debug.Log(GameMgr.instance.id);
        instance = this;
        StartBattle(GameMgr.instance.protocol);
       
    }


    //开始战斗
    public void StartBattle(ProtocolBytes proto)
    {
        //解析协议
  
        int start = 0;
        string protoName = proto.GetString(start, ref start);
        if (protoName != "Fight")
            return;
        //玩家总数
        int count = proto.GetInt(start, ref start);
      
        //每一个玩家
        for (int i = 0; i < count; i++)
        {
            string id = proto.GetString(start, ref start);
            int team = proto.GetInt(start, ref start);
            int swopID = proto.GetInt(start, ref start);
            GeneratePlayer(id, team, swopID);
            
        }
        NetMgr.srvConn.msgDist.AddListener("UpdateUnitInfo", RecvUpdateUnitInfo);
        NetMgr.srvConn.msgDist.AddListener("Shooting", RecvShooting);
        NetMgr.srvConn.msgDist.AddListener("Hit", RecvHit);
        NetMgr.srvConn.msgDist.AddListener("Grenade", RecvGrenade);
        NetMgr.srvConn.msgDist.AddListener("KillInfo", RecvKillInfo);
       
    }


    public void GeneratePlayer(string id,int team, int swopID)
    {
        //获取出生点
        Debug.Log(" ID=" + id+" swopID=" + swopID);
        Transform sp = GameObject.Find("SwopPoints").transform;
        Transform swopTrans;
        if (team == 1)
        {
            Transform teamSwop = sp.GetChild(0);
            swopTrans = teamSwop.GetChild(swopID - 1);
        }
        else
        {
            Transform teamSwop = sp.GetChild(1);
            swopTrans = teamSwop.GetChild(swopID - 1);
        }
        if (swopTrans == null)
        {
            Debug.LogError("GeneratePlayer出生点错误！");
            return;
        }
        GameObject playObj;
        if (id == GameMgr.instance.id)
        {
            playObj = (GameObject)Instantiate(playPrefabs[0]);
            playObj.name = id;
            playObj.transform.position = swopTrans.position;
            playObj.transform.rotation = swopTrans.rotation;
            cameraFreeLook.LookAt = playObj.transform;
            cameraFreeLook.Follow = playObj.transform;
            playObj.GetComponent<HpManager>().ctrlType = HpManager.CtrlType.player;
        }
       else if(id != GameMgr.instance.id && team == GameMgr.instance.camp) 
        {
            playObj = (GameObject)Instantiate(playPrefabs[2]);
            playObj.name = id;
            playObj.transform.position = swopTrans.position;
            playObj.transform.rotation = swopTrans.rotation;
            playObj.GetComponent<HpManager>().ctrlType = HpManager.CtrlType.net;
        }
        else
        {
            playObj = (GameObject)Instantiate(playPrefabs[1]);
            playObj.name = id;
            playObj.transform.position = swopTrans.position;
            playObj.transform.rotation = swopTrans.rotation;
            playObj.GetComponent<HpManager>().ctrlType = HpManager.CtrlType.net;          
        }
        playObj.GetComponent<BattlePlayer>().camp = team;
        playObj.GetComponent<BattlePlayer>().id = id;
        //列表处理
        list.Add(id, playObj.GetComponent<BattlePlayer>());
        
    }

    public void RecvUpdateUnitInfo(ProtocolBase protocol)
    {
        //解析协议
        int start = 0;
        ProtocolBytes proto = (ProtocolBytes)protocol;
        string protoName = proto.GetString(start, ref start);
        string id = proto.GetString(start, ref start);
        Vector3 nPos;
        Vector3 nRot;
        float Speed=0;
        nPos.x = proto.GetFloat(start, ref start);
        nPos.y = proto.GetFloat(start, ref start);
        nPos.z = proto.GetFloat(start, ref start);
        nRot.x = proto.GetFloat(start, ref start);
        nRot.y = proto.GetFloat(start, ref start);
        nRot.z = proto.GetFloat(start, ref start);
        Speed = proto.GetFloat(start, ref start);
        //处理
        if (!list.ContainsKey(id))
        {
            Debug.Log("RecvUpdateUnitInfo bt == null ");
            return;
        }
       
        if (id == GameMgr.instance.id)
            return;
        else
        {
            list[id].play.NetForecastInfo(nPos, nRot,Speed);
        }
    }



    public void RecvShooting(ProtocolBase protocol)
    {
        //解析协议
        int start = 0;
        ProtocolBytes proto = (ProtocolBytes)protocol;
        string protoName = proto.GetString(start, ref start);
        string id = proto.GetString(start, ref start);
      
          //处理
        if (!list.ContainsKey(id))
        {
            Debug.Log("RecvShooting bt == null ");
            return;
        }

        if (id == GameMgr.instance.id)
            return;
        else
        {
            list[id].thirdPersonNet.Shoot();
        }

    }

    public void RecvGrenade(ProtocolBase protocol)
    {
        //解析协议
        int start = 0;
        ProtocolBytes proto = (ProtocolBytes)protocol;
        string protoName = proto.GetString(start, ref start);
        string id = proto.GetString(start, ref start);

        //处理
        if (!list.ContainsKey(id))
        {
            Debug.Log("RecvGrenade bt == null ");
            return;
        }

        if (id == GameMgr.instance.id)
            return;
        else
        {
            list[id].thirdPersonNet.Grenade();
        }
    }
    public void RecvHit(ProtocolBase protocol)
    {
        //解析协议
        int start = 0;
        ProtocolBytes proto = (ProtocolBytes)protocol;
        string protoName = proto.GetString(start, ref start);      
        string id = proto.GetString(start, ref start);
        int hurt = proto.GetInt(start, ref start);
        //获取
        Debug.Log("hurt:" + hurt);
        if (!list.ContainsKey(id))
        {
            Debug.Log("RecvHit bt == null ");
            return;
        }
        if (id==GameMgr.instance.id)
        {
           list[id].hpMgr.hp -= hurt;       
        }
        else
        {
            list[id].hpMgr.hp -= hurt;
        }
         
       
    }   

  

    public void RecvKillInfo(ProtocolBase protocol)
    {   
        int start = 0;
        ProtocolBytes proto = (ProtocolBytes)protocol;
        string protoName = proto.GetString(start, ref start);
        string kill= proto.GetString(start, ref start);
        string bekill = proto.GetString(start, ref start);
        //xxx击杀xxxx;
        Debug.Log(kill+"击杀了"+ bekill);
        PanelMgr.instance.OpenPanel<KillPanel>("", kill + "击杀了" + bekill);          
    }
}