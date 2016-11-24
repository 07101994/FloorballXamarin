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
                e.Time = time.ToString();
                e.Type = type;
                e.TeamId = teamId;

                db.Insert(e);

                if (playerId != -1 && type != "I" && type != "B")
                {
                    ChangeStatisticFromPlayer(playerId, teamId, type, db, "increase");
                }

                return e.Id;
            }
        }

        public void AddEvents(List<EventModel> model)
        {
            foreach (var m in model)
            {
                AddEvent(m.Id, m.MatchId, m.Type, m.Time, m.PlayerId, m.EventMessageId, m.TeamId);
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

            //db.Insert(stat);
            db.Update(stat);
            //db.Statistics.Attach(stat);

            //var entry = db.Entry(stat);
            //entry.Property(e => e.Number).IsModified = true;

        }

        #endregion

    }
}
