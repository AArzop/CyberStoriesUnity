namespace CyberStories.DBO
{
    [System.Serializable]
    public class DialogueLine
    {
        public string Name;
        public string TextLine;

        //answers and nextLine MUST have the same size (one answer and its corresponding nextLine)
        public string[] Answers;
        public DialogueLine[] NextLine;
    }
}