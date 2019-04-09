using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingQuestManager : BaseQuestManager
{
    public override float GetLevelEvaluation()
    {
        float evaluation = 0f;

        foreach (var pair in stepsScore)
            evaluation += pair.Value;

        return evaluation;
    }
}
