using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleFramework
{
    /// <summary>
    /// ResourceManager
    /// 处理资源的加载
    /// 1，资源全部加载完毕才能使用
    /// 2，调试模式下无需下载
    /// </summary>
    public class ResourceManager : AssetBase
    {
        bool isDownloadOver;
        public ResourceManager()
        {
            isDownloadOver = false;
        }
        public void UpdateAssets(System.Action<bool> whenOver = null)
        {
            Debug.Log("更新资源");
            AssetsInit((bool result) =>
            {
                isDownloadOver = result;
                if (whenOver != null) whenOver(isDownloadOver);
                Debug.Log("isdownloadover = " + isDownloadOver);
            });
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        public void LoadAsset(string name, System.Action<Object> callback)
        {
            if (!isDownloadOver && !AppConst.isEditorDebug) { Debug.Log("有未下载完的资源"); return; }
            if (System.IO.Path.GetExtension(name) == "")
                name += ".prefab";
            getObject(name, callback);
        }
        public void PreLoadSceneAssets(string sceneName)
        {
            sceneSourceDic.Clear();
            List<string> alist = gameAssetConfig.GetAssetListBySceneName(sceneName);
            foreach (string str in alist)
            {
                LoadAsset(str, (Object obj) => {
                    if(!sceneSourceDic.ContainsKey(str)) sceneSourceDic.Add(str, obj);
                });
            }
        }
        public void PreLoadGameAssets()
        {
            List<string> alist = gameAssetConfig.GetGameassetList();
            foreach (string str in alist)
            {
                LoadAsset(str, (Object obj) =>
                {
                    if(!gloabalSourceDic.ContainsKey(str)) gloabalSourceDic.Add(str, obj);
                });
            }
        }
        public string[] GetAssetListBySceneName(string scenename = "")
        {
            string[] arr;
            try
            {
                if (scenename == "")
                    arr = gameAssetConfig.GetGameassetList().ToArray();
                else
                    arr = gameAssetConfig.GetAssetListBySceneName(scenename).ToArray();
            }
            catch (System.Exception ex)
            {
                arr = new string[] { "" };
            }
            return arr;
        }
    }
}
