using System.Collections.Generic;

namespace CyberStories.DataAccess
{
    public static class Player
    {
        /// <summary>
        /// Get X best players from the server on the level <paramref name="level"/>
        /// </summary>
        /// <param name="level">The level to get the players</param>
        /// <returns></returns>
        public static IList<DBO.Player> GetBestPlayersByLevel(string level)
        {
            // TODO : Get data from db
            return new List<DBO.Player>
            {
                new DBO.Player
                {
                    Id = 1,
                    Name = "Toto",
                    Score = 50
                },
                new DBO.Player
                {
                    Id = 1,
                    Name = "Bis",
                    Score = 90
                }
            };
        }
    }
}