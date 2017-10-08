using Floorball.LocalDB.Tables;
using FloorballServer.Models.Floorball;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite.Net.Interop;

namespace Floorball.LocalDB.Repository
{
    public class EventRepository : Repository
    {
        public EventRepository(ISQLitePlatform platform, string databasePath) : base(platform, databasePath)
        {
        }

        #region GET


        public IEnumerable<Event> GetEventsByMatch(int matchId)
        {

            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Event>().Where(e => e.MatchId == matchId);
            }

        }

        public Event GetEventById(int eventId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetWithChildren<Event>(eventId);
            }
        }

		public IEnumerable<Event> GetEventsByLeague(int leagueId)
		{ 

			using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
				var matchIds = db.GetAllWithChildren<Match>().Where(m => m.LeagueId == leagueId).Select(m => m.Id);
				return db.GetAllWithChildren<Event>().Where(m => matchIds.Contains(m.MatchId));
            }

		}

        public Event GetAssisByGoal(int eventid)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var goal = db.Get<Event>(eventid);

                return db.GetAllWithChildren<Event>(e => e.Time == goal.Time && e.Type == EventType.A && e.MatchId == goal.MatchId).First();
            }

        }

        #endregion

        #region POST


        public int AddEvent(int id, int matchId, EventType type, TimeSpan time, int playerId, int evenetMessageId, int teamId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Event e = new Event();
                e.Id = id;
                e.MatchId = matchId;
                e.PlayerId = playerId;
                e.EventMessageId = evenetMessageId;
                e.Time = time;
                e.Type = type;
                e.TeamId = teamId;

                db.Insert(e);

                if (playerId != -1 && type != EventType.T && type != EventType.S)
                {
                    ChangeStatisticFromPlayer(playerId, teamId, (StatType)Enum.Parse(typeof(EventType), type.ToString()), db, "up");
                }

                if (type == EventType.G && !Manager.Instance.IsInit)
                {
                    ChangeMatchGoals(db,matchId,teamId, time, "up");
                }

                return e.Id;
            }
        }

        private void ChangeMatchGoals(SQLiteConnection db, int matchId, int teamId, TimeSpan time, string direction)
        {
            Match m = db.Get<Match>(matchId);
            m.Time = m.Time < time ? time : m.Time;
            if (teamId == m.HomeTeamId)
            {
                m.ScoreH = direction == "up" ? (short)(m.ScoreH + 1) : (short)(m.ScoreH - 1);
            }
            else
            {
                m.ScoreA = direction == "up" ? (short)(m.ScoreA + 1) : (short)(m.ScoreA - 1);
            }

            db.Update(m);
        }

        public void AddEvents(List<EventModel> model)
        {
            foreach (var m in model)
            {
                try
                {
                    AddEvent(m.Id, m.MatchId, m.Type, m.Time, m.PlayerId, m.EventMessageId, m.TeamId);
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion

        #region DELETE

        public void RemoveEvent(int eventId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var e = db.GetWithChildren<Event>(eventId);

                if (e != null)
                {
                    int t;

                    var match = db.GetWithChildren<Match>(e.MatchId);
                    var homeTeam = db.GetWithChildren<Team>(match.HomeTeamId);
                    var awayTeam = db.GetWithChildren<Team>(match.AwayTeamId);

                    if (homeTeam.Players.Select(p => p.Id).Contains(e.PlayerId))
                    {
                        t = homeTeam.Id;
                    }
                    else
                    {
                        t = awayTeam.Id;
                    }

                    ChangeStatisticFromPlayer(e.PlayerId, t, (StatType)Enum.Parse(typeof(EventType), e.Type.ToString()), db, "down");

                    if (e.Type == EventType.G && !Manager.Instance.IsInit)
                    {
                        ChangeMatchGoals(db, e.MatchId, e.TeamId, e.Time, "down");
                    }

                    db.Delete(e);

                }
            }
        }

        #endregion

        #region PUT

        private void ChangeStatisticFromPlayer(int playerId, int teamId, StatType type, SQLiteConnection db, string direction)
        {

            Statistic stat = db.GetAllWithChildren<Statistic>().Where(s => s.PlayerId == playerId && s.TeamId == teamId && s.Type == type).First();

            if (direction == "up")
            {
                stat.Number++;
            }
            else
            {
                stat.Number--;
            }

            db.Update(stat);

        }

        public int UpdateEvent(Event ev)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Event e = db.Find<Event>(ev.Id);

                e.Id = ev.Id;
                e.MatchId = ev.MatchId;
                e.PlayerId = ev.PlayerId;
                e.EventMessageId = ev.EventMessageId;
                e.Time = ev.Time;
                e.Type = ev.Type;
                e.TeamId = ev.TeamId;

                db.Update(e);

                //if (playerId != -1 && type != "I" && type != "B")
                //{
                //    ChangeStatisticFromPlayer(playerId, teamId, type, db, "increase");
                //}

                return e.Id;
            }
        }

        #endregion

    }
}
