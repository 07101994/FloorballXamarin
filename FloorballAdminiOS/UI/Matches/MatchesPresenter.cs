using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FloorballAdminiOS.Interactor;
using FloorballAdminiOS.UI;
using FloorballAdminiOS.UI.Matches;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Matches
{
    public class MatchesPresenter : Presenter<MatchesScreen>
    {
        
        MatchesInteractor matchesInteractor;

        public List<MatchModel> Matches { get; set; }

        public List<TeamModel> Teams { get; set; }

        public List<LeagueModel> Leagues { get; set; }

        public MatchesPresenter()
        {
            Matches = new List<MatchModel>();
            Teams = new List<TeamModel>();
            Leagues = new List<LeagueModel>();

        }

		public async override void AttachScreen(MatchesScreen screen)
		{
			base.AttachScreen(screen);

			matchesInteractor = new MatchesInteractor();
            await InitAsync();

		}

		public override void DetachScreen()
		{
			base.DetachScreen();

		}

        public async Task InitAsync()
        {
            var model = await matchesInteractor.GetModel();

            Matches = model.Matches;
            Teams = model.Teams;
            Leagues = model.Leagues;

        }

    }
}
