using System;
using System.Collections.Generic;
using Floorball.REST.RequestModels;

namespace Floorball.REST.RESTHelpers
{
    public partial class RESTHelper : RESTHelperBase
    {
		public static void RemovePlayerFromMatch(int playerId, int matchId)
		{
			Network.Delete(new HTTPDeleteRequestModel
			{
				Url = "/api/floorball/matches/{matchId}/players/{playerId}}",
				UrlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "matchId", matchId.ToString() } },
				ErrorMsg = "Error during removing player from match!"
			});
		}

		public static void RemovePlayerFromTeam(int playerId, int teamId)
		{
			Network.Delete(new HTTPDeleteRequestModel
			{
				Url = "/api/floorball/teams/{teamId}/players/{playerId}}",
				UrlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "teamId", teamId.ToString() } },
				ErrorMsg = "Error during removing player from team!"
			});
		}

		public async static void RemoveEventAsync(int eventId)
		{
			await Network.DeleteAsync(new HTTPDeleteRequestModel
			{
				Url = "/api/floorball/events/{id}",
				UrlParams = new Dictionary<string, string>() { { "id", eventId.ToString() } },
				ErrorMsg = "Error during removing event!"
			});
		}
	}
}
