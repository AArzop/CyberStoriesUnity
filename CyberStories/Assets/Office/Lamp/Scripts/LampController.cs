using UnityEngine;
using UnityEngine.Serialization;

namespace CyberStories.Office.Controllers
{
    public class LampController : MonoBehaviour
    {
        [FormerlySerializedAs("light")] public new Light Light;

        /// <summary>
        /// Switch on/off the light when the button is in the down state
        /// </summary>
        public void OnButtonDown()
        {
            Light.enabled = !Light.enabled;
        }
    }
}