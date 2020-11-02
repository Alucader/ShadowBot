using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EmergeAI : MonoBehaviour
{
  
    public Transform[] points;
    //坐标索引
    public int pointIndex;
    public float moveSpeed;
    public bool viewFlag;
    public float attackDis;
    //目标
    public GameObject target;

   
    void Start()
    {
        StartCoroutine(MasterGO());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MoveSelf()
    {      
        Vector3 nextPosition = points[pointIndex].position;
        this.transform.LookAt(nextPosition);
        this.transform.Translate(moveSpeed * Vector3.forward * Time.deltaTime, Space.Self);
        //判断距离
        if (Vector3.Distance(this.transform.position, nextPosition) < 0.1)
        {
            //更换下一个坐标点
            pointIndex = Random.Range(0, 4);//随机范围{0,1,2,3}
        }
    }
    void FollowTarget()
    {
        //获取调用时目标坐标
        Vector3 targetPostion = target.transform.position;
        this.transform.LookAt(targetPostion);
        this.transform.Translate(moveSpeed * Vector3.forward * Time.deltaTime, Space.Self);
        Debug.Log("FollowTarget");
    }
    void Attack()
    {
        Debug.Log("打你");
    }
    IEnumerator MasterGO()
    {
        while (true)
        {
            //等待固定帧更新完成
            yield return new WaitForFixedUpdate();
            if (viewFlag)
            {
                //求二者距离
                float distance = Vector3.Distance(target.transform.position, this.transform.position);              
                if (distance <= attackDis) {
                   // Debug.Log("attack");
                    Attack();
                    continue;
                }
                FollowTarget();
                continue;
            }
            //没有看到目标，自由移动
            MoveSelf();
        }
    }
}

