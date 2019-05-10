using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberStories.DBO
{
    [System.Serializable]
    public class DialogueLine
    {
        public string name;
        public string textLine;

        //answers and nextLine MUST have the same size (one answer and its corresponding nextLine)
        public string[] answers;
        public DialogueLine[] nextLine;
    }
}
