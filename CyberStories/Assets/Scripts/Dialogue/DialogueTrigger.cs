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
    public Animator animator;
    private bool isEnd = false;
    private bool isDialogue = false;

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
                isEnd = false;
                isDialogue = false;
                animator.SetBool("IsOpen", false);
            }
            else
            {
                FindObjectOfType<DialogueSystem>().StartDialogue(firstLine, textMeshPro);
                isDialogue = true;
                animator.SetBool("IsOpen", true);
            }
        }
    }
}
