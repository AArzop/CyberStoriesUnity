using UnityEngine;

namespace CyberStories.Office.Controllers
{
    public class SpeakerController : MonoBehaviour
    {
        public AudioSource audioSource;

        public void SwitchOnOffAudio()
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            else
                audioSource.Play();
        }
    }
}