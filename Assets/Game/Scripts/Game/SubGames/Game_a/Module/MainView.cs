using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using UnityEngine.UI;

public class MainView :BaseView
{
    public enum vEvent
    {
        vBack,
        vFire,
    }
    public GameObject gunObj;
    public GameObject bullet;
    private Gun gun;
    protected override void Awake()
    {
        base.Awake();
        SetEntity(ControllerManager.GetController<MainController>());
        gun = new Gun(gunObj, bullet);
    }
    protected override void OnOpen(Object param = null)
    {
        base.OnOpen(param);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            Back();
        if (Input.GetMouseButtonDown(0))
            Fire();
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
                Fire();
                break;
            default:
                break;
        }
    }

    void Fire()
    {
        gun.Shut();
    }
    void Back()
    {
        mEntity.Dispath(vEvent.vBack);
    }
}
