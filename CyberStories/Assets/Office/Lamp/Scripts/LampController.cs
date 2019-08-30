using UnityEngine;

namespace CyberStories.Office.Controllers
{
    public class LampController : MonoBehaviour
    {
        public new Light light;

        /// <summary>
        /// Switch on/off the light when the button is in the down state
        /// </summary>
        public void OnButtonDown()
        {
            light.enabled = !light.enabled;
        }
    }
}