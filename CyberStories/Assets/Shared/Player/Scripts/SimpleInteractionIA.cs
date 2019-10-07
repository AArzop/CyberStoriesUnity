using UnityEngine;
using Valve.VR.InteractionSystem;

namespace CyberStories.Shared.Player
{
    [RequireComponent(typeof(Interactable))]
    public class SimpleInteractionIA : BaseInteractionIA
    {
        private Interactable interactable;
        private DialogueTrigger dialogue;

        private bool interactionDone = false;

        private void Start()
        {
            interactable = GetComponent<Interactable>();
            dialogue = GetComponent<DialogueTrigger>();
        }

        private void HandHoverUpdate(Hand hand)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting();

            if (startingGrabType != GrabTypes.None)
            {
                if (LookAtPlayerOnIteraction)
                    LookAtPlayer();
                interactionDone = true;
            }
        }

        /// <summary>
        /// Check if the gameObject has been interacted once
        /// </summary>
        /// <returns>True if the user has ended an interaction with a gameObject. Else, false.</returns>
        public override bool IsDone()
        {
            return interactionDone && dialogue.DialogueHasEnded;
        }

        public override float MinRemainingDistance()
        {
            return 1.5f;
        }
    }
}