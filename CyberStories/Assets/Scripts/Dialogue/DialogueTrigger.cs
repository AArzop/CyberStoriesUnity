using CyberStories.DBO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class DialogueTrigger : MonoBehaviour
{
    public DialogueLine firstLine;
    public Canvas canvas;
    public TextMeshProUGUI textMeshPro;
    private bool isEnd = false;
    private bool isDialogue = false;

    private void Start()
    {
        canvas.GetComponent<CanvasGroup>().alpha = 0f;
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None)
        {
            if (isDialogue && !isEnd)
            {
                isEnd = FindObjectOfType<DialogueSystem>().nextLine();                
                if (isEnd)
                {
                    isDialogue = false;
                }
            }
            else if (!isDialogue && isEnd)
            {
                canvas.GetComponent<CanvasGroup>().alpha = 0f;
                isEnd = false;
                isDialogue = false;
            }
            else
            {
                FindObjectOfType<DialogueSystem>().StartDialogue(firstLine, textMeshPro);
                isDialogue = true;
                canvas.GetComponent<CanvasGroup>().alpha = 1f;
            }
        }
    }
}
