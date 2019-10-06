using UnityEngine;
using Valve.VR.InteractionSystem;

namespace CyberStories.Shared.Player
{
    [RequireComponent(typeof(Interactable))]
    public class SimpleInteractionIA : BaseInteractionIA
    {
        private Interactable interactable;

        [Min(1)]
        public uint interactionRequired = 1;
        private uint interactionDone = 0;

        private void Start()
        {
            interactable = GetComponent<Interactable>();
        }

        private void HandHoverUpdate(Hand hand)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting();

            if (startingGrabType != GrabTypes.None)
            {
                ++interactionDone;
            }
        }

        /// <summary>
        /// Check if the gameObject has been interacted once
        /// </summary>
        /// <returns>True if the user has ended an interaction with a gameObject. Else, false.</returns>
        public override bool isDone()
        {
            return interactionDone >= interactionRequired;
        }

        public override float MinRemainingDistance()
        {
            return 1.5f;
        }
    }
}