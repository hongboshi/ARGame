using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using SimpleFramework.Game;
using System;

public class Game_MainLobby : IGameBase
{
    public Game_MainLobby(GameType type, string gName) : base(type, gName)
    {
    }

    protected override string[] sceneName
    {
        get
        {
            return new string[] {"mainLobby" };
        }
    }

    protected override void RegiestScene()
    {
        AppFacade.Ins.GetMgr<SceneMgr>().RegiestScene(sceneName[0], new MainScene_State(SceneMgr.mController));
    }
}
