using UnityEngine;

namespace CyberStories.Office.Controllers
{
    public class SpeakerController : MonoBehaviour
    {
        public AudioSource AudioSource;

        public void SwitchOnOffAudio()
        {
            if (AudioSource.isPlaying)
                AudioSource.Stop();
            else
                AudioSource.Play();
        }
    }
}