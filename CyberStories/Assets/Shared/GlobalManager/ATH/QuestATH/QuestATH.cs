using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class QuestATH : MonoBehaviour
{
    [FormerlySerializedAs("listQuestText")] public Text ListQuestText;

    public void UpdateQuestATH()
    {
        ListQuestText.text = GlobalManager.QuestManager.GetCurrentStep()?.GetQuestInformation();
    }

    private void Start()
    {
        UpdateQuestATH();
    }
}
