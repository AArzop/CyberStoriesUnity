using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using CyberStories.BusinessManagement;
using CyberStories.Controllers.Leaderboard;

namespace CyberStories.Controllers
{
    public class MenuManagerController : MonoBehaviour
    {
        #region fields
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
                                                               .Where(button => button != current)
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


    }
}