using UnityEngine;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers
{
    public class MenuButtonController : MonoBehaviour
    {
        private bool isMenuButtonActive;

        public bool isLevel;
        public Sprite unselectedSprite;
        public Sprite selectedSprite;

        public bool IsMenuButtonActive
        {
            get => isMenuButtonActive;
            set
            {
                if (isMenuButtonActive == value)
                    return;
                isMenuButtonActive = value;
                Image image = GetComponent<Image>();
                Sprite currentSprite = isMenuButtonActive ? selectedSprite : unselectedSprite;
                image.sprite = currentSprite;
            }
        }

        public void Start()
        {
            isMenuButtonActive = false;
        }
    }
}