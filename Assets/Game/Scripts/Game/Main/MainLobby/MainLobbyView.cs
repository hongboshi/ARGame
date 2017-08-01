using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using UnityEngine.UI;
using SimpleFramework.Game;
public class MainLobbyView : BaseView
{
    protected override void OnClicked(Button sender)
    {
        base.OnClicked(sender);
        string name = sender.name;
        if (name.Contains("game_"))
        {
            IGameBase gb = AppFacade.Ins.GetMgr<GameManager>().GetGame(name);
            if (gb != null) gb.StartUp();
        }
        else if (name.Contains("Setting"))
        {

        }
    }
}
