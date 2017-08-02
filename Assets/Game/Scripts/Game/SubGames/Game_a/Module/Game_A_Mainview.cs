using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using UnityEngine.UI;

public class Game_A_Mainview :BaseView
{
    public enum vEvent
    {
        vBack,
        vFire,
    }
    protected override void Awake()
    {
        base.Awake();
        SetEntity(ControllerManager.GetController<Game_A_Controller>());
    }
    protected override void OnOpen(Object param = null)
    {
        base.OnOpen(param);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Back();
    }
    protected override void OnClicked(Button sender)
    {
        base.OnClicked(sender);
        switch (sender.name)
        {
            case "Back":
                Back();
                break;
            case "Fire":
                break;
            default:
                break;
        }
    }
    void Back()
    {
        mEntity.Dispath(vEvent.vBack);
    }
}
