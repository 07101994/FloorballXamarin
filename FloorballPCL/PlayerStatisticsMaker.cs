using Floorball.LocalDB.Tables;
using FloorballServer.Models.Floorball;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Floorball
{
    public class PlayerStatisticsMaker
    {

        public static IEnumerable<PlayerStatisticsModel> CreatePlayerStatistics(IEnumerable<Statistic> statistics)
        {

            List<PlayerStatisticsModel> models = new List<PlayerStatisticsModel>();

            IEnumerable<int> ids = statistics.Select(s => s.PlayerId).Distinct();
            foreach (var id in ids)
            {
                IEnumerable<Statistic> playerStats = statistics.Where(s => s.PlayerId == id);

                PlayerStatisticsModel model = new PlayerStatisticsModel();
                model.PlayerId = id;
                model.TeamId = playerStats.First().TeamId;
                model.Goals = playerStats.Where(s => s.Type == StatType.G).First().Number;
                model.Assists = playerStats.Where(s => s.Type ==StatType.A).First().Number;

                int penaltyTime = playerStats.Where(s => s.Type == StatType.P2).First().Number * 2;
                penaltyTime += playerStats.Where(s => s.Type == StatType.P5).First().Number * 5;

                model.Penalties = penaltyTime + " (" + playerStats.Where(s => s.Type == StatType.P10).First().Number * 10 + ")";

                models.Add(model);

            }

            return models;

        }

    }
}
