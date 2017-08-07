using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using SimpleFramework.Game;

public enum AppEvent
{
    appLog,  //log
    resLoadOver,   //资源更新完毕


}
public class AppFacade :Facade 
{

    #region 变量
    private static AppFacade _ins;
    public static AppFacade Ins {
        get { if (_ins == null) _ins = new AppFacade();return _ins; }
    }
    private AppFacade() :base()
    { }
    #endregion
    public override void StartUp()
    {
        base.StartUp();  
        //更新资源
        AppFacade.Ins.GetMgr<ResourceManager>().UpdateAssets(result => {
            Dispath(AppEvent.resLoadOver, result);  //消息通知出去
        });
    }

}
