using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Helper;
using FloorballAdminiOS.Interactor.Entity;
using FloorballPCL;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Match
{
    public class MatchPresenter : EntityPresenter<EntityScreen>
    {
        MatchInteractor matchInteractor;

        MatchModel match;

        List<TeamModel> teams;
        List<LeagueModel> leagues;
        List<StadiumModel> stadiums;

        List<PlayerModel> homeTeamPlayers;
        List<PlayerModel> awayTeamPlayers;
        List<RefereeModel> referees;

        List<RefereeModel> matchReferees;
        List<PlayerModel> matchPlayers;

        public MatchPresenter(ITextManager textManager) : base(textManager)
        {
        }

        public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			matchInteractor = new MatchInteractor();
            Url = "/api/floorball/matches/{id}";
		}

        public override void ClearModel()
        {
            match = null;
            Model.Clear();
        }

        public override void DetachScreen()
		{
			base.DetachScreen();
		}

        public override string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Match" : "Update Match";
        }

        public override List<List<EntityTableViewModel>> SetTableViewModel()
        {
			
            var rounds = UIHelper.GetNumbers(1, 30);

            Model.Add(new List<EntityTableViewModel>());
    
            Model.Last().Add(new EntityTableViewModel { Label = "League", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : leagues.Single(l => l.Id == match.Id).Name });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(leagues.Select(l => l.Name), leagues.Select(l => l.Id)) });
            Model.Last().Add(new EntityTableViewModel { Label = "Home Team", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : teams.Single(t => t.Id == match.HomeTeamId).Name });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(teams.Select(t => t.Name), teams.Select(t => t.Id)) });
			Model.Last().Add(new EntityTableViewModel { Label = "Away Team", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : teams.Single(t => t.Id == match.AwayTeamId).Name });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(teams.Select(t => t.Name), teams.Select(t => t.Id)) });
            Model.Last().Add(new EntityTableViewModel { Label = "Round", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : match.Round.ToString() });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(rounds, rounds) });
            Model.Last().Add(new EntityTableViewModel { Label = "Date", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : match.Date.ToString() });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.DatePicker, IsVisible = false, Value = "" });
            Model.Last().Add(new EntityTableViewModel { Label = "Time", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : match.Time.ToString() });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.TimePicker, IsVisible = false, Value = "" });
            Model.Last().Add(new EntityTableViewModel { Label = "Stadium", CellType = TableViewCellType.Label, IsVisible = true, Value = match == null ? "" : stadiums.Single(s => s.Id == match.StadiumId).Name });
			Model.Last().Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Value = new UIFloorballPickerViewModel(stadiums.Select(s => s.Name), stadiums.Select(s => s.Id)) });

            Model.Add(new List<EntityTableViewModel>());

			Model.Last().Add(new EntityTableViewModel
            {
                Label = TextManager.GetText("HomeTeamPlayers"),
                Value = new List<List<NavigationModel>>
                {
                    homeTeamPlayers.Where(p => matchPlayers.Select(p1 => p1.Id).ToList().Contains(p.Id)).Select(p => new NavigationModel{Id = p.Id, Title = p.FirstName + " " + p.LastName, Subtitle = p.BirthDate.ToString("yyyy-MM-dd")}).OrderBy(p => p.Title).ToList(),
                    homeTeamPlayers.Where(p => !matchPlayers.Select(p1 => p1.Id).ToList().Contains(p.Id)).Select(p => new NavigationModel{Id = p.Id, Title = p.FirstName + " " + p.LastName, Subtitle = p.BirthDate.ToString("yyyy-MM-dd")}).OrderBy(p => p.Title).ToList()
                }
            });
			Model.Last().Add(new EntityTableViewModel
			{
				Label = TextManager.GetText("AwayTeamPlayers"),
				Value = new List<List<NavigationModel>>
				{
					awayTeamPlayers.Where(p => matchPlayers.Select(p1 => p1.Id).ToList().Contains(p.Id)).Select(p => new NavigationModel{Id = p.Id, Title = p.FirstName + " " + p.LastName, Subtitle = p.BirthDate.ToString("yyyy-MM-dd")}).OrderBy(p => p.Title).ToList(),
					awayTeamPlayers.Where(p => !matchPlayers.Select(p1 => p1.Id).ToList().Contains(p.Id)).Select(p => new NavigationModel{Id = p.Id, Title = p.FirstName + " " + p.LastName, Subtitle = p.BirthDate.ToString("yyyy-MM-dd")}).OrderBy(p => p.Title).ToList()
				}
			});
			Model.Last().Add(new EntityTableViewModel
			{
				Label = TextManager.GetText("MatchReferees"),
				Value = new List<List<NavigationModel>>
				{
                    referees.Where(r => matchReferees.Select(r1 => r1.Id).ToList().Contains(r.Id)).Select(r => new NavigationModel{Id = r.Id, Title = r.Name, Subtitle = TextManager.GetText(r.Country)}).OrderBy(r => r.Title).ToList(),
					referees.Where(r => !matchReferees.Select(r1 => r1.Id).ToList().Contains(r.Id)).Select(r => new NavigationModel{Id = r.Id, Title = r.Name, Subtitle = TextManager.GetText(r.Country)}).OrderBy(r => r.Title).ToList(),
				}
			});

			return Model;
        }

        protected async override Task Save(UpdateType crud)
        {
            if (crud == UpdateType.Create)
            {
				await matchInteractor.AddEntity(Url, "Error during adding match!", match);
			}
            else
            {
				await matchInteractor.UpdateEntity(Url, "Error during updating match!", match);
			}
			
        }

        public async override Task SetDataFromServer(UpdateType crud)
        {
            var tasks = new List<Task>();

            Task<MatchModel> matchTask = null;

            Task<List<PlayerModel>> homeTeamPlayersTask = null;
            Task<List<PlayerModel>> awayTeamPlayersTask = null;
            Task<List<RefereeModel>> refereesTask = null;

            Task<List<PlayerModel>> matchPlayersTask = null;
            Task<List<RefereeModel>> matchRefereesTask = null;

            if (crud == UpdateType.Update)
            {
				matchTask = matchInteractor.GetEntityById(Url, "Error during getting match", EntityId);
				tasks.Add(matchTask);

                homeTeamPlayersTask = matchInteractor.GetNavEntities<PlayerModel>("api/floorball/teams/{id}/players", "Error during getting home players",1);
                tasks.Add(homeTeamPlayersTask);

                awayTeamPlayersTask = matchInteractor.GetNavEntities<PlayerModel>("api/floorball/teams/{id}/players", "Error during getting away players",2);
				tasks.Add(homeTeamPlayersTask);

                refereesTask = matchInteractor.GetEntities<RefereeModel>("api/floorball/referees", "Error during getting referees");
                tasks.Add(refereesTask);

                matchPlayersTask = matchInteractor.GetNavEntities<PlayerModel>("api/floorball/matches/{id}/players", "Error getting players for match", EntityId);
                tasks.Add(matchPlayersTask);

                matchRefereesTask = matchInteractor.GetNavEntities<RefereeModel>("api/floorball/matches/{id}/referees", "Error getting referees for match", EntityId);
                tasks.Add(matchRefereesTask);
			}

			Task<List<LeagueModel>> leaguesTask = matchInteractor.GetEntities<LeagueModel>("api/floorball/leagues", "Error during getting leagues");
			tasks.Add(leaguesTask);

            Task<List<TeamModel>> teamsTask = matchInteractor.GetEntities<TeamModel>("api/floorball/teams","Error during getting teams");
            tasks.Add(teamsTask);

			Task<List<StadiumModel>> stadiumsTask = matchInteractor.GetEntities<StadiumModel>("api/floorball/stadiums", "Error during getting stadiums");
			tasks.Add(stadiumsTask);

            await Task.WhenAll(tasks);

            if (crud == UpdateType.Update)
            {
                match = matchTask.Result;
                homeTeamPlayers = homeTeamPlayersTask.Result;
                awayTeamPlayers = awayTeamPlayersTask.Result;
                referees = refereesTask.Result;
                matchPlayers = matchPlayersTask.Result;
                matchReferees = matchRefereesTask.Result;
            }

            leagues = leaguesTask.Result;
            teams = teamsTask.Result;
            stadiums = stadiumsTask.Result;

            SetTableViewModel();

        }

        protected override void Validate()
        {
			match = new MatchModel()
			{
				LeagueId = Model[0][1].PickerValueAsInt,
				HomeTeamId = Model[0][3].PickerValueAsInt,
				AwayTeamId = Model[0][5].PickerValueAsInt,
				Round = Model[0][7].PickerValueAsShort,
				Date = Model[0][9].PickerValueAsDateTime,
				Time = Model[0][11].PickerValueAsTimeSpan,
				StadiumId = Model[0][13].PickerValueAsInt,
				State = StateEnum.Confirmed,
				ScoreH = 0,
				ScoreA = 0
			};
        }

		public override string GetNavigationTextSelected(int rowNumber)
		{
            if (rowNumber == 0) 
            {
                return TextManager.GetText("SelectedHomePlayers");
            } else if (rowNumber == 1)
            {
                return TextManager.GetText("SelectedAwayPlayers");
            } 

            return TextManager.GetText("SelectedReferees");
		}

		public override string GetNavigationTextNonSelected(int rowNumber)
		{
			if (rowNumber == 0)
			{
				return TextManager.GetText("NonSelectedHomePlayers");
			}
			else if (rowNumber == 1)
			{
				return TextManager.GetText("NonSelectedAwayPlayers");
			}

			return TextManager.GetText("NonSelectedReferees");
		}
    }
}
