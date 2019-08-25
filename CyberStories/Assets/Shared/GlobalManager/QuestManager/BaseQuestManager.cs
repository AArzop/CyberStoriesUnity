using System.Collections;
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

        GlobalManager.details = new PhishingDetails();

        // First launch of the scene
        if (GlobalManager.questManager == null)
        {
            GlobalManager.questManager = this;
            currentStep = stepsList[0];
            currentStepIndex = 0;
        }
    }

    public BaseQuest GetCurrentStep()
    {
        return currentStep;
    }

    public void FullfillStep()
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
