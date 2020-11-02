using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MasterAI : MonoBehaviour
{
    [Range(0, 180)]
    public int viewAngle;
    public Color color1 = Color.red;
    public float viewRadius = 2;
    public GameObject target;
    public EmergeAI mosterAI;
    void OnDrawGizmos()
    {
       // Handles.color = color1;
        int angle = viewAngle / 2;
        Vector3 startLine = Quaternion.Euler(0, -angle, 0) * this.transform.forward;

      //  Handles.DrawSolidArc(this.transform.position, this.transform.up, startLine, viewAngle, viewRadius);
    }
    void SeeOther()
    {
    
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        Vector3 myVector3 = target.transform.position - this.transform.position;
        
        float angle = Vector3.Angle(myVector3, this.transform.forward);
        if (distance <=viewRadius && angle<= viewAngle / 2) 
        {        
            mosterAI.viewFlag = true;         
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SeeOther();
    }
}
