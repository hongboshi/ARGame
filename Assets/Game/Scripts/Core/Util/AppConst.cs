﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public static class AppConst
{
    public static string serAddr;
    public static string serPort;
    //编辑模式下
    public static bool isEditorDebug {
        get {
            return true;
        }
    }
    public static bool openDebugRecord
    {
        get { return true; }
    }
    public static RuntimePlatform Platform
    {
        get { return Application.platform; }
    }
    public static bool IsPcPlat
    {
        get {
            return true; //return (Platform == RuntimePlatform.WindowsEditor || Platform == RuntimePlatform.WindowsPlayer);
        }
    }
    public static bool IsIosPlat
    {
        get {
            return Platform == RuntimePlatform.IPhonePlayer;
        }
    }
    public static bool IsAndroidPlat
    {
        get {
            return Platform == RuntimePlatform.Android;
        }
    }
}
