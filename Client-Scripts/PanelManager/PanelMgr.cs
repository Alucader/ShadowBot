using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public enum PanelLayer
{
    //面板
    Panel,
    //提示
    Tips,
}
public class PanelMgr : MonoBehaviour
{
    //单例
    public static PanelMgr instance;
    //画布
    private GameObject canvas;
    //面板字典
    public Dictionary<string, PanelBase> dict;
    //层级字典
    private Dictionary<PanelLayer, Transform> layerDict;
    public void Awake()
    {
        instance = this;
        InitLayer();
        dict = new Dictionary<string, PanelBase>();
    }

    #region 初始化层
    private void InitLayer()
    {
        //画布
        canvas = GameObject.Find("Canvas");
        if (canvas == null)
            Debug.LogError("panelMgr.InitLayer 失败, Canvas 为空");
        //各个层级
        layerDict = new Dictionary<PanelLayer, Transform>();

        foreach (PanelLayer pl in Enum.GetValues(typeof(PanelLayer)))
        {            
            string name = pl.ToString();
            Debug.Log(pl.ToString());
            Transform transform = canvas.transform.Find(name);
            if (transform == null)
            {
                Debug.Log("未找到"+ name);
            }
            layerDict.Add(pl, transform);
        }
    }
    #endregion 

    #region 打开面板
    public void OpenPanel<T>(string skinPath, params object[] args) where T : PanelBase
    {
     //   try
     //    {
            //已经打开
            string name = typeof(T).ToString();
           // Debug.Log(name);
            if (dict.ContainsKey(name))
            {
                ClosePanel(name);
            }
                //return;
            //面板脚本
            PanelBase panel = canvas.AddComponent<T>();
            panel.Init(args);
            dict.Add(name, panel);
            //加载预设体
            skinPath = (skinPath != "" ? skinPath : panel.skinPath);
            GameObject skin = Resources.Load<GameObject>(skinPath);
            if (skin == null)
                Debug.LogError("panelMgr.OpenPanel fail, skin is null,skinPath = " + skinPath);
            panel.skin = (GameObject)Instantiate(skin);
            //坐标
            Transform skinTrans = panel.skin.transform;
            PanelLayer layer = panel.layer;

            Transform parent = layerDict[layer];
            skinTrans.SetParent(parent, false);

            //panel的生命周期
            panel.OnShowing();
            panel.OnShowed();
           // Debug.Log("打开完毕");
     /*   }
        catch (Exception e)
        {
            Debug.Log(e.Message);
           // dict.Remove(typeof(T).ToString());
        }*/
    }
    #endregion 

    #region 关闭
    public void ClosePanel(string name)
    {
        PanelBase panel = (PanelBase)dict[name];
        if (panel == null)
            return;
        panel.OnClosing();
        dict.Remove(name);
        panel.OnClosed();
       // panel.skin.transform.DOScaleY(0, 0.1f);
        GameObject.Destroy(panel.skin);
        Component.Destroy(panel);
    }
    #endregion 
}


