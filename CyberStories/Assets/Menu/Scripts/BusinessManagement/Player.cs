using System.Collections.Generic;
using UnityEngine;

namespace CyberStories.BusinessManagement
{
    public static class Player
    {
        /// <summary>
        /// <see cref="DataAccess.Player.GetBestPlayersByLevel(string)"/>
        /// </summary>
        /// <param name="level">The level to get the players</param>
        /// <returns></returns>
        public static IList<DBO.Player> GetBestPlayersByLevel(string level, string response)
        {
            Debug.Log(response);
            return DataAccess.Player.GetBestPlayersByLevel(level);
        }
    }
}