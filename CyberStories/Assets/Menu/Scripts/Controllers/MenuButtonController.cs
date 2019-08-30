using UnityEngine;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers
{
    public class MenuButtonController : MonoBehaviour
    {
        private bool isMenuButtonActive;

        public bool IsLevel;
        public Sprite UnselectedSprite;
        public Sprite SelectedSprite;

        public bool IsMenuButtonActive
        {
            get => isMenuButtonActive;
            set
            {
                if (isMenuButtonActive == value)
                    return;
                isMenuButtonActive = value;
                Image image = GetComponent<Image>();
                Sprite currentSprite = isMenuButtonActive ? SelectedSprite : UnselectedSprite;
                image.sprite = currentSprite;
            }
        }

        public void Start()
        {
            isMenuButtonActive = false;
        }
    }
}