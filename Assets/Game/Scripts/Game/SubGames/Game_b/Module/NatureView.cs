using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using UnityEngine.UI;

public class NatureView : BaseView {

    public enum vEvent
    {
        vBack,
    }
    protected override void Awake()
    {
        base.Awake();
        SetEntity(ControllerManager.GetController<NatureController>());
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
