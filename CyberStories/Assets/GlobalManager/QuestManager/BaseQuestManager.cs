using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuestManager : MonoBehaviour
{
    public List<BaseQuest> stepsList;

    protected BaseQuest currentStep;
    protected int currentStepIndex;

    protected void Awake()
    {
        if (GlobalManager.questManager == null)
        {
            GlobalManager.questManager = this;
            currentStep = stepsList[0];
            currentStepIndex = 0;
        }
        else
        {
            stepsList = GlobalManager.questManager.stepsList;
            currentStep = GlobalManager.questManager.GetCurrentStep();
            currentStepIndex = stepsList.IndexOf(currentStep);
        }
    }

    public BaseQuest GetCurrentStep()
    {
        return currentStep;
    }

    public void FullfillStep()
    {
        ++currentStepIndex;
        if (currentStepIndex < stepsList.Count)
        {
            currentStep = stepsList[currentStepIndex];
        }
        else
        {
            EndLevel();
        }
    }

    protected void EndLevel()
    {

    }

    public abstract float GetLevelEvaluation();
}
