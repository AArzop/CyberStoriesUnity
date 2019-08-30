using UnityEngine;
using UnityEngine.UI;

public class QuestATH : MonoBehaviour
{
    public Text ListQuestText;

    public void UpdateQuestATH()
    {
        ListQuestText.text = GlobalManager.QuestManager.GetCurrentStep()?.GetQuestInformation();
    }

    private void Start()
    {
        UpdateQuestATH();
    }
}
