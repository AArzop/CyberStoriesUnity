using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers.Leaderboard
{
    public class LeaderboardController : MonoBehaviour
    {
        public Text ErrorMessage;

        public void UpdateLeaderboard(IList<DBO.Player> players)
        {
            ErrorMessage.text = "";
            LeaderboardPlayerController[] playerControllers = GetComponentsInChildren<LeaderboardPlayerController>(true);
            int maxPlayer = playerControllers.Length;
            if (players.Count < playerControllers.Length)
            {
                SetLeaderboardPlayerVisibility(players.Count);
                maxPlayer = players.Count;
            }

            for (int i = 0; i < maxPlayer; ++i)
                UpdatePlayer(i, players[i]);
        }

        /// <summary>
        /// Set player name and his score in the leaderboard
        /// </summary>
        /// <param name="rank">The player rank (0 based)</param>
        /// <param name="player">The player to be added on the leaderboard</param>
        private void UpdatePlayer(int rank, DBO.Player player)
        {
            LeaderboardPlayerController playerController = GetComponentsInChildren<LeaderboardPlayerController>(true)[rank];
            playerController.PlayerNameText.text = player.Name;
            playerController.PlayerScoreText.text = player.Score.ToString();
            playerController.IsActive = true;
        }

        private void SetLeaderboardPlayerVisibility(int rankMin)
        {
            LeaderboardPlayerController[] playerControllers = GetComponentsInChildren<LeaderboardPlayerController>(true);

            for (int i = rankMin; i < playerControllers.Length; ++i)
                playerControllers[i].IsActive = false;
        }

        public void DisplayError(string errMsg)
        {
            SetLeaderboardPlayerVisibility(0);
            ErrorMessage.text = errMsg;
        }
    }
}