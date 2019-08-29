using System.Collections.Generic;

namespace CyberStories.DataAccess
{
    public static class Player
    {
        /// <summary>
        /// Get the 3 best players from the server on the level <paramref name="level"/>
        /// </summary>
        /// <param name="level">The level to get the players</param>
        /// <returns></returns>
        public static IList<DBO.Player> GetBestPlayersByLevel(string level)
        {
            // TODO : Get data from db
            if (level == "Level 1")
                return new List<DBO.Player>
                {
                    new DBO.Player
                    {
                        id = 1,
                        name = "Toto",
                        score = 50
                    },
                    new DBO.Player
                    {
                        id = 2,
                        name = "Bis",
                        score = 7
                    }
                };
            else
                return new List<DBO.Player>
                {
                    new DBO.Player
                    {
                        id = 3,
                        name = "Abc",
                        score = 50
                    },
                    new DBO.Player
                    {
                        id = 4,
                        name = "def",
                        score = 7
                    },
                    new DBO.Player
                    {
                        id = 5,
                        name = "last",
                        score = 0
                    }
                };
        }
    }
}