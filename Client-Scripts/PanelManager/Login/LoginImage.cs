using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginImage : PanelBase
{
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "LoginImage";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {
        int n = 1, m = 4;
        n =++ m;
        Debug.Log(n);
        //Transform skinTrans = skin.transform;       
    }
    
}
