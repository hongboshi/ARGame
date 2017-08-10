using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using SimpleFramework.Game;
using System;

public class Game_SeaFish :IGameBase
{
    public Game_SeaFish(GameType type, string name) : base(type, name) { }
    protected override string[] sceneName
    {
        get
        {
            return new string[] {"SeaFish"};
        }
    }
}
