using System;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using SimpleFramework.Game;
using UnityEngine.SceneManagement;

public class GameMeau
{
    [MenuItem("Tools/GameEditor/AddGame")]
    static void showGameEditorWindow()
    {
        EditorWindow.GetWindow(typeof(GameEditorWindow));
    }
    [MenuItem("Tools/GameEditor/AddScene")]
    static void showSceneEditorWindow()
    {
        EditorWindow.GetWindow(typeof(SceneEditorWindow));
    }
}
public class SceneEditorWindow : EditorWindow
{
    string[] _names;
    void InitGameNames()
    {
        GameEnum genum = new GameEnum();
        System.Type type = typeof(GameEnum);
        System.Reflection.MemberInfo[] minfos = type.GetMembers();
        _names = new string[minfos.Length];
        for (int i = 0; i < minfos.Length; i++)
        {
            _names[i] = minfos[i].Name;
        }
    }

    SceneEditorWindow()
    {
        this.titleContent = new GUIContent("SceneEditor");
    }
    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("SceneEditor");

        GUILayout.Space(10);
        GUILayout.EndVertical();
    }
}
public class GameEditorWindow : EditorWindow
{

    public Texture TxTexture;
    string gameName = "";
    string description = "";
    IGameBase buggyGameObject;

    GameEditorWindow()
    {
        this.titleContent = new GUIContent("GameEditor");
    }



    //绘制窗口界面
    void OnGUI()
    {
        GUILayout.BeginVertical();

        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("GameEditor");

        GUILayout.Space(10);
      //  (1,_names);
        gameName = EditorGUILayout.TextField("Game Name", gameName);
        GUILayout.Space(10);
        if (GUILayout.Button("Add Scene"))
        {
       //     GameEnum.
           Scene s1 =  EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
           // EditorSceneManager.SaveScene(;
        }

        // EditorSceneManager.CreateScene("kjk");
        //  EditorGUILayout.DropdownButton()

        GUILayout.Space(10);
        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        GUILayout.Label("Currently Scene:" + EditorSceneManager.GetActiveScene().name);

        GUILayout.Space(10);
        GUILayout.Label("Time:" + System.DateTime.Now);


        GUILayout.Space(10);
       // buggyGameObject =
       //     (GameObject)EditorGUILayout.ObjectField("Buggy Go", buggyGameObject, typeof(IGameBase), true);

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Description", GUILayout.MaxWidth(80));
        description = EditorGUILayout.TextArea(description, GUILayout.MaxHeight(75));
        GUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (GUILayout.Button("CreateGame"))
        {
            //SaveBug();
        }

        if (GUILayout.Button("Save Bug With Screenshoot"))
        {
         //   SaveBugWithScreeshot();
        }

        EditorGUILayout.EndVertical();//布局开始和结束相对应，缺少时可能出现窗口中的元素无法自适应的情况
    }

    

    private void SaveBugWithScreeshot()
    {
        Writer();
        ScreenCapture.CaptureScreenshot("Assets/BugReports/" + gameName + "/" + gameName + ".png");
        //Application.CaptureScreenshot("Assets/BugReports/" + bugReporterName + "/" + bugReporterName + ".png");
    }

    private void SaveBug()
    {
        Writer();
    }

    //IO类，用来写入保存信息
    void Writer()
    {
        Directory.CreateDirectory("Assets/BugReports/" + gameName);
        StreamWriter sw = new StreamWriter("Assets/BugReports/" + gameName + "/" + gameName + ".txt");
        sw.WriteLine(gameName);
        sw.WriteLine(DateTime.Now.ToString());
        sw.WriteLine(EditorSceneManager.GetActiveScene().name);
        sw.WriteLine(description);
        sw.Close();
    }
}

class GameStructor
{
    string _gName;
    string[] _gbase;
    public GameStructor()
    {

    }
}

public static class GameCreate
{
    /// <summary>
    /// 创建游戏目录
    /// </summary>
    /// <param name="name"></param>
    public static void CreateGameFloder(string name)
    {

    }
}