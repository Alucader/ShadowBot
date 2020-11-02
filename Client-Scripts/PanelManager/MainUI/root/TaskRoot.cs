using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskRoot : MonoBehaviour
{
    public Toggle story;
    public Toggle week;
    public Toggle action;
    public List<GameObject> scroll = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
       // story.isOn = true;
        story.onValueChanged.AddListener(isOn => { if (isOn) { scroll[0].SetActive(true); } else { scroll[0].SetActive(false);} });
        week.onValueChanged.AddListener(isOn => { if (isOn) { scroll[1].SetActive(true); } else { scroll[1].SetActive(false); } });
        action.onValueChanged.AddListener(isOn => { if (isOn) { scroll[2].SetActive(true); } else { scroll[2].SetActive(false);  } });

    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
}
