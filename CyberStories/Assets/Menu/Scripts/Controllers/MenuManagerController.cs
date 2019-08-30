using CyberStories.BusinessManagement;
using CyberStories.Menu.Controllers.Leaderboard;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers
{
    public class MenuManagerController : MonoBehaviour
    {
        #region fields

        private static readonly IDictionary<string, string> ScenesByTag = new Dictionary<string, string>
        {
            {"Level 1", "Fishing/Fishing"}
        };
        
        public Canvas UiHeaderCanvas;
        
        public Text DescriptionText;
        
        public LeaderboardController LeaderboardController;

        private string currentTag;

        public LevelChangerController.LevelChanger LevelChanger;

        private PlayersDataAccess dataConnect;

        #endregion

        // Start is called before the first frame update
        private void Start()
        {
            dataConnect = GetComponent<PlayersDataAccess>();
        }

        public void ButtonMenuClicked(MenuButtonController buttonController)
        {
            buttonController.IsMenuButtonActive = true;
            Button current = buttonController.GetComponent<Button>();

            // Get all active button controllers
            List<MenuButtonController> buttons = UiHeaderCanvas.GetComponentsInChildren<Button>()
                .Where(button => button != current && button.GetComponent<MenuButtonController>() != null)
                .Select(button => button.GetComponent<MenuButtonController>())
                .Where(button => button.IsMenuButtonActive)
                .ToList();

            foreach (var button in buttons)
                button.IsMenuButtonActive = false;

            currentTag = current.tag;
            UpdateLevelContentTag();
        }

        private void UpdateLevelContentTag()
        {
            // Update description content
            DescriptionText.text = Level.GetDescriptionByTag(currentTag);

            // Update leaderboard
            if (dataConnect.IsLoading)
            {
                LeaderboardController.DisplayError("Récupération des meilleurs agents en cours");
            }
            else if (!dataConnect.IsError)
            {
                IList<DBO.Player> players = Player.GetBestPlayersByLevel(currentTag, dataConnect.Response);
                LeaderboardController.UpdateLeaderboard(players);
            }
            else
            {
                LeaderboardController.DisplayError("Erreur de téléchargement.");
            }
        }

        public void PlayButtonClicked()
        {
            if (currentTag == null)
                return;
            if (!ScenesByTag.TryGetValue(currentTag, out string sceneName)) return;
            LevelChanger.SceneToLoad = sceneName;
            LevelChanger.ChangeScene();
        }
    }
}