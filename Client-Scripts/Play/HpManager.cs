using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    public ThirdPersonPlayer thirdPerson;
    public PlayController controller;
    private bool isLive;
    [Header("HP")]
    public Text hpText;
    public int totalHP = 100;
    public int hp;
    Animator anim;
    private ScoreBoard sb;
    public CtrlType ctrlType = CtrlType.none;
    public enum CtrlType  //操控类型
    {
        none,
        player,
        net,
    }
    void Start()
    {
        sb = (ScoreBoard)FindObjectOfType(typeof(ScoreBoard)) as ScoreBoard;
        hp = totalHP;
        isLive = true;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (ctrlType == CtrlType.player)
        {
            hpText.text = hp.ToString();
            if (hp <= 0)
            {
                hp = 0;
                Death();
            }
        }

        if (ctrlType == CtrlType.net)
        {
            if (hp <= 0)
            {
                hp = 0;
                Death();
            }
        }

    }

    public void Live()
    {
        if (ctrlType == CtrlType.player)
        {
            if (isLive == false)
            {
                hp = totalHP;
                controller.enabled = true;
                thirdPerson.enabled = true;
                anim.ResetTrigger("Death");
                anim.SetTrigger("Live");
                isLive = true;
            }
        }

        else if (ctrlType == CtrlType.net)
        {
            if (isLive == false)
            {
                hp = totalHP;
                anim.ResetTrigger("Death");
                anim.SetTrigger("Live");
                isLive = true;
            }
        
        }
    }

    void Death()
    {
        if (ctrlType == CtrlType.player)
        {
            if (isLive == true)
            {
                controller.enabled = false;
                thirdPerson.enabled = false;
                anim.SetTrigger("Death");
                anim.ResetTrigger("Live");
                isLive = false;
                if (GameMgr.instance.camp == 1)
                    sb.AddCamp2();
                else
                    sb.AddCamp1();
                StartCoroutine(ReLive());
            }
        }
        else if (ctrlType == CtrlType.net)
        {
            if (isLive == true)
            {
                anim.SetTrigger("Death");
                anim.ResetTrigger("Live");
                isLive = false;
                if (gameObject.GetComponent<BattlePlayer>().camp == 1)
                    sb.AddCamp2();
                else
                {
                    sb.AddCamp1();
                }                  
                StartCoroutine(ReLive());       
            }
        }
       
        
    }
    IEnumerator ReLive()
    {
        yield return new WaitForSeconds(5.0f);
        Transform sp = GameObject.Find("SwopPoints").transform;
        Transform swopTrans;
        if (GameMgr.instance.camp == 1)
        {
            Transform teamSwop = sp.GetChild(0);
            swopTrans = teamSwop.GetChild(0);
            gameObject.transform.position = swopTrans.position;
        }
        else
        {
            Transform teamSwop = sp.GetChild(1);
            swopTrans = teamSwop.GetChild(0);
            gameObject.transform.position = swopTrans.position;
        }
        try
        {
            ProtocolBytes protocol = new ProtocolBytes();
            protocol.AddString("Revive");
            string id = gameObject.GetComponent<BattlePlayer>().id;
            protocol.AddString(id);
            NetMgr.srvConn.Send(protocol);
            Live();
        }
        catch
        {

        }
       
    }
}


