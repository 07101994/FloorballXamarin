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
            if (Model.Count == 0) 
            {
				Model.Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { Label = "Year", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(UIHelper.GetNumbers(2012, 2018)) });
				Model.Add(new EntityTableViewModel { Label = "Country", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
				Model.Add(new EntityTableViewModel { Label = "Type", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
				Model.Add(new EntityTableViewModel { Label = "Class", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
				Model.Add(new EntityTableViewModel { Label = "Round", CellType = TableViewCellType.Label, IsVisible = true, Value = "" });
				Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(iOSHelper.GetCountries()) });
				Model.Add(new EntityTableViewModel { Label = "Gendre", CellType = TableViewCellType.SegmenControl, IsVisible = true, Value = new SegmentControlModel { Segments = new List<Tuple<string, string>> { new Tuple<string, string>("men", "men"), new Tuple<string, string>("women", "women") } } });

			}

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
				Name = Model[0].ValueAsString,
				Year = Model[2].PickerValueAsDateTime,
				Country = Model[4].PickerValueAsCountriesEnum,
				type = Model[6].PickerValueAsString,
				ClassName = Model[8].PickerValueAsString,
				Rounds = Model[10].PickerValueAsInt,
				Sex = Model[12].SegmentModelSelectedValue
			};

			await EntityPresenter.AddEntity("Error during adding league!");

            Model.Clear();
        }
    }
}
