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
    public class RefereeRepository : Repository
    {

        #region GET

        public IEnumerable<Referee> GetAllReferee()
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Referee>();
            }
        }

        public Referee GetRefereeById(int id)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetWithChildren<Referee>(id);
            }
        }

       

        #endregion

        #region POST

        public int AddReferee(int id, string name, CountriesEnum country, short number = 0, short penalty = 0)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Referee r = new Referee();
                r.Id = id;
                r.Name = name;
                r.Number = number;
                r.Penalty = penalty;
                r.Matches = new List<Match>();
                r.Country = country;

                db.Insert(r);

                return r.Id;
            }
        }

        public void AddReferees(List<RefereeModel> model)
        {
            foreach (var m in model)
            {
                try
                {
                    AddReferee(m.Id, m.Name, m.Country, m.Number, m.Penalty);
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion

        #region PUT

        public void AddRefereeToMatch(int refereeId, int matchId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var referee = db.GetWithChildren<Referee>(refereeId);

                var match = db.GetWithChildren<Match>(matchId);

                match.Referees.Add(referee);
                referee.Matches.Add(match);

                db.UpdateWithChildren(match);
                db.UpdateWithChildren(referee);

            }
        }

        public void AddRefereesAndMatches(Dictionary<int, List<int>> dict)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var referees = db.GetAllWithChildren<Referee>();
                var matches = db.GetAllWithChildren<Match>();

                foreach (var d in dict)
                {
                    Match match = matches.SingleOrDefault(m => m.Id == d.Key);

                    foreach (var refereeId in d.Value)
                    {
                        Referee referee = referees.SingleOrDefault(r => r.Id == refereeId);

                        referee.Matches.Add(match);
                        match.Referees.Add(referee);

                        db.UpdateWithChildren(referee);
                        db.UpdateWithChildren(match);
                    }
                }
            }
        }

        #endregion

    }
}
