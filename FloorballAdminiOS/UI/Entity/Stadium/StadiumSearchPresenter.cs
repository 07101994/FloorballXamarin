using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FloorballAdminiOS.Interactor.Search;
using FloorballAdminiOS.UI.EntitySearch;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Stadium
{
    public class StadiumSearchPresenter : EntitySearchPresenter<EntitySearchScreen>
    {
		SearchInteractor<StadiumModel> interactor;

		public override void AttachScreen(EntitySearchScreen screen)
		{
			base.AttachScreen(screen);
			interactor = new SearchInteractor<StadiumModel>();

		}

        public async override Task GetEntitiesFromServer()
        {
			List<Task> tasks = new List<Task>();

			Task<List<StadiumModel>> entitiesTask = interactor.GetEntities("api/floorball/stadiums", "Error during getting stadiums");
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

			StadiumModel prevEntity = null;

			List<SearchCell> searchList = new List<SearchCell>();

			foreach (var entity in entities.OrderBy(e => e.Name))
			{

				if (prevEntity == null || entity.Name != prevEntity.Name)
				{
					SearchModel.Add(new List<SearchCell>());
					Titles.Add(new SearchTitle { MainTitle = "", Subtitle = "" });
				}

				SearchModel.Last().Add(new SearchCell
				{
					Title = entity.Name,
					Subtitle = entity.Address,
					RightDetail = ""
				});

				prevEntity = entity;
			}

			FilteredSearchModel = new List<List<SearchCell>>(SearchModel);
			FilteredTitles = new List<SearchTitle>(Titles);

		}

    }
}
