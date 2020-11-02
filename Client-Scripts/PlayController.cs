using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    public float velocity = 9.0f;
    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public float _vertSpeed;
    public float desiredRotationSpeed = 0.1f;
    public Animator anim;
    public float Speed;
    public float allowPlayerRotation = 0.1f;
    public Camera cam;
    public CharacterController _controller;
    public bool isGrounded;
    public bool canMove;
    [Header("操控类型")]
    public CtrlType ctrlType = CtrlType.player;
    public enum CtrlType  //操控类型
    {
        none,
        player,
        computer,
        net,
    }
  
    private float lastSendInfoTime = float.MinValue;  //网络同步
    //last 上次的位置信息
    Vector3 lPos;
    Vector3 lRot;
    //forecast 预测的位置信息
    Vector3 fPos;
    Vector3 fRot;
    float nspeed;
    //时间间隔
    float delta = 1;
    //上次接收的时间
    float lastRecvInfoTime = float.MinValue;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        _controller = this.GetComponent<CharacterController>();
        if (ctrlType == CtrlType.net)
            _controller.enabled = false;
        InitNetCtrl();
    }

    // Update is called once per frame
    void Update()
    {
        if (ctrlType == CtrlType.player)
        {
            PlayerCtrl();
            InputMagnitude();
        }
        if (ctrlType == CtrlType.net)
        {          
            NetUpdate();
        }
        
    }

    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

       
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;    
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        _controller.Move(desiredMoveDirection * Time.deltaTime * velocity);
        
        // transform.Translate(desiredMoveDirection * Time.deltaTime * velocity);           
    }

    void InputMagnitude()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if (Speed > allowPlayerRotation)
        {
            anim.SetFloat("Blend", Speed);
            PlayerMoveAndRotation();
        }
        else if (Speed < allowPlayerRotation)
        {
            anim.SetFloat("Blend", 0);
        }
    }

    public void PlayerCtrl()
    {
        if (ctrlType != CtrlType.player)
            return;     
        //网络同步
        if (Time.time - lastSendInfoTime > 0.2f)
        {
            SendUnitInfo();
            lastSendInfoTime = Time.time;
        }
    }

    #region 发送位置信息
    public void SendUnitInfo()
    {
        ProtocolBytes proto = new ProtocolBytes();
        proto.AddString("UpdateUnitInfo");  // 协议名：UpdateUnitInfo
        //位置旋转
        Vector3 pos = transform.position;
        Vector3 rot = transform.eulerAngles;
        proto.AddFloat(pos.x);
        proto.AddFloat(pos.y);
        proto.AddFloat(pos.z);
        proto.AddFloat(rot.x);
        proto.AddFloat(rot.y);
        proto.AddFloat(rot.z);
        proto.AddFloat(Speed);
        NetMgr.srvConn.Send(proto);
    }
    #endregion

    public void NetForecastInfo(Vector3 nPos, Vector3 nRot,float speed)
    {
        //预测的位置
        //  fPos = lPos + (nPos - lPos) * 2;
        //  fRot = lRot + (nRot - lRot) * 2;

        fPos = nPos;
        fRot = nRot;
        nspeed = speed;
        if (Time.time - lastRecvInfoTime > 0.3f)
        {
            fPos = nPos;
            fRot = nRot;
            //nspeed =0 ;
        }
        //时间
        delta = Time.time - lastRecvInfoTime;
        //更新
        lPos = nPos;
        lRot = nRot;
        lastRecvInfoTime = Time.time;
    }

    public void InitNetCtrl()
    {
        lPos = transform.position;
        lRot = transform.eulerAngles;    
        fPos = transform.position;
        fRot = transform.eulerAngles;     
    }

    public void NetUpdate()
    {
        //当前位置
        Vector3 pos = transform.position;
        Vector3 rot = transform.eulerAngles;
        if (Vector3.Distance(pos,fPos) <= 0.1)
            nspeed = 0;
        //更新位置
        if (delta > 0)
        {
            anim.SetFloat("Blend", nspeed);
            transform.position = Vector3.Lerp(pos, fPos, delta);
            
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(rot), Quaternion.Euler(fRot), delta);          
        }
    }
}
