using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Helper;
using FloorballAdminiOS.UI.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Delegate
{
    public class TeamDelegate : BaseDelegate, IDelegate
    {

        public EntityPresenter<TeamModel> EntityPresenter { get; set; }

        public string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Team" : "Update Team";
        }

        public List<EntityTableViewModel> GetTableViewModel()
        {
            if (Model.Count == 0)
            {
				Model.Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { Label = "Gendre", CellType = TableViewCellType.SegmenControl, IsVisible = true, Value = new SegmentControlModel { Segments = new List<Tuple<string, string>> { new Tuple<string, string>("men", "men"), new Tuple<string, string>("women", "women") } } });
				Model.Add(new EntityTableViewModel { Label = "Year", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(UIHelper.GetNumbers(2012, 2018)) });
				Model.Add(new EntityTableViewModel { Label = "League", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(UIHelper.GetNumbers(2012, 2018)) });
				Model.Add(new EntityTableViewModel { Label = "Coach", CellType = TableViewCellType.TextField, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { Label = "Stadium", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(UIHelper.GetNumbers(2012, 2018)) });
				Model.Add(new EntityTableViewModel { Label = "Country", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
				Model.Add(new EntityTableViewModel { Label = "TeamId", CellType = TableViewCellType.TextField, IsVisible = true, Value = "" });
			}

           	return Model;
        }

        public async Task Save(List<EntityTableViewModel> model)
        {
			Model = model;

			EntityPresenter.Model = new TeamModel()
			{
				
			};

			await EntityPresenter.AddEntity("Error during adding team!");

            Model.Clear();
        }

        public async Task SetDataFromServer()
        {
            await EntityPresenter.GetEntity("Error during getting team", "1");
        }
    }
}
