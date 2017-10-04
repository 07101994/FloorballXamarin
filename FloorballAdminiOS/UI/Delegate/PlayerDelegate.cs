using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.UI.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Delegate
{
    public class PlayerDelegate : BaseDelegate, IDelegate
    {
        public EntityPresenter<PlayerModel> EntityPresenter { get; set; }

        public string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Player" : "Update Player";
        }

        public List<EntityTableViewModel> GetTableViewModel()
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

        public async Task Save(List<EntityTableViewModel> model)
        {
			Model = model;

			EntityPresenter.Model = new PlayerModel()
			{

			};

			await EntityPresenter.AddEntity("Error during adding player!");

            Model.Clear();
        }

        public async Task SetDataFromServer()
        {
            await EntityPresenter.GetEntity("Error during getting player", "1");
        }
    }
}
