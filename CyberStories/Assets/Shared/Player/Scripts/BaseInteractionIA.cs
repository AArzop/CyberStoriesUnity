using UnityEngine;

namespace CyberStories.Shared.Player
{
    public abstract class BaseInteractionIA : MonoBehaviour
    {
        public abstract bool isDone();

        public abstract float MinRemainingDistance();
    }
}