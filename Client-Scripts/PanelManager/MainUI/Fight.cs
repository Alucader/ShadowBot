using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fight : PanelBase
{
    public override void Init(params object[] args)
    {
        base.Init(args);
        skinPath = "FriendPanel";
        layer = PanelLayer.Panel;
    }

    public override void OnShowing()
    {

        Transform skinTrans = skin.transform;
    }
}
