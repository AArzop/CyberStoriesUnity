using CyberStories.DBO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class DialogueTrigger : MonoBehaviour
{
    [FormerlySerializedAs("firstLine")] public DialogueLine FirstLine;
    [FormerlySerializedAs("canvas")] public Canvas Canvas;
    [FormerlySerializedAs("textMeshPro")] public TextMeshProUGUI TextMeshPro;
    [FormerlySerializedAs("animator")] public Animator Animator;
    private bool isEnd = false;
    private bool isDialogue = false;

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None)
        {
            if (isDialogue && !isEnd)
            {
                isEnd = FindObjectOfType<DialogueSystem>().NextLine();                
                if (isEnd)
                {                    
                    isDialogue = false;
                }
            }
            else if (!isDialogue && isEnd)
            {
                isEnd = false;
                isDialogue = false;
                Animator.SetBool("IsOpen", false);
            }
            else
            {
                FindObjectOfType<DialogueSystem>().StartDialogue(FirstLine, TextMeshPro);
                isDialogue = true;
                Animator.SetBool("IsOpen", true);
            }
        }
    }
}
