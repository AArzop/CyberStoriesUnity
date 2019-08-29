using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers.Leaderboard
{
    public class LeaderboardPlayerController : MonoBehaviour
    {
        [FormerlySerializedAs("PlayerNameText")] public Text playerNameText;
        [FormerlySerializedAs("PlayerScoreText")] public Text playerScoreText;

        public bool IsActive
        {
            set
            {
                gameObject.SetActive(value);
            }
        }
    }
}