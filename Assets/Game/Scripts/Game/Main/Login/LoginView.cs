using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using UnityEngine.UI;
using System;
public class LoginView : BaseView {

    public enum vEvent
    {
        vLogin_wechat,
        vLogin_guest,
        //vRegiest,
    }
    public Button loginByWechat;
    public Button loginByGuest;
    public Text tip;

    protected override void Start()
    {
        base.Start();
        SetEntity(ControllerManager.GetController<LoginController>());
        Init();
    }

    void Init()
    {
        if (loginByWechat == null || loginByGuest == null)
            Debug.LogError("null check");
        loginByWechat.onClick.AddListener(Login_wechat);
        loginByGuest.onClick.AddListener(Login_guest);  
    }

    void Login_wechat()
    {
        //if (account.text == "" || pwd.text == "")
        //{
        //    ShowTip("请输入账号或密码");
        //    return;
        //}
        //mEntity.Dispath(vEvent.vLogin, account.text, pwd.text);
        mEntity.Dispath(vEvent.vLogin_wechat);
    }
    void Login_guest()
    {
        mEntity.Dispath(vEvent.vLogin_guest);
    }
    void ShowTip(string str)
    {
        tip.text = str;
    }
}
