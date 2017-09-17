using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball.REST.RequestModels;
using FloorballServer.Models.Floorball;

namespace Floorball.REST.RESTHelpers
{
    public partial class RESTHelper : RESTHelperBase
    {
		public static PlayerModel AddPlayerToTeam(int playerId, int teamId)
		{
			return Network.Put<int,PlayerModel>(new HTTPPutRequestModel<int>
			{
				Url = "/api/floorball/teams/{teamId}/players/{playerId}}",
                UrlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "teamId", teamId.ToString() } },
				ErrorMsg = "Error during adding player to team!"
			});
		}

		public static PlayerModel AddPlayerToMatch(int playerId, int matchId)
		{
			return Network.Put<int, PlayerModel>(new HTTPPutRequestModel<int>
			{
				Url = "/api/floorball/matches/{matchId}/players/{playerId}}",
				UrlParams = new Dictionary<string, string>() { { "playerId", playerId.ToString() }, { "matchId", matchId.ToString() } },
				ErrorMsg = "Error during adding player to match!"
			});
		}
	}
}
