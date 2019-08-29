using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers.Leaderboard
{
    public class LeaderboardPlayerController : MonoBehaviour
    {
        public Text playerNameText;
        public Text playerScoreText;

        public bool IsActive
        {
            set
            {
                gameObject.SetActive(value);
            }
        }
    }
}