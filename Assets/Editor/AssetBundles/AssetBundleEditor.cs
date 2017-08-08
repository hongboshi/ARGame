using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace SimpleFramework
{
    public class AssetBundleEditor
    {
        private static AssetbundleConfig abConfig = new AssetbundleConfig();
        private static AssetBundleManifest _manifest = null;
        public static AssetBundleManifest Manifest
        {
            get
            {
                if (_manifest == null)
                {
                    AssetBundle ab = AssetBundle.LoadFromFile(Path.Combine(AssetBundlesOutputPath,getPlatformFolder()));

                    _manifest = ab.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
                    ab.Unload(false);  //即时释放
                }
                return _manifest;
            }
        }

        public static string sourcePath = Application.dataPath + "/ForAssetbundleMaking";
        static string AssetBundlesOutputPath
        {
            get
            {
                string path = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf('/'));
                return Path.Combine(path + "/abRes", getPlatformFolder());
            }
        }
        [MenuItem("Tools/BuildAssetBundle")]
        public static void BuildSourceToAB()
        {
            ClearAssetBundlesName();
            Pack();
            string outputPath = AssetBundlesOutputPath;
            Debug.Log("delete:" + FileUtil.DeleteFileOrDirectory(outputPath));
            if (Directory.Exists(outputPath))
                Directory.Delete(outputPath);
            Directory.CreateDirectory(outputPath);
            Debug.Log("BuildSourceToAB outputPath = "+outputPath);
            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.UncompressedAssetBundle, EditorUserBuildSettings.activeBuildTarget);
           // AssetDatabase.Refresh(ImportAssetOptions.);
            Debug.Log("打包完成");
            //生成resouceinfo文件
            CreatResourceinfo();
            //生成资源脚本信息
            CreateAssetScriptsRelationFile();

            CopyAbToStreamingAssets();
        }
        [MenuItem("Tools/CreateAssetbundleMakingFolder")]
        public static void Init()
        {
            string path1 = sourcePath + "/ForPackWithDependices";
            string path2 = sourcePath + "/ForPackWithNoDependices";
            if (!Directory.Exists(path1))
            {
                Directory.CreateDirectory(path1);
            }
            if (!Directory.Exists(path2))
            {
                Directory.CreateDirectory(path2);
            }
            TextAsset ta = Resources.Load<TextAsset>("gameAssetConfig");
            if (ta == null)
            {
                string str = new GameAssetConfig().ToStr();
                Debug.Log(str);
                FileHelper.CreateFile("D:/", "gameAssetConfig.txt", str, false);
                Debug.LogError("copy the file:"+"d:/gameAssetConfig.txt "+"to resources");
            }
        }
        //对应平台的目录
        public static string getPlatformFolder()
        {
            string targetPlatform = "";
            if (AppConst.IsAndroidPlat) targetPlatform = "Android";
            else if (AppConst.IsIosPlat) targetPlatform = "IOS";
            else if (AppConst.IsPcPlat) targetPlatform = "Windows";
            return targetPlatform;
        }
        /// <summary>
        /// 生成资源配置信息： 资源包信息， 资源与包的关系信息
        /// </summary>
        static void CreatResourceinfo()
        {
            string outputPath = AssetBundlesOutputPath;
            //if (Directory.Exists(outputPath))
            //{
            //    Directory.Delete(outputPath, true);
            //}
            //Directory.CreateDirectory(outputPath);
            Dictionary<string, Hash128> abDic = new Dictionary<string, Hash128>();

            GetABInfo(outputPath, ref abDic);
            AssetVersion rinfo = new AssetVersion();
            rinfo.bundlesInfo = abDic;

            abConfig.resourceVersion = rinfo.assetVersion;
            Debug.Log(rinfo.assetVersion);
            rinfo.manifestName = outputPath.Substring(outputPath.LastIndexOf("\\") + 1);
            Debug.Log("manifestname = "+rinfo.manifestName);
            //File.Create(outputPath+"assetResources.txt",
            FileHelper.CreateFile(outputPath, "\\assetVersionInfo.txt", rinfo.ToStr(), false);
            FileHelper.CreateFile(outputPath, "\\abConfig.txt", abConfig.ToString(), false);

            //AssetVersion localRinfo = DecodeLocalResourcesinfo();
            //Debug.Log("old:" + rinfo.ToStr() + "   -new:" + localRinfo.ToStr());
            ////比较新的assetbundle与之前的是否一致
            //if (!AssetVersion.CompareBundlesInfo(rinfo, localRinfo))
            //{
            //    // rinfo.assetVersion = VRFramework.ResourceManager.AssetVersionNum + 1;
            //    abConfig.resourceVersion = rinfo.assetVersion;
            //    Debug.Log(rinfo.assetVersion);
            //    rinfo.manifestName = outputPath.Substring(outputPath.LastIndexOf("\\") + 1);
            //    //File.Create(outputPath+"assetResources.txt",
            //    FileHelper.CreateFile(outputPath, "\\assetVersionInfo.txt", rinfo.ToStr(), false);
            //    FileHelper.CreateFile(outputPath, "\\abConfig.txt", abConfig.ToString(), false);
            //    Debug.LogError("It's not an error!!!! -- 手动替换掉resources目录下的resourceinfo.txt文件");

            //    return;
            //}
            //Debug.Log("新的assetbundle包与原来的一致!");
        }
        /// <summary>
        /// 生成资源脚本关系文件
        /// </summary>
        static void CreateAssetScriptsRelationFile()
        {
        }

        /// <summary>
        /// 解析资源信息
        /// </summary>
        /// <returns></returns>
        static AssetVersion DecodeLocalResourcesinfo()
        {
            TextAsset ta = Resources.Load(getPlatformFolder() + "/assetVersionInfo") as TextAsset;
            if (ta == null)
            {
                Debug.LogError("DecodeLocalResourcesinfo ERROR,not found resourcesinfo.txt at Resources Directory:" + getPlatformFolder());
                return null;
            }
            AssetVersion local = AssetVersion.FromString(ta.text);
            return local;
        }

        static void GetABInfo(string source, ref Dictionary<string, Hash128> dic)
        {
            DirectoryInfo floder = new DirectoryInfo(source);
            FileSystemInfo[] files = floder.GetFileSystemInfos();
            string assetbundlename = "";
            Debug.Log("source:" + source);
            string replacestr = AssetBundlesOutputPath.Replace('/', '\\');
            replacestr += "\\";
            foreach (FileSystemInfo f in files)
            {
                if (f is DirectoryInfo)
                {
                    GetABInfo(f.FullName, ref dic);
                }
                else
                {
                    if (!f.Name.EndsWith(".unity3d"))
                        continue;
                    assetbundlename = f.FullName.Replace(replacestr, "");
                    Debug.Log("assetbundlename:" + assetbundlename + " - replacestr:" + replacestr);

                    Hash128 hash = Manifest.GetAssetBundleHash(assetbundlename);
                    if (hash != null)
                        dic.Add(assetbundlename, hash);
                }
            }
        }

        static void ClearAssetBundlesName()
        {
            int length = AssetDatabase.GetAllAssetBundleNames().Length;
            string[] temName = new string[length];
            if (length == 0) return;
            for (int i = 0; i < length; i++)
            {
                temName[i] = AssetDatabase.GetAllAssetBundleNames()[i];
            }
            for (int i = 0; i < temName.Length; i++)
            {
                AssetDatabase.RemoveAssetBundleName(temName[i], true);
            }
            abConfig.ClearAssetbundleDic();
        }

        static void Pack()
        {
            Pack(sourcePath + "/ForPackWithDependices");  //处理依赖关系打包方式
            Pack(sourcePath + "/ForPackWithNoDependices", false);  //不处理依赖关系打包
        }

        static void Pack(string source, bool isDetailDepends = true)
        {
            Debug.Log("source: " + source);
            
            DirectoryInfo folder = new DirectoryInfo(source);
            FileSystemInfo[] files = folder.GetFileSystemInfos();
            int length = files.Length;
            for (int i = 0; i < length; i++)
            {
                if (files[i] is DirectoryInfo)
                {
                    Pack(files[i].FullName);
                }
                else
                {
                    if (!files[i].Name.EndsWith(".meta"))
                    {
                        if (files[i].Name.EndsWith(".cs") || files[i].Name.Equals("assetVersionInfo.txt"))
                            continue;
                        if (isDetailDepends)
                            fileWithDepends(files[i].FullName);
                        else
                            file(files[i].FullName);
                    }
                }
            }
        }
        /// <summary>
        /// 打包 处理依赖关系
        /// </summary>
        /// <param name="source"></param>
        static void fileWithDepends(string source)
        {
            string _source = Replace(source);
            if (_source == "") return;
            _source = _source.Substring(Application.dataPath.Length + 1);
            _source = "Assets/" + _source;
            string[] dps = AssetDatabase.GetDependencies(_source);
            foreach (var dp in dps)
            {
                if (dp.EndsWith(".cs"))
                    continue;
                AssetImporter assetImporter = AssetImporter.GetAtPath(dp);
                string guid = AssetDatabase.AssetPathToGUID(dp);
                string fullname = dp.Replace("Assets/ForAssetbundleMaking/", "");
                fullname = fullname.Substring(fullname.IndexOf('/') + 1);
                string bagname = fullname.Replace(Path.GetExtension(fullname), ".unity3d");
                assetImporter.assetBundleName = bagname;
                Debug.Log("bagna:" + bagname);
                abConfig.AddAssetBundle(fullname, bagname);  //资源名和包名关系
            }
        }
        /// <summary>
        /// 打包 不处理依赖关系
        /// </summary>
        /// <param name="source"></param>
        static void file(string source)
        {
            Debug.Log("file source " + source);
            string _source = Replace(source);
            string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);
            AssetImporter assetImport = AssetImporter.GetAtPath(_assetPath);

            string fullname = _assetPath.Replace("Assets/ForAssetbundleMaking/", "");
            fullname = fullname.Substring(fullname.IndexOf('/') + 1);
            Debug.Log("              fullname:" + fullname);

            string bagname = fullname.Replace(Path.GetExtension(fullname), ".unity3d");
            Debug.Log("bagna:" + bagname);
            assetImport.assetBundleName = bagname;

            abConfig.AddAssetBundle(fullname, bagname);
        }

        static string Replace(string s)
        {
            return s.Replace("\\", "/");
        }
        static void CopyAbToStreamingAssets()
        {
            string streamingPath = Application.streamingAssetsPath + "/Assetbundles/" + getPlatformFolder();
            if (Directory.Exists(streamingPath))
                Directory.Delete(streamingPath,true);
            FileHelper.CopyFolderTo(AssetBundlesOutputPath, streamingPath);
        }


    }
}


