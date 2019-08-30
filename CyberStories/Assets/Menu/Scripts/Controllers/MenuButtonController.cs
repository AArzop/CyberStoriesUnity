using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers
{
    public class MenuButtonController : MonoBehaviour
    {
        private bool isMenuButtonActive;

        [FormerlySerializedAs("isLevel")] public bool IsLevel;
        [FormerlySerializedAs("unselectedSprite")] public Sprite UnselectedSprite;
        [FormerlySerializedAs("selectedSprite")] public Sprite SelectedSprite;

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