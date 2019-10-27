using System.Collections.Generic;
using System.Linq;

namespace CyberStories.BusinessManagement
{
    public static class Player
    {
        private static readonly int PlayerLimit = 5;

        /// <summary>
        /// <see cref="DataAccess.Player.GetBestPlayersByLevel(string)"/>
        /// </summary>
        /// <param name="level">The level to get the players</param>
        /// <returns></returns>
        public static IList<DBO.Player> GetBestPlayersByLevel(string level, PlayersDataAccess dataConnect)
        {
            List<DBO.Player> playerModels = DataAccess.Player.GetPlayersScoreByLevel(dataConnect, level);
            return playerModels.OrderByDescending(player => player.Score)
                               .Take(PlayerLimit)
                               .ToList();
        }
    }
}