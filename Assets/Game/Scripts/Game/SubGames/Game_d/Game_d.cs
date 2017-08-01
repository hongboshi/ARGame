using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using SimpleFramework.Game;
using System;

public class Game_d :IGameBase
{
    public Game_d(GameType type, string name) : base(type, name) { }

    protected override string[] sceneName
    {
        get
        {
            return new string[] { "" };
        }
    }
}
