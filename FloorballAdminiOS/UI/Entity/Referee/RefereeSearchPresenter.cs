using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Interactor.Search;
using FloorballAdminiOS.UI.EntitySearch;
using FloorballPCL;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Referee
{
    public class RefereeSearchPresenter : EntitySearchPresenter<EntitySearchScreen>
    {

		SearchInteractor<RefereeModel> interactor;

        public RefereeSearchPresenter(ITextManager textManager) : base(textManager)
        {
        }

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
                    Titles.Add(new SearchTitle { MainTitle =  AppDelegate.SharedAppDelegate.TextManager.GetText(entity.Country), Subtitle = "" });
				}

				SearchModel.Last().Add(new SearchCell
				{
                    Id = entity.Id,
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
