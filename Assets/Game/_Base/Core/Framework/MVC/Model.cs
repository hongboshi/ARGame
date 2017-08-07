using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace SimpleFramework
{
    /// <summary>
    /// model层
    /// 1，数据层
    /// 2，也可放置常用的api
    /// </summary>
    public abstract class Model : IFrameUpdate
    {
        //private ModelManager modelManager = ModelManager.Instance;
        public Model()
        {
            FrameUpdateManager.Instance.AddFrame(this);
        }

        public virtual void UpdateDo(float time)
        { }

        ~Model()
        {
            FrameUpdateManager.Instance.RemoveFrame(this);
        }
    }
}
