using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuest : MonoBehaviour
{
    public BaseQuestManager questManager;
    public bool IsDone { get; set; }

    private void Awake()
    {
        IsDone = false;
    }

    public abstract void SetupQuest();
    public abstract void CheckQuest();
    public abstract void EndQuest();
    public abstract float EvaluateQuest();
}
