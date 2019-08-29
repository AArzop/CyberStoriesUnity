using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseApplication : MonoBehaviour
{
    public GlobalScreen globalScreen;

    public Sprite icon;

    public abstract void ResetApplication();
}