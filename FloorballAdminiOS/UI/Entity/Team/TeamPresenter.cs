﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Helper;
using FloorballAdminiOS.Interactor.Entity;
using FloorballPCL;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Team
{
    public class TeamPresenter : EntityPresenter<EntityScreen>
    {
		TeamInteractor teamInteractor;

        TeamModel team;

        List<StadiumModel> stadiums;
        List<LeagueModel> leagues;
        List<PlayerModel> players;

        List<PlayerModel> teamPlayers;

        public TeamPresenter(ITextManager textManager) : base(textManager)
        {
        }

        public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			teamInteractor = new TeamInteractor();
            Url = "/api/floorball/teams/{id}";
		}

        public override void ClearModel()
        {
            team = null;
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

        public override List<List<EntityTableViewModel>> SetTableViewModel()
        {
			
            var years = UIHelper.GetNumbers(2012, 2018);
			var genders = iOSHelper.GetGenders();
			var genderEnums = UIHelper.GetGenderEnums();
			var countries = iOSHelper.GetCountries();
			var countriesEnum = UIHelper.GetCountriesEnum();

            Model.Add( new List<EntityTableViewModel>());

            Model.Last().Add(new EntityTableViewModel { Label = "Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = team == null ? "" : team.Name });
            Model.Last().Add(new EntityTableViewModel { Label = "Gender", CellType = TableViewCellType.SegmenControl, IsVisible = true, Value = new SegmentControlModel(genders, genderEnums) });
            Model.Last().Add(new EntityTableViewModel { Label = "Year", CellType = TableViewCellType.Label, IsVisible = true, Value = team == null ? "" : team.Year.Year.ToString() });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(years, years) });
            Model.Last().Add(new EntityTableViewModel { Label = "League", CellType = TableViewCellType.Label, IsVisible = true, Value = team == null ? "" : leagues.Single(l => l.Id == team.LeagueId).Name });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(leagues.Select(l => l.Name), leagues.Select(l => l.Id)) });
            Model.Last().Add(new EntityTableViewModel { Label = "Coach", CellType = TableViewCellType.TextField, IsVisible = true, Value = team == null ? "" : team.Coach });
            Model.Last().Add(new EntityTableViewModel { Label = "Stadium", CellType = TableViewCellType.Label, IsVisible = true, Value = team == null ? "" : stadiums.Single(s => s.Id == team.StadiumId).Name });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(stadiums.Select(s => s.Name), stadiums.Select(s => s.Id)) });
            Model.Last().Add(new EntityTableViewModel { Label = "Country", CellType = TableViewCellType.Label, IsVisible = true, Value = team == null ? "" : TextManager.GetText(team.Country) });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(countries, countriesEnum) });
            Model.Last().Add(new EntityTableViewModel { Label = "TeamId", CellType = TableViewCellType.TextField, IsVisible = true, Value = team == null ? "" : team.TeamId.ToString() });

            Model.Add(new List<EntityTableViewModel>());
            Model.Last().Add(new EntityTableViewModel{Label = TextManager.GetText("TeamMembers"), Value = new List<List<NavigationModel>>
                {
                    players.Where(p => teamPlayers.Select(p1 => p1.Id).ToList().Contains(p.Id)).Select(p => new NavigationModel{Id = p.Id, Title = p.FirstName + " " + p.LastName, Subtitle = p.BirthDate.ToString("yyyy-MM-dd")}).OrderBy(p => p.Title).ToList(),
                    players.Where(p => !teamPlayers.Select(p1 => p1.Id).ToList().Contains(p.Id)).Select(p => new NavigationModel{Id = p.Id, Title = p.FirstName + " " + p.LastName, Subtitle = p.BirthDate.ToString("yyyy-MM-dd")}).OrderBy(p => p.Title).ToList()
                }});

			return Model;
        }

        protected async override Task Save(UpdateType crud)
		{

            if (crud == UpdateType.Create)
            {
				await teamInteractor.AddEntity(Url, "Error during adding team!", team);
			} 
            else
            {
				await teamInteractor.UpdateEntity(Url, "Error during updating team!", team);
			}

        }

        public async override Task SetDataFromServer(UpdateType crud)
        {
            List<Task> tasks = new List<Task>();

            Task<TeamModel> teamTask = null;
            Task<List<PlayerModel>> playersTask = null;
            Task<List<PlayerModel>> teamPlayersTask = null;

            if (crud == UpdateType.Update)
            {
                teamTask = teamInteractor.GetEntityById(Url, "Error during getting team", EntityId);    
                tasks.Add(teamTask);

				playersTask = teamInteractor.GetEntities<PlayerModel>("api/floorball/players", "Error during getting players");
				tasks.Add(playersTask);

                teamPlayersTask = teamInteractor.GetNavEntities<PlayerModel>("api/floorball/teams/{id}/players", "Error during getting players to team", EntityId);
				tasks.Add(teamPlayersTask);
            }

			Task<List<StadiumModel>> stadiumsTask = teamInteractor.GetEntities<StadiumModel>("api/floorball/stadiums", "Error during getting stadiums");
			tasks.Add(stadiumsTask);

			Task<List<LeagueModel>> leaguesTask = teamInteractor.GetEntities<LeagueModel>("api/floorball/leagues", "Error during getting leagues");
			tasks.Add(leaguesTask);


            await Task.WhenAll(tasks);

            if (crud == UpdateType.Update)
            {
                team = teamTask.Result;     
                players = playersTask.Result;
                teamPlayers = teamPlayersTask.Result;
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

        public override string GetNavigationTextSelected(int rowNumber)
        {
            return rowNumber == 0 ? TextManager.GetText("SelectedPlayers") : "";
        }

        public override string GetNavigationTextNonSelected(int rowNumber)
        {
            return rowNumber == 0 ? TextManager.GetText("NonSelectedPlayers") : "";
        }

    }
}
