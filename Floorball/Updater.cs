using Floorball.LocalDB;
using Floorball.REST;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Floorball
{
    public class Updater
    {

        public List<string> UpdateDataList { get; set; }

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
            UpdateDataList = new List<string>();
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

            foreach (var updateData in UpdateDataList)
            {
                var deserializedObject = JsonConvert.DeserializeObject<Dictionary<string,dynamic>>(updateData);

                foreach (var key in deserializedObject.Keys)
                {
                    switch (key)
                    {
                        case "addPlayer":

                            Manager.AddPlayer(deserializedObject[key]["firstName"], deserializedObject[key]["secondName"], Convert.ToInt32(deserializedObject[key]["regNum"]), Convert.ToInt32(deserializedObject[key]["number"]), DateTime.Parse(deserializedObject[key]["date"].ToString()));

                            break;

                        case "addTeam":

                            Manager.AddTeam(deserializedObject[key]["id"], deserializedObject[key]["name"], DateTime.ParseExact(deserializedObject[key]["year"], "yyyy", CultureInfo.InvariantCulture), deserializedObject[key]["coach"], deserializedObject[key]["sex"], deserializedObject[key]["country"], deserializedObject[key]["stadiumId"], deserializedObject[key]["leagueId"]);

                            break;

                        case "addMatch":


                            break;

                        case "addLeague":

                            Manager.AddLeague(deserializedObject[key]["id"], deserializedObject[key]["name"], DateTime.ParseExact(deserializedObject[key]["year"], "yyyy", CultureInfo.InvariantCulture), deserializedObject[key]["type"], deserializedObject[key]["classname"], deserializedObject[key]["rounds"], deserializedObject[key]["country"]);

                            break;

                        case "addEvent":

                            Manager.AddEvent(deserializedObject[key]["id"], deserializedObject[key]["matchId"], deserializedObject[key]["type"], TimeSpan.ParseExact(deserializedObject[key]["time"], "h\\h\\:m\\m\\:s\\s", CultureInfo.InvariantCulture), deserializedObject[key]["playerId"], deserializedObject[key]["eventMessageId"], deserializedObject[key]["teamId"]);

                            break;

                        case "addReferee":

                            Manager.AddReferee(deserializedObject[key]["id"], deserializedObject[key]["name"], deserializedObject[key]["country"]);

                            break;

                        case "addEventMessage":

                            break;

                        case "addStadium":

                            Manager.AddStadium(deserializedObject[key]["id"], deserializedObject[key]["name"], deserializedObject[key]["address"]);

                            break;

                        case "playerToTeam":


                            break;

                        case "playerToMatch":


                            break;

                        case "refereeToMatch":

                            break;

                        case "removePlayerFromTeam":


                            break;

                        case "removePlayerFromMatch":


                            break;

                        case "removeRefereeFromMatch":

                            break;

                        //case "addStat":


                        //    break;

                        //case "removeStat":


                        //    break;

                        default:
                            break;
                    }
                }
            }

            LastSyncDate = DateTime.Now;

            return false;
        }


    }
}
