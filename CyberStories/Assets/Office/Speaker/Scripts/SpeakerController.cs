using UnityEngine;
using UnityEngine.Serialization;

namespace CyberStories.Office.Controllers
{
    public class SpeakerController : MonoBehaviour
    {
        [FormerlySerializedAs("_audioSource")] public AudioSource audioSource;

        public void SwitchOnOffAudio()
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            else
                audioSource.Play();
        }
    }
}