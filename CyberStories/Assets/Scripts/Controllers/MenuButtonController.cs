using UnityEngine;
using UnityEngine.UI;

namespace CyberStories.Controllers
{
    public class MenuButtonController : MonoBehaviour
    {
        private bool _isMenuButtonActive;

        public bool isLevel;
        private static Color DefaultHighlightedColor;
        private static Color DefaultNormalColor;
        private static Color DefaultSelectedColor;

        static MenuButtonController()
        {
            DefaultHighlightedColor = Color.yellow;
            DefaultNormalColor = Color.white;
            DefaultSelectedColor = Color.blue;
        }

        public bool IsMenuButtonActive
        {
            get => _isMenuButtonActive;
            set
            {
                if (_isMenuButtonActive == value)
                    return;
                _isMenuButtonActive = value;
                Button button = GetComponent<Button>();
                ColorBlock colorBlock = button.colors;

                if (_isMenuButtonActive)
                    colorBlock.normalColor = DefaultSelectedColor;
                else
                    colorBlock.normalColor = DefaultNormalColor;
                button.colors = colorBlock;
            }
        }

        public void Start()
        {
            _isMenuButtonActive = false;
            ColorBlock colorBlock = GetComponent<Button>().colors;
            colorBlock.highlightedColor = DefaultHighlightedColor;
            GetComponent<Button>().colors = colorBlock;
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