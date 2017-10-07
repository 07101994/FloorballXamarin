using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FloorballAdminiOS.Interactor.Search;
using FloorballAdminiOS.UI.EntitySearch;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Team
{
    public class TeamSearchPresenter : EntitySearchPresenter<EntitySearchScreen>
    {

		SearchInteractor<TeamModel> interactor;

		public override void AttachScreen(EntitySearchScreen screen)
		{
			base.AttachScreen(screen);
			interactor = new SearchInteractor<TeamModel>();

		}

        public async override Task GetEntitiesFromServer()
        {
			List<Task> tasks = new List<Task>();

			Task<List<TeamModel>> entitiesTask = interactor.GetEntities("api/floorball/teams", "Error during getting teams");
			tasks.Add(entitiesTask);

			Task<List<LeagueModel>> leaguesTask = interactor.GetEntities<LeagueModel>("api/floorball/leagues", "Error during getting leagues");
            tasks.Add(leaguesTask);

            /*dTask<List<TeamModel>> teamsTask = interactor.GetEntities<TeamModel>("api/floorball/teams", "Error during getting teams");
            tasks.Add(teamsTask);*/

			//Task<Dictionary<int, List<int>>> playersAndTeamsTask =  interactor.GetEntityMappings("api/floorball/players/teams", "Error during getting teams for player");

			await Task.WhenAll(tasks);

			var entities = entitiesTask.Result;
			var leagues = leaguesTask.Result;
            //var teams = teamsTask.Result;

			TeamModel prevEntity = null;

			List<SearchCell> searchList = new List<SearchCell>();

			foreach (var entity in entities.OrderByDescending(e => e.LeagueId).ThenBy(e => e.Name))
			{

				if (prevEntity == null || entity.Name != prevEntity.Name)
				{
					SearchModel.Add(new List<SearchCell>());
                    var league = leagues.Single(l => l.Id == entity.LeagueId);
					Titles.Add(new SearchTitle { MainTitle = league.Name, Subtitle = league.Sex });
				}

				SearchModel.Last().Add(new SearchCell
				{
					Title = entity.Name,
					Subtitle = entity.Coach,
					RightDetail = entity.Year.Year.ToString()
				});

				prevEntity = entity;
			}

			FilteredSearchModel = new List<List<SearchCell>>(SearchModel);
			FilteredTitles = new List<SearchTitle>(Titles);
        }
    }
}
