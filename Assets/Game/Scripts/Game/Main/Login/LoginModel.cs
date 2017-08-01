using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;

public class LoginModel : Model
{
    public string acc { get; private set; }
    private string pwd;

    public void SetAccPwd(string acc, string pwd)
    {
        Debug.Log("SET ACC AND PWD");
        this.acc = acc;
        this.pwd = pwd;
    }

    public void ShowAcc()
    {
        Debug.Log("acc ="+acc);
    }
}
