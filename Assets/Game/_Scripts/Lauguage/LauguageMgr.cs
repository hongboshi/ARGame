using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ILauguage
{
    /// <summary>
    /// UI界面text设置
    /// </summary>
    void InitUIText();
}

public enum LauguageType
{
    China,
    English,
    Korea,
    Jopan,


    NULL,
}

/// <summary>
/// 多语言管理类
/// 支持动态切换语言
/// </summary>
class LauguageMgr
{
#region  //内部变量
    //多语言文件路径 （放在streamingAsset目录,方便客户端动态更新）
    public const string lauguageConfigFilePath = "";
    private List<ILauguage> lauList;
    private LauguageData data;
    private static LauguageMgr _ins;
    public static LauguageMgr GetIns() { if (_ins == null) _ins = new LauguageMgr(); return _ins; }
    private LauguageMgr()
    {
        lauList = new List<ILauguage>();
        data = new LauguageData(lauguageConfigFilePath);
    }
    public LauguageType curLauguage
    {
        get { return data.curLauguage; }
    }
    #endregion
#region //外部接口
    public void SetLauguage(LauguageType type)
    {
        data.SetLauguageType(type);
    }
    /// <summary>
    /// 通过索引得到对应的值
    /// </summary>
    /// <param name="key">索引键</param>
    /// <param name="beizu">备注（方便编辑 维护）</param>
    /// <returns></returns>
    public string GetText(string key,string beizu = "")
    {
        string value = data.GetText(key);
#if UNITY_EDITOR 
        if (string.Empty == value)
            data.SetBeizu(key, beizu);
#endif
        return value;
    }
    public void ChangeLauguage(LauguageType type)
    {
        if (type == curLauguage) return;
        SetLauguage(type);
        lauList.ForEach((x) => {
            x.InitUIText();
        });
    }
    //public void SaveToFile()
    //{
    //    data.DeSerialize();
    //}
    public void AddILauguage(ILauguage lau)
    {
        lauList.Add(lau);
    }
    public void RemoveILauguage(ILauguage lau)
    {
        lauList.Remove(lau);
    }
#endregion
}
