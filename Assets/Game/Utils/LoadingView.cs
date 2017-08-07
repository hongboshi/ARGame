using System.Collections;
using UnityEngine.SceneManagement;
using SimpleFramework;
using UnityEngine;

public class LoadingView : BaseView
{
    private UnityEngine.AsyncOperation async;
    public static string loadingScene;
    public UnityEngine.UI.Image pImage;
    protected override void Start()
    {
        base.Start();
        nowProgress = 0;
        StartCoroutine(LoadAsync(loadingScene));
    }
    bool isBegin;
    int nowProgress;
    IEnumerator LoadAsync(string sname)
    {
        isBegin = true;
        async = SceneManager.LoadSceneAsync(sname);
        async.allowSceneActivation = false;
        yield return async;
        Debug.Log("not yeit");
   //     async.completed += LoadOver;
    }
    void loadAsync(string sname)
    {
        isBegin = true;
        async = SceneManager.LoadSceneAsync(sname);
        async.allowSceneActivation = false;
    }

    void LoadOver()
    {
        isBegin = false;
        LoadingBar(100);
        async.allowSceneActivation = true;
        async = null;
    }

    void Update()
    {
        if (!isBegin) return;
        if (async.progress <= 0.89f) //加载中
        {
            LoadingBar((int)(100 * async.progress));
            return;
        }
        isBegin = false;
        LoadOver();
    }

    void LoadingBar(int value)
    {
        while (nowProgress < value)
        {
            nowProgress += 1;
            pImage.fillAmount = nowProgress * 0.01f;
        }
    }
}
