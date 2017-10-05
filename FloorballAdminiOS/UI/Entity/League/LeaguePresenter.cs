using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Helper;
using FloorballAdminiOS.Interactor.Entity;
using FloorballPCL.Exceptions;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.League
{
    public class LeaguePresenter : EntityPresenter<EntityScreen>
    {

        LeagueInteractor leagueInteractor;

        LeagueModel leagueModel;

        public override void AttachScreen(EntityScreen screen)
        {
            base.AttachScreen(screen);

            leagueInteractor = new LeagueInteractor();
            Url = "/api/floorball/leagues";
        }

        public override void DetachScreen()
        {
            base.DetachScreen();
        }

		public override List<EntityTableViewModel> SetTableViewModel()
		{

			var years = UIHelper.GetNumbers(2012, 2018);
			var rounds = UIHelper.GetNumbers(1, 30);
			var countries = iOSHelper.GetCountries();
			var countriesEnum = UIHelper.GetCountriesEnum();
			var leagueTypes = iOSHelper.GetLeagueTypes();
			var leagueTypesEnum = UIHelper.GetLeagueTypeEnums();
			var classes = iOSHelper.GetClasses();
			var classesEnum = UIHelper.GetClassEnums();
			var genders = iOSHelper.GetGenders();
			var genderEnums = UIHelper.GetGenderEnums();

			Model.Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = leagueModel == null ? "" : leagueModel.Name });
			Model.Add(new EntityTableViewModel { Label = "Year", CellType = TableViewCellType.Label, IsVisible = true, Value = leagueModel == null ? "" : leagueModel.Year.Year.ToString() });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(years, years) });
			Model.Add(new EntityTableViewModel { Label = "Country", CellType = TableViewCellType.Label, IsVisible = true, Value = leagueModel == null ? "" : leagueModel.Country.ToFriendlyString() });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(countries, countriesEnum) });
			Model.Add(new EntityTableViewModel { Label = "Type", CellType = TableViewCellType.Label, IsVisible = true, Value = leagueModel == null ? "" : leagueModel.type });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(leagueTypes, leagueTypesEnum) });
			Model.Add(new EntityTableViewModel { Label = "Class", CellType = TableViewCellType.Label, IsVisible = true, Value = leagueModel == null ? "" : leagueModel.ClassName });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(classes, classesEnum) });
			Model.Add(new EntityTableViewModel { Label = "Rounds", CellType = TableViewCellType.Label, IsVisible = true, Value = leagueModel == null ? "" : leagueModel.Rounds.ToString() });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(rounds, rounds) });
			Model.Add(new EntityTableViewModel { Label = "Gender", CellType = TableViewCellType.SegmenControl, IsVisible = true, Value = new SegmentControlModel(genders, genderEnums) });

			
			return Model;

		}

		public override async Task SetDataFromServer(UpdateType crud)
		{
            
			if (crud == UpdateType.Update)
			{
				leagueModel = await leagueInteractor.GetEntityById(Url, "Error during getting league", "1");
			}
        
            SetTableViewModel();

		}

		public override string GetTableHeader(UpdateType crud)
		{
			return crud == UpdateType.Create ? "Add League" : "Update League";
		}

		protected override async Task Save()
		{

			await leagueInteractor.AddEntity(Url, "Error during adding league!", leagueModel);

			leagueModel = null;

		}

        protected override void Validate()
        {
			leagueModel = new LeagueModel()
			{
				Name = Model[0].ValueAsString,
				Year = Model[2].PickerValueAsDateTime,
				Country = Model[4].PickerValueAsCountriesEnum,
				type = Model[6].PickerValueAsString,
				ClassName = Model[8].PickerValueAsString,
				Rounds = Model[10].PickerValueAsInt,
				Sex = Model[12].SegmentModelSelectedValue.ToString()
			};

			if (leagueModel.Name == "")
			{
				throw new ValidationException("Validation error", "Name cannot be empty", null);
			}
        }

        public override void ClearModel()
        {

			Model.Clear();

        }
    }
}
