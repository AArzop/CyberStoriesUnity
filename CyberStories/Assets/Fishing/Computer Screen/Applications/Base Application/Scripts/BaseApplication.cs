using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseApplication : MonoBehaviour
{
    [FormerlySerializedAs("GlobalScreen")] public GlobalScreen globalScreen;

    [FormerlySerializedAs("Icon")] public Sprite icon;

    public abstract void ResetApplication();
}