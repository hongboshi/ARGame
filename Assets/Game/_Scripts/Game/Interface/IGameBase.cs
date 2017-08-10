using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SimpleFramework.Game
{
    //游戏类型
    public enum GameType
    {
        NonAR, //非ar
        AR,    //ar
    }
    //游戏枚举
    public class GameEnum
    {
        public const string mainLobby = "game_mainLobby";
        public const string SeaFish = "game_SeaFish";
        public const string b = "game_b";
        public const string c = "game_c";
        public const string d = "game_d";
        public const string e = "game_e";
        public const string f = "game_f";
        public const string g = "game_g";
        public const string h = "game_h";
        public const string i = "game_i";

    }
    /// <summary>
    /// 每一个游戏demo对应一个gamebase
    /// </summary>
    public abstract class IGameBase
    {
        static string preGameName = "";
        internal static IGameBase NowActiveGame = null;
        protected GameType gType { get; private set; }             //游戏类型
        protected string gameName { get; }                         //游戏名称
        protected abstract string[] sceneName { get; }             //游戏场景
        public IGameBase(GameType type, string name)
        {
            gType = type;
            this.gameName = name;
            RegiestScene();
        }
        //返回上一个游戏
        public void GoBack()
        {
            if (preGameName != "")
                AppFacade.Ins.GetMgr<GameManager>().GetGame(preGameName).StartUp();
        }
        //加载游戏内场景
        public void LoadScene(string name)
        {
            AppFacade.Ins.GetMgr<SceneMgr>().LoadScene(name);
        }
        //加载游戏内场景
        public void LoadScene(int index)
        {
            if (index < 0 || index > sceneName.Length - 1)
            {
                Debug.LogError("load scene is out of arry");
            }
            LoadScene(sceneName[index]);
        }
        //场景状态注册
        protected virtual void RegiestScene()
        {
            for (int i = 0; i < sceneName.Length; i++)
            {
                AppFacade.Ins.GetMgr<SceneMgr>().RegiestScene(sceneName[i], new ISceneState(SceneMgr.mController));               
            }
        }
        void UnRegiestScene()
        {
            for (int i = 0; i < sceneName.Length; i++)
            {
                AppFacade.Ins.GetMgr<SceneMgr>().UnRegiestScene(sceneName[i]);
            }
        }

        /// <summary>
        /// 启动游戏
        /// 卸载之前的游戏
        /// </summary>
        /// <param name="param"></param>
        internal void StartUp(object param = null)
        {
            if (this == NowActiveGame) return;
            else if (NowActiveGame != null) NowActiveGame.UnLoad();          
            //   else  NowActiveGame?.UnLoad();    //unity版本得支持c#4.0以上
            NowActiveGame = this;
            LoadScene(0);
           // AppFacade.Ins.GetMgr<SceneMgr>().LoadScene(sceneName[0]);
            OnStartUp(param);
            Debug.Log("启动游戏" + gameName);    
        }
        //启动回调
        protected virtual void OnStartUp(object param = null)
        {

        }
        //卸载
        internal void UnLoad()
        {
            preGameName = gameName;
            Debug.Log("卸载游戏" + gameName);
            UnRegiestScene();
            OnUnLoad();
        }
        //卸载回调
        protected virtual void OnUnLoad()
        {
        }
    }
}
