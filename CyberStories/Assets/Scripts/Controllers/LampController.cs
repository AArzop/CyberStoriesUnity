using UnityEngine;

namespace CyberStories.Controllers
{
    public class LampController : MonoBehaviour
    {
        public Light Light;

        /// <summary>
        /// Switch on/off the light when the button is in the down state
        /// </summary>
        public void OnButtonDown()
        {
            Light.enabled = !Light.enabled;
        }
    }
}