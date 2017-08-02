using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        public const string a = "game_a";
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
        internal static IGameBase NowActiveGame = null;                        
        protected GameType gType { get; private set; }             //游戏类型
        protected string gameName { get; }                         //游戏名称
        protected abstract string[] sceneName { get;}             //游戏场景
        public IGameBase(GameType type, string name)
        {
            gType = type;
            this.gameName = name;
            RegiestScene();
        }

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
            AppFacade.Ins.GetMgr<SceneMgr>().LoadScene(sceneName[0]);
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
