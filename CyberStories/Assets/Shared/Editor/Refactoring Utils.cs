using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class RefactoringUtils : MonoBehaviour
{
    private static void ForEachAssetAtAbsPath(IEnumerable<string> absPaths, Action<GameObject> lambda)
    {
        foreach (string absPath in absPaths)
        {
            string projectPath = AbsPathToProjectPath(absPath);
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(projectPath);
            foreach (Object asset in assets)
            {
                if (!(asset is GameObject go)) continue;
                lambda(go);
            }
        }
    }

    private static string AbsPathToProjectPath(string absPath)
    {
        // https://answers.unity.com/questions/24060/can-assetdatabaseloadallassetsatpath-load-all-asse.html
        return "Assets" + absPath.Replace(Application.dataPath, "").Replace('\\', '/');
    }

    [MenuItem("Assets/Tools/Reserialize")]
    private static void Reserialize()
    {
        string[] prefabs = Directory.GetFiles(Application.dataPath, "*.prefab", SearchOption.AllDirectories);
        string[] scenes = Directory.GetFiles(Application.dataPath, "*.unity", SearchOption.AllDirectories);

        List<string> paths = new List<string>();

        void AddPathInResIfHasComponentInSelectedComponent(GameObject go)
        {
            // test if the selected game object contains one of the selected scripts
            if (!Selection.objects.Any(selected => go.GetComponent(selected.name))) return;
            
            string path = AssetDatabase.GetAssetPath(go);
            if (!paths.Contains(path))
            {
                paths.Add(path);
            }
        }

        ForEachAssetAtAbsPath(prefabs, AddPathInResIfHasComponentInSelectedComponent);

        foreach (string sceneFilename in scenes)
        {
            Scene scene = EditorSceneManager.OpenScene(sceneFilename);
            Object[] deps = EditorUtility.CollectDependencies(scene.GetRootGameObjects());
            foreach (Object dep in deps)
            {
                if (!dep) continue;
                if (!Selection.objects.Any(selected => dep.Equals(selected))) continue;

                string path = AbsPathToProjectPath(sceneFilename);
                if (!paths.Contains(path))
                {
                    paths.Add(path);
                }
            }
        }

        foreach (string path in paths)
        {
            Debug.Log("Reserializing " + path);
        }
        AssetDatabase.ForceReserializeAssets(paths);
    }
}