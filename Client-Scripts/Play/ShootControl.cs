using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using RootMotion.FinalIK;
public class ShootControl : MonoBehaviour
{
 /*   public TPSCamera tpsCamera;//控制相机视角的脚本
    public Camera cam;//摄像机
    public float range;//射线距离
    private float offsetDis;//摄像机与玩家的距离
    public Vector3 targetPos;//目标位置
    private AimIK aimIK;//对应的final ik组件
    public float speed;//移动速度
    public float rotateSpeed;//旋转速度

    private void Awake()
    {
        tpsCamera = GameObject.Find("TPSCameraParent").GetComponent<TPSCamera>();//获取相机的父物体
        cam = tpsCamera.GetComponentInChildren<Camera>();//相机为tpsCamera的子物体
        aimIK = GetComponent<AimIK>();//获取AimIk组件
        offsetDis = Vector3.Distance(transform.position, cam.transform.position);//初始化offsetDis
    }

    private void Update()
    {
        SetTarget();//设置瞄准的目标位置
        OnKeyEvent();//处理按键响应
    }

    /// <summary>
    /// 设置瞄准的目标
    /// 从摄像机位置向摄像机正方向发射射线（即从屏幕视口中心发出）
    /// 射线的长度=range，可以近似设为子弹的射程
    /// 若射线打到非玩家的物体则将该物体设为目标
    /// 若射线没有打到物体则将目标设为射线的终点
    /// </summary>
    public void SetTarget()
    {
        //从摄像机位置向摄像机正方向发射射线（即从屏幕视口中心发出）
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            //若射线打到非玩家的物体则将该物体设为目标
            //我这里并没有进行判断该物体是否是玩家，因为我设置的玩家位于屏幕的偏左下位置，射线不会穿过玩家
            //需要的话，可以给玩家设定layer,然后让射线屏蔽这个layer
            targetPos = hit.point;
        }
        else
        {
            //若射线没有打到物体则将目标设为射线的终点
            targetPos = cam.transform.position + (cam.transform.forward * range);
        }
        //画出射线便于观察（不会显示在game中）
        Debug.DrawRay(cam.transform.position, cam.transform.forward * range, Color.green);

        //按下鼠标右键时开启AimIK，进入瞄准状态
        if (Input.GetMouseButtonDown(1))
        {
            aimIK.enabled = true;
        }

        //按住鼠标右键时为瞄准状态，人物身体始终朝向摄像机的前方
        if (Input.GetMouseButton(1))
        {
            RotateBodyToTarget();
        }
        else//松开右键时为自由视角状态，关闭AimIK，不进行瞄准
        {
            //注意这里使用Disale(),不要直接enabled=false,原因不清楚
            aimIK.Disable();
        }
    }

    /// <summary>
    /// 旋转玩家身体，使玩家朝向摄像机的水平前方
    /// </summary>
    private void RotateBodyToTarget()
    {
        Vector3 rotEulerAngles = cam.transform.eulerAngles;//获取摄像机的旋转的欧拉角
        rotEulerAngles.x = 0;//垂直方向不进行旋转
        //使用插值让玩家平滑转向
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotEulerAngles), rotateSpeed * Time.deltaTime);
        SetAimIKTarget();//更新AimIK的target的位置
    }

    /// <summary>
    /// 更新AimIK的target的位置
    /// </summary>
    private void SetAimIKTarget()
    {
        //将AimIK的target位置设为之前射线检测到的位置
        aimIK.solver.target.position = targetPos;
    }

    /// <summary>
    /// 管理键盘的响应，这里只用来控制玩家移动,不重要，可以忽略
    /// </summary>
    private void OnKeyEvent()
    {
        //Horizontal和Vertical的默认按键为ad←→和ws↑↓
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            Vector3 moveDir = new Vector3(h, 0, v);
            transform.Translate(moveDir * speed * Time.deltaTime);
            RotateBodyToTarget();
        }
    }*/

}
