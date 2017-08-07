using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
namespace SimpleFramework
{
    public sealed class ModelManager
    {
        private static ModelManager _ins;
        private Dictionary<string, Model> mDic;
        public ModelManager()
        {
            _ins = this;
            mDic = new Dictionary<string, Model>();
        }
        public static T GetModel<T>() where T : Model
        {
            try
            {
                return (T)_ins.getModel(typeof(T));
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
