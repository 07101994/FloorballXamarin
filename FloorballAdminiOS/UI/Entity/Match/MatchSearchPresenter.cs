using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FloorballAdminiOS.Interactor.Search;
using FloorballAdminiOS.UI.EntitySearch;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Match
{
    public class MatchSearchPresenter : EntitySearchPresenter<EntitySearchScreen>
    {
		SearchInteractor<MatchModel> interactor;

		public override void AttachScreen(EntitySearchScreen screen)
		{
			base.AttachScreen(screen);
			interactor = new SearchInteractor<MatchModel>();

		}

        public async override Task GetEntitiesFromServer()
        {

            List<Task> tasks = new List<Task>();

			Task<List<MatchModel>> entitiesTask = interactor.GetEntities("api/floorball/matches", "Error during getting matches");
            tasks.Add(entitiesTask);

            Task<List<LeagueModel>> leaguesTask = interactor.GetEntities<LeagueModel>("api/floorball/leagues", "Error during getting leagues");
            tasks.Add(leaguesTask);

            Task<List<TeamModel>> teamsTask = interactor.GetEntities<TeamModel>("api/floorball/teams", "Error during getting teams");
            tasks.Add(teamsTask);

            await Task.WhenAll(tasks);

            var entities = entitiesTask.Result;
            var leagues = leaguesTask.Result;
            var teams = teamsTask.Result;

			MatchModel prevEntity = null;

			List<SearchCell> searchList = new List<SearchCell>();

			foreach (var entity in entities.OrderBy(e => e.LeagueId).ThenBy(e => e.Round).ThenBy(e => e.Date))
			{

				if (prevEntity == null || entity.LeagueId != prevEntity.LeagueId)
				{
					SearchModel.Add(new List<SearchCell>());
                    var league = leagues.Single(l => l.Id == entity.LeagueId);
                    Titles.Add(new SearchTitle { MainTitle = league.Name + " - " + league.ClassName, Subtitle = league.Sex });
				}

                SearchModel.Last().Add(new SearchCell
                {
                    Title = teams.Single(t => t.Id == entity.HomeTeamId).Name + " - " + teams.Single(t => t.Id == entity.AwayTeamId).Name,
                    Subtitle = entity.Date.ToString("yyyy-MM-dd hh:mm"),
                    RightDetail = entity.Round.ToString()
				});

				prevEntity = entity;
			}

			FilteredSearchModel = new List<List<SearchCell>>(SearchModel);
			FilteredTitles = new List<SearchTitle>(Titles);

        }
    }
}
