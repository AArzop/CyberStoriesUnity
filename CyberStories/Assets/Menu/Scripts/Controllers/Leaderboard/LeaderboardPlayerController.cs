﻿using UnityEngine;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers.Leaderboard
{
    public class LeaderboardPlayerController : MonoBehaviour
    {
        public Text playerNameText;
        public Text playerScoreText;

        public bool IsActive
        {
            set => gameObject.SetActive(value);
        }
    }
}