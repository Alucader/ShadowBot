using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnRange : MonoBehaviour
{
    Image image;

    void Start()
    {
        image = this.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = 0.1f;
    }

  
    void Update()
    {
        
    }
}
