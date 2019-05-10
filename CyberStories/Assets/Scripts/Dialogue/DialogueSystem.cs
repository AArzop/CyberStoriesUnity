using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CyberStories.DBO;
using System;

public class DialogueSystem : MonoBehaviour
{
    private  DialogueLine actualLine;

    public void StartDialogue(DialogueLine firstLine)
    {
        actualLine = firstLine;

        Debug.Log("start dialogue of " + actualLine.name);

        nextLine();
    }

    //answer is the answer that you clicked on (its list number, -1 is when you call the function without answers)
    private void nextLine(int answer = -1)
    {
        if (actualLine.answers.Length == 0)
        {
            EndDialogue();
            return;
        }
        Debug.Log(actualLine.name + ": " + actualLine.textLine);
    }

    private void EndDialogue()
    {
        Debug.Log("End of dialogue");
    }
}
