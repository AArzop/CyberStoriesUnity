using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestATH : MonoBehaviour
{
    public Text listQuestText;

    public void UpdateQuestATH()
    {
        listQuestText.text = GlobalManager.questManager.GetCurrentStep()?.GetQuestInformation();
    }

    private void Start()
    {
        UpdateQuestATH();
    }
}
