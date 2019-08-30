using UnityEngine;
using UnityEngine.Serialization;

namespace CyberStories.Office.Controllers
{
    public class SpeakerController : MonoBehaviour
    {
        [FormerlySerializedAs("audioSource")] public AudioSource AudioSource;

        public void SwitchOnOffAudio()
        {
            if (AudioSource.isPlaying)
                AudioSource.Stop();
            else
                AudioSource.Play();
        }
    }
}