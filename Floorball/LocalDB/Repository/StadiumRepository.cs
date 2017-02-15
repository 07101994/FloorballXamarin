using Floorball.LocalDB.Tables;
using FloorballServer.Models.Floorball;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Repository
{
    public class StadiumRepository : Repository
    {

        #region GET

        public Stadium GetStadiumById(int stadiumId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetWithChildren<Stadium>(stadiumId);
            }
        }

        public IEnumerable<Stadium> GetAllStadium()
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Stadium>();
            }
        }

        #endregion

        #region POST

        public int AddStadium(int id, string name, string address)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Stadium s = new Stadium();
                s.Id = id;
                s.Name = name;
                s.Address = address;

                db.Insert(s);

                return s.Id;
            }
        }

        public void AddStadiums(List<StadiumModel> model)
        {
            foreach (var m in model)
            {
                try
                {
                    AddStadium(m.Id, m.Name, m.Address);
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion


    }
}
