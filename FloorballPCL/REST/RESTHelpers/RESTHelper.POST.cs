using Floorball.Exceptions;
using Floorball.Model;
using Floorball.Updater;
using FloorballServer.Models.Floorball;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Floorball.REST.RequestModels;
using Floorball.REST.RESTHelpers;
using FloorballPCL.Model;

namespace Floorball.REST.RESTHelpers
{
    public partial class RESTHelper : RESTHelperBase
    {

        public static int AddLeague(string name, DateTime year, LeagueTypeEnum type, ClassEnum classname, short rounds)
        {
            return Network.Post<LeagueModel,int>(new HTTPPostRequestModel<LeagueModel>
            {
                Url = "/api/floorball/leagues",
                ErrorMsg = "Error during adding league!",
				Body = new LeagueModel
				{
					Name = name,
					Year = year,
					Type = type,
					Class = classname,
					Rounds = rounds
				}
            });
        }

        public static int AddPlayer(string firstName, string lastName, int id, int number, DateTime year)
        {
			return Network.Post<PlayerModel, int>(new HTTPPostRequestModel<PlayerModel>
			{
				Url = "/api/floorball/players",
				ErrorMsg = "Error during adding player!",
				Body = new PlayerModel
				{
                    FirstName = firstName,
                    LastName = lastName,
                    Id = id,
                    Number = (short)number,
                    BirthDate = year
				}
			});
        }

        public static int AddMatch(int leagueId, int round, int homeTeamId, int awayTeamId, DateTime date, int stadiumId)
        {
			return Network.Post<MatchModel, int>(new HTTPPostRequestModel<MatchModel>
			{
				Url = "/api/floorball/matches",
				ErrorMsg = "Error during adding match!",
				Body = new MatchModel
				{
					LeagueId = leagueId,
                    Round = (short)round,
                    HomeTeamId = homeTeamId,
                    AwayTeamId = awayTeamId,
                    Date = date,
                    StadiumId = stadiumId
				}
			});
        }

        public static int AddReferee(string name)
        {
			return Network.Post<RefereeModel, int>(new HTTPPostRequestModel<RefereeModel>
			{
				Url = "/api/floorball/referees",
				ErrorMsg = "Error during adding referee!",
				Body = new RefereeModel
				{
                    Name = name
				}
			});

        }

        public static int AddStadium(string name, string address, string city, string country, string postCode)
        {
			return Network.Post<StadiumModel, int>(new HTTPPostRequestModel<StadiumModel>
			{
				Url = "/api/floorball/stadiums",
				ErrorMsg = "Error during adding stadium!",
				Body = new StadiumModel
				{
                    Name = name,
                    Address = address,
                    City = city,
                    Country = country,
                    PostCode = postCode
				}
			});
        }

        public static int AddTeam(string name, DateTime year, string coach, GenderEnum gender, CountriesEnum country, int stadiumId, int leagueId)
        {
            return Network.Post<TeamModel, int>(new HTTPPostRequestModel<TeamModel>
            {
                Url = "/api/floorball/teams",
                ErrorMsg = "Error during adding team!",
                Body = new TeamModel
                {
                    Name = name,
                    Year = year,
                    Coach = coach,
                    Gender = gender,
                    Country = country,
                    StadiumId = stadiumId,
                    LeagueId = leagueId
                }
            });
        }

        public static int AddEvent(int matchId, EventType type, TimeSpan time, int playerId, int eventMessageId, int teamId)
        {
			return Network.Post<EventModel, int>(new HTTPPostRequestModel<EventModel>
			{
				Url = "/api/floorball/events",
				ErrorMsg = "Error during adding event!",
				Body = new EventModel
				{
                    MatchId = matchId,
                    TeamId = teamId,
                    Type = type,
                    Time = time,
                    EventMessageId = eventMessageId,
                    PlayerId = playerId
				}
			});

        }

        public static async Task<UserModel> LoginAsync(string userName, string password) 
        {
            return await Network.PostAsync<UserModel, UserModel>(new HTTPPostRequestModel<UserModel>
            {
				Url = "/api/floorball/login",
				ErrorMsg = "Error during login!",
				Body = new UserModel
				{
                    UserName = userName,
                    Password = password
				}
            });
        }

    }
}
