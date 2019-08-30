using UnityEngine;

public abstract class BaseApplication : MonoBehaviour
{
    public GlobalScreen GlobalScreen;
    public Sprite Icon;

    public abstract void ResetApplication();
}