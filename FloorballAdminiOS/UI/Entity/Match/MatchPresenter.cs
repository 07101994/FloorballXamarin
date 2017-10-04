using System;
using System.Collections.Generic;
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

		public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			matchInteractor = new MatchInteractor();
			Url = "/api/floorball/matches";
		}

		public override void DetachScreen()
		{
			base.DetachScreen();
		}

        public override string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Match" : "Update Match";
        }

        public override List<EntityTableViewModel> GetTableViewModel()
        {
			if (Model.Count == 0)
			{
				Model.Add(new EntityTableViewModel { Label = "League", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
				Model.Add(new EntityTableViewModel { Label = "Home Team", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
				Model.Add(new EntityTableViewModel { Label = "Away Team", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
				Model.Add(new EntityTableViewModel { Label = "Round", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
				Model.Add(new EntityTableViewModel { Label = "Date", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
				Model.Add(new EntityTableViewModel { Label = "Time", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
				Model.Add(new EntityTableViewModel { Label = "Stadium", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });

			}

			return Model;
        }

        public async override Task Save(List<EntityTableViewModel> model)
        {
			Model = model;

			var matchModel = new MatchModel()
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

            await matchInteractor.AddEntity(Url, "Error during adding match!", matchModel);

			Model.Clear();
        }

        public async override Task SetDataFromServer()
        {
            await matchInteractor.GetEntityById(Url, "Error during getting match", "1");

        }
    }
}
