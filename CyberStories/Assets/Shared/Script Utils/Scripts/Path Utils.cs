using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathUtils : MonoBehaviour
{
    public static string AbsToProject(string absPath)
    {
        // https://answers.unity.com/questions/24060/can-assetdatabaseloadallassetsatpath-load-all-asse.html
        return "Assets" + absPath.Replace(Application.dataPath, "").Replace('\\', '/');
    }
}