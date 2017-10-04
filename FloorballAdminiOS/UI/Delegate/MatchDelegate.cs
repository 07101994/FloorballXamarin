using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Helper;
using FloorballAdminiOS.UI.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Delegate
{
    public class MatchDelegate : BaseDelegate, IDelegate
    {
        public EntityPresenter<MatchModel> EntityPresenter { get; set; }

        public string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Match" : "Update Match";
        }

        public List<EntityTableViewModel> GetTableViewModel()
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

        public async Task Save(List<EntityTableViewModel> model)
        {
			Model = model;

			EntityPresenter.Model = new MatchModel()
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

			await EntityPresenter.AddEntity("Error during adding match!");

            Model.Clear();
        }

        public async Task SetDataFromServer()
        {
            await EntityPresenter.GetEntity("Error during getting match", "1");
        }
    }
}
