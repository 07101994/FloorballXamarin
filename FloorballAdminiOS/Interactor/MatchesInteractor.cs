using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball.REST.RESTHelpers;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.Interactor
{
    public class MatchesInteractor : Interactor
    {
        public MatchesInteractor()
        {
        }

        public async Task<Model> GetModel() 
        {
            List<Task> tasks = new List<Task>();

            Task<List<MatchModel>> matches = RESTHelper.GetMatchesAsync();
            tasks.Add(matches);

			Task<List<TeamModel>> teams = RESTHelper.GetTeamsAsync();
			tasks.Add(teams);

			Task<List<LeagueModel>> leagues = RESTHelper.GetLeaguesAsync();
			tasks.Add(leagues);


			await Task.WhenAll(tasks);

            return new Model
            {
                Matches = matches.Result,
                Teams = teams.Result,
                Leagues = leagues.Result
            };

        }

        public class Model
        {
			public List<MatchModel> Matches { get; set; }

			public List<TeamModel> Teams { get; set; }

			public List<LeagueModel> Leagues { get; set; }
            
        }

    }
}
