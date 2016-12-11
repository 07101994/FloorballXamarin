using Floorball.LocalDB.Tables;
using FloorballServer.Models.Floorball;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Floorball.LocalDB.Repository
{
    public class LeagueRepository : Repository
    {

        #region GET


        public IEnumerable<League> GetAllLeague()
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<League>();
            }
        }

        public IEnumerable<DateTime> GetAllYear()
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<League>().Select(l => l.Year).Distinct().OrderBy(t => t.Year);
            }
        }

        public int GetNumberOfRoundsInLeague(int leagueId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<League>().Where(l => l.Id == leagueId).First().Rounds;
            }
        }

        public League GetLeagueById(int leagueId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetWithChildren<League>(leagueId);
            }
        }

        public IEnumerable<League> GetLeaguesByYear(DateTime year)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<League>().Where(l => l.Year == year);
            }
        }


        public IEnumerable<League> GetLeaguesByReferee(int refereeId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var leagueIds = db.GetWithChildren<Referee>(refereeId).Matches.Select(m => m.LeagueId).Distinct();

                List<League> leagues = new List<League>();
                foreach (var id in leagueIds)
                {
                    leagues.Add(db.Get<League>(id));
                }

                return leagues;
            }
        }

        #endregion

        #region POST

        public int AddLeague(int id, string name, DateTime year, string type, string classname, int rounds, CountriesEnum country, string sex)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {

                League l = new League();
                l.Id = id;
                l.Name = name;
                l.Year = year;
                l.Type = type;
                l.ClassName = classname;
                l.Rounds = rounds;
                l.Country = country;
				l.Sex = sex;

                db.Insert(l);

                return l.Id;
            }
        }

        public void AddLeagues(List<LeagueModel> model)
        {
            foreach (var m in model)
            {
                AddLeague(m.Id, m.Name, m.Year, m.type, m.ClassName, m.Rounds, m.Country, m.Sex);
            }
        }

        public async void AddLeaguesAsync(List<LeagueModel> model)
        {
            await Task.Run(() => AddLeagues(model));
        }

        #endregion


    }
}
