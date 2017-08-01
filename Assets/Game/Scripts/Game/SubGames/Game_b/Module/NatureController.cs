using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using SimpleFramework.Game;
public class NatureController : Controller
{
    protected override void AddViewListenner()
    {
        base.AddViewListenner();
        AddListener(NatureView.vEvent.vBack, Back);
    }
    protected override void RemoveViewListenner()
    {
        base.RemoveViewListenner();
        RemoveListener(NatureView.vEvent.vBack, Back);
    }

    void Back(params object[] objs)
    {
        AppFacade.Ins.GetMgr<GameManager>().GetGame(GameEnum.mainLobby).StartUp();
    }
}
