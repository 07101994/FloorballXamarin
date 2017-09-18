using Floorball.LocalDB.Repository;
using Floorball.LocalDB.Tables;
using Floorball.REST;
using FloorballServer.Models.Floorball;
using SQLite;
using SQLite.Net;
using SQLite.Net.Interop;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Floorball.REST.RESTHelpers;
using Floorball.Exceptions;

namespace Floorball.LocalDB
{
    public class Manager
    {

        public bool IsInit { get; set; }

        private static Manager Current { get; set; }

        private ISQLitePlatform Platform { get; set; }
        private string DatabasePath { get; set; }

        public static Manager Instance
        {
            get 
            {
				if (Current == null)
				{
                    Current = new Manager();
				}

				return Current;
            }
            
        }

        protected Manager()
        {
            Platform = UnitOfWork.DBManager.GetPlatform();
            DatabasePath = UnitOfWork.DBManager.GetDatabasePath();
        }

        private void CreateDatabase()
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                DropTables(db);

                CreateTables(db);
            }

        }

        private void CreateTables(SQLiteConnection db)
        {
            db.CreateTable<PlayerTeam>();
            db.CreateTable<PlayerMatch>();
            db.CreateTable<RefereeMatch>();
            db.CreateTable<EventMessage>();
            db.CreateTable<League>();
            db.CreateTable<Match>();
            db.CreateTable<Player>();
            db.CreateTable<Referee>();
            db.CreateTable<Stadium>();
            db.CreateTable<Team>();
            db.CreateTable<Statistic>();
            db.CreateTable<Event>();
        }

        private async Task CreateDatabaseAsync()
        {
            await Task.Run(() => CreateDatabase());
        }

        public async Task<DateTime> InitLocalDatabase()
        {
            //await CreateDatabaseAsync();
            CreateDatabase();
            await InitDatabaseFromServerAsync();
            return DateTime.Now;
        }

        private void DropTables(SQLiteConnection db)
        {
            db.DropTable<PlayerTeam>();
            db.DropTable<PlayerMatch>();
            db.DropTable<RefereeMatch>();
            db.DropTable<EventMessage>();
            db.DropTable<League>();
            db.DropTable<Match>();
            db.DropTable<Player>();
            db.DropTable<Referee>();
            db.DropTable<Stadium>();
            db.DropTable<Team>();
            db.DropTable<Statistic>();
            db.DropTable<Event>();
        }

        public async Task InitDatabaseFromServerAsync()
        {
            Task<List<EventMessageModel>> eventMessagesTask;
            Task<List<LeagueModel>> leaguesTask;
            Task<List<RefereeModel>> refereesTask;
            Task<List<PlayerModel>> playersTask;
            Task<List<StadiumModel>> stadiumsTask;
            Task<List<TeamModel>> teamsTask;
            Task<List<MatchModel>> matchesTask;
            Task<Dictionary<int, List<int>>> playersAndTeamsTask;
            Task<Dictionary<int, List<int>>> playersAndMatchesTask;
            Task<Dictionary<int, List<int>>> refereesAndMatchesTask;
            Task<List<EventModel>> eventsTask;

			try
            {
				List<Task> tasks = new List<Task>();

				eventMessagesTask = RESTHelper.GetEventMessagesAsync();
				tasks.Add(eventMessagesTask);
				leaguesTask = RESTHelper.GetLeaguesAsync();
				tasks.Add(leaguesTask);
				refereesTask = RESTHelper.GetRefereesAsync();
				tasks.Add(refereesTask);
				playersTask = RESTHelper.GetPlayersAsync();
				tasks.Add(playersTask);
				stadiumsTask = RESTHelper.GetStadiumsAsync();
				tasks.Add(stadiumsTask);
				teamsTask = RESTHelper.GetTeamsAsync(true);
				tasks.Add(teamsTask);
				matchesTask = RESTHelper.GetMatchesAsync();
				tasks.Add(matchesTask);
				playersAndTeamsTask = RESTHelper.GetPlayersAndTeamsAsync();
				tasks.Add(playersAndTeamsTask);
				playersAndMatchesTask = RESTHelper.GetPlayersAndMatchesAsync();
				tasks.Add(playersAndMatchesTask);
				refereesAndMatchesTask = RESTHelper.GetRefereesAndMatchesAsync();
				tasks.Add(refereesAndMatchesTask);
				eventsTask = RESTHelper.GetEventsAsync();
				tasks.Add(eventsTask);

				await Task.WhenAll(tasks);

			}
            catch (Exception ex)
            {
                throw new CommunicationException("Error during getting data from server!", ex);
            }


            Database db = new Database {
                EventMessages = eventMessagesTask.Result,
                Leagues = leaguesTask.Result,
                Referees = refereesTask.Result,
                Players = playersTask.Result,
                Stadiums = stadiumsTask.Result,
                Teams = teamsTask.Result,
                Matches = matchesTask.Result,
                PlayersAndTeams = playersAndTeamsTask.Result,
                PlayersAndMatches = playersAndMatchesTask.Result,
                RefereesAndMatches = refereesAndMatchesTask.Result,
                Events = eventsTask.Result
            };

            try
            {
                await AddTablesAsync(db);
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Error during database init!", ex);
            }

        }

        private async Task AddTablesAsync(Database db)
        {
            await Task.Run(() => AddTables(db));
        }

        private void AddTables(Database db)
        {
            UnitOfWork UoW = new UnitOfWork();

            IsInit = true;
            UoW.EventMessageRepo.AddEventMessages(db.EventMessages);
            UoW.LeagueRepo.AddLeagues(db.Leagues);
            UoW.RefereeRepo.AddReferees(db.Referees);
            UoW.PlayerRepo.AddPlayers(db.Players);
            UoW.StadiumRepo.AddStadiums(db.Stadiums);
            UoW.TeamRepo.AddTeams(db.Teams);
            UoW.MatchRepo.AddMatches(db.Matches);
            UoW.PlayerRepo.AddPlayersAndTeams(db.PlayersAndTeams);
            UoW.PlayerRepo.AddPlayersAndMatches(db.PlayersAndMatches);
            UoW.RefereeRepo.AddRefereesAndMatches(db.RefereesAndMatches);
            UoW.EventRepo.AddEvents(db.Events);
            IsInit = false;
        }

    }
}
