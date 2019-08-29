using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers
{
    public class MenuButtonController : MonoBehaviour
    {
        private bool isMenuButtonActive;

        public bool isLevel;
        [FormerlySerializedAs("UnselectedSprite")] public Sprite unselectedSprite;
        [FormerlySerializedAs("SelectedSprite")] public Sprite selectedSprite;

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