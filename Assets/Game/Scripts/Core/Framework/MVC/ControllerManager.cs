using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
namespace SimpleFramework
{
    public sealed class ControllerManager
    {
        private static ControllerManager _ins;
        private Dictionary<string, Controller> cDic;
        public static T GetController<T>() where T : Controller
        {
            try
            {
                return (T)_ins.getC(typeof(T));
            }
            catch
            {
                return null;
            }
        }
        //public T GetController<T>() where T : Controller
        //{
        //    try
        //    {
        //        return (T)getC(typeof(T));
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        public ControllerManager()
        {
            _ins = this;
            cDic = new Dictionary<string, Controller>();
        }
        Controller getC(System.Type type)
        {
            Controller c = null;
            cDic.TryGetValue(type.Name, out c);
            if (c == null)
            {
                object obj = Assembly.GetAssembly(type).CreateInstance(type.FullName);
                if (obj is Controller)
                {
                    c = (Controller)obj;
                    cDic.Add(type.Name, c);
                }
            }
            return c;
        }
        //初始化
        public void Init()
        {
            Debug.Log("controller模块初始化");
        }
    }
}