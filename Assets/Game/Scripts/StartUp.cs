using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFramework;
using SimpleFramework.Game;

public class StartUp : MonoBehaviour {

    public enum vEvent
    {
        vMoviePlayOver, //视频播放完毕
    }

    public string serAddr;
    public string serPort;
    public float startMovieTime;  //启动视频的时长
    private AppMovie startMovie;
    void Awake()
    {
        AppConst.serPort = serAddr;
       AppConst.serPort = serPort;
    }

	// Use this for initialization
	void Start () {
        AppFacade.Ins.StartUp();   //启动框架
        startMovie = new AppMovie(startMovieTime);   //播放开始动画
        AppFacade.Ins.AddListener(vEvent.vMoviePlayOver, OnLoadOver);
        startMovie.Play(() =>
        {
            AppFacade.Ins.Dispath(vEvent.vMoviePlayOver);
        });
      //  OnLoadOver(null);
    }
    void OnLoadOver(params object[] objs)
    {
        AppFacade.Ins.GetMgr<GameManager>().GetGame(GameEnum.mainLobby).StartUp();
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