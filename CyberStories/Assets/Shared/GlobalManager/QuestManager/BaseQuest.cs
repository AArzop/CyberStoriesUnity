using UnityEngine;

public abstract class BaseQuest : MonoBehaviour
{
    public BaseQuestManager QuestManager;
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public bool IsDone { get; set; }

    public string QuestAthKey;
    protected string InfoQuest;

    protected void Awake()
    {
        IsDone = false;

        if (QuestAthKey != string.Empty)
            InfoQuest = GlobalManager.GetLocalization(QuestAthKey);
    }

    public abstract void SetupQuest();
    public abstract void CheckQuest();
    public abstract void EndQuest();
    public abstract void EvaluateQuest();

    public abstract string GetQuestInformation();
}
