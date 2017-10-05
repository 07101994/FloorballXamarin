using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Helper;
using FloorballAdminiOS.Interactor.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Match
{
    public class MatchPresenter : EntityPresenter<EntityScreen>
    {
        MatchInteractor matchInteractor;

        MatchModel match;

        List<TeamModel> teams;
        List<LeagueModel> leagues;
        List<StadiumModel> stadiums;

		public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			matchInteractor = new MatchInteractor();
			Url = "/api/floorball/matches";
		}

        public override void ClearModel()
        {
            Model.Clear();
        }

        public override void DetachScreen()
		{
			base.DetachScreen();
		}

        public override string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Match" : "Update Match";
        }

        public override List<EntityTableViewModel> SetTableViewModel()
        {
			
            var rounds = UIHelper.GetNumbers(1, 30);

            Model.Add(new EntityTableViewModel { Label = "League", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : leagues.Single(l => l.Id == match.Id).Name });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(leagues.Select(l => l.Name), leagues.Select(l => l.Id)) });
            Model.Add(new EntityTableViewModel { Label = "Home Team", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : teams.Single(t => t.Id == match.HomeTeamId).Name });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(teams.Select(t => t.Name), teams.Select(t => t.Id)) });
			Model.Add(new EntityTableViewModel { Label = "Away Team", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : teams.Single(t => t.Id == match.AwayTeamId).Name });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(teams.Select(t => t.Name), teams.Select(t => t.Id)) });
            Model.Add(new EntityTableViewModel { Label = "Round", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : match.Round.ToString() });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(rounds, rounds) });
            Model.Add(new EntityTableViewModel { Label = "Date", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : match.Date.ToString() });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.DatePicker, IsVisible = false, Value = "" });
            Model.Add(new EntityTableViewModel { Label = "Time", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : match.Time.ToString() });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.DatePicker, IsVisible = false, Value = "" });
            Model.Add(new EntityTableViewModel { Label = "Stadium", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : stadiums.Single(s => s.Id == match.StadiumId).Name });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(stadiums.Select(s => s.Name), stadiums.Select(s => s.Id)) });


			return Model;
        }

        protected async override Task Save()
        {

            await matchInteractor.AddEntity(Url, "Error during adding match!", match);

			Model.Clear();
        }

        public async override Task SetDataFromServer(UpdateType crud)
        {
            var tasks = new List<Task>();

            Task<MatchModel> matchTask = null;

            if (crud == UpdateType.Update)
            {
				matchTask = matchInteractor.GetEntityById(Url, "Error during getting match", "1");
				tasks.Add(matchTask);
			}

			Task<List<LeagueModel>> leaguesTask = matchInteractor.GetEntities<LeagueModel>("api/floorball/leagues", "Error during getting leagues");
			tasks.Add(leaguesTask);

            Task<List<TeamModel>> teamsTask = matchInteractor.GetEntities<TeamModel>("api/floorball/teams","Error during getting teams");
            tasks.Add(teamsTask);

			Task<List<StadiumModel>> stadiumsTask = matchInteractor.GetEntities<StadiumModel>("api/floorball/stadiums", "Error during getting stadiums");
			tasks.Add(stadiumsTask);

            await Task.WhenAll(tasks);

            if (crud == UpdateType.Update)
            {
                match = matchTask.Result;
            }

            leagues = leaguesTask.Result;
            teams = teamsTask.Result;
            stadiums = stadiumsTask.Result;

            SetTableViewModel();

        }

        protected override void Validate()
        {
			match = new MatchModel()
			{
				LeagueId = Model[1].PickerValueAsInt,
				HomeTeamId = Model[3].PickerValueAsInt,
				AwayTeamId = Model[5].PickerValueAsInt,
				Round = Model[7].PickerValueAsShort,
				Date = Model[9].PickerValueAsDateTime,
				Time = Model[11].PickerValueAsTimeSpan,
				StadiumId = Model[13].PickerValueAsInt,
				State = StateEnum.Confirmed,
				GoalsH = 0,
				GoalsA = 0
			};
        }
    }
}
