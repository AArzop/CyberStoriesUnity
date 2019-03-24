using System.Collections.Generic;
using UnityEngine;

namespace CyberStories.Controllers.Leaderboard
{
    public class LeaderboardController : MonoBehaviour
    {

        public void UpdateLeaderboard(IList<DBO.Player> players)
        {
            LeaderboardPlayerController[] playerControllers = GetComponentsInChildren<LeaderboardPlayerController>();
            int maxPlayer = playerControllers.Length;
            if (players.Count < playerControllers.Length)
            {
                SetLeaderboardPlayerVisibility(players.Count, false);
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
            LeaderboardPlayerController playerController = GetComponentsInChildren<LeaderboardPlayerController>()[rank];
            playerController.PlayerNameText.text = player.Name;
            playerController.PlayerScoreText.text = player.Score.ToString();
            playerController.IsActive = true;
        }

        private void SetLeaderboardPlayerVisibility(int rankMin, bool isVisible)
        {
            LeaderboardPlayerController[] playerControllers = GetComponentsInChildren<LeaderboardPlayerController>();

            for (int i = rankMin; i < playerControllers.Length; ++i)
                playerControllers[i].IsActive = false;
        }
    }
}