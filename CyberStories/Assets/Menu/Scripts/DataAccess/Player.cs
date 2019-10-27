using CyberStories.Shared.ScriptUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberStories.DataAccess
{
    public static class Player
    {
        public static IDictionary<string, Func<PlayerModel, double>> ScoreDictionaryUtils { get; private set; }

        static Player()
        {
            // TODO: Must update the backend in order to reduce this code
            ScoreDictionaryUtils = new Dictionary<string, Func<PlayerModel, double>>()
            {
                { "Level 1", (playerModel) => playerModel.gameResults.Max(x => x.stage1Score) },
                { "Level 2", (playerModel) => playerModel.gameResults.Max(x => x.stage2Score) },
                { "Level 3", (playerModel) => playerModel.gameResults.Max(x => x.stage3Score) },
                { "Level 4", (playerModel) => playerModel.gameResults.Max(x => x.stage4Score) },
            };
        }

        /// <summary>
        /// Get the from the server
        /// </summary>
        /// <returns>The list of players</returns>
        public static List<DBO.Player> GetPlayersScoreByLevel(PlayersDataAccess dataConnect, string level)
        {
            List<PlayerModel> playerModels = JsonHelper.FromJson<PlayerModel>(dataConnect.Response).ToList();
            return playerModels.Select(playerModel => new DBO.Player
                               {
                                   Id = playerModel.id,
                                   Score = ScoreDictionaryUtils[level](playerModel),
                                   Name = playerModel.pseudo,
                                   Email = playerModel.email
                               })
                               .ToList();
        }
    }
}