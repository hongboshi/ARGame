using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using UnityEngine.UI;

public class Game_B_View : BaseView {

    public enum vEvent
    {
        vBack,
    }
    protected override void Awake()
    {
        base.Awake();
        SetEntity(ControllerManager.GetController<Game_B_Controller>());
    }

    protected override void OnClicked(Button sender)
    {
        Debug.Log("clickkkk");
        base.OnClicked(sender);
        if (sender.name.Contains("Back"))
        {
            mEntity.Dispath(vEvent.vBack);
        }
    }
}
