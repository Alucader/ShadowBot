using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginRoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Application.runInBackground = true;
        PanelMgr.instance.OpenPanel<LoginPanel>("");
        PanelMgr.instance.OpenPanel<LoginImage>("");
        
    }

    // Update is called once per frame
  
}
