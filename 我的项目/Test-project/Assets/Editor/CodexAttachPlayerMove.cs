using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

// 一次性编辑器工具：把 Assets/Scripts/PlayerMove.cs 挂到场景里的 Player 上。用完可删除本文件。
[InitializeOnLoad]
public static class CodexAttachPlayerMove
{
    private const string PlayerName = "Player";
    private const string ScriptPath = "Assets/Scripts/PlayerMove.cs";
    private const string BackupFolder = ".codex-backup";
    private const string MarkerFile = "attach-player-move.done";

    private static string ProjectRoot
    {
        get { return Directory.GetParent(Application.dataPath).FullName; }
    }

    static CodexAttachPlayerMove()
    {
        EditorApplication.delayCall += RunOnce;
    }

    private static void RunOnce()
    {
        if (File.Exists(Path.Combine(ProjectRoot, BackupFolder, MarkerFile)))
        {
            return;
        }

        Attach();
    }

    [MenuItem("Codex/给 Player 挂上 PlayerMove 脚本")]
    public static void Attach()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Debug.LogWarning("[Codex] 请先停止播放模式，再挂脚本。");
            return;
        }

        if (PrefabStageUtility.GetCurrentPrefabStage() != null)
        {
            Debug.LogWarning("[Codex] 当前处于 Prefab 编辑模式，已跳过。");
            return;
        }

        GameObject player = FindInActiveScene(PlayerName);
        if (player == null)
        {
            Debug.LogError("[Codex] 场景里没有找到 " + PlayerName);
            return;
        }

        MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(ScriptPath);
        if (script == null)
        {
            Debug.LogError("[Codex] 找不到脚本 " + ScriptPath);
            return;
        }

        System.Type type = script.GetClass();
        if (type == null)
        {
            Debug.LogError("[Codex] 脚本还没编译完成，稍后再试。");
            return;
        }

        if (player.GetComponent(type) == null)
        {
            player.AddComponent(type);
        }

        EditorUtility.SetDirty(player);
        EditorSceneManager.MarkSceneDirty(player.scene);

        Directory.CreateDirectory(Path.Combine(ProjectRoot, BackupFolder));
        File.WriteAllText(Path.Combine(ProjectRoot, BackupFolder, MarkerFile), System.DateTime.Now.ToString("s"));

        Debug.Log("[Codex] 已把 " + type.Name + " 挂到 " + PlayerName + " 上。按 Ctrl+S 保存场景，然后按 Play，用 A / D 试试。");
    }

    private static GameObject FindInActiveScene(string objectName)
    {
        Scene scene = SceneManager.GetActiveScene();
        foreach (GameObject root in scene.GetRootGameObjects())
        {
            if (root.name == objectName)
            {
                return root;
            }
        }

        return null;
    }
}
