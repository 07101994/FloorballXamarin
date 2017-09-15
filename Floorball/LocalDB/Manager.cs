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
using Floorball.REST.RequestModels;
using Floorball.REST.RESTHelpers;

namespace Floorball.LocalDB
{
    public class Manager
    {

        public bool IsInit { get; set; }
        private IRESTManager Network;

        private static Manager Current { get; set; }

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
            Network = new RESTManager();
        }

        private ISQLitePlatform Platform
        {
            get
            {
#if __IOS__

                return new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();

#else

#if __ANDROID__

                return new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
#endif
#endif
            }
        }

        private string DatabasePath
        {
            get
            {
                var sqliteFilename = "Floorball.db3";

#if __IOS__

                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
                var path = Path.Combine(libraryPath, sqliteFilename);
                
#else

#if __ANDROID__
        
                string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
        
                var path = Path.Combine(documentsPath, sqliteFilename);

#else
        
                // WinPhone
        
                var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);

#endif
#endif
                return path;
            }
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
            List<Task> tasks = new List<Task>();

            Task<List<EventMessageModel>> eventMessagesTask = RESTHelper.GetEventMessagesAsync();
            tasks.Add(eventMessagesTask);
            Task<List<LeagueModel>> leaguesTask = RESTHelper.GetLeaguesAsync();
            tasks.Add(leaguesTask);
            Task<List<RefereeModel>> refereesTask = RESTHelper.GetRefereesAsync();
            tasks.Add(refereesTask);
            Task<List<PlayerModel>> playersTask = RESTHelper.GetPlayersAsync();
            tasks.Add(playersTask);
            Task<List<StadiumModel>> stadiumsTask = RESTHelper.GetStadiumsAsync();
            tasks.Add(stadiumsTask);
            Task<List<TeamModel>> teamsTask = RESTHelper.GetTeamsAsync(true);
            tasks.Add(teamsTask);
            Task<List<MatchModel>> matchesTask = RESTHelper.GetMatchesAsync();
            tasks.Add(matchesTask);
            Task<Dictionary<int, List<int>>> playersAndTeamsTask = RESTHelper.GetPlayersAndTeamsAsync();
            tasks.Add(playersAndTeamsTask);
            Task<Dictionary<int, List<int>>> playersAndMatchesTask = RESTHelper.GetPlayersAndMatchesAsync();
            tasks.Add(playersAndMatchesTask);
            Task<Dictionary<int, List<int>>> refereesAndMatchesTask = RESTHelper.GetRefereesAndMatchesAsync();
            tasks.Add(refereesAndMatchesTask);
            Task<List<EventModel>> eventsTask = RESTHelper.GetEventsAsync();
            tasks.Add(eventsTask);

            await Task.WhenAll(tasks);

            Database db = new Database
            {
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

            await AddTablesAsync(db);

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
