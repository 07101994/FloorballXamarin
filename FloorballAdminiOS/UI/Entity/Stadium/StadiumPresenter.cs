using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Interactor.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Stadium
{
    public class StadiumPresenter : EntityPresenter<EntityScreen>
    {
		StadiumInteractor stadiumInteractor;

		public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			stadiumInteractor = new StadiumInteractor();
			Url = "/api/floorball/stadiums";
		}

		public override void DetachScreen()
		{
			base.DetachScreen();
		}

        public override string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Stadium" : "Update Stadium";
        }

        public override List<EntityTableViewModel> GetTableViewModel()
        {
			if (Model.Count == 0)
			{
				Model.Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { Label = "Address", CellType = TableViewCellType.TextField, IsVisible = true, Value = "" });
			}

			return Model;
        }

        public async override Task Save(List<EntityTableViewModel> model)
        {
			Model = model;

			var stadiumModel = new StadiumModel()
			{

			};

            await stadiumInteractor.AddEntity(Url, "Error during adding stadium!", stadiumModel);

			Model.Clear();
        }

        public async override Task SetDataFromServer()
        {
            await stadiumInteractor.GetEntityById(Url, "Error during getting stadium", "1");
        }
    }
}
