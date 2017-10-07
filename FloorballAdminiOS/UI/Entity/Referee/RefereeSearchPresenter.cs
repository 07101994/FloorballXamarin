using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Interactor.Search;
using FloorballAdminiOS.UI.EntitySearch;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Referee
{
    public class RefereeSearchPresenter : EntitySearchPresenter<EntitySearchScreen>
    {

		SearchInteractor<RefereeModel> interactor;

		public override void AttachScreen(EntitySearchScreen screen)
		{
			base.AttachScreen(screen);
			interactor = new SearchInteractor<RefereeModel>();

		}

        public async override Task GetEntitiesFromServer()
        {

			List<Task> tasks = new List<Task>();

			Task<List<RefereeModel>> entitiesTask = interactor.GetEntities("api/floorball/referees", "Error during getting referees");
			tasks.Add(entitiesTask);

			/*Task<List<LeagueModel>> leaguesTask = interactor.GetEntities<LeagueModel>("api/floorball/leagues", "Error during getting leagues");
            tasks.Add(leaguesTask);

            Task<List<TeamModel>> teamsTask = interactor.GetEntities<TeamModel>("api/floorball/teams", "Error during getting teams");
            tasks.Add(teamsTask);*/

			//Task<Dictionary<int, List<int>>> playersAndTeamsTask =  interactor.GetEntityMappings("api/floorball/players/teams", "Error during getting teams for player");

			await Task.WhenAll(tasks);

			var entities = entitiesTask.Result;
            /*var leagues = leaguesTask.Result;
            var teams = teamsTask.Result;*/

            RefereeModel prevEntity = null;

			List<SearchCell> searchList = new List<SearchCell>();

			foreach (var entity in entities.OrderBy(e => e.Country).ThenBy(e => e.Name).ThenByDescending(e => e.Number))
			{

				if (prevEntity == null || entity.Country != prevEntity.Country)
				{
					SearchModel.Add(new List<SearchCell>());
					Titles.Add(new SearchTitle { MainTitle = entity.Country.ToFriendlyString(), Subtitle = "" });
				}

				SearchModel.Last().Add(new SearchCell
				{
					Title = entity.Name,
                    Subtitle = "Match count: " + entity.Number.ToString(),
					RightDetail = ""
				});

				prevEntity = entity;
			}

			FilteredSearchModel = new List<List<SearchCell>>(SearchModel);
			FilteredTitles = new List<SearchTitle>(Titles);

        }
    }
}
