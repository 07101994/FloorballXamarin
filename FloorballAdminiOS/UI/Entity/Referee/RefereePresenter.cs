using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Interactor.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Referee
{
    public class RefereePresenter : EntityPresenter<EntityScreen>
    {
        RefereeInteractor refereeInteractor;

		public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			refereeInteractor = new RefereeInteractor();
			Url = "/api/floorball/referees";
		}

		public override void DetachScreen()
		{
			base.DetachScreen();
		}

        public override string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Referee" : "Update Referee";
        }

        public override List<EntityTableViewModel> GetTableViewModel()
        {
			if (Model.Count == 0)
			{
				Model.Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = "" });
			}

			return Model;
        }

        public async override Task Save(List<EntityTableViewModel> model)
        {
			Model = model;

			var refereeModel = new RefereeModel()
			{

			};

            await refereeInteractor.AddEntity(Url, "Error during adding referee!", refereeModel);

			Model.Clear();
        }

        public async override Task SetDataFromServer()
        {
            await refereeInteractor.GetEntityById(Url,"Error during getting referee", "1");
        }
    }
}
