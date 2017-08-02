using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public struct GameItemData
{
    public string title;
    public string name;
    public string desc;
    public GameItemData(string title, string name = "", string desc = "")
    {
        this.name = name;
        this.desc = desc;
        this.title = title;
    }
}
public class GameItem : MonoBehaviour {
    public GameItemData data;
    public Image gameImage;
    public Text gameText;

    private void Awake()
    {
        gameImage = GetComponentInChildren<Image>();
        gameText = GetComponentInChildren<Text>();
    }

    public void SetGameTex(GameItemData data, UnityEngine.Sprite sp = null)
    {
        this.data = data;
        gameText.text = data.name;
        if(sp != null)
            gameImage.sprite = sp;
    }
}
