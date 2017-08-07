using UnityEngine;
using System.Collections.Generic;

namespace SimpleFramework
{
    public class AssetScriptsMgr
    {
        Dictionary<string, AssetScriptRelationship> assetScriptDic;
        private static AssetScriptsMgr _ins;
        public static AssetScriptsMgr getIns()
        {
            if (_ins == null)
                _ins = new AssetScriptsMgr();
            return _ins;
        }
        private AssetScriptsMgr()
        {
            assetScriptDic = new Dictionary<string, AssetScriptRelationship>();
        }
        public AssetScriptRelationship getAssetScriptRelationship(string assetName)
        {
            if (assetScriptDic.ContainsKey(assetName))
                return assetScriptDic[assetName];
            return null;
        }

        public class AssetScriptRelationship
        {
            string rootObjName;                  //资源名       
            Dictionary<string, string> tranScriptDic;   //脚本列表（子节点名，脚本名）

            public AssetScriptRelationship(string rootname)
            {
                rootObjName = rootname;
                tranScriptDic = new Dictionary<string, string>();
            }
            public void BindScripts(Object root)
            {
                Debug.Log("绑定资源上的脚本");
                if (root.name.Contains(rootObjName))
                {
                    Transform t;
                    Transform rootTrans = ((Transform)root);
                    System.Type type = null;

                    foreach (KeyValuePair<string, string> kp in tranScriptDic)
                    {
                        t = null;
                        t = rootTrans.Find(kp.Key);
                        if (t != null)
                        {
                            type = DllManager.GetType(kp.Value);
                            addComponent(t, type);
                        }
                    }
                }
            }
            void addComponent(Transform t, System.Type type)
            {
                Component c = t.GetComponent(type);
                if (c != null)
                    GameObject.DestroyImmediate(c);
                t.gameObject.AddComponent(type);
            }
        }
    }
}
