using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
namespace SimpleFramework
{
    public sealed class ModelManager
    {
        private static ModelManager _ins;
        private static ModelManager Ins
        {
            get { if (_ins == null) _ins = new ModelManager(); return _ins; }
        }
        private Dictionary<string, Model> mDic;
        public ModelManager()
        {
            mDic = new Dictionary<string, Model>();
        }
        public static T GetModel<T>() where T : Model
        {
            try
            {
                return (T)Ins.getModel(typeof(T));
            }
            catch
            {
                Debug.LogError("get model null, model type = " + (typeof(T)).Name);
                return null;
            }

        }
        Model getModel(System.Type type)
        {
            Model m = null;
            mDic.TryGetValue(type.Name, out m);
            if (m == null)
            {
                object obj = Assembly.GetAssembly(type).CreateInstance(type.FullName);
                if (obj is Model)
                {
                    m = (Model)obj;
                    mDic.Add(type.Name, m);
                }
            }
            return m;
        }
        public void Init()
        {
            Debug.Log("Model模块初始化");
        }
    }
}
