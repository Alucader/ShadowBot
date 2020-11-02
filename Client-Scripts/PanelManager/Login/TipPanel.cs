using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TipPanel : PanelBase
{
    private Text text1;
    string str = "";

    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "TipPanel";
        layer = PanelLayer.Tips;
        if (args.Length == 1)
        {
            str = (string)args[0];
        }
    }

    public override void OnShowing()
    {

        Transform skinTrans = skin.transform;
        text1 = skinTrans.Find("Text").GetComponent<Text>();
        skinTrans.DOScaleY(1, 0.4f);
        text1.DOText(str, 0.2f);
        StartCoroutine(CloseIEnum());
        
    }

   IEnumerator CloseIEnum()
    {
        yield return new WaitForSeconds(1.2f);
        Transform skinTrans = skin.transform;
        skinTrans.DOScaleY(0, 0.2f);
        yield return new WaitForSeconds(0.25f);
        Close();
    }
 
}
