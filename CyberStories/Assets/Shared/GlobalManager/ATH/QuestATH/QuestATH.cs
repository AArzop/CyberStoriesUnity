using UnityEngine;
using UnityEngine.UI;

public class QuestATH : MonoBehaviour
{
    public Text listQuestText;

    public void UpdateQuestATH()
    {
        listQuestText.text = GlobalManager.QuestManager.GetCurrentStep()?.GetQuestInformation();
    }

    private void Start()
    {
        UpdateQuestATH();
    }
}
