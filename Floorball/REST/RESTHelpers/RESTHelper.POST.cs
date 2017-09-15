using Floorball.Exceptions;
using Floorball.Model;
using Floorball.Updater;
using FloorballServer.Models.Floorball;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Floorball.REST.RequestModels;
using Floorball.REST.RESTHelpers;

namespace Floorball.REST.RESTHelpers
{
    public partial class RESTHelper : RESTHelperBase
    {
        private static FloorballSerializer deserial = new FloorballSerializer(new JsonSerializer());
        private static string ServerURL = "https://floorball.azurewebsites.net";
        //private static string ServerURL = "http://192.168.0.20:8080";
        //private static string ServerURL = "http://192.168.173.1:8088";
        //private static string ServerURL = "http://192.168.173.1:8088";

        private static void CheckError(RestResponse response, string message)
        {
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new FloorballException(message, null);
            }
            else
            {
                if (response.ErrorException != null)
                {
                    throw new CommunicationException(response.ErrorMessage, response.ErrorException);
                }
            }
        }

		//public static void AddPlayerToTeam(int playerId, int teamId)
		//{
		//    try
		//    {
		//        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
		//        Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "teamId", teamId.ToString() } };
		//        RestResponse response = client.ExecuteRequest("/api/floorball/teams/{teamId}/players/{playerId}}", Method.PUT, urlParams) as RestResponse;

		//    }
		//    catch (Exception)
		//    {

		//        throw;
		//    }

		//}

		//public static void AddPlayerToMatch(int playerId, int matchId)
		//{
		//    try
		//    {
		//        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
		//        Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "matchId", matchId.ToString() } };
		//        RestResponse response = client.ExecuteRequest("/api/floorball/matches/{matchId}/players/{playerId}}", Method.PUT, urlParams) as RestResponse;

		//    }
		//    catch (Exception)
		//    {

		//        throw;
		//    }

		//}

		//public static void RemovePlayerFromMatch(int playerId, int matchId)
		//{
		//    try
		//    {
		//        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
		//        Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "matchId", matchId.ToString() } };
		//        RestResponse response = client.ExecuteRequest("/api/floorball/matches/{matchId}/players/{playerId}}", Method.DELETE, urlParams) as RestResponse;

		//    }
		//    catch (Exception)
		//    {

		//        throw;
		//    }

		//}

		//public static void RemovePlayerFromTeam(int playerId, int teamId)
		//{
		//    try
		//    {
		//        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
		//        Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "teamId", teamId.ToString() } };
		//        RestResponse response = client.ExecuteRequest("/api/floorball/teams/{teamId}/players/{playerId}}", Method.DELETE, urlParams) as RestResponse;

		//    }
		//    catch (Exception)
		//    {

		//        throw;
		//    }

		//}

        //public static int AddLeague(string name, DateTime year, string type, string class1, int rounds)
        //{
        //    try
        //    {
        //        LeagueModel model = new LeagueModel();
        //        model.ClassName = class1;
        //        model.Name = name;
        //        model.Rounds = rounds;
        //        model.type = type;
        //        model.Year = year;

        //        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
        //        RestResponse response = client.ExecuteRequest("/api/floorball/leagues", Method.POST, null, null, model) as RestResponse;

        //        return deserial.Deserialize<int>(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //public static int AddPlayer(string firstName, string secondName, int regNum, int number, DateTime year)
        //{
        //    try
        //    {
        //        PlayerModel model = new PlayerModel();
        //        model.FirstName = firstName;
        //        model.SecondName = secondName;
        //        model.Number = (short)number;
        //        model.RegNum = regNum;
        //        model.BirthDate = year;

        //        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
        //        RestResponse response = client.ExecuteRequest("/api/floorball/players", Method.POST, null, null, model) as RestResponse;

        //        return deserial.Deserialize<int>(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //public static int AddMatch(int leagueId, int round, int homeTeamId, int awayTeamId, DateTime date, int stadiumId)
        //{
        //    try
        //    {
        //        MatchModel model = new MatchModel();
        //        model.LeagueId = leagueId;
        //        model.Round = (short)round;
        //        model.HomeTeamId = homeTeamId;
        //        model.AwayTeamId = awayTeamId;
        //        model.Date = date;
        //        model.StadiumId = stadiumId;

        //        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
        //        RestResponse response = client.ExecuteRequest("/api/floorball/matches", Method.POST, null, null, model) as RestResponse;

        //        return deserial.Deserialize<int>(response);

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public static int AddReferee(string name)
        //{
        //    try
        //    {
        //        RefereeModel model = new RefereeModel();
        //        model.Name = name;

        //        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
        //        RestResponse response = client.ExecuteRequest("/api/floorball/referees", Method.POST, null, null, model) as RestResponse;

        //        return deserial.Deserialize<int>(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //public static int AddStadium(string name, string address)
        //{
        //    try
        //    {
        //        StadiumModel model = new StadiumModel();
        //        model.Name = name;
        //        model.Address = address;

        //        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
        //        RestResponse response = client.ExecuteRequest("/api/floorball/stadiums", Method.POST, null, null, model) as RestResponse;

        //        return deserial.Deserialize<int>(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //public static int AddTeam(string name, DateTime year, string coach, string sex, CountriesEnum country, int stadiumId, int leagueId)
        //{
        //    try
        //    {
        //        TeamModel model = new TeamModel();
        //        model.Name = name;
        //        model.Year = year;
        //        model.Coach = coach;
        //        model.Sex = sex;
        //        model.Country = country;
        //        model.StadiumId = stadiumId;
        //        model.LeagueId = leagueId;

        //        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
        //        RestResponse response = client.ExecuteRequest("/api/floorball/teams", Method.POST, null, null, model) as RestResponse;

        //        return deserial.Deserialize<int>(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //public static LeagueModel GetLeagueById(int leagueId)
        //{
        //    try
        //    {
        //        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
        //        Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
        //        RestResponse response = client.ExecuteRequest("/api/floorball/leagues/{id}", Method.GET, urlParams) as RestResponse;

        //        return deserial.Deserialize<LeagueModel>(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //public static PlayerModel GetPlayerById(int playerId)
        //{
        //    try
        //    {
        //        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
        //        Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", playerId.ToString() } };
        //        RestResponse response = client.ExecuteRequest("/api/floorball/players/{id}", Method.GET, urlParams) as RestResponse;

        //        return deserial.Deserialize<PlayerModel>(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //public static EventMessageModel GetEventMessageById(int eventMessageId)
        //{
        //    try
        //    {
        //        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
        //        Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", eventMessageId.ToString() } };
        //        RestResponse response = client.ExecuteRequest("/api/floorball/eventmessages/{id}", Method.GET, urlParams) as RestResponse;

        //        return deserial.Deserialize<EventMessageModel>(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //public static int AddEvent(int matchId, string type, TimeSpan time, int playerId, int eventMessageId, int teamId)
        //{
        //    try
        //    {
        //        EventModel eventModel = new EventModel();
        //        eventModel.MatchId = matchId;
        //        eventModel.TeamId = teamId;
        //        eventModel.Type = type;
        //        eventModel.Time = time;//.ToString(@"h\h\:m\m\:s\s", System.Globalization.CultureInfo.InvariantCulture);
        //        eventModel.EventMessageId = eventMessageId;
        //        eventModel.PlayerId = playerId;

        //        FloorballRESTClient client = new FloorballRESTClient(ServerURL);
        //        Dictionary<string, string> headers = new Dictionary<string, string>() { { "Content-Type", "application/json" } };
        //        RestResponse response = client.ExecuteRequest("/api/floorball/events", Method.POST, null, null, eventModel, headers) as RestResponse;

        //        return deserial.Deserialize<int>(response);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        public async static void RemoveEventAsync(int eventId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", eventId.ToString() } };
                await client.ExecuteRequestAsync("/api/floorball/events/{id}", Method.DELETE, urlParams);

            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
