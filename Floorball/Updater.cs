using Floorball.LocalDB;
using Floorball.REST;
using FloorballServer.Models.Floorball;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Floorball
{
    public class Updater
    {

        public List<UpdateData> UpdateDataList { get; set; }

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
        }

        /// <summary>
        /// Get updates from server
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool UpdateDatabaseFromServer(DateTime date)
        {
            if (GetUpdates(date))
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
        private bool GetUpdates(DateTime date)
        {
            try
            {
                UpdateDataList = RESTHelper.GetUpdates(date);
            }
            catch (Exception)
            {

                return false;
            }

            return false;
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

                //var deserializedObject = JsonConvert.DeserializeObject<Dictionary<string,dynamic>>(updateData);

                //foreach (var key in deserializedObject.Keys)
                //{
                //    switch (key)
                //    {
                //        case "addPlayer":

                //            Manager.AddPlayer(deserializedObject[key]["firstName"], deserializedObject[key]["secondName"], Convert.ToInt32(deserializedObject[key]["regNum"]), Convert.ToInt32(deserializedObject[key]["number"]), DateTime.Parse(deserializedObject[key]["date"].ToString()));

                //            break;

                //        case "addTeam":

                //            Manager.AddTeam(deserializedObject[key]["id"], deserializedObject[key]["name"], DateTime.ParseExact(deserializedObject[key]["year"], "yyyy", CultureInfo.InvariantCulture), deserializedObject[key]["coach"], deserializedObject[key]["sex"], deserializedObject[key]["country"], deserializedObject[key]["stadiumId"], deserializedObject[key]["leagueId"]);

                //            break;

                //        case "addMatch":


                //            break;

                //        case "addLeague":

                //            Manager.AddLeague(deserializedObject[key]["id"], deserializedObject[key]["name"], DateTime.ParseExact(deserializedObject[key]["year"], "yyyy", CultureInfo.InvariantCulture), deserializedObject[key]["type"], deserializedObject[key]["classname"], deserializedObject[key]["rounds"], deserializedObject[key]["country"]);

                //            break;

                //        case "addEvent":

                //            Manager.AddEvent(deserializedObject[key]["id"], deserializedObject[key]["matchId"], deserializedObject[key]["type"], TimeSpan.ParseExact(deserializedObject[key]["time"], "h\\h\\:m\\m\\:s\\s", CultureInfo.InvariantCulture), deserializedObject[key]["playerId"], deserializedObject[key]["eventMessageId"], deserializedObject[key]["teamId"]);

                //            break;

                //        case "addReferee":

                //            Manager.AddReferee(deserializedObject[key]["id"], deserializedObject[key]["name"], deserializedObject[key]["country"]);

                //            break;

                //        case "addEventMessage":

                //            break;

                //        case "addStadium":

                //            Manager.AddStadium(deserializedObject[key]["id"], deserializedObject[key]["name"], deserializedObject[key]["address"]);

                //            break;

                //        case "playerToTeam":


                //            break;

                //        case "playerToMatch":


                //            break;

                //        case "refereeToMatch":

                //            break;

                //        case "removePlayerFromTeam":


                //            break;

                //        case "removePlayerFromMatch":


                //            break;

                //        case "removeRefereeFromMatch":

                //            break;

                //        default:
                //            break;
                //    }
                //}
            }

            LastSyncDate = DateTime.Now;

            return false;
        }

        private void UpdateRefereeToMatch(UpdateData u)
        {
            if (u.IsAdding)
            {
                Dictionary<string, int> dict = u.Entity as Dictionary<string, int>;
                Manager.AddRefereeToMatch(dict["refereeId"], dict["matchId"]);
            }
            else
            {

            }
        }

        private void UpdatePlayerToMatch(UpdateData u)
        {
            if (u.IsAdding)
            {
                Dictionary<string, int> dict = u.Entity as Dictionary<string, int>;
                Manager.AddPlayerToMatch(dict["playerId"], dict["matchId"]);
            }
            else
            {

            }
        }

        private void UpdatePlayerToTeam(UpdateData u)
        {
            if (u.IsAdding)
            {
                Dictionary<string, int> dict = u.Entity as Dictionary<string, int>;
                Manager.AddPlayerToTeam(dict["playerId"], dict["teamId"]);
            }
            else
            {

            }
        }

        private void UpdateEventMessage(UpdateData u)
        {
            if (u.IsAdding)
            {
                EventMessageModel message = u.Entity as EventMessageModel;
                Manager.AddEventMessage(message.Id, message.Code, message.Message);
            }
            else
            {

            }
        }

        private void UpdateEvent(UpdateData u)
        {
            if (u.IsAdding)
            {
                EventModel eventm = u.Entity as EventModel;
                Manager.AddEvent(eventm.Id, eventm.MatchId, eventm.Type, eventm.Time, eventm.PlayerId, eventm.EventMessageId, eventm.TeamId);
            }
            else
            {

            }
        }

        private void UpdateReferee(UpdateData u)
        {
            if (u.IsAdding)
            {
                RefereeModel referee = u.Entity as RefereeModel;
                Manager.AddReferee(referee.Id, referee.Name, referee.Country);
            }
            else
            {

            }
        }

        private void UpdateStadium(UpdateData u)
        {
            if (u.IsAdding)
            {
                StadiumModel stadium = u.Entity as StadiumModel;
                Manager.AddStadium(stadium.Id, stadium.Name, stadium.Address);
            }
            else
            {

            }
        }

        private void UpdatePlayer(UpdateData u)
        {
            if (u.IsAdding)
            {
                PlayerModel player = u.Entity as PlayerModel;
                Manager.AddPlayer(player.FirstName, player.SecondName, player.RegNum, player.Number, player.BirthDate);
            }
            else
            {

            }
        }

        private void UpdateMatch(UpdateData u)
        {
            if (u.IsAdding)
            {
                MatchModel match = u.Entity as MatchModel;
                Manager.AddMatch(match.Id, match.HomeTeamId, match.AwayTeamId, match.GoalsH, match.GoalsA, match.Round, match.State, match.Time, match.Date, match.LeagueId, match.StadiumId);
            }
            else
            {

            }
        }

        private void UpdateTeam(UpdateData u)
        {
            if (u.IsAdding)
            {
                TeamModel team = u.Entity as TeamModel;
                Manager.AddTeam(team.Id, team.Name, team.Year, team.Coach, team.Sex, team.Country, team.StadiumId, team.LeagueId);
            }
            else
            {

            }
        }

        private void UpdateLeague(UpdateData u)
        {
            if (u.IsAdding)
            {
                LeagueModel league = u.Entity as LeagueModel;
                Manager.AddLeague(league.Id, league.Name, league.Year, league.type, league.ClassName, league.Rounds, league.Country);
            }
            else
            {
                
            }
        }
    }
}
