using UnityEngine;

namespace CyberStories.Shared.Player
{
    public abstract class BaseInteractionIA : MonoBehaviour
    {
        public Valve.VR.InteractionSystem.Player player;

        public bool isLookingAtPlayerOnIteraction = false;

        public abstract bool IsDone();

        public abstract float MinRemainingDistance();

        public void LookAtPlayer()
        {
            if (isLookingAtPlayerOnIteraction)
            {
                Vector3 relativePos = player.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            }
        }
    }
}