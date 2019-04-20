using CyberStories.BusinessManagement;
using CyberStories.Menu.Controllers.Leaderboard;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers
{
    public class MenuManagerController : MonoBehaviour
    {
        #region fields
        private static readonly IDictionary<string, string> ScenesByTag = new Dictionary<string, string>
        {
            { "Level 1", "Office" }
        };

        public Canvas UIHeaderCanvas;

        public Text DescriptionText;

        public LeaderboardController LeaderboardController;

        private string _currentTag;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            Button[] buttons = UIHeaderCanvas.GetComponentsInChildren<Button>();
        }

        public void ButtonMenuClicked(MenuButtonController buttonController)
        {
            buttonController.IsMenuButtonActive = true;
            Button current = buttonController.GetComponent<Button>();

            // Get all active button controllers
            List<MenuButtonController> buttons = UIHeaderCanvas.GetComponentsInChildren<Button>()
                                                               .Where(button => button != current && button.GetComponent<MenuButtonController>() != null)
                                                               .Select(button => button.GetComponent<MenuButtonController>())
                                                               .Where(button => button.IsMenuButtonActive)
                                                               .ToList();

            foreach (var button in buttons)
                button.IsMenuButtonActive = false;

            _currentTag = current.tag;
            UpdateLevelContentTag();
        }

        private void UpdateLevelContentTag()
        {
            // Update description content
            DescriptionText.text = Level.GetDescriptionByTag(_currentTag);

            // Update leaderboard
            IList<DBO.Player> players = Player.GetBestPlayersByLevel(_currentTag);

            LeaderboardController.UpdateLeaderboard(players);
        }

        public void PlayButtonClicked()
        {
            if (_currentTag == null)
                return;
            if (ScenesByTag.TryGetValue(_currentTag, out string sceneName))
                SceneManager.LoadScene(sceneName);      // TODO: Crash
        }

    }
}