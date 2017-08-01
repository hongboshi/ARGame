using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void EntityDel(params object[] objs);

namespace SimpleFramework
{
    /// <summary>
    /// 消息实体 
    /// </summary>
    public class Entity
    {
        Dictionary<Enum, EntityDel> _delDic;

        public void Dispath(Enum e, params object[] objs)
        {
            EntityDel del = null;
            _delDic.TryGetValue(e, out del);
            if (del != null)
                del(objs);
            else
            {
                Debug.LogWarning("entity is didnot add listenner, enum = "+e);
            }
        }
        public Entity()
        {
            _delDic = new Dictionary<Enum, EntityDel>();
        }
        public void AddListener(Enum e, EntityDel del)
        {
            EntityDel d = null;
            _delDic.TryGetValue(e, out d);
            if (d == null)
            {
                _delDic.Add(e, del);
            }
            else Delegate.Combine(d, del);
        }
        public void RemoveListener(Enum e, EntityDel del = null)
        {
            EntityDel d = null;
            _delDic.TryGetValue(e, out d);
            if (d == null) return;
            if (del != null)
                Delegate.Remove(d, del);
            else
                _delDic.Remove(e);
        }
    }
    public class GlobalEntity :Entity
    {

    }
}
