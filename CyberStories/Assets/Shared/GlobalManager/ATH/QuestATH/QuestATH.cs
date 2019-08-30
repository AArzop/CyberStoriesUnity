using UnityEngine;
using UnityEngine.UI;

// Resharper CLI seems not to support abbreviations
// https://resharper-support.jetbrains.com/hc/en-us/community/posts/206005504-ReShaper-Command-Line-Tools-Are-Ignoring-Allowed-Abbreviations

// ReSharper disable once InconsistentNaming
public class QuestATH : MonoBehaviour
{
    public Text ListQuestText;

    // ReSharper disable once InconsistentNaming
    public void UpdateQuestATH()
    {
        ListQuestText.text = GlobalManager.QuestManager.GetCurrentStep()?.GetQuestInformation();
    }

    private void Start()
    {
        UpdateQuestATH();
    }
}
