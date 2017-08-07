using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFramework;
using SimpleFramework.Game;

public class StartUp : MonoBehaviour {

    public string serAddr;
    public string serPort;
    public Text tipArea;
    public float startMovieTime;  //启动视频的时长
    private AppMovie startMovie;
    void Awake()
    {
        AppConst.serPort = serAddr;
        AppConst.serPort = serPort;
    }

	// Use this for initialization
	void Start () {
        AppFacade.Ins.AddListener(AppEvent.appLog, UpdateTips);  //debug重定向
        AppFacade.Ins.AddListener(AppEvent.resLoadOver, OnResLoadOver);  //监听资源更新结果
        AppFacade.Ins.StartUp();   //启动框架    

        startMovie = new AppMovie(startMovieTime);   //播放开始动画
        startMovie.Play(() =>
        {
            isMovieOver = true;
            OnLoadOver();
        });     
    }
    private void OnDestroy()
    {
        AppFacade.Ins.RemoveListener(AppEvent.appLog, UpdateTips);
        AppFacade.Ins.RemoveListener(AppEvent.resLoadOver, OnResLoadOver);
    }

    void UpdateTips(params object[] objs)
    {
        if (objs.Length == 2)
        {
            BugType bt = (BugType)objs[0];
            string msg = (string)objs[1];
            if (tipArea != null)
                tipArea.text += msg+"\n";
        }
    }

    bool isMovieOver = false;
    bool isResLoadOver = false;
    void OnLoadOver()
    {
        AppFacade.Ins.GetMgr<ResourceManager>().LoadAsset("bb", (obj) => {
            Debug.Log(obj.name);
            GameObject oo = GameObject.Instantiate(obj) as GameObject;
        });
        //if(isMovieOver && isResLoadOver)
        //    AppFacade.Ins.GetMgr<GameManager>().GetGame(GameEnum.mainLobby).StartUp();
    }
    void OnResLoadOver(params object[] objs)
    {
        bool result = (bool)objs[0];
        isResLoadOver = result;
        if (!isResLoadOver) Debug.LogError("res down load error");
        OnLoadOver();
    }
}

/// <summary>
/// 启动movie
/// 资源必须放在streamAssets下面
/// </summary>
public class AppMovie
{
  //  private MovieTexture _mTex;
    private float movieTime;
    private bool _init;
    public AppMovie(float time)
    {
        movieTime = time;
        //AppFacade.Ins.GetMgr<ResourceManager>().LoadAsset("startMovie", (Object obj) =>
        //{
        //    if (AppConst.IsPcPlat)
        //    {
        //        if (obj != null)
        //        {
        //            _mTex = obj as MovieTexture;
        //        }
        //    }
        //});
    }

    public void Play(System.Action theEnd)
    {
        AppFacade.Ins.gMono.StartCoroutine(GlobalMono.DelaySometimeExcute(movieTime, theEnd));
        if (AppConst.IsPcPlat)
        {
            //if (_mTex != null)
            //{
            //    _mTex.Play();
            //    _mTex.loop = true;
            //}
        }
    }
}