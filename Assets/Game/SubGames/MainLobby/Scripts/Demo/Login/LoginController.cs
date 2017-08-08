using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using SimpleFramework.Game;

public class LoginController :Controller
{
    protected override void AddViewListenner()
    {
        base.AddViewListenner();
        AddListener(LoginView.vEvent.vLogin_wechat, OnLogin_wechat);
        AddListener(LoginView.vEvent.vLogin_guest, OnLogin_guest);
    }
    protected override void RemoveViewListenner()
    {
        base.RemoveViewListenner();
        RemoveListener(LoginView.vEvent.vLogin_wechat, OnLogin_wechat);
        RemoveListener(LoginView.vEvent.vLogin_guest, OnLogin_guest);
    }

    public override void UpdateDo(float time)
    {
        base.UpdateDo(time);
        ModelManager.GetModel<LoginModel>().ShowAcc();
    }

    void OnLogin_wechat(params object[] objs)
    {
        bool result = true;

        if (result)
        {
            ModelManager.GetModel<LoginModel>().SetAccPwd("ss", "dd");
            AppFacade.Ins.GetMgr<SceneMgr>().LoadScene("mainLobby");
        }
        else
        { }
    }
    void OnLogin_guest(params object[] objs)
    {
        bool result = true;

        if (result)
        {
            ModelManager.GetModel<LoginModel>().SetAccPwd("ss", "dd");
            // AppFacade.Ins.GetMgr<SceneMgr>().LoadScene("main");  
            IGameBase.NowActiveGame.LoadScene(0);
        }
        else
        { }
    }
    void OnRegiest(params object[] objs)
    { }
}
