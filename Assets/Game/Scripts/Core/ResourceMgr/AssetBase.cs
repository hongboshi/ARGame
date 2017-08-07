using UnityEngine;
using System.IO;
//using System;
using System.Collections.Generic;

namespace SimpleFramework
{
    /// <summary>
    /// 资源管理类
    /// 负责所有资源更新，下载，加载
    /// </summary>
    public class AssetBase 
    {
        private AssetbundleConfig abConfig;
        private AssetBundleLoader loader;
        protected GameAssetConfig gameAssetConfig;
        private AssetVersion curAssetVersion;
        protected Dictionary<string, Object> gloabalSourceDic;   //全局的 完整生命周期
        protected Dictionary<string, Object> sceneSourceDic;     //跟随场景的

        protected int assetVersion_server;

        public AssetBase()
        {
            gloabalSourceDic = new Dictionary<string, Object>();
            sceneSourceDic = new Dictionary<string, Object>();
            curAssetVersion = getAssetinfoFromLocal();
            abConfig = loadABConfig();
            gameAssetConfig = loadGameAssetConfig();
            if (curAssetVersion == null)
            {
                Debug.LogError("curAssetVersion null");
            }
            else
            {
                Debug.Log("本地资源版本号：" + curAssetVersion.assetVersion);
            //    AppFacade.Ins.Log(BugType.log, "本地资源版本号"+ curAssetVersion.assetVersion);
            }
            loader = new AssetBundleLoader();
        }
        //资源初始化
        protected void AssetsInit(System.Action<bool> whenOver)
        {
            init(whenOver);
        }

        void init(System.Action<bool> whenOver)
        {
            if (checkUpdate())                                           //检查资源是否需要更新
            {
                Debug.Log("更新资源配置信息");
                pullAssetinfoFromSer((AssetVersion newAsset) =>
                {
                    Debug.Log("更新资源");
                    updateAssets(newAsset, (bool result) =>
                    {
                        if (result)
                        {
                            Debug.Log("资源下载完毕");
                            AssetbundleHelp.UnLoadBundles();
                            FileStream file = File.Create(AssetBundleInfo.assetPath_local + "/" + "assetVersionInfo.txt");
                            StreamWriter sw = new StreamWriter(file);
                            sw.Write(newAsset.ToStr());
                            sw.Close();
                            file.Close();
                            if (whenOver != null)
                                whenOver(true);
                        }
                        else if (whenOver != null) whenOver(false);
                    });

                });
            }
            else
            {
                AssetbundleHelp.UnLoadBundles();
                UnityEngine.Debug.Log("没有要更新的资源");
          //      AppFacade.Ins.Log(BugType.log, "没有要更新的资源");
                if (whenOver != null)
                    whenOver(true);
            }
        }

        protected void getObject(string name, System.Action<Object> callback)
        {
            if (Path.GetExtension(name) == "")
            {
                callback(null);
                return;
            }
            name = name.ToLower();   //相对路径 带后缀
            if (gloabalSourceDic.ContainsKey(name))
            {
                Debug.Log("load from gloabalSourceDic:" + name);
                callback(gloabalSourceDic[name]);
            }
            else if (sceneSourceDic.ContainsKey(name))
            {
                Debug.Log("load from sceneSourceDic:" + name);
                callback(sceneSourceDic[name]);
            }
            else
            {
                loadAsset(name, (Object obj)=> {
                    if (obj != null)
                        sceneSourceDic.Add(name, obj);
                    callback(obj);
                });
            }
        }

        void loadAsset(string assetName, System.Action<Object> callback)
        {
            //通过资源名称找到对应的assetbundle
            //查找对应的assetbundle
            //如果找到 获取所有的依赖项
            //通过依赖项找到对应的assetbundle
            //从对应的assetbundle里取出资源
            //未找到 则去resource里面取
            Debug.Log("assetName --:" + assetName);
            string abname = abConfig.GetBundlenameByAssetname(assetName);

            string fullname = AssetBundleInfo.assetPath_local + "/" + abname;
            Debug.Log("assetbundle path:" + fullname);
            if (abname == "" || !File.Exists(fullname))  //
            {
                Debug.Log("load from resource:" + assetName);
                callback(ResourcesLoad(assetName)); return;
            }
            loader.LoadFromStreamAssetsPathSync(fullname, (AssetBundle ab) =>
            {
                try
                {
                    if (ab != null)
                    {
                        string reallyName = assetName;
                        assetName = assetName.Substring(assetName.LastIndexOf('/') + 1);
                        assetName = assetName.Replace(Path.GetExtension(assetName), "");
                        Object[] obs = ab.LoadAllAssets();
                        Object oo = null;
                        for (int i = 0; i < obs.Length; i++)
                        {
                            if (obs[i].name.Equals(assetName, System.StringComparison.OrdinalIgnoreCase))
                            {
                                oo = obs[i];
                                bindScripts(oo);
                            }
                        }
                        Debug.Log("load resource from assetsbundle");
                        callback(oo);
                      //  if (oo != null) sceneSourceDic.Add(reallyName, oo);
                    }
                }
                catch (System.Exception ex) { Debug.LogError(ex); }
            });
        }
        /// <summary>
        /// 绑定物体上所有脚本关系
        /// </summary>
        /// <param name="rootObjName"></param>
        void bindScripts(Object rootObj)
        {
            AssetScriptsMgr.AssetScriptRelationship relation = AssetScriptsMgr.getIns().getAssetScriptRelationship(rootObj.name);
            if (relation != null)
                relation.BindScripts(rootObj);
            else
            {
                Debug.Log("no relationship on this obj:" + rootObj.name);
            }
        }
        void loadScript(string name, System.Action<Object> callback)
        {

        }

        bool checkUpdate()
        {
            return false;
            //if (ResourcesMgr.AssetVersionNum > curAssetVersion.assetVersion)
            //    return true;
            //return false;
        }
        void updateAssets(AssetVersion newAsset, System.Action<bool> whenDownloadOver = null)
        {
            if (newAsset == null || curAssetVersion == null)
            {
                Debug.LogError("newAsset or localAsset null");
                if (whenDownloadOver != null) whenDownloadOver(false);
                return;
            }
            Dictionary<string, Hash128> updateList = new Dictionary<string, Hash128>();
            Debug.Log("assetversion1:" + newAsset.ToStr() + "old kkkk:" + curAssetVersion.ToStr());
            AssetVersion.CompareAndGetUpdateList(curAssetVersion, newAsset, out updateList);
            //更新总的依赖文件
            loader.UpdateManifestFromServer(AssetBundleInfo.assetPath_server, (AssetBundleManifest am) =>
            {
                Debug.Log("服务器更新了manifest"); AssetBundleInfo.SetManifest(am);
                if (updateList != null)
                {
                    loader.UpdateFromServer(AssetBundleInfo.assetPath_server, updateList, whenDownloadOver);
                }
                else
                {
                    if (whenDownloadOver != null)
                        whenDownloadOver(true);
                }
            });

            //loader.UpdateFromServer()
        }
        /// <summary>
        /// 拉资源配置信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="back"></param>
        void pullAssetinfoFromSer(System.Action<AssetVersion> back)
        {
            string VersionPath = AssetBundleInfo.assetPath_server + "/" + "assetVersionInfo.txt";      //资源版本信息
            string ConfigPath = AssetBundleInfo.assetPath_server + "/" + "abConfig.txt";               //资源包配置信息
            string GameAssetConfigPath = AssetBundleInfo.assetPath_server + "/" + "gameAssetConfig.txt";  //游戏资源预加载信息
            loader.LoadFileFromServer(GameAssetConfigPath, (string str0) => {
                gameAssetConfig = GameAssetConfig.FromStr(str0);
                loader.LoadFileFromServer(ConfigPath, (string str) => {
                    Debug.Log("config file download"); abConfig = AssetbundleConfig.FromStr(str);
                    loader.LoadFileFromServer(VersionPath, (string str1) => {
                        AssetVersion av = AssetVersion.FromString(str1);
                        if (back != null) back(av);
                    });
                });
            });
        }
        AssetVersion getAssetinfoFromLocal()
        {
            string fullpath = AssetBundleInfo.assetPath_local + "/" + "assetVersionInfo.txt";
            if (File.Exists(fullpath))
            {
                StreamReader sr = File.OpenText(fullpath);
                string text = sr.ReadToEnd();
                sr.Close();
                return AssetVersion.FromString(text);
            }
            else
            {
                fullpath = AssetBundleInfo.curPlatformFloder + "/assetVersionInfo";
                Debug.Log("fullpath:" + fullpath);
                TextAsset ta = Resources.Load<TextAsset>(fullpath);
                if (ta == null)
                {
                    Debug.LogError("not find AssetVersionInfo at resource dir");
                    return null;
                }
                return AssetVersion.FromString(ta.text);
            }
        }
        AssetbundleConfig loadABConfig()
        {
            string fullpath = AssetBundleInfo.assetPath_local + "\\" + "abConfig.txt";
            fullpath = fullpath.Replace("/", "\\");
            if (File.Exists(fullpath))
            {
                StreamReader sr = File.OpenText(fullpath);
                string text = sr.ReadToEnd();
                sr.Close();
                return AssetbundleConfig.FromStr(text);
            }
            else
            {
                fullpath = AssetBundleInfo.curPlatformFloder + "/abConfig";
                Debug.Log("fullpath:" + fullpath);
                TextAsset ta = Resources.Load<TextAsset>(fullpath);
                if (ta == null)
                {
                    Debug.LogError("not find abConfig at resource dir");
                    return null;
                }
                return AssetbundleConfig.FromStr(ta.text);
            }
        }
        GameAssetConfig loadGameAssetConfig()
        {
            string fullpath = AssetBundleInfo.assetPath_local + "\\" + "gameAssetConfig.txt";
            fullpath = fullpath.Replace("/", "\\");
            if (File.Exists(fullpath))
            {
                StreamReader sr = File.OpenText(fullpath);
                string text = sr.ReadToEnd();
                sr.Close();
                return GameAssetConfig.FromStr(text);
            }
            else
            {
                GameAssetConfig gac = null;
                TextAsset ta = Resources.Load<TextAsset>("gameAssetConfig") as TextAsset;
                if (ta != null)
                {
                    Debug.Log("textasset config is not null:" + ta.text);
                    gac = GameAssetConfig.FromStr(ta.text);
                }
                if (gac == null)
                    Debug.LogError("GameAssetConfig file is not exsit!");
                return gac;
            }
        }
        Object ResourcesLoad(string name)
        {
            string ext = Path.GetExtension(name);
            if (ext != "")
            {
                name = name.Replace(ext, "");
            }
            return Resources.Load(name);
        }
    }
}