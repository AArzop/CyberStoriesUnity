using UnityEngine;
using UnityEngine.UI;

namespace CyberStories.Controllers.Leaderboard
{
    public class LeaderboardPlayerController : MonoBehaviour
    {
        public Text PlayerNameText;
        public Text PlayerScoreText;

        public bool IsActive
        {
            set
            {
                gameObject.SetActive(value);
            }
        }
    }
}