using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFramework.Game;

namespace SimpleFramework
{
    public abstract class Controller : Entity, IFrameUpdate
    {
        internal Controller()
        {
            FrameUpdateManager.Instance.AddFrame(this);
            AddViewListenner();
            Debug.Log("init controller");
        }
        public virtual void UpdateDo(float time)
        { }
        ~Controller()
        {
            FrameUpdateManager.Instance.RemoveFrame(this);
            RemoveViewListenner();
        }

        protected virtual void AddViewListenner()
        {

        }
        protected virtual void RemoveViewListenner()
        {

        }
    }
}
