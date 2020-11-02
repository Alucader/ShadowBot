using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr instance;
    public ProtocolBytes protocol;
    public string id = "Bot";
    public int camp = 0;
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
          //  gameObject.AddComponent<Battle>();
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Application.runInBackground = true;      
    }
    void Update()
    {

        NetMgr.Update();
    }
}


