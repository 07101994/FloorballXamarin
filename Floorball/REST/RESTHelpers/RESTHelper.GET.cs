using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball.Model;
using Floorball.REST.RequestModels;
using FloorballServer.Models.Floorball;

namespace Floorball.REST.RESTHelpers
{
    public partial class RESTHelper : RESTHelperBase
    {

		#region simple GET

		public async static Task<List<PlayerModel>> GetPlayersAsync()
		{
			return await Network.GetAsync<List<PlayerModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/players",
				ErrorMsg = "Error during getting players from server!"
			});

		}

		public async static Task<List<LeagueModel>> GetLeaguesAsync()
		{
			return await Network.GetAsync<List<LeagueModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/leagues",
				ErrorMsg = "Error during getting leagues from server!"
			});

		}

		public async static Task<List<RefereeModel>> GetRefereesAsync()
		{
			return await Network.GetAsync<List<RefereeModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/referees",
				ErrorMsg = "Error during getting referees from server!"
			});

		}

		public async static Task<List<TeamModel>> GetTeamsAsync(bool withImage = false)
		{
			return await Network.GetAsync<List<TeamModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/teams",
				ErrorMsg = "Error during getting teams from server!"
			});

		}

		public async static Task<List<MatchModel>> GetMatchesAsync()
		{
			return await Network.GetAsync<List<MatchModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/matches",
				ErrorMsg = "Error during getting matches from server!"
			});

		}

		public async static Task<UpdateModel> GetUpdatesAsync(DateTime date)
		{

			return await Network.GetAsync<UpdateModel>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/updates",
				QueryParams = new Dictionary<string, string>() { { "date", date.ToString() } },
				ErrorMsg = "Error during getting updates from server!"
			});

		}


		public async static Task<List<StadiumModel>> GetStadiumsAsync()
		{

			return await Network.GetAsync<List<StadiumModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/stadiums",
				ErrorMsg = "Error during getting stadiums from server!"
			});

		}

		public async static Task<List<EventModel>> GetEventsAsync()
		{
			return await Network.GetAsync<List<EventModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/events",
				ErrorMsg = "Error during getting events from server!"
			});
		}

		public async static Task<List<EventMessageModel>> GetEventMessagesAsync()
		{
			return await Network.GetAsync<List<EventMessageModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/eventmessages",
				ErrorMsg = "Error during getting event messages from server!"
			});

		}

		public async static Task<List<StatisticModel>> GetStatisticsAsync()
		{
			return await Network.GetAsync<List<StatisticModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/statistics",
				ErrorMsg = "Error during getting statistics from server!"
			});

		}

		#endregion

		public async static Task<Dictionary<int, List<int>>> GetPlayersAndTeamsAsync()
		{
			return await Network.GetAsync<Dictionary<int, List<int>>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/players/teams",
				ErrorMsg = "Error during getting players for teams!"
			});

		}

		public async static Task<Dictionary<int, List<int>>> GetPlayersAndMatchesAsync()
		{

			return await Network.GetAsync<Dictionary<int, List<int>>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/players/matches",
				ErrorMsg = "Error during getting players for matches!"
			});

		}

		public async static Task<Dictionary<int, List<int>>> GetRefereesAndMatchesAsync()
		{

			return await Network.GetAsync<Dictionary<int, List<int>>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/referees/matches",
				ErrorMsg = "Error during getting referees for matches!"
			});

		}

		public async static Task<List<TeamModel>> GetTeamsByLeagueAsync(int leagueId)
		{
            return await Network.GetAsync<List<TeamModel>>(new HTTPGetRequestModel()
            {
                Url = "/api/floorball/leagues/{id}/teams",
                UrlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } },
                ErrorMsg = "Error during getting teams to league!"
            });

		}

		public async static Task<TeamModel> GetTeamByIdAsync(int teamId)
		{
            return await Network.GetAsync<TeamModel>(new HTTPGetRequestModel()
            {
                Url = "/api/floorball/teams/{id}",
                UrlParams = new Dictionary<string, string>() { { "id", teamId.ToString() } },
                ErrorMsg = "Error during getting team!"
            });

		}

		public async static Task<List<MatchModel>> GetMatchesByLeagueAync(int leagueId)
		{
			return await Network.GetAsync<List<MatchModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/leagues/{id}/matches",
				UrlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } },
				ErrorMsg = "Error during getting matches for league!"
			});
		}

		public async static Task<MatchModel> GetMatchByIdAsync(int matchId)
		{

            return await Network.GetAsync<MatchModel>(new HTTPGetRequestModel()
            {
                Url = "/api/floorball/matches/{id}",
                UrlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } },
                ErrorMsg = "Error during getting match!"
            });

		}

		public async static Task<RefereeModel> GetRefereeByIdAsync(int refereeId)
		{
			return await Network.GetAsync<RefereeModel>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/referees/{id}",
				UrlParams = new Dictionary<string, string>() { { "id", refereeId.ToString() } },
				ErrorMsg = "Error during getting referee!"
			});

		}

		public async static Task<List<PlayerModel>> GetPlayersByTeamAsync(int teamId)
		{
			return await Network.GetAsync<List<PlayerModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/teams/{id}/players",
				UrlParams = new Dictionary<string, string>() { { "id", teamId.ToString() } },
				ErrorMsg = "Error during getting players for team!"
			});

		}

		public async static Task<List<PlayerModel>> GetPlayersByLeagueAsync(int leagueId)
		{
			return await Network.GetAsync<List<PlayerModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/leagues/{id}/players",
				UrlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } },
				ErrorMsg = "Error during getting players for league!"
			});

		}



        public async static Task<List<MatchModel>> GetMatchesByRefereeAsync(int refereeId)
        {
			return await Network.GetAsync<List<MatchModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/referees/{id}/matches",
				UrlParams = new Dictionary<string, string>() { { "id", refereeId.ToString() } },
				ErrorMsg = "Error during getting matches for referee!"
			});

        }

		public async static Task<List<PlayerModel>> GetPlayersByMatchAsync(int matchId)
		{
			return await Network.GetAsync<List<PlayerModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/matches/{id}/players",
				UrlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } },
				ErrorMsg = "Error during getting players for match!"
			});

		}

		public async static Task<List<string>> GetAllYearAsync()
		{
			return await Network.GetAsync<List<string>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/years",
				ErrorMsg = "Error during getting players for match!"
			});
		}


		public async static Task<List<MatchModel>> GetActualMatchesAsync()
		{
			return await Network.GetAsync<List<MatchModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/matches/actual",
				ErrorMsg = "Error during getting actual matches from server!"
			});
			
		}

		public async static Task<int> GetRoundsByLeagueAsync(int leagueId)
		{
			return await Network.GetAsync<int>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/leagues/{id}/rounds",
                UrlParams =  new Dictionary<string, string>() { { "id", leagueId.ToString() } },
				ErrorMsg = "Error during getting rounds number of the league!"
			});
		}

		public async static Task<List<StatisticModel>> GetStatisticsByLeagueAsync(int leagueId)
		{
			return await Network.GetAsync<List<StatisticModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/leagues/{id}/statistics",
				UrlParams = new Dictionary<string, string>() { { "id", leagueId.ToString() } },
				ErrorMsg = "Error during getting statistics for league!"
			});
		}

		public async static Task<EventModel> GetEventByIdAsync(int eventId)
		{
			return await Network.GetAsync<EventModel>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/events/{id}",
				UrlParams = new Dictionary<string, string>() { { "id", eventId.ToString() } },
				ErrorMsg = "Error during getting event!"
			});

		}

		public async static Task<List<EventModel>> GetEventsByMatchAsync(int matchId)
		{
			return await Network.GetAsync<List<EventModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/matches/{id}/events",
				UrlParams = new Dictionary<string, string>() { { "id", matchId.ToString() } },
				ErrorMsg = "Error during getting events for match!"
			});
		}

		public async static Task<List<EventMessageModel>> GetEventMessagesByCategoryAsync(char category)
		{
			return await Network.GetAsync<List<EventMessageModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/eventmessages",
				QueryParams = new Dictionary<string, string>() { { "categoryNumber", category.ToString() } },
				ErrorMsg = "Error during getting eventmessages for category!"
			});
		}

		public async static Task<List<LeagueModel>> GetLeaguesByYearAsync(string year)
		{
			return await Network.GetAsync<List<LeagueModel>>(new HTTPGetRequestModel()
			{
				Url = "/api/floorball/leagues",
				QueryParams = new Dictionary<string, string>() { { "year", year } },
				ErrorMsg = "Error during getting leagues!"
			});
		}

	}

}
