using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ScoreBoard : MonoBehaviour
{
    public Text camp1;
    public Text camp2;
    int c1 = 0;
    int c2 = 0;
    bool isstart=true;
    void Start()
    {
        camp1.text =c1.ToString();
        camp2.text = c2.ToString();
    }
    void Update()
    {
        if (isstart)
        {
            if (c1 >= 10)
            {
                //游戏结算
                ProtocolBytes protocol = new ProtocolBytes();
                protocol.AddString("Result");
                protocol.AddInt(1);
                NetMgr.srvConn.Send(protocol);

                if (GameMgr.instance.camp == 1)
                    PanelMgr.instance.OpenPanel<ResultPanel>("", "WIN");
                else
                    PanelMgr.instance.OpenPanel<ResultPanel>("", "LOSE");
                isstart = false;
                GameObject.Find(GameMgr.instance.id).GetComponent<ThirdPersonPlayer>().enabled = false;
                GameObject.Find(GameMgr.instance.id).GetComponent<PlayController>().enabled = false;
            }

            else if (c2 >= 10)
            {
                ProtocolBytes protocol = new ProtocolBytes();
                protocol.AddString("Result");
                protocol.AddInt(2);
                NetMgr.srvConn.Send(protocol);

                if (GameMgr.instance.camp == 2)
                    PanelMgr.instance.OpenPanel<ResultPanel>("", "WIN");
                else
                    PanelMgr.instance.OpenPanel<ResultPanel>("", "LOSE");
                isstart = false;
                GameObject.Find(GameMgr.instance.id).GetComponent<ThirdPersonPlayer>().enabled = false;
                GameObject.Find(GameMgr.instance.id).GetComponent<PlayController>().enabled=false ;
            }
        }
        

    }
    public void AddCamp1()
    {
        c1++;
        camp1.text = c1.ToString();
    }
    public void AddCamp2()
    {
        c2++;
        camp2.text = c2.ToString();
    }
}
