using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CursorLockModes : MonoBehaviour
{
   /// public GameObject EscPanel;
    bool isActive;
    // Start is called before the first frame update
    void Start()
    {
      //  EscPanel.SetActive(false);

       Cursor.lockState = CursorLockMode.Locked;//锁定指针到视图中心
       Cursor.visible = false;//隐藏指针
    }

    // Update is called once per frame
    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Escape)&& !isActive)
        {
            Cursor.lockState = CursorLockMode.None;
         //   EscPanel.SetActive(true);
            isActive = true;
        }
           else if (Input.GetKeyDown(KeyCode.Escape) && isActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
        //    EscPanel.SetActive(false);
            isActive = false;
        }
    }
    
   public void Not()
    {
        Cursor.lockState = CursorLockMode.Locked;
     //   EscPanel.SetActive(false);
        isActive = false;
    }

    public void Yes()
    {
       if( NetMgr.srvConn.status==Connection.Status.Connected)
            SceneManager.LoadScene("Room");
       else
            SceneManager.LoadScene("Login");
    }

}
