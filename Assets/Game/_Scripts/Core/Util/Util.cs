using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static T GetComponentIfNoExist<T>(this Transform trans) where T : Component
    {
        T t = trans.GetComponent<T>();
        if (t == null) t = trans.gameObject.AddComponent<T>();
        return t;
    }
    public static T GetComponentIfNoExist<T>(this GameObject obj) where T : Component
    {
        T t = obj.GetComponent<T>();
        if (t == null) t = obj.AddComponent<T>();
        return t;
    }
}
