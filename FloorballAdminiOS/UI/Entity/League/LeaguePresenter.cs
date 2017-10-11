using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Helper;
using FloorballAdminiOS.Interactor.Entity;
using FloorballPCL;
using FloorballPCL.Exceptions;
using FloorballServer.Models.Floorball;
using Foundation;

namespace FloorballAdminiOS.UI.Entity.League
{
    public class LeaguePresenter : EntityPresenter<EntityScreen>
    {

        LeagueInteractor leagueInteractor;

        LeagueModel leagueModel;

        public LeaguePresenter(ITextManager textManager) : base(textManager)
        {
        }

        public override void AttachScreen(EntityScreen screen)
        {
            base.AttachScreen(screen);

            leagueInteractor = new LeagueInteractor();
            Url = "/api/floorball/leagues/{id}";
        }

        public override void DetachScreen()
        {
            base.DetachScreen();
        }

		public override List<List<EntityTableViewModel>> SetTableViewModel()
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

            Model.Add(new List<EntityTableViewModel>());

			Model.Last().Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = leagueModel == null ? "" : leagueModel.Name });
			Model.Last().Add(new EntityTableViewModel { Label = "Year", CellType = TableViewCellType.Label, IsVisible = true, Value = leagueModel == null ? "" : leagueModel.Year.Year.ToString() });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(years, years) });
			Model.Last().Add(new EntityTableViewModel { Label = "Country", CellType = TableViewCellType.Label, IsVisible = true, Value = leagueModel == null ? "" : TextManager.GetText(leagueModel.Country)});
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(countries, countriesEnum) });
            Model.Last().Add(new EntityTableViewModel { Label = "Type", CellType = TableViewCellType.Label, IsVisible = true, Value = leagueModel == null ? "" : TextManager.GetText(leagueModel.Type) });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(leagueTypes, leagueTypesEnum) });
            Model.Last().Add(new EntityTableViewModel { Label = "Class", CellType = TableViewCellType.Label, IsVisible = true, Value = leagueModel == null ? "" :  TextManager.GetText(leagueModel.Class) });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(classes, classesEnum) });
			Model.Last().Add(new EntityTableViewModel { Label = "Rounds", CellType = TableViewCellType.Label, IsVisible = true, Value = leagueModel == null ? "" : leagueModel.Rounds.ToString() });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(rounds, rounds) });
			Model.Last().Add(new EntityTableViewModel { Label = "Gender", CellType = TableViewCellType.SegmenControl, IsVisible = true, Value = new SegmentControlModel(genders, genderEnums) });

			
			return Model;

		}

		public override async Task SetDataFromServer(UpdateType crud)
		{
            
			if (crud == UpdateType.Update)
			{
				leagueModel = await leagueInteractor.GetEntityById(Url, "Error during getting league", EntityId);
			}
        
            SetTableViewModel();

		}

		public override string GetTableHeader(UpdateType crud)
		{
			return crud == UpdateType.Create ? "Add League" : "Update League";
		}

		protected override async Task Save(UpdateType crud)
		{

            if (crud == UpdateType.Create)
            {
				await leagueInteractor.AddEntity(Url, "Error during adding league!", leagueModel);
			} 
            else
            {
                await leagueInteractor.UpdateEntity(Url, "Error during updating league!", leagueModel);
            }

		}

        protected override void Validate()
        {
			leagueModel = new LeagueModel()
			{
				Name = Model[0][0].ValueAsString,
				Year = Model[0][2].PickerValueAsDateTime,
				Country = Model[0][4].GetPickerValuesAsEnum<CountriesEnum>(),
				Type = Model[0][6].GetPickerValuesAsEnum<LeagueTypeEnum>(),
				Class = Model[0][8].GetPickerValuesAsEnum<ClassEnum>(),
				Rounds = Model[0][10].PickerValueAsShort,
				Gender = Model[0][12].SegmentModelSelectedValue.ToString().ToEnum<GenderEnum>()
			};

			if (leagueModel.Name == "")
			{
				throw new ValidationException("Validation error", "Name cannot be empty", null);
			}
        }

        public override void ClearModel()
        {
            leagueModel = null;
			Model.Clear();

        }
    }
}
