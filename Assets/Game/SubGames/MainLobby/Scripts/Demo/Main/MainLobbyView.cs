using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework;
using UnityEngine.UI;
using SimpleFramework.Game;

public class MainLobbyView : BaseView
{
 
    public GameItemData[] gameNames = new GameItemData[] { new GameItemData(GameEnum.SeaFish,"西游记",""),new GameItemData(GameEnum.b,"斗破苍穹",""),new GameItemData(GameEnum.c,"战狼",""),new GameItemData(GameEnum.d,"天龙八部",""),new GameItemData(GameEnum.e,"金瓶梅",""), new GameItemData(GameEnum.f,"屠龙宝刀",""), new GameItemData(GameEnum.g,"深海捕鱼",""),new GameItemData(GameEnum.h,"丧尸围城","") };
    protected override void Start()
    {
        base.Start();
        GameItem[] gItems = GetComponentsInChildren<GameItem>();
        int min = Mathf.Min(gameNames.Length, gItems.Length);
        for (int i = 0; i < min; i++)
        {
            gItems[i].SetGameTex(gameNames[i]);
        }
    }
    protected override void OnClicked(Button sender)
    {
        base.OnClicked(sender);
        string name = sender.name;
        GameItem gi = sender.GetComponent<GameItem>();
        if (gi != null)
        {
            IGameBase gb = AppFacade.Ins.GetMgr<GameManager>().GetGame(gi.data.title);
            Debug.Log("onclicked name = "+gi.data.title);
            if (gb != null) gb.StartUp();
            return;
        }
        if (name.Contains("Setting"))
        {

        }
    }
}
