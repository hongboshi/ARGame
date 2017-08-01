using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
public class HomeScene_State :ISceneState
{
    public HomeScene_State(SceneStateController Controller) : base(Controller)
    { }
    public override void StateBegin()
    {
        base.StateBegin();
       
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
  //      Debug.Log("i'm home scene updae");
    }
}
