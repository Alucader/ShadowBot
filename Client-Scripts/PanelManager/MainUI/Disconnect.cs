using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Disconnect : PanelBase
{
    private Button btn;

    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "Disconnect";
        layer = PanelLayer.Tips;
    }

    public override void OnShowing()
    {

        Transform skinTrans = skin.transform;
        btn = skinTrans.GetComponent<Button>();      
       // skinTrans.DOScaleX(1, 0.2f);     
        btn.onClick.AddListener(OnBtnClick);
    }

    public void OnBtnClick()
    {
        SceneManager.LoadScene(0);
        Close();
    }
}
