using Floorball.LocalDB.Tables;
using FloorballServer.Models.Floorball;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Floorball.LocalDB.Repository
{
    public class TeamRepository : Repository
    {

        #region GET

        public IEnumerable<Team> GetTeamsByLeague(int id)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {

                return db.GetAllWithChildren<Team>(t => t.LeagueId == id);

            }
        }

        public IEnumerable<Team> GetTeamsByYear(DateTime year)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Team>().Where(t => t.Year == year);
            }
        }

        public IEnumerable<Team> GetAllTeam()
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Team>();
            }
        }

        public Team GetTeamById(int id)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetWithChildren<Team>(id);
            }
        }

		public IEnumerable<Team> GetTeamsByPlayer(int playerId)
		{
			using (var db = new SQLiteConnection(Platform, DatabasePath))
			{
				return db.GetWithChildren<Player>(playerId).Teams;
			}
		}

		public List<Team> GetTeamsByMatches(IEnumerable<Match> matches)
		{
			List<Team> teams = new List<Team>();

			foreach (var match in matches)
			{

				teams.Add(GetTeamById(match.HomeTeamId));
				teams.Add(GetTeamById(match.AwayTeamId));
			}

			return teams;
		}

        #endregion


        #region POST

        public int AddTeam(int id, string name, DateTime year, string coach, string sex, CountriesEnum country, int stadiumId, int leagueId, string imageName, short get = 0, short scored = 0, short points = 0, short standing = -1)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Team t = new Team();
                t.Id = id;
                t.Name = name;
                t.Year = year;
                t.Coach = coach;
                t.Sex = sex;
                t.Country = country;
                t.Get = get;
                t.Scored = scored;
                t.Points = points;
                t.StadiumId = stadiumId;
                t.LeagueId = leagueId;
                t.Standing = standing != -1 ? standing : (short)(db.GetAllWithChildren<Team>().Where(t1 => t1.LeagueId == leagueId).Count() + 1);
                t.ImageName = imageName;

                db.Insert(t);

                return t.Id;
            }
        }

        public void AddTeams(List<TeamModel> model)
        {
            foreach (var m in model)
            {
                try
                {
                    if (m.Image != null)
                    {
                        ImageManager.SaveImage(m.Image, m.ImageName);
                    }
                    AddTeam(m.Id, m.Name, m.Year, m.Coach, m.Sex, m.Country, m.StadiumId, m.LeagueId, m.ImageName, m.Get, m.Scored, m.Points, m.Standing);
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion

        #region PUT

        public int UpdateTeam(Team team)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Team t = db.Find<Team>(team.Id);

                t.Name = team.Name;
                t.Year = team.Year;
                t.Coach = team.Coach;
                t.Sex = team.Sex;
                t.Country = team.Country;
                t.Get = team.Get;
                t.Scored = team.Scored;
                t.Points = team.Points;
                t.StadiumId = team.StadiumId;
                t.LeagueId = team.LeagueId;
                t.ImageName = team.ImageName;

                db.Update(t);

                return t.Id;
            }
        }

        #endregion

    }
}
