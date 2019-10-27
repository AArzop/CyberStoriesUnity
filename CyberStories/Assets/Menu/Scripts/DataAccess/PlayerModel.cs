using System;
using System.Collections.Generic;

namespace CyberStories.DataAccess
{
    [Serializable]
    public class PlayerModel
    {
        public int id;
        public List<GameResult> gameResults;
        public string pseudo;
        public string email;
    }
}