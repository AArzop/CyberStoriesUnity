using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuestManager : MonoBehaviour
{
    public List<BaseQuest> stepsList;
    public ClickToChangeScene nextSceneChanger;
    protected BaseQuest currentStep;
    protected int currentStepIndex;

    protected void Awake()
    {
        if (nextSceneChanger != null)
            nextSceneChanger.gameObject.SetActive(false);

        GlobalManager.Details = new PhishingDetails();

        // First launch of the scene
        if (GlobalManager.QuestManager == null)
        {
            GlobalManager.QuestManager = this;
            currentStep = stepsList[0];
            currentStepIndex = 0;
        }
    }

    public BaseQuest GetCurrentStep()
    {
        return currentStep;
    }

    public void FulfillStep()
    {
        currentStep.EvaluateQuest();
        ++currentStepIndex;

        if (currentStepIndex < stepsList.Count)
        {
            currentStep = stepsList[currentStepIndex];
            currentStep.SetupQuest();
        }
        else
        {
            EndLevel();
            if (nextSceneChanger != null)
                nextSceneChanger.gameObject.SetActive(true);
        }
    }

    protected abstract void EndLevel();
}