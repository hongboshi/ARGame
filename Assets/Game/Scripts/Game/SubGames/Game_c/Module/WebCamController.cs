using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using SimpleFramework.Game;

public class WebCamController : Controller
{
    protected override void AddViewListenner()
    {
        base.AddViewListenner();
        AddListener(WebCamView.vEvent.vBack, Back);
    }
    void Back(params object[] objs)
    {
        AppFacade.Ins.GetMgr<GameManager>().GetGame(GameEnum.mainLobby).StartUp();
    }
}
