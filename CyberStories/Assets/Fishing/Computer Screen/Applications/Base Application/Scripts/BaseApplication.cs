using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseApplication : MonoBehaviour
{
    public GlobalScreen GlobalScreen;

    public Sprite Icon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public abstract void ResetApplication();
}
