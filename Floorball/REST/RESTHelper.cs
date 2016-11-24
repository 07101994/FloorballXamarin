using Floorball.Exceptions;
using FloorballServer.Models.Floorball;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Floorball.REST
{
    class RESTHelper
    {
        private static JsonDeserializer deserial = new JsonDeserializer();
        private static string ServerURL = "http://192.168.0.20:8080";
        //private static string ServerURL = "http://192.168.173.1:8088";
        //private static string ServerURL = "http://192.168.173.1:8088";

        public async static Task<List<PlayerModel>> GetPlayersAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/players", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült a játékosok lekérdezése!");

                return deserial.Deserialize<List<PlayerModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

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

        public async static Task<List<TeamModel>> GetTeamsByLeagueAsync(int leagueId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/leagues/{id}/teams", Method.GET, urlParams) as RestResponse;

                CheckError(response,"Nem sikerült a csapatok lekérdezése a bajnoksághoz!");

                return deserial.Deserialize<List<TeamModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<TeamModel> GetTeamByIdAsync(int teamId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", teamId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/teams/{id}", Method.GET, urlParams) as RestResponse;

                CheckError(response, "Nem sikerült a csapat lekérdezése!");

                return deserial.Deserialize<TeamModel>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<List<MatchModel>> GetMatchesByLeagueAync(int leagueId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/leagues/{id}/matches", Method.GET, urlParams) as RestResponse;

                CheckError(response, "Nem sikerült a meccsek lekérdezése a bajnoksághoz!");

                return deserial.Deserialize<List<MatchModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<MatchModel> GetMatchByIdAsync(int matchId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/matches/{id}", Method.GET, urlParams) as RestResponse;

                CheckError(response, "Nem sikerült a mérkőzés lekérdezése!");

                return deserial.Deserialize<MatchModel>(response);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<List<RefereeModel>> GetRefereesAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/referees", Method.GET) as RestResponse;

                CheckError(response,"Nem sikerült a bírók lekérdezése!");

                return deserial.Deserialize<List<RefereeModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<RefereeModel> GetRefereeByIdAsync(int refereeId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", refereeId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/referee/{id}", Method.GET, urlParams) as RestResponse;

                CheckError(response, "Nem sikerült a bíró lekérdezése!");

                return deserial.Deserialize<RefereeModel>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<List<PlayerModel>> GetPlayersByTeamAsync(int teamId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", teamId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/teams/{id}/players", Method.GET, urlParams) as RestResponse;

                CheckError(response, "Nem sikerült a csapat játékosainak a lekérdezése!");

                return deserial.Deserialize<List<PlayerModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<List<PlayerModel>> GetPlayersByLeagueAsync(int leagueId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/leagues/{id}/players", Method.GET, urlParams) as RestResponse;

                CheckError(response,"Nem sikerül a játékosokat lekérdezni a bajnoksághoz!");

                return deserial.Deserialize<List<PlayerModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public async static Task<List<MatchModel>> GetMatchesByRefereeAsync(int refereeId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", refereeId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/referee/{id}/matches", Method.GET, urlParams) as RestResponse;

                CheckError(response, "Nem sikerült a bíróhoz tartozó meccsek lekérdezése!");

                return deserial.Deserialize<List<MatchModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
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

        public async static Task<List<LeagueModel>> GetLeaguesAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/leagues", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült a bajnokságok lekérdezése!");

                return deserial.Deserialize<List<LeagueModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<List<StadiumModel>> GetStadiumsAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/stadiums", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült a stadionok lekérdezése");

                return deserial.Deserialize<List<StadiumModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<List<MatchModel>> GetMatchesAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/matches", Method.GET) as RestResponse;

                CheckError(response,"Nem sikerült a mérkőzések lekérdezése!");

                return deserial.Deserialize<List<MatchModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<List<EventModel>> GetEventsAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/events", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült az események lekérdezése!");

                return deserial.Deserialize<List<EventModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<List<EventMessageModel>> GetEventMessagesAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/eventmessages", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült az eseményüzenetek lekérdezése!");

                return deserial.Deserialize<List<EventMessageModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<List<StatisticModel>> GetStatisticsAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/statistics", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült a statisztikák lekérdezése");

                return deserial.Deserialize<List<StatisticModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<List<TeamModel>> GetTeamsAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/teams", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült a csapatok lekérdezése!");

                return deserial.Deserialize<List<TeamModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<List<string>> GetAllYearAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/years", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült az évek lekérdezése!");

                return deserial.Deserialize<List<string>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async static Task<List<MatchModel>> GetActualMatchesAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/matches/actual", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült az aktuális mérkőzések lekérdezése!");

                return deserial.Deserialize<List<MatchModel>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<int> GetRoundsByLeagueAsync(int leagueId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/leagues/{id}/rounds", Method.GET, urlParams) as RestResponse;

                CheckError(response, "Nem sikerült a fordulók lekérdezése!");

                return deserial.Deserialize<int>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async static Task<Dictionary<int, List<int>>> GetPlayersAndTeamsAsync()
        {
            try
            {

                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/players/teams", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült a játékos, csapat lekérdezése!");

                return deserial.Deserialize<Dictionary<int, List<int>>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<Dictionary<int, List<int>>> GetPlayersAndMatchesAsync()
        {
            try
            {

                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/players/matches", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült a játékosok, meccsek lekérdezése");

                return deserial.Deserialize<Dictionary<int, List<int>>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<Dictionary<int, List<int>>> GetRefereesAndMatchesAsync()
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                RestResponse response = await client.ExecuteRequest("/api/floorball/referees/matches", Method.GET) as RestResponse;

                CheckError(response, "Nem sikerült a bírók, mérkőzések lekérdezése!");

                return deserial.Deserialize<Dictionary<int, List<int>>>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async static Task<List<StatisticModel>> GetStatisticsByLeagueAsync(int leagueId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/leagues/{id}/statistics", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<List<StatisticModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async static Task<EventModel> GetEventByIdAsync(int eventId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", eventId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/events/{id}", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<EventModel>(response);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async static Task<List<EventModel>> GetEventsByMatchAsync(int matchId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/matches/{id}/events", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<List<EventModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async static Task<List<EventMessageModel>> GetEventMessagesByCategoryAsync(char category)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> queryParams = new Dictionary<string, string>() { { "categoryNumber", category.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/eventmessages", Method.GET, null, queryParams) as RestResponse;

                return deserial.Deserialize<List<EventMessageModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async static Task<List<LeagueModel>> GetLeaguesByYearAsync(string year)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> queryParams = new Dictionary<string, string>() { { "year", year } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/leaguesbyyear", Method.GET, null, queryParams) as RestResponse;

                return deserial.Deserialize<List<LeagueModel>>(response);
            }
            catch (Exception)
            {

                throw;
            }

        }

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
                await client.ExecuteRequest("/api/floorball/events/{id}", Method.DELETE, urlParams);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async static Task<List<PlayerModel>> GetPlayersByMatchAsync(int matchId)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> urlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/matches/{id}/players", Method.GET, urlParams) as RestResponse;

                return deserial.Deserialize<List<PlayerModel>>(response);
            }
            catch (Exception)
            {
				
                throw;
            }

        }

        public async static Task<List<UpdateData>> GetUpdatesAsync(DateTime date)
        {
            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);
                Dictionary<string, string> queryParams = new Dictionary<string, string>() { { "date", date.ToString() } };
                RestResponse response = await client.ExecuteRequest("/api/floorball/updates", Method.GET, null, queryParams) as RestResponse;

                CheckError(response,"Nem sikerült a frissíések letöltése!");

                return JsonConvert.DeserializeObject<List<UpdateData>>(deserial.Deserialize<string>(response));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
