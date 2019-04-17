using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQuestManager : MonoBehaviour
{
    public List<BaseQuest> stepsList;
    protected Dictionary<BaseQuest, float> stepsScore;
    protected BaseQuest currentStep;
    protected int currentStepIndex;

    protected void Awake()
    {
        stepsScore = new Dictionary<BaseQuest, float>();

        // First launch of the scene
        if (GlobalManager.questManager == null)
        {
            GlobalManager.questManager = this;
            currentStep = stepsList[0];
            currentStepIndex = 0;
        }
        else // level in 2 scenes, have to copy. Avoid if possible
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
        stepsScore[currentStep] = currentStep.EvaluateQuest();
        ++currentStepIndex;

        if (currentStepIndex < stepsList.Count)
        {
            currentStep = stepsList[currentStepIndex];
            currentStep.SetupQuest();
        }
        else
        {
            EndLevel();
        }
    }

    protected abstract void EndLevel();

    public abstract float GetLevelEvaluation();
}
