using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
namespace SimpleFramework
{
    public sealed class ControllerManager
    {
        private Dictionary<string, Controller> cDic;
        private static ControllerManager _ins;
        private static ControllerManager ins { get { if (_ins == null) _ins = new ControllerManager(); return _ins; } }
        public static T GetController<T>() where T : Controller
        {
            try
            {
                return (T)ins.getC(typeof(T));
            }
            catch
            {
                return null;
            }
        }
        public ControllerManager()
        {
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

        }
    }
}