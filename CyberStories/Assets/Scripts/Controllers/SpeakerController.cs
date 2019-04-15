using UnityEngine;

namespace CyberStories.Controllers
{
    public class SpeakerController : MonoBehaviour
    {
        public AudioSource _audioSource;

        public void SwitchOnOffAudio()
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
            else
                _audioSource.Play();
        }
    }
}