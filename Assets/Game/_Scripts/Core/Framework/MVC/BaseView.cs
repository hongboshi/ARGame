using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFramework
{
    public abstract class BaseView : MonoBehaviour
    {
        private bool isOpen = false;
        protected Entity mEntity;
        protected Button[] mbtns;
        protected virtual void Awake()
        {
        }
        protected void SetEntity(Entity entity)
        {
            mEntity = entity;
        }
        // Use this for initialization
        protected virtual void Start()
        {
            Open(null);
        }
        public void Open(Object param = null)
        {
            gameObject.SetActive(true);
            if (isOpen) return;
            isOpen = true;
            if (mbtns == null || mbtns.Length == 0)
            {
                mbtns = GetComponentsInChildren<Button>();
                Debug.Log("btn length = "+mbtns.Length);
                for (int i = 0; i < mbtns.Length; i++)
                {
                    Button btn = mbtns[i];
                    btn.onClick.AddListener(() => { OnClicked(btn); });
                }
                  //  mbtns[i].onClick.AddListener(() => { Debug.Log("current clicked i = "+i); OnClicked(mbtns[i]); });
            }
            OnOpen(param);
        }
        protected virtual void OnOpen(Object param = null) { }
        protected virtual void OnClose(Object param = null) { }
        
        public void Close(Object param = null)
        {
            gameObject.SetActive(false);
            if (!isOpen) return;
            isOpen = false;
            OnClose(param);
        }
        protected virtual void OnClicked(Button sender)
        {
            
        }
    }
}
