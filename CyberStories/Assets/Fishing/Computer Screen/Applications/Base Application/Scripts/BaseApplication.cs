using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseApplication : MonoBehaviour
{
    [FormerlySerializedAs("globalScreen")] public GlobalScreen GlobalScreen;

    [FormerlySerializedAs("icon")] public Sprite Icon;

    public abstract void ResetApplication();
}