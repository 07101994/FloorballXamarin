using Floorball.LocalDB.Tables;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite.Net.Interop;

namespace Floorball.LocalDB.Repository
{
    public class StatisticRepository : Repository
    {
        public StatisticRepository(ISQLitePlatform platform, string databasePath) : base(platform, databasePath)
        {
        }

        #region GET

        public IEnumerable<Statistic> GetAllStatistic()
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Statistic>();
            }
        }

        public IEnumerable<Statistic> GetStatisticsByLeague(int leagueId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                IEnumerable<int> teams = db.GetAllWithChildren<Team>().Where(t => t.LeagueId == leagueId).Select(t => t.Id);

                return db.GetAllWithChildren<Statistic>().Where(s => teams.Contains(s.TeamId));
            }
        }

        public IEnumerable<Statistic> GetStatisticsByPlayer(int playerId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Statistic>().Where(s => s.PlayerId == playerId);
            }
        }

        #endregion

    }
}
