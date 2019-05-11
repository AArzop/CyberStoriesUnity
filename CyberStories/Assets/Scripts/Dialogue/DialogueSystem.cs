using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CyberStories.DBO;
using System;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    private DialogueLine actualLine;
    private TextMeshProUGUI textDialogue;

    public void StartDialogue(DialogueLine firstLine, TextMeshProUGUI tmPro)
    {
        actualLine = firstLine;
        textDialogue = tmPro;
        nextLine();
    }

    //answer is the answer that you clicked on (its list number, -1 is when you call the function without answers)
    //returns false if dialogue end, true otherwise
    public bool nextLine(int answer = -1)
    {        
        textDialogue.text = actualLine.name + ": " + actualLine.textLine;

        if (actualLine.answers.Length == 0 || actualLine.nextLine.Length != actualLine.answers.Length)
        {
            EndDialogue();
            return true;
        }

        if (answer != -1 && answer < actualLine.nextLine.Length)
        {
            actualLine = actualLine.nextLine[answer];
            //set all buttons
        }
        else if (answer == -1)
        {
            actualLine = actualLine.nextLine[0];
        }
        return false;
    }

    private void EndDialogue()
    {
        Debug.Log("End of dialogue");
    }
}
