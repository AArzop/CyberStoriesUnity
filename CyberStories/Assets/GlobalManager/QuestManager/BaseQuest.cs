using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuest : MonoBehaviour
{
    public BaseQuestManager questManager;
    public bool IsDone { get; set; }

    public string questATHKey;
    protected string infoQuest;

    protected void Awake()
    {
        IsDone = false;

        if (questATHKey != string.Empty)
            infoQuest = GlobalManager.GetLocalization(questATHKey);
    }

    public abstract void SetupQuest();
    public abstract void CheckQuest();
    public abstract void EndQuest();
    public abstract void EvaluateQuest();

    public abstract string GetQuestInformation();
}
