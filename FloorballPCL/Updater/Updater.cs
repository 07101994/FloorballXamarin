﻿using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using Floorball.REST;
using FloorballServer.Models.Floorball;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Floorball.REST.RESTHelpers;
using SQLite.Net.Interop;

namespace Floorball.Updater
{
    public class Updater
    {

        public List<UpdateData> UpdateDataList { get; set; }

        public UnitOfWork UoW { get; set; }

        public DateTime LastSyncDate { get; set; }

        private static Updater instance;

        public DateTime SyncDate { get; set; }

        public delegate void UpdateEventhandler();
        public event UpdateEventhandler UpdateStarted;
        public event UpdateEventhandler UpdateEnded;

        public bool IsSyncing { get; set; }

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
        public async Task UpdateDatabaseFromServer(DateTime date)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
					IsSyncing = true;

					RaiseEvent(UpdateStarted);

					await Task.Delay(10000);

					await GetUpdates(date);

					UpdateDatabase();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                RaiseEvent(UpdateEnded);
                IsSyncing = false;
            }
            
        }

        private void RaiseEvent(UpdateEventhandler e)
        {
            if (e != null)
            {
                e();
            }
        }

        /// <summary>
        /// Get updates from server
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private async Task GetUpdates(DateTime date)
        {
            var updateModel = await RESTHelper.GetUpdatesAsync(date);
            SyncDate = updateModel.UpdateTime;
            UpdateDataList = updateModel.Updates;
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
                        UpdatePlayerTeam(u);
                        break;
                    case UpdateEnum.PlayerMatch:
                        UpdatePlayerMatch(u);
                        break;
                    case UpdateEnum.RefereeMatch:
                        UpdateRefereeMatch(u);
                        break;
                    default:
                        break;
                }
            }

            LastSyncDate = SyncDate;

            return false;
        }

        private void UpdateRefereeMatch(UpdateData u)
        {
            try
            {
                JObject c = u.Entity as JObject;

                switch (u.UpdateType)
                {
                    case UpdateType.Create:

                        UoW.RefereeRepo.AddRefereeToMatch(c.Value<int>("refereeId"), c.Value<int>("matchId"));

                        break;
                    case UpdateType.Delete:

                        UoW.RefereeRepo.RemoveRefereeFromMatch(c.Value<int>("refereeId"), c.Value<int>("matchId"));

                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdatePlayerMatch(UpdateData u)
        {
            try
            {
                JObject c = u.Entity as JObject;
                switch (u.UpdateType)
                {
                    case UpdateType.Create:

                        UoW.PlayerRepo.AddPlayerToMatch(c.Value<int>("playerId"), c.Value<int>("matchId"));

                        break;
                    case UpdateType.Delete:

                        UoW.PlayerRepo.RemovePlayerFromMatch(c.Value<int>("playerId"), c.Value<int>("matchId"));

                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdatePlayerTeam(UpdateData u)
        {
            try
            {
                JObject c = u.Entity as JObject;
                switch (u.UpdateType)
                {
                    case UpdateType.Create:

                        UoW.PlayerRepo.AddPlayerToTeam(c.Value<int>("playerId"), c.Value<int>("teamId"));

                        break;
                    case UpdateType.Delete:

                        UoW.PlayerRepo.RemovePlayerFromTeam(c.Value<int>("playerId"), c.Value<int>("teamId"));

                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateEventMessage(UpdateData u)
        {
            try
            {
                JObject c = u.Entity as JObject;
                switch (u.UpdateType)
                {
                    case UpdateType.Create:

                        UoW.EventMessageRepo.AddEventMessage(c.Value<int>("Id"), c.Value<int>("Code"), c.Value<string>("Message"));

                        break;
                    case UpdateType.Update:

                        UoW.EventMessageRepo.UpdateEventMessage(new EventMessage {
                            Id = c.Value<int>("Id"),
                            Code = c.Value<int>("Code"),
                            Message = c.Value<string>("Message")
                        });

                        break;
                    case UpdateType.Delete:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateEvent(UpdateData u)
        {
            try
            {
                JObject c = u.Entity as JObject;

                switch (u.UpdateType)
                {
                    case UpdateType.Create:

                        UoW.EventRepo.AddEvent(c.Value<int>("Id"), c.Value<int>("MatchId"), c.Value<EventType>("Type"), TimeSpan.ParseExact(c.Value<string>("Time"), "g", CultureInfo.InvariantCulture), c.Value<int>("PlayerId"), c.Value<int>("EventMessageId"), c.Value<int>("TeamId"));

                        break;
                    case UpdateType.Update:

                        UoW.EventRepo.UpdateEvent(new Event {
                            Id = c.Value<int>("Id"),
                            MatchId = c.Value<int>("MatchId"),
                            Type = c.Value<EventType>("Type"),
                            Time = TimeSpan.ParseExact(c.Value<string>("Time"), "g", CultureInfo.InvariantCulture),
                            PlayerId = c.Value<int>("PlayerId"),
                            EventMessageId = c.Value<int>("EventMessageId"),
                            TeamId = c.Value<int>("TeamId")
                        });

                        break;
                    case UpdateType.Delete:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateReferee(UpdateData u)
        {

            try
            {
                JObject c = u.Entity as JObject;

                switch (u.UpdateType)
                {
                    case UpdateType.Create:

                        UoW.RefereeRepo.AddReferee(c.Value<int>("Id"), c.Value<string>("Name"), c.Value<CountriesEnum>("Country"));

                        break;
                    case UpdateType.Update:

                        UoW.RefereeRepo.UpdateRefereee(new Referee {
                            Id = c.Value<int>("Id"),
                            Name = c.Value<string>("Name"),
                            Country = c.Value<CountriesEnum>("Country")
                        });

                        break;
                    case UpdateType.Delete:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateStadium(UpdateData u)
        {
            try
            {
                JObject c = u.Entity as JObject;

                switch (u.UpdateType)
                {
                    case UpdateType.Create:

                        UoW.StadiumRepo.AddStadium(c.Value<int>("Id"), c.Value<string>("Name"), c.Value<string>("Address"),c.Value<string>("Country"),c.Value<string>("City"),c.Value<string>("PostCode"));

                        break;
                    case UpdateType.Update:

                        UoW.StadiumRepo.UpdateStadium(new Stadium {
                            Id = c.Value<int>("Id"),
                            Name = c.Value<string>("Name"),
                            Address = c.Value<string>("Address"),
                            City = c.Value<string>("City"),
                            Country = c.Value<string>("Country"),
                            PostCode = c.Value<string>("PostCode")
                        });

                        break;
                    case UpdateType.Delete:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdatePlayer(UpdateData u)
        {
            try
            {
                JObject c = u.Entity as JObject;

                switch (u.UpdateType)
                {
                    case UpdateType.Create:

                        UoW.PlayerRepo.AddPlayer(c.Value<string>("FirstName"), c.Value<string>("SecondName"), c.Value<int>("RegNum"), c.Value<short>("Number"), DateTime.ParseExact(c.Value<string>("BirthDate"), "yyyy-MM-dd", CultureInfo.InvariantCulture));

                        break;
                    case UpdateType.Update:

                        UoW.PlayerRepo.UpdatePlayer(new Player {
                            FirstName = c.Value<string>("FirstName"),
                            LastName = c.Value<string>("SecondName"),
                            Id = c.Value<int>("RegNum"),
                            Number = c.Value<short>("Number"),
                            BirthDate = DateTime.ParseExact(c.Value<string>("BirthDate"), "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        });

                        break;
                    case UpdateType.Delete:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateMatch(UpdateData u)
        {
            try
            {
                JObject c = u.Entity as JObject;

                switch (u.UpdateType)
                {
                    case UpdateType.Create:

                        UoW.MatchRepo.AddMatch(c.Value<int>("Id"), c.Value<int>("HomeTeamId"), c.Value<int>("AwayTeamId"), c.Value<short>("GoalsH"), c.Value<short>("GoalsA"), c.Value<short>("Round"), c.Value<StateEnum>("State"), TimeSpan.ParseExact(c.Value<string>("Time"), "g", CultureInfo.InvariantCulture), DateTime.ParseExact(c.Value<string>("Date"), "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture), c.Value<int>("LeagueId"), c.Value<int>("StadiumId"));

                        break;
                    case UpdateType.Update:

                        UoW.MatchRepo.UpdateMatch(new Match {
                            Id = c.Value<int>("Id"),
                            HomeTeamId = c.Value<int>("HomeTeamId"),
                            AwayTeamId = c.Value<int>("AwayTeamId"),
                            ScoreH = c.Value<short>("GoalsH"),
                            ScoreA = c.Value<short>("GoalsA"),
                            Round= c.Value<short>("Round"),
                            State = c.Value<StateEnum>("State"),
                            Time = TimeSpan.ParseExact(c.Value<string>("Time"), "g", CultureInfo.InvariantCulture),
                            Date = DateTime.ParseExact(c.Value<string>("Date"), "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture),
                            LeagueId = c.Value<int>("LeagueId"),
                            StadiumId = c.Value<int>("StadiumId")
                        });

                        break;
                    case UpdateType.Delete:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateTeam(UpdateData u)
        {
            try
            {
                JObject c = u.Entity as JObject;

                switch (u.UpdateType)
                {
                    case UpdateType.Create:

                        UnitOfWork.ImageManager.SaveImage(c.Value<byte[]>("Image"), c.Value<string>("ImageName"));
                        UoW.TeamRepo.AddTeam(c.Value<int>("Id"), c.Value<string>("Name"), DateTime.ParseExact(c.Value<string>("Year"), "yyyy", CultureInfo.InvariantCulture), c.Value<string>("Coach"), c.Value<GenderEnum>("Gender"), c.Value<CountriesEnum>("Country"), c.Value<int>("StadiumId"), c.Value<int>("LeagueId"), c.Value<string>("ImageName"));

                        break;
                    case UpdateType.Update:

                        UnitOfWork.ImageManager.SaveImage(c.Value<byte[]>("Image"), c.Value<string>("ImageName"));
                        UoW.TeamRepo.UpdateTeam(new Team {
                            Id = c.Value<int>("Id"),
                            Name = c.Value<string>("Name"),
                            Year = DateTime.ParseExact(c.Value<string>("Year"), "yyyy", CultureInfo.InvariantCulture),
                            Coach = c.Value<string>("Coach"),
                            Gender = c.Value<GenderEnum>("Gender"),
                            Country = c.Value<CountriesEnum>("Country"),
                            StadiumId = c.Value<int>("StadiumId"),
                            LeagueId = c.Value<int>("LeagueId"),
                            ImageName = c.Value<string>("ImageName")
                        });

                        break;
                    case UpdateType.Delete:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateLeague(UpdateData u)
        {
            try
            {
                JObject c = u.Entity as JObject;

                switch (u.UpdateType)
                {
                    case UpdateType.Create:

                        UoW.LeagueRepo.AddLeague(c.Value<int>("Id"), c.Value<string>("Name"), DateTime.ParseExact(c.Value<string>("Year"), "yyyy", CultureInfo.InvariantCulture), c.Value<LeagueTypeEnum>("Type"), c.Value<ClassEnum>("Class"), c.Value<short>("Rounds"), c.Value<CountriesEnum>("Country"), c.Value<GenderEnum>("Gender"));

                        break;
                    case UpdateType.Update:

                        UoW.LeagueRepo.UpdateLeague(new League {
                            Id = c.Value<int>("Id"),
                            Name = c.Value<string>("Name"),
                            Year = DateTime.ParseExact(c.Value<string>("Year"), "yyyy", CultureInfo.InvariantCulture),
                            Type = c.Value<LeagueTypeEnum>("Type"),
                            Class = c.Value<ClassEnum>("Class"),
                            Rounds = c.Value<short>("Rounds"),
                            Country = c.Value<CountriesEnum>("Country"),
                            Gender = c.Value<GenderEnum>("Gender")
                        }); 

                        break;
                    case UpdateType.Delete:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
