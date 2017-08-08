using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
/// <summary>
/// 登陆场景
/// </summary>
public class LoginScene_State : ISceneState
{
    public LoginScene_State(SceneStateController Controller) : base(Controller)
    {
    }
    public override void StateBegin()
    {
        base.StateBegin();
        Debug.Log("i'm LoginScene_State stateBegin");
    }
    public override void StateEnd()
    {
        base.StateEnd();
        Debug.Log("i'm LoginScene_State StateEnd");
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        Debug.Log("i'm LoginScene_State StateUpdate");
    }
}
