using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.UI.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Delegate
{
    public class StadiumDelegate : BaseDelegate, IDelegate
    {
        public EntityPresenter<StadiumModel> EntityPresenter { get; set; }

        public string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Stadium" : "Update Stadium";
        }

        public List<EntityTableViewModel> GetTableViewModel()
        {

            if (Model.Count == 0) 
            {
				Model.Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { Label = "Address", CellType = TableViewCellType.TextField, IsVisible = true, Value = "" });    
            }

			return Model;
        }

        public async Task Save(List<EntityTableViewModel> model)
        {
			Model = model;

			EntityPresenter.Model = new StadiumModel()
			{

			};

			await EntityPresenter.AddEntity("Error during adding stadium!");

            Model.Clear();
        }

        public async Task SetDataFromServer()
        {
            await EntityPresenter.GetEntity("Error during getting stadium", "1");
        }
    }
}
