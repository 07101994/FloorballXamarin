using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Interactor.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Player
{
    public class PlayerPresenter : EntityPresenter<EntityScreen>
    {
        PlayerInteractor playerInteractor;

		public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			playerInteractor = new PlayerInteractor();
			Url = "/api/floorball/players";
		}

		public override void DetachScreen()
		{
			base.DetachScreen();
		}

        public override string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Player" : "Update Player";
        }

        public override List<EntityTableViewModel> GetTableViewModel()
        {
			if (Model.Count == 0)
			{
				Model.Add(new EntityTableViewModel { Label = "First Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { Label = "Last Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { Label = "Birth Date", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(UIHelper.GetNumbers(1950, 2012)) });
			}


			return Model;
        }

        public async override Task Save(List<EntityTableViewModel> model)
        {
			Model = model;

			var playerModel = new PlayerModel()
			{

			};

            await playerInteractor.AddEntity(Url, "Error during adding player!", playerModel);

			Model.Clear();
        }

        public async override Task SetDataFromServer()
        {
            await playerInteractor.GetEntityById(Url, "Error during getting player", "1");
        }
    }
}
