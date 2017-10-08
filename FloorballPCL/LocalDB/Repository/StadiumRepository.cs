using Floorball.LocalDB.Tables;
using FloorballServer.Models.Floorball;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net.Interop;

namespace Floorball.LocalDB.Repository
{
    public class StadiumRepository : Repository
    {
        public StadiumRepository(ISQLitePlatform platform, string databasePath) : base(platform, databasePath)
        {
        }

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

        public int AddStadium(int id, string name, string address, string city, string country, string postCode)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Stadium s = new Stadium
                {
                    Id = id,
                    Name = name,
                    Address = address,
                    City = city,
                    Country = country,
                    PostCode = postCode                    
                };

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
                    AddStadium(m.Id, m.Name, m.Address, m.City, m.Country, m.PostCode);
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion

        #region PUT

        public int UpdateStadium(Stadium stadium)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Stadium s = db.Find<Stadium>(stadium.Id);

                s.Name = stadium.Name;
                s.Address = stadium.Address;
                s.Country = stadium.Country;
                s.City = stadium.City;
                s.PostCode = stadium.PostCode;

                db.Update(s);

                return s.Id;
            }
        }

        #endregion

    }
}
