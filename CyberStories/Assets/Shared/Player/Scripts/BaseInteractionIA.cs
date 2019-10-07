using UnityEngine;

namespace CyberStories.Shared.Player
{
    public abstract class BaseInteractionIA : MonoBehaviour
    {
        public Valve.VR.InteractionSystem.Player player;

        public bool LookAtPlayerOnIteraction = false;

        public abstract bool IsDone();

        public abstract float MinRemainingDistance();

        public void LookAtPlayer()
        {
            if (LookAtPlayerOnIteraction)
            {
                Vector3 relativePos = player.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            }
        }
    }
}