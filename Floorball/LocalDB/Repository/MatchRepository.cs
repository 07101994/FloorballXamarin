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
    public class MatchRepository : Repository
    {

        #region GET


        public IEnumerable<Match> GetMatchesByLeague(int id)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Match>().Where(m => m.LeagueId == id);
            }
        }

        public Match GetMatchById(int id)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetWithChildren<Match>(id);
            }
        }

        public IEnumerable<Match> GetActualMatches()
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                DateTime threshold = DateTime.Now.AddDays(3).AddYears(-1);

                return db.GetAllWithChildren<Match>().Where(m => DateTime.Compare(DateTime.Now.AddYears(-1), m.Date) < 0 && DateTime.Compare(m.Date, threshold) < 0 || m.State == StateEnum.Playing);
            }
        }

        public IEnumerable<Match> GetMatchesByPlayer(int playerId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Player>().Where(p => p.RegNum == playerId).First().Matches;

            }
        }

        public IEnumerable<Match> GetMatchesByTeam(int teamId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Match>().Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId);
            }
        }

        public IEnumerable<Match> GetAllMatch()
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Match>();
            }
        }

        public IEnumerable<Match> GetMatchesByReferee(int refereeId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Referee>().Where(r => r.Id == refereeId).First().Matches;

            }
        }

        #endregion

        #region POST

        public void AddMatch(int id, int homeTeamId, int awayTeamId, short goalsH, short goalsA, short round, StateEnum state, TimeSpan time, DateTime date, int leagueId, int stadiumId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Match m = new Match();
                m.Id = id;
                m.HomeTeamId = homeTeamId;
                m.AwayTeamId = awayTeamId;
                m.GoalsH = goalsH;
                m.GoalsA = goalsA;
                m.LeagueId = leagueId;
                m.Round = round;
                m.StadiumId = stadiumId;
                m.State = state;
                m.Time = time;
                m.Date = date;
                m.Referees = new List<Referee>();
                m.Players = new List<Player>();

                db.Insert(m);
            }
        }

        public void AddMatches(List<MatchModel> model)
        {
            foreach (var m in model)
            {
                AddMatch(m.Id, m.HomeTeamId, m.AwayTeamId, m.GoalsH, m.GoalsA, m.Round, m.State, m.Time, m.Date, m.LeagueId, m.StadiumId);
            }
        }

        #endregion

        #region PUT

        public void UpdateMatchState(int matchId, StateEnum newState)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Match match = db.Get<Match>(matchId);
                match.State = newState;
                db.Update(match);

            }
        }

        public void UpdateMatchTime(int matchId, TimeSpan newTime)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Match match = db.Get<Match>(matchId);
                match.Time = newTime;
                db.Update(match);

            }
        }

        #endregion

    }
}
