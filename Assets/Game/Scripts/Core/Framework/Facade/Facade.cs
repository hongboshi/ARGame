using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using SimpleFramework.Game;
namespace SimpleFramework
{
    public abstract class Facade
    {
        private Dictionary<string, object> managerDic;
        private GlobalEntity gEntity;
        public Facade()
        {
           
        }
        private GameObject _gGameobject;
        public GameObject gGameObject
        {
            get
            {
                if (_gGameobject == null)
                {
                    _gGameobject = new GameObject("gGameObject");
                    GameObject.DontDestroyOnLoad(_gGameobject);
                }
                return _gGameobject;
            }
        }
        private GlobalMono _gMono;
        public GlobalMono gMono
        {
            get { if (_gMono == null) _gMono = gGameObject.AddComponent<GlobalMono>();  return _gMono; }
        }
        public virtual void StartUp()
        {
            InitFramework();
        }
        #region 获取管理器
        public T GetMgr<T>() where T : class
        {
            try
            {
                return (T)GetManager(typeof(T));
            }
            catch(System.Exception ex)
            {
                
                Debug.LogError(string.Format("get manager null, manager = {0}, error =" ,typeof(T).Name,ex));
                return null;
            }
}

        object GetManager(System.Type type)
        {
            object oo = null;
            managerDic.TryGetValue(type.Name, out oo);
            if (oo == null)
            {
                oo = Assembly.GetAssembly(type).CreateInstance(type.FullName);
                managerDic.Add(type.Name, oo);
            }
            return oo;
        }
        #endregion
        #region 全局事件
        public void Dispath(System.Enum e, params object[] objs)
        {
            gEntity.Dispath(e, objs);
        }
        public void AddListener(System.Enum e, EntityDel del)
        {
            gEntity.AddListener(e, del);
        }
        public void RemoveListener(System.Enum e, EntityDel del)
        {
            gEntity.RemoveListener(e, del);
        }
        #endregion
        public void SendCommand()
        {

        }
        void InitFramework()
        {
            Debug.Log("启动框架");
            managerDic = new Dictionary<string, object>();
            gEntity = new GlobalEntity();
            GetMgr<ControllerManager>().Init();
            GetMgr<ModelManager>().Init();
            GetMgr<GameManager>();
            GetMgr<SceneMgr>();
        }
    }
}
