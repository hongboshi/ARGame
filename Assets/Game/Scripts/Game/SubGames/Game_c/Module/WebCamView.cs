using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleFramework;

public class WebCamView :BaseView
{
    public enum vEvent
    {
        vBack,
    }
    public GameObject renderObj;
    public string deviceName;
    WebCamTexture tex;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        SetEntity(ControllerManager.GetController<WebCamController>());
        OpenCamera();
    }
    bool flag = true;
    GameObject tempObj = null;
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            if(tempObj == null)
                tempObj = GameObject.Find("Image");
            flag = !flag;
            if (tempObj != null) tempObj.SetActive(flag);
        }
    }

    protected override void OnClicked(Button sender)
    {
        base.OnClicked(sender);
        string name = sender.name;
        switch (name)
        {
            case "Back":
                CloseCamera();
                mEntity.Dispath(vEvent.vBack);
                break;
        }
    }

    void OpenCamera()
    {
        StartCoroutine(CallCamera());
    }
    IEnumerator CallCamera()
    {
        Debug.Log("开始录像");
        var op = Application.RequestUserAuthorization(UserAuthorization.WebCam);
        yield return op;
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            if (devices.Length == 0) yield break;
            deviceName = devices[0].name;
            tex = new WebCamTexture(deviceName, 400, 300, 10);
            renderObj.GetComponent<Renderer>().material.mainTexture = tex;
            tex.Play();
        }
    }
    void CloseCamera()
    {
        Debug.Log("录像结束");
        if (tex != null)
            tex.Stop();
    }
}
