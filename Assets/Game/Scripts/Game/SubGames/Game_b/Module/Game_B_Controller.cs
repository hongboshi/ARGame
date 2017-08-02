using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using SimpleFramework.Game;
public class Game_B_Controller : Controller
{
    protected override void AddViewListenner()
    {
        base.AddViewListenner();
        AddListener(Game_B_View.vEvent.vBack, Back);
    }
    protected override void RemoveViewListenner()
    {
        base.RemoveViewListenner();
        RemoveListener(Game_B_View.vEvent.vBack, Back);
    }

    void Back(params object[] objs)
    {
        AppFacade.Ins.GetMgr<GameManager>().GetGame(GameEnum.mainLobby).StartUp();
    }
}
