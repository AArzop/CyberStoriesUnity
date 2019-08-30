using UnityEngine.Serialization;

namespace CyberStories.DBO
{
    [System.Serializable]
    public class DialogueLine
    {
        [FormerlySerializedAs("name")] public string Name;
        [FormerlySerializedAs("textLine")] public string TextLine;

        //answers and nextLine MUST have the same size (one answer and its corresponding nextLine)
        [FormerlySerializedAs("answers")] public string[] Answers;
        [FormerlySerializedAs("nextLine")] public DialogueLine[] NextLine;
    }
}