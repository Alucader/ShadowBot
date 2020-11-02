using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIRoot : MonoBehaviour
{
 
    void Start()
    {
        PanelMgr.instance.OpenPanel<RoomListPanel>("");
    }
  
    void Update()
    {
       
    }
}
