using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using SimpleFramework.Game;

public class Game_A_Controller : Controller
{
    protected override void AddViewListenner()
    {
        base.AddViewListenner();
        AddListener(Game_A_Mainview.vEvent.vBack, Back);
    }

    protected override void RemoveViewListenner()
    {
        base.RemoveViewListenner();
        RemoveListener(Game_A_Mainview.vEvent.vBack, Back);
   
    }

    void Back(params object[] objs)
    {
        AppFacade.Ins.GetMgr<GameManager>().GetGame(GameEnum.mainLobby).StartUp();
    }
}
