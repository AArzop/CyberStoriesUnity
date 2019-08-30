using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CyberStories.Menu.Controllers.Leaderboard
{
    public class LeaderboardPlayerController : MonoBehaviour
    {
        [FormerlySerializedAs("playerNameText")] public Text PlayerNameText;
        [FormerlySerializedAs("playerScoreText")] public Text PlayerScoreText;

        public bool IsActive
        {
            set => gameObject.SetActive(value);
        }
    }
}