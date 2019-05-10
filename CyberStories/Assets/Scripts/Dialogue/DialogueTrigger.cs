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
    public TextMeshProUGUI textMeshPro;
    private bool isDialogue = false;

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None)
        {
            if (isDialogue)
            {
                isDialogue = FindObjectOfType<DialogueSystem>().nextLine();
            }
            else
            {
                FindObjectOfType<DialogueSystem>().StartDialogue(firstLine, textMeshPro);
                isDialogue = true;
            }
        }
    }
}
