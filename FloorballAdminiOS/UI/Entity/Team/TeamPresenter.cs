using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Helper;
using FloorballAdminiOS.Interactor.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Team
{
    public class TeamPresenter : EntityPresenter<EntityScreen>
    {
		TeamInteractor teamInteractor;

		public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			teamInteractor = new TeamInteractor();
			Url = "/api/floorball/teams";
		}

		public override void DetachScreen()
		{
			base.DetachScreen();
		}

        public override string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Team" : "Update Team";
        }

        public override List<EntityTableViewModel> GetTableViewModel()
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

        public async override Task Save(List<EntityTableViewModel> model)
        {
			Model = model;

			var teamModel = new TeamModel()
			{

			};

            await teamInteractor.AddEntity(Url,"Error during adding team!", teamModel);

			Model.Clear();
        }

        public async override Task SetDataFromServer()
        {
            await teamInteractor.GetEntityById(Url, "Error during getting team", "1");
        }
    }
}
