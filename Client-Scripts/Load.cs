using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using System.ComponentModel;
public class Load : MonoBehaviour
{
    [SerializeField] static string strNextNmae;
    public static Load instance;
    public Text test;
    public Slider slider;
    [SerializeField] float lerp;
    AsyncOperation async;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
       // print(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Loading")
        {
         
            lerp = 0;
            async = SceneManager.LoadSceneAsync(strNextNmae);
            async.allowSceneActivation = false;
        }
    }


    public void LoadNextScene(string nextName)
    {
        strNextNmae = nextName;
        Debug.Log(strNextNmae);
        SceneManager.LoadScene("Loading");
    }

    void Update()
    {
        if (test && slider)
        {
            lerp = Mathf.Lerp(lerp, async.progress, Time.deltaTime*2.0f);
            test.text = ((int)(lerp / 9 * 10 * 100)).ToString() + "%";
            slider.value = lerp / 9 * 10;
            if (lerp / 9 * 10 >= 0.98)
            {
                test.text = "100%";
                slider.value = 1;
                async.allowSceneActivation = true;
            }
        }


    }
}
