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
			Model.Add(new EntityTableViewModel { Label = "League", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
			Model.Add(new EntityTableViewModel { Label = "Home Team", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
			Model.Add(new EntityTableViewModel { Label = "Away Team", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
			Model.Add(new EntityTableViewModel { Label = "Round", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
			Model.Add(new EntityTableViewModel { Label = "Date", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
			Model.Add(new EntityTableViewModel { Label = "Time", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
			Model.Add(new EntityTableViewModel { Label = "Stadium", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });

            return Model;
        }

        public async Task Save(List<EntityTableViewModel> model)
        {
			Model = model;

			EntityPresenter.Model = new MatchModel()
			{
				
			};

			await EntityPresenter.AddEntity("Error during adding match!");
        }

        public async Task SetDataFromServer()
        {
            await EntityPresenter.GetEntity("Error during getting team", "1");
        }
    }
}
