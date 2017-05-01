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

        public IEnumerable<int> GetAllYear()
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<League>().OrderBy(l => l.Year).Select(l => l.Year.Year).Distinct();
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
                return db.GetAllWithChildren<League>().Where(l => l.Year.Year == year.Year);
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

        public IEnumerable<League> GetLeaguesByMatches(IEnumerable<Match> matches)
        {
            List<League> leagues = new List<League>();

            foreach (var match in matches.DistinctBy(m => m.LeagueId))
            {
                leagues.Add(GetLeagueById(match.LeagueId));
            }

            return leagues;
        }

        public List<League> GetLeaguesByIds(List<int> leagueIds)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<League>(l => leagueIds.Contains(l.Id));
            }
        }

		public List<League> GetLeaguesByCountry(CountriesEnum country)
		{
			using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<League>(l => l.Country == country);
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
                try
                {
                    AddLeague(m.Id, m.Name, m.Year, m.type, m.ClassName, m.Rounds, m.Country, m.Sex);
                }
                catch (Exception)
                {
                }
            }
        }

        public async void AddLeaguesAsync(List<LeagueModel> model)
        {
            await Task.Run(() => AddLeagues(model));
        }

        #endregion

        #region PUT

        public int UpdateLeague(League league)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {

                League l = db.Find<League>(league.Id);

                l.Name = league.Name;
                l.Year = league.Year;
                l.Type = league.Type;
                l.ClassName = league.ClassName;
                l.Rounds = league.Rounds;
                l.Country = league.Country;
                l.Sex = league.Sex;

                db.Update(l);

                return l.Id;
            }

        }

        #endregion

    }
}
