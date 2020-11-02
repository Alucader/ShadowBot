using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class IPSet : MonoBehaviour
{
    public InputField inputip;
    public InputField port;
    public GameObject obj;
    public void OnClickY()
    {
        NetworkAddress.ip = inputip.text;
        NetworkAddress.port = Int32.Parse( port.text);
    }

    public void OnClickN()
    {
        obj.SetActive(false);
    }
}
