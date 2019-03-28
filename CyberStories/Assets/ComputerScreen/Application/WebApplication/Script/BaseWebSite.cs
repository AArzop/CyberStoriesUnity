using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWebSite : MonoBehaviour
{
    public string Name;
    public Sprite Icon;

    public string Url { get; set; }
}
