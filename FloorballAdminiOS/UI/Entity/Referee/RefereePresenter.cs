using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Interactor.Entity;
using FloorballPCL;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Referee
{
    public class RefereePresenter : EntityPresenter<EntityScreen>
    {
        RefereeInteractor refereeInteractor;

        RefereeModel referee;

        public RefereePresenter(ITextManager textManager) : base(textManager)
        {
        }

        public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			refereeInteractor = new RefereeInteractor();
            Url = "/api/floorball/referees/{id}";
		}

        public override void ClearModel()
        {
            referee = null;
            Model.Clear();
        }

        public override void DetachScreen()
		{
			base.DetachScreen();
		}

        public override string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Referee" : "Update Referee";
        }

        public override List<EntityTableViewModel> SetTableViewModel()
        {
            Model.Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = referee == null ? "" : referee.Name });

			return Model;
        }

        protected async override Task Save(UpdateType crud)
        {
            if (crud == UpdateType.Create)
            {
				await refereeInteractor.AddEntity(Url, "Error during adding referee!", referee);
			}
            else
            {
				await refereeInteractor.UpdateEntity(Url, "Error during updating referee!", referee);
			}

        }

        public async override Task SetDataFromServer(UpdateType crud)
        {
            if (crud == UpdateType.Update)
            {
                referee = await refereeInteractor.GetEntityById(Url, "Error during getting referee", EntityId);    
            }

            await Task.FromResult<object>(null);

            SetTableViewModel();
        }

        protected override void Validate()
        {
			referee = new RefereeModel()
			{

			};
        }
    }
}
