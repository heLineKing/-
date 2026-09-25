using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

// 一次性编辑器工具：给 Player 加刚体与碰撞箱并落到 Ground 顶面。用完可删除本文件。
[InitializeOnLoad]
public static class CodexSceneSetup
{
    private const string PlayerName = "Player";
    private const string GroundName = "Ground";
    private const string BackupFolder = ".codex-backup";
    private const string MarkerFile = "scene-setup.done";

    private static string ProjectRoot
    {
        get { return Directory.GetParent(Application.dataPath).FullName; }
    }

    static CodexSceneSetup()
    {
        EditorApplication.delayCall += RunOnce;
    }

    private static void RunOnce()
    {
        if (File.Exists(Path.Combine(ProjectRoot, BackupFolder, MarkerFile)))
        {
            return;
        }

        Setup();
    }

    [MenuItem("Codex/给 Player 加物理并落到地面")]
    public static void Setup()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Debug.LogWarning("[Codex] 请先停止播放模式，再执行搭建。");
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

        GameObject ground = FindInActiveScene(GroundName);
        if (ground == null)
        {
            Debug.LogError("[Codex] 场景里没有找到 " + GroundName);
            return;
        }

        Rigidbody2D body = player.GetComponent<Rigidbody2D>();
        if (body == null)
        {
            body = player.AddComponent<Rigidbody2D>();
        }

        body.bodyType = RigidbodyType2D.Dynamic;
        body.gravityScale = 3f;
        body.freezeRotation = true;
        body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;

        SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();
        Vector2 spriteSize = renderer != null && renderer.sprite != null
            ? (Vector2)renderer.sprite.bounds.size
            : new Vector2(1.3125f, 1.375f);

        BoxCollider2D box = player.GetComponent<BoxCollider2D>();
        if (box == null)
        {
            box = player.AddComponent<BoxCollider2D>();
        }

        box.size = new Vector2(spriteSize.x - 0.125f, spriteSize.y);
        box.offset = new Vector2(0f, box.size.y * 0.5f);

        Collider2D groundCollider = ground.GetComponent<Collider2D>();
        float groundTop = groundCollider != null ? groundCollider.bounds.max.y : ground.transform.position.y;

        Vector3 position = player.transform.position;
        player.transform.position = new Vector3(position.x, groundTop, position.z);

        EditorUtility.SetDirty(player);
        EditorSceneManager.MarkSceneDirty(player.scene);

        Directory.CreateDirectory(Path.Combine(ProjectRoot, BackupFolder));
        File.WriteAllText(Path.Combine(ProjectRoot, BackupFolder, MarkerFile), System.DateTime.Now.ToString("s"));

        Debug.Log(string.Format(
            "[Codex] Player 已就绪：gravityScale={0} 碰撞箱={1}x{2} 偏移y={3} 落点y={4}（Ground 顶面 {5}）。按 Ctrl+S 保存场景。",
            body.gravityScale, box.size.x, box.size.y, box.offset.y, player.transform.position.y, groundTop));
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
