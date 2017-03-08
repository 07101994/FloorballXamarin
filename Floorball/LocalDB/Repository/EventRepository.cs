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
    public class EventRepository : Repository
    {
        
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

        #endregion

        #region POST


        public int AddEvent(int id, int matchId, string type, TimeSpan time, int playerId, int evenetMessageId, int teamId)
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

                if (playerId != -1 && type != "I" && type != "B")
                {
                    ChangeStatisticFromPlayer(playerId, teamId, type, db, "increase");
                }

                if (type == "G")
                {
                    AddGoalToMatch(matchId,teamId, db);
                }

                return e.Id;
            }
        }

        private void AddGoalToMatch(int matchId, int teamId, SQLiteConnection db)
        {
            Match m = db.Get<Match>(matchId);
            if (teamId == m.HomeTeamId)
            {
                m.GoalsH++;
            }
            else
            {
                m.GoalsA++;
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

                if (e.Type == "G")
                {
                    int t;

                    var match = db.GetWithChildren<Match>(e.MatchId);
                    var homeTeam = db.GetWithChildren<Team>(match.HomeTeamId);
                    var awayTeam = db.GetWithChildren<Team>(match.AwayTeamId);

                    if (homeTeam.Players.Select(p => p.RegNum).Contains(e.PlayerId))
                    {
                        t = homeTeam.Id;
                    }
                    else
                    {
                        t = awayTeam.Id;
                    }

                    ChangeStatisticFromPlayer(e.PlayerId, t, e.Type, db, "reduce");

                    var e1 = db.GetAllWithChildren<Event>().Where(ev => ev.MatchId == e.MatchId && ev.Time == e.Time && ev.Type == "A").First();

                    var match1 = db.GetWithChildren<Match>(e1.MatchId);
                    var homeTeam1 = db.GetWithChildren<Team>(match1.HomeTeamId);
                    var awayTeam1 = db.GetWithChildren<Team>(match1.AwayTeamId);


                    if (homeTeam1.Players.Select(p => p.RegNum).Contains(e1.PlayerId))
                    {
                        t = homeTeam1.Id;
                    }
                    else
                    {
                        t = awayTeam1.Id;
                    }

                    db.Delete(e1);
                }

                db.Delete(e);
            }
        }

        #endregion

        #region PUT

        private void ChangeStatisticFromPlayer(int playerId, int teamId, string type, SQLiteConnection db, string direction)
        {

            Statistic stat = db.GetAllWithChildren<Statistic>().Where(s => s.PlayerRegNum == playerId && s.TeamId == teamId && s.Name == type).First();

            if (direction == "increase")
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
