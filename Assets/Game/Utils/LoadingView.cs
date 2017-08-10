using System.Collections;
using UnityEngine.SceneManagement;
using SimpleFramework;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingView : BaseView
{
    private UnityEngine.AsyncOperation async;
    public static string loadingScene;
    public UnityEngine.UI.Image pImage;
    Text progressText;
    protected override void Start()
    {
        base.Start();
        pImage.fillAmount = 0;
        LoadAsync(loadingScene);
        progressText = GameObject.Find("progressText").GetComponent<Text>();
    }
    bool isBegin;

    void LoadAsync(string name)
    {
        isBegin = true;
        async = SceneManager.LoadSceneAsync(name);
        async.allowSceneActivation = false;
        //柔和加载
        Loading(60);
    }
    void Loading(int value)
    {
        if (isBegin)
        {
            LoadingBar(value, 1.5f).OnComplete(() =>
            {
                Loading(value+(int)(value*0.2));
            });
        }
        else
        {
            LoadingBar(100, 1f).OnComplete(LoadOver);
        }

    }

    void LoadOver()
    {
        isBegin = false;
        async.allowSceneActivation = true;
        async = null;
    }

    void Update()
    {
        if (!isBegin) return;
        if (async.progress <= 0.89f) //加载中
        {
            //LoadingBar((int)(100 * async.progress));
            return;
        }
        isBegin = false;
       // LoadOver();
    }

    Tweener LoadingBar(int value, float duration)
    {
        Tweener tw = pImage.DOFillAmount(value * 0.01f, duration);
        tw.OnUpdate(()=> { progressText.text = ((int)(tw.fullPosition*100)).ToString()+"%"; } );
        return tw;
    }
    //void ShowProgressText()
    //{
    //    progressText.text = 
    //}
}
