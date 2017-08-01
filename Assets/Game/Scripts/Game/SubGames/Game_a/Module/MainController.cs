using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using SimpleFramework.Game;
public class MainController : Controller
{
    protected override void AddViewListenner()
    {
        base.AddViewListenner();
        AddListener(MainView.vEvent.vBack, Back);
        AddListener(MainView.vEvent.vFire, Fire);
    }

    protected override void RemoveViewListenner()
    {
        base.RemoveViewListenner();
        RemoveListener(MainView.vEvent.vBack, Back);
        RemoveListener(MainView.vEvent.vFire, Fire);
    }

    void Back(params object[] objs)
    {
        AppFacade.Ins.GetMgr<GameManager>().GetGame(GameEnum.mainLobby).StartUp();
    }
    void Fire(params object[] objs)
    {
        AppFacade.Ins.Dispath(GEvent.gEvent.gShotting);
    }
}
