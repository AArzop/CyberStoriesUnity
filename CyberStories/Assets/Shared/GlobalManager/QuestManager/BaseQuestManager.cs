using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuestManager : MonoBehaviour
{
    public List<BaseQuest> StepsList;
    public ClickToChangeScene NextSceneChanger;
    protected BaseQuest CurrentStep;
    protected int CurrentStepIndex;

    protected void Awake()
    {
        if (NextSceneChanger != null)
            NextSceneChanger.gameObject.SetActive(false);

        GlobalManager.Details = new PhishingDetails();

        // First launch of the scene
        if (GlobalManager.QuestManager == null)
        {
            GlobalManager.QuestManager = this;
            CurrentStep = StepsList[0];
            CurrentStepIndex = 0;
        }
    }

    public BaseQuest GetCurrentStep()
    {
        return CurrentStep;
    }

    public void FulfillStep()
    {
        CurrentStep.EvaluateQuest();
        ++CurrentStepIndex;

        if (CurrentStepIndex < StepsList.Count)
        {
            CurrentStep = StepsList[CurrentStepIndex];
            CurrentStep.SetupQuest();
        }
        else
        {
            EndLevel();
            if (NextSceneChanger != null)
                NextSceneChanger.gameObject.SetActive(true);
        }
    }

    protected abstract void EndLevel();
}