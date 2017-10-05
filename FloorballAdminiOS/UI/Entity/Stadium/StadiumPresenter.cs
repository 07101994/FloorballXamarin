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

        StadiumModel stadium;

		public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			stadiumInteractor = new StadiumInteractor();
			Url = "/api/floorball/stadiums";
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
            return crud == UpdateType.Create ? "Add Stadium" : "Update Stadium";
        }

        public override List<EntityTableViewModel> SetTableViewModel()
        {
            Model.Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = stadium == null ? "" : stadium.Name });
            Model.Add(new EntityTableViewModel { Label = "Address", CellType = TableViewCellType.TextField, IsVisible = true, Value = stadium == null ? "" : stadium.Address });

			return Model;
        }

        protected async override Task Save()
        {

            await stadiumInteractor.AddEntity(Url, "Error during adding stadium!", stadium);

			Model.Clear();
        }

        public async override Task SetDataFromServer(UpdateType crud)
        {

            if (crud == UpdateType.Update)
            {
                stadium = await stadiumInteractor.GetEntityById(Url, "Error during getting stadium", "1");    
            }

            await Task.FromResult<object>(null);

            SetTableViewModel();
        }

        protected override void Validate()
        {
			stadium = new StadiumModel()
			{

			};
        }
    }
}
