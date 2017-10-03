using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Helper;
using FloorballAdminiOS.UI.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Delegate
{
    public class LeagueDelegate : BaseDelegate, IDelegate
    {
        
        public EntityPresenter<LeagueModel> EntityPresenter { get; set; }

        public List<EntityTableViewModel> GetTableViewModel()
        {

			Model.Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { Label = "Year", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(UIHelper.GetNumbers(2012, 2018)) });
			Model.Add(new EntityTableViewModel { Label = "Country", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
			Model.Add(new EntityTableViewModel { Label = "Type", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
			Model.Add(new EntityTableViewModel { Label = "Class", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
			Model.Add(new EntityTableViewModel { Label = "Round", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
			Model.Add(new EntityTableViewModel { Label = "Gendre", CellType = TableViewCellType.SegmenControl, IsVisible = true, Model = new SegmentControlModel { Segments = new List<Tuple<string, string>> { new Tuple<string, string>("men", "men"), new Tuple<string, string>("women", "women") } } });

            return Model;

		}

        public async Task SetDataFromServer()
        {
            await EntityPresenter.GetEntity("Error during getting league", "1");
        }

        public string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add League" : "Update League";
        }

        public async Task Save(List<EntityTableViewModel> model)
        {
            Model = model;

			EntityPresenter.Model = new LeagueModel()
			{
				Name = Model[0].ModelAsString,
				Year = Model[1].ModelAsDateTime,
				Country = Model[3].ModelAsCountriesEnum,
				type = Model[5].ModelAsString,
				ClassName = Model[7].ModelAsString,
				Rounds = Model[9].ModelAsInt,
				Sex = Model[11].ModelAsString
			};

			await EntityPresenter.AddEntity("Error during adding league!");
        }
    }
}
