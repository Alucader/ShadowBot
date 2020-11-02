using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class ResultPanel : PanelBase 
{

    private Text text1;
    private Button button;
    string str = "";

    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "ResultPanel";
        layer = PanelLayer.Panel;
        if (args.Length == 1)
        {
            str = (string)args[0];
        }
    }

    public override void OnShowing()
    {

        Transform skinTrans = skin.transform;
        text1 = skinTrans.Find("reText").GetComponent<Text>();
        button = skinTrans.Find("rebtn").GetComponent<Button>();
        skinTrans.DOScaleY(1, 0.4f);
        text1.text= str;
        button.onClick.AddListener(OnBack);

    }
   void OnBack()
    {
        NetMgr.srvConn.msgDist.Clear();
        SceneManager.LoadScene("Room");
    }

}
