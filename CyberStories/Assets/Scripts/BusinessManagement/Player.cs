using System.Collections.Generic;

namespace CyberStories.BusinessManagement
{
    public static class Player
    {
        /// <summary>
        /// <see cref="DataAccess.Player.GetBestPlayersByLevel(string)"/>
        /// </summary>
        /// <param name="level">The level to get the players</param>
        /// <returns></returns>
        public static IList<DBO.Player> GetBestPlayersByLevel(string level)
        {
            return DataAccess.Player.GetBestPlayersByLevel(level);
        }
    }
}