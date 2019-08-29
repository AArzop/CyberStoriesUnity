using System.Collections;
using UnityEngine;
using CyberStories.DBO;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    private DialogueLine actualLine;
    private TextMeshProUGUI textDialogue;

    public void StartDialogue(DialogueLine firstLine, TextMeshProUGUI tmPro)
    {
        actualLine = firstLine;
        textDialogue = tmPro;
        NextLine();
    }

    //answer is the answer that you clicked on (its list number, -1 is when you call the function without answers)
    //returns false if dialogue end, true otherwise
    public bool NextLine(int answer = -1)
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(actualLine.name + ": " + actualLine.textLine));

        if (actualLine.answers.Length == 0 || actualLine.nextLine.Length != actualLine.answers.Length)
        {
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

    private IEnumerator TypeSentence(string sentence)
    {
        textDialogue.text = "";
        foreach (char letter in sentence)
        {
            textDialogue.text += letter;
            yield return null;
        }
    }
}