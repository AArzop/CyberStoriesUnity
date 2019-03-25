using UnityEngine;
using UnityEngine.UI;

namespace CyberStories.Controllers
{
    public class MenuButtonController : MonoBehaviour
    {
        private bool _isMenuButtonActive;

        public bool isLevel;
        public Sprite UnselectedSprite;
        public Sprite SelectedSprite;

        public bool IsMenuButtonActive
        {
            get => _isMenuButtonActive;
            set
            {
                if (_isMenuButtonActive == value)
                    return;
                _isMenuButtonActive = value;
                Image image = GetComponent<Image>();
                Sprite currentSprite = _isMenuButtonActive ? SelectedSprite : UnselectedSprite;
                image.sprite = currentSprite;
            }
        }

        public void Start()
        {
            _isMenuButtonActive = false;
        }

        public void Exit()
        {
            #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public void Play()
        {
            // Change scene
        }
    }
}