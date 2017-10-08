using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FloorballAdminiOS.Interactor.Search;
using FloorballAdminiOS.UI.EntitySearch;
using FloorballPCL;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Player
{
    public class PlayerSearchPresenter : EntitySearchPresenter<EntitySearchScreen>
    {
		SearchInteractor<PlayerModel> interactor;

        public PlayerSearchPresenter(ITextManager textManager) : base(textManager)
        {
        }

        public override void AttachScreen(EntitySearchScreen screen)
		{
			base.AttachScreen(screen);
			interactor = new SearchInteractor<PlayerModel>();

		}

        public async override Task GetEntitiesFromServer()
        {

			List<Task> tasks = new List<Task>();

			Task<List<PlayerModel>> entitiesTask = interactor.GetEntities("api/floorball/players", "Error during getting players");
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

			PlayerModel prevEntity = null;

			List<SearchCell> searchList = new List<SearchCell>();

			foreach (var entity in entities.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ThenBy(e => e.BirthDate))
			{

				if (prevEntity == null || entity.FirstName != prevEntity.FirstName)
				{
					SearchModel.Add(new List<SearchCell>());
					Titles.Add(new SearchTitle { MainTitle = "", Subtitle = "" });
				}

				SearchModel.Last().Add(new SearchCell
				{
                    Id = entity.Id,
					Title = entity.FirstName + "  " + entity.LastName,
					Subtitle = entity.BirthDate.ToString("yyyy-MM-dd"),
                    RightDetail = TextManager.GetText(entity.Gender)
				});

				prevEntity = entity;
			}

			FilteredSearchModel = new List<List<SearchCell>>(SearchModel);
			FilteredTitles = new List<SearchTitle>(Titles);


        }
    }
}
