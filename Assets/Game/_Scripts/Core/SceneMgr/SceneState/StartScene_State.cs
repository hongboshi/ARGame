using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
/// <summary>
/// 启动场景
/// </summary>
public class StartScene_State : ISceneState
{
    public StartScene_State(SceneStateController Controller) : base(Controller)
    {
        
    }

    public override void StateBegin()
    {
        base.StateBegin();
        Debug.Log("i'm startscene_state stateBegin");
    }

    public override void StateEnd()
    {
        base.StateEnd();
        Debug.Log("i'm startscene_state stateend");
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Debug.Log("i'm startscene_state update");
    }
}
