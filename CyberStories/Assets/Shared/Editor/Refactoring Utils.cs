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
    [MenuItem("Assets/Tools/Reserialize related scenes and prefabs")]
    private static void ReserializeRelatedScenesAndPrefabs()
    {
        string[] prefabs = Directory.GetFiles(Application.dataPath, "*.prefab", SearchOption.AllDirectories);
        string[] scenes = Directory.GetFiles(Application.dataPath, "*.unity", SearchOption.AllDirectories);

        List<string> relatedPaths = new List<string>();

        relatedPaths.AddRange(FindRelatedPrefabs(prefabs));
        relatedPaths.AddRange(FindRelatedScenes(scenes));

        // reserialize
        foreach (string path in relatedPaths)
        {
            Debug.Log("Reserializing " + path);
        }

        AssetDatabase.ForceReserializeAssets(relatedPaths);
    }

    private static IEnumerable<string> FindRelatedScenes(IEnumerable<string> scenes)
    {
        List<string> paths = new List<string>();
        foreach (string sceneFilename in scenes)
        {
            Scene scene = EditorSceneManager.OpenScene(sceneFilename);
            Object[] deps = EditorUtility.CollectDependencies(scene.GetRootGameObjects());
            foreach (Object dep in deps)
            {
                if (!dep) continue;
                // test if the scene dependency contains one of the selected scripts
                if (!Selection.objects.Any(selected => dep.Equals(selected))) continue;

                string path = PathUtils.AbsToProject(sceneFilename);
                if (!paths.Contains(path))
                {
                    paths.Add(path);
                }
            }
        }

        return paths;
    }

    private static IEnumerable<string> FindRelatedPrefabs(IEnumerable<string> prefabs)
    {
        List<string> paths = new List<string>();

        foreach (string absPath in prefabs)
        {
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(PathUtils.AbsToProject(absPath));
            foreach (Object asset in assets)
            {
                if (!(asset is GameObject go)) continue;
                // test if the game object contains one of the selected scripts
                if (!Selection.objects.Any(selected => go.GetComponent(selected.name))) continue;

                string path = AssetDatabase.GetAssetPath(go);
                if (!paths.Contains(path))
                {
                    paths.Add(path);
                }
            }
        }

        return paths;
    }
}