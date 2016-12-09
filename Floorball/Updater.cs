using Floorball.LocalDB;
using Floorball.REST;
using FloorballServer.Models.Floorball;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Floorball
{
    public class Updater
    {

        public List<UpdateData> UpdateDataList { get; set; }

        public UnitOfWork UoW { get; set; }

        public DateTime LastSyncDate { get; set; }

        private static Updater instance;

        public static Updater Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Updater();
                }

                return instance;
                
            }
        }

        private Updater()
        {
            UpdateDataList = new List<UpdateData>();
            UoW = new UnitOfWork();
        }

        /// <summary>
        /// Get updates from server
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<bool> UpdateDatabaseFromServer(DateTime date)
        {
            if (await GetUpdates(date))
            {
                UpdateDatabase();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get updates from server
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private async Task<bool> GetUpdates(DateTime date)
        {
            try
            {
                UpdateDataList = await RESTHelper.GetUpdatesAsync(date);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool UpdateDatabase()
        {

            foreach (var u in UpdateDataList)
            {
                switch (u.Type)
                {
                    case UpdateEnum.League:
                        UpdateLeague(u);
                        break;
                    case UpdateEnum.Team:
                        UpdateTeam(u);
                        break;
                    case UpdateEnum.Match:
                        UpdateMatch(u);
                        break;
                    case UpdateEnum.Player:
                        UpdatePlayer(u);
                        break;
                    case UpdateEnum.Stadium:
                        UpdateStadium(u);
                        break;
                    case UpdateEnum.Referee:
                        UpdateReferee(u);
                        break;
                    case UpdateEnum.Event:
                        UpdateEvent(u);
                        break;
                    case UpdateEnum.EventMessage:
                        UpdateEventMessage(u);
                        break;
                    case UpdateEnum.PlayerTeam:
                        UpdatePlayerToTeam(u);
                        break;
                    case UpdateEnum.PlayerMatch:
                        UpdatePlayerToMatch(u);
                        break;
                    case UpdateEnum.RefereeMatch:
                        UpdateRefereeToMatch(u);
                        break;
                    default:
                        break;
                }
            }

            LastSyncDate = DateTime.Now;

            return false;
        }

        private void UpdateRefereeToMatch(UpdateData u)
        {
            if (u.IsAdding)
            {
                JObject c = u.Entity as JObject;
                UoW.RefereeRepo.AddRefereeToMatch(c.Value<int>("refereeId"), c.Value<int>("matchId"));
            }
            else
            {

            }
        }

        private void UpdatePlayerToMatch(UpdateData u)
        {
            if (u.IsAdding)
            {
                JObject c = u.Entity as JObject;
                UoW.PlayerRepo.AddPlayerToMatch(c.Value<int>("playerId"), c.Value<int>("matchId"));
            }
            else
            {

            }
        }

        private void UpdatePlayerToTeam(UpdateData u)
        {
            if (u.IsAdding)
            {
                JObject c = u.Entity as JObject;
                UoW.PlayerRepo.AddPlayerToTeam(c.Value<int>("playerId"), c.Value<int>("teamId"));
            }
            else
            {

            }
        }

        private void UpdateEventMessage(UpdateData u)
        {
            if (u.IsAdding)
            {
                JObject c = u.Entity as JObject;
                UoW.EventMessageRepo.AddEventMessage(c.Value<int>("Id"), c.Value<int>("Code"), c.Value<string>("Message"));
            }
            else
            {

            }
        }

        private void UpdateEvent(UpdateData u)
        {
            if (u.IsAdding)
            {
                JObject c = u.Entity as JObject;
                UoW.EventRepo.AddEvent(c.Value<int>("Id"), c.Value<int>("MatchId"), c.Value<string>("Type"), TimeSpan.ParseExact(c.Value<string>("Time"), "g", CultureInfo.InvariantCulture), c.Value<int>("PlayerId"), c.Value<int>("EventMessageId"), c.Value<int>("TeamId"));
            }
            else
            {

            }
        }

        private void UpdateReferee(UpdateData u)
        {
            if (u.IsAdding)
            {
                JObject c = u.Entity as JObject;
                UoW.RefereeRepo.AddReferee(c.Value<int>("Id"), c.Value<string>("Name"), c.Value<CountriesEnum>("Country"));
            }
            else
            {

            }
        }

        private void UpdateStadium(UpdateData u)
        {
            if (u.IsAdding)
            {
                JObject c = u.Entity as JObject;
                UoW.StadiumRepo.AddStadium(c.Value<int>("Id"), c.Value<string>("Name"), c.Value<string>("Address"));
            }
            else
            {

            }
        }

        private void UpdatePlayer(UpdateData u)
        {
            if (u.IsAdding)
            {
                JObject c = u.Entity as JObject;
                UoW.PlayerRepo.AddPlayer(c.Value<string>("FirstName"), c.Value<string>("SecondName"), c.Value<int>("RegNum"), c.Value<int>("Number"), DateTime.ParseExact(c.Value<string>("BirthDate"), "yyyy-MM-dd", CultureInfo.InvariantCulture));
            }
            else
            {

            }
        }

        private void UpdateMatch(UpdateData u)
        {
            if (u.IsAdding)
            {
                JObject c = u.Entity as JObject;
                UoW.MatchRepo.AddMatch(c.Value<int>("Id"), c.Value<int>("HomeTeamId"), c.Value<int>("AwayTeamId"), c.Value<short>("GoalsH"), c.Value<short>("GoalsA"), c.Value<short>("Round"), c.Value<StateEnum>("State"), TimeSpan.ParseExact(c.Value<string>("Time"), "g", CultureInfo.InvariantCulture), DateTime.ParseExact(c.Value<string>("Date"), "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture), c.Value<int>("LeagueId"), c.Value<int>("StadiumId"));
            }
            else
            {

            }
        }

        private void UpdateTeam(UpdateData u)
        {
            if (u.IsAdding)
            {
                JObject c = u.Entity as JObject;
                UoW.TeamRepo.AddTeam(c.Value<int>("Id"), c.Value<string>("Name"), DateTime.ParseExact(c.Value<string>("Year"), "yyyy", CultureInfo.InvariantCulture), c.Value<string>("Coach"), c.Value<string>("Sex"), c.Value<CountriesEnum>("Country"), c.Value<int>("StadiumId"), c.Value<int>("LeagueId"));
            }
            else
            {

            }
        }

        private void UpdateLeague(UpdateData u)
        {
            if (u.IsAdding)
            {
                JObject c = u.Entity as JObject;
                UoW.LeagueRepo.AddLeague(c.Value<int>("Id"), c.Value<string>("Name"), DateTime.ParseExact(c.Value<string>("Year"), "yyyy", CultureInfo.InvariantCulture), c.Value<string>("Type"), c.Value<string>("ClassName"), c.Value<int>("Rounds"), c.Value<CountriesEnum>("Country"), c.Value<string>("Sex"));
            }
            else
            {
                
            }
        }
    }
}
