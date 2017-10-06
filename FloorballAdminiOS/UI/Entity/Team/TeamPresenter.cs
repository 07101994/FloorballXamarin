using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Helper;
using FloorballAdminiOS.Interactor.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Team
{
    public class TeamPresenter : EntityPresenter<EntityScreen>
    {
		TeamInteractor teamInteractor;

        TeamModel team;

        List<StadiumModel> stadiums;
        List<LeagueModel> leagues;

		public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			teamInteractor = new TeamInteractor();
			Url = "/api/floorball/teams";
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
            return crud == UpdateType.Create ? "Add Team" : "Update Team";
        }

        public override List<EntityTableViewModel> SetTableViewModel()
        {
			
            var years = UIHelper.GetNumbers(2012, 2018);
			var genders = iOSHelper.GetGenders();
			var genderEnums = UIHelper.GetGenderEnums();
			var countries = iOSHelper.GetCountries();
			var countriesEnum = UIHelper.GetCountriesEnum();

            Model.Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = team == null ? "" : team.Name });
            Model.Add(new EntityTableViewModel { Label = "Gender", CellType = TableViewCellType.SegmenControl, IsVisible = true, Value = new SegmentControlModel(genders, genderEnums) });
            Model.Add(new EntityTableViewModel { Label = "Year", CellType = TableViewCellType.Label, IsVisible = true, Value = team == null ? "" : team.Year.Year.ToString() });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(years, years) });
            Model.Add(new EntityTableViewModel { Label = "League", CellType = TableViewCellType.Label, IsVisible = true, Value = team == null ? "" : leagues.Single(l => l.Id == team.LeagueId).Name });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(leagues.Select(l => l.Name), leagues.Select(l => l.Id)) });
            Model.Add(new EntityTableViewModel { Label = "Coach", CellType = TableViewCellType.TextField, IsVisible = true, Value = team == null ? "" : team.Coach });
            Model.Add(new EntityTableViewModel { Label = "Stadium", CellType = TableViewCellType.Label, IsVisible = true, Value = team == null ? "" : stadiums.Single(s => s.Id == team.StadiumId).Name });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(stadiums.Select(s => s.Name), stadiums.Select(s => s.Id)) });
            Model.Add(new EntityTableViewModel { Label = "Country", CellType = TableViewCellType.Label, IsVisible = true, Value = team == null ? "" : team.Country.ToFriendlyString() });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(countries, countriesEnum) });
            Model.Add(new EntityTableViewModel { Label = "TeamId", CellType = TableViewCellType.TextField, IsVisible = true, Value = team == null ? "" : team.TeamId.ToString() });
		


			return Model;
        }

        protected async override Task Save()
		{
			team = new TeamModel()
			{

			};

            await teamInteractor.AddEntity(Url,"Error during adding team!", team);

			Model.Clear();
        }

        public async override Task SetDataFromServer(UpdateType crud)
        {
            List<Task> tasks = new List<Task>();

            Task<TeamModel> teamTask = null;

            if (crud == UpdateType.Update)
            {
                teamTask = teamInteractor.GetEntityById(Url, "Error during getting team", "1");    
                tasks.Add(teamTask);
            }

			Task<List<StadiumModel>> stadiumsTask = teamInteractor.GetEntities<StadiumModel>("api/floorball/stadiums", "Error during getting stadiums");
			tasks.Add(stadiumsTask);

			Task<List<LeagueModel>> leaguesTask = teamInteractor.GetEntities<LeagueModel>("api/floorball/leagues", "Error during getting leagues");
			tasks.Add(leaguesTask);

            await Task.WhenAll(tasks);

            if (crud == UpdateType.Update)
            {
                team = teamTask.Result;     
            }

            stadiums = stadiumsTask.Result;
            leagues = leaguesTask.Result;

            SetTableViewModel();
        }

        protected override void Validate()
        {
			team = new TeamModel()
			{

			};
        }
    }
}
