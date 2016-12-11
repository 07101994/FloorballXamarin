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
    public class EventMessageRepository : Repository
    {

        #region GET

        public IEnumerable<EventMessage> GetAllEventMessage()
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<EventMessage>();
            }
        }

        public IEnumerable<EventMessage> GetEventMessagesByCategory(char catagoryStartNumber)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<EventMessage>().Where(e => e.Code.ToString()[0] == catagoryStartNumber);
            }
        }

        public EventMessage GetEventMessageById(int id)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetWithChildren<EventMessage>(id);
            }
        }

        #endregion

        #region POST

        public void AddEventMessage(int id, int code, string message)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                EventMessage e = new EventMessage();
                e.Id = id;
                e.Code = code;
                e.Message = message;

                db.Insert(e);
            }
        }

        public void AddEventMessages(List<EventMessageModel> model)
        {
            foreach (var m in model)
            {
                AddEventMessage(m.Id, m.Code, m.Message);
            }
        }

        public async void AddEventMessagesAsync(List<EventMessageModel> model)
        {
            await Task.Run(() => AddEventMessages(model));

        }

        #endregion

    }
}
