using System;

namespace CyberStories.DataAccess
{
    [Serializable]
    public class GameResult
    {
        public int id;
        public string date;
        public double stage1Score;
        public double stage2Score;
        public double stage3Score;
        public double stage4Score;
        public bool visible;
        public int player;
    }
}