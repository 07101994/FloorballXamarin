using Floorball.LocalDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball
{
    public class Updater
    {
        public bool UpdateRequired { get; set; }

        public List<string> UpdateDataList { get; set; }


        private static Updater instance;

        public Updater Instance
        {
            get
            {
                if (instance == null)
                    return new Updater();

                return instance;
                
            }
        }

        public bool UpdateDatabaseFromServer()
        {

            //foreach (var updateData in UpdateDataList)
            //{
            //    dynamic deserializedObject = JsonConvert.DeserializeObject(updateData);

            //    foreach (var type in deserializedObject.updateList)
            //    {

            //    }
            //}

            foreach (var updateData in UpdateDataList)
            {
                var deserializedObject = JsonConvert.DeserializeObject<Dictionary<string,dynamic>>(updateData);

                foreach (var key in deserializedObject.Keys)
                {
                    switch (key)
                    {
                        case "addPlayer":

                            Manager.AddPlayer(deserializedObject[key]["name"], Convert.ToInt32(deserializedObject[key]["regNum"]), Convert.ToInt32(deserializedObject[key]["number"]),DateTime.Now);

                            break;

                        case "addTeam":
                            break;

                        case "addMatch":
                            break;

                        case "addLeague":
                            break;

                        case "addEvent":
                            break;

                        case "addReferee":
                            break;

                        case "addEventMessage":
                            break;

                        case "addStadium":
                            break;

                        case "addStatistic":
                            break;

                        default:
                            break;
                    }
                }
            }





            UpdateRequired = false;

            return false;
        }


    }
}
