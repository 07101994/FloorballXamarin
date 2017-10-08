using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Interactor.Search;
using FloorballAdminiOS.UI.EntitySearch;
using FloorballPCL;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.League
{
    public class LeagueSearchPresenter : EntitySearchPresenter<EntitySearchScreen>
    {
        SearchInteractor<LeagueModel> interactor;

        public LeagueSearchPresenter(ITextManager textManager) : base(textManager)
        {
        }

        public override void AttachScreen(EntitySearchScreen screen)
        {
            base.AttachScreen(screen);
            interactor = new SearchInteractor<LeagueModel>();

        }

        public async override Task GetEntitiesFromServer()
        {

            var entities = await interactor.GetEntities("api/floorball/leagues", "Error during getting leagues");

            LeagueModel prevEntity = null;

            List<SearchCell> searchList = new List<SearchCell>();

            foreach (var entity in entities.OrderBy(e => e.Country).ThenBy(e => e.Year).ThenBy(e => e.Name))
            {

                if (prevEntity == null || entity.Country != prevEntity.Country)
                {
                    SearchModel.Add(new List<SearchCell>());
                    Titles.Add(new SearchTitle { MainTitle = TextManager.GetText(entity.Country), Subtitle = "" });
                }

				SearchModel.Last().Add(new SearchCell
				{
                    Id = entity.Id,
					Title = entity.Name,
					Subtitle = entity.Year.Year.ToString() + " - " + entity.Type,
                    RightDetail = AppDelegate.SharedAppDelegate.TextManager.GetText(entity.Gender)
				});

                prevEntity = entity;
            }

            FilteredSearchModel =  new List<List<SearchCell>>(SearchModel);
            FilteredTitles = new List<SearchTitle>(Titles);
        }

    }
}
