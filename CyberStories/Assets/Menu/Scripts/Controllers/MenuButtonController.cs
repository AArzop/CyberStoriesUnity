using UnityEngine;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers
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
    }
}