using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [MenuItem("Assets/Tools/Set Dirty")]
    private static void SetDirty() {
        foreach (Object o in Selection.objects) {
            EditorUtility.SetDirty(o);
        }
    }
}
