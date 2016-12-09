﻿using Floorball.LocalDB.Repository;
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

namespace Floorball.LocalDB
{
    public class Manager
    {

        private static ISQLitePlatform Platform
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

        private static string DatabasePath
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

        private static void CreateDatabase()
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                DropTables(db);

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

        }

        public static async Task<string> InitLocalDatabase()
        {
            CreateDatabase();
            await InitDatabaseFromServerAsync();
            return DateTime.Now.ToString();
        }

        private static void DropTables(SQLiteConnection db)
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

        public async static Task InitDatabaseFromServerAsync()
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
            Task<List<TeamModel>> teamsTask = RESTHelper.GetTeamsAsync();
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

            UnitOfWork UoW = new UnitOfWork();

            UoW.EventMessageRepo.AddEventMessages(eventMessagesTask.Result);
            UoW.LeagueRepo.AddLeagues(leaguesTask.Result);
            UoW.RefereeRepo.AddReferees(refereesTask.Result);
            UoW.PlayerRepo.AddPlayers(playersTask.Result);
            UoW.StadiumRepo.AddStadiums(stadiumsTask.Result);
            UoW.TeamRepo.AddTeams(teamsTask.Result);
            UoW.MatchRepo.AddMatches(matchesTask.Result);
            UoW.PlayerRepo.AddPlayersAndTeams(playersAndTeamsTask.Result);
            UoW.PlayerRepo.AddPlayersAndMatches(playersAndMatchesTask.Result);
            UoW.RefereeRepo.AddRefereesAndMatches(refereesAndMatchesTask.Result);
            UoW.EventRepo.AddEvents(eventsTask.Result);

        }

        #region GET

        //public static IEnumerable<DateTime> GetAllYear()
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<League>().Select(l => l.Year).Distinct().OrderBy(t => t.Year);
        //    }
        //}

        //public static IEnumerable<EventMessage> GetAllEventMessage()
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        return db.GetAllWithChildren<EventMessage>();
        //    }
        //}

        //public static IEnumerable<Statistic> GetAllStatistic()
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Statistic>();
        //    }
        //}


        //public static IEnumerable<League> GetAllLeague()
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<League>();
        //    }
        //}

        //public static IEnumerable<Player> GetAllPlayer()
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Player>();
        //    }
        //}

        //public static IEnumerable<Team> GetTeamsByLeague(int id)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {

        //        return db.GetAllWithChildren<Team>(t => t.LeagueId == id);

        //    }
        //}

        //public static IEnumerable<Match> GetMatchesByLeague(int id)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Match>().Where(m => m.LeagueId == id);
        //    }
        //}


        //public static IEnumerable<Team> GetTeamsByYear(DateTime year)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Team>().Where(t => t.Year == year);
        //    }
        //}

        //public static IEnumerable<Player> GetPlayersByTeam(int id)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Player>().Where(p => p.Teams.Select(t => t.Id).Contains(id));
        //    }
        //}

        //public static IEnumerable<Player> GetPlayersByLeague(int leagueId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {

        //        IEnumerable<Team> teams = db.GetAllWithChildren<Team>().Where(t => t.LeagueId == leagueId);

        //        List<Player> players = new List<Player>();

        //        foreach (var team in teams)
        //        {
        //            players.AddRange(team.Players);
        //        }

        //        return players;
        //    }
        //}

        //public static IEnumerable<Player> GetPlayersByMatch(int id)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetWithChildren<Match>(id).Players;
        //    }
        //}

        //public static IEnumerable<Statistic> GetStatisticsByLeague(int leagueId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        IEnumerable<int> teams = db.GetAllWithChildren<Team>().Where(t => t.LeagueId == leagueId).Select(t => t.Id);

        //        return db.GetAllWithChildren<Statistic>().Where(s => teams.Contains(s.TeamId));
        //    }
        //}

        //public static IEnumerable<Statistic> GetStatisticsByPlayer(int playerId)
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Statistic>().Where(s => s.PlayerRegNum == playerId);
        //    }
        //}

        //public static Stadium GetStadiumById(int stadiumId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetWithChildren<Stadium>(stadiumId);
        //    }
        //}

        //public static Match GetMatchById(int id)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetWithChildren<Match>(id);
        //    }
        //}

        //public static IEnumerable<Match> GetActualMatches()
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        DateTime threshold = DateTime.Now.AddDays(3).AddYears(-1);

        //        return db.GetAllWithChildren<Match>().Where(m => DateTime.Compare(DateTime.Now.AddYears(-1), m.Date) < 0 && DateTime.Compare(m.Date, threshold) < 0 || m.State == StateEnum.Playing);
        //    }
        //}

        //public static IEnumerable<Referee> GetAllReferee()
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Referee>();
        //    }
        //}

        //public static IEnumerable<Match> GetMatchesByReferee(int refereeId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Referee>().Where(r => r.Id == refereeId).First().Matches;

        //    }
        //}

        //public static IEnumerable<Match> GetMatchesByPlayer(int playerId)
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Player>().Where(p => p.RegNum == playerId).First().Matches;

        //    }
        //}

        //public static IEnumerable<Match> GetMatchesByTeam(int teamId)
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Match>().Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId);
        //    }
        //}


        //public static IEnumerable<Stadium> GetAllStadium()
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Stadium>();
        //    }
        //}

        //public static IEnumerable<Team> GetAllTeam()
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Team>();
        //    }
        //}

        //public static IEnumerable<Match> GetAllMatch()
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Match>();
        //    }
        //}

        //public static int GetNumberOfRoundsInLeague(int leagueId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<League>().Where(l => l.Id == leagueId).First().Rounds;
        //    }
        //}

        //public static League GetLeagueById(int leagueId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetWithChildren<League>(leagueId);
        //    }
        //}

        //public static IEnumerable<League> GetLeaguesByYear(DateTime year)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<League>().Where(l => l.Year == year);
        //    }
        //}


        //public static IEnumerable<League> GetLeaguesByReferee(int refereeId)
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        var leagueIds = db.GetWithChildren<Referee>(refereeId).Matches.Select(m => m.LeagueId).Distinct();

        //        List<League> leagues = new List<League>();
        //        foreach (var id in leagueIds)
        //        {
        //            leagues.Add(db.Get<League>(id));
        //        }

        //        return leagues;
        //    }
        //}

        //public static IEnumerable<EventMessage> GetEventMessagesByCategory(char catagoryStartNumber)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<EventMessage>().Where(e => e.Code.ToString()[0] == catagoryStartNumber);
        //    }
        //}

        //public static EventMessage GetEventMessageById(int id)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetWithChildren<EventMessage>(id);
        //    }
        //}

        //public static IEnumerable<Event> GetEventsByMatch(int matchId)
        //{

        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetAllWithChildren<Event>().Where(e => e.MatchId == matchId);
        //    }

        //}

        //public static Event GetEventById(int eventId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetWithChildren<Event>(eventId);
        //    }
        //}

        //public static Team GetTeamById(int id)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetWithChildren<Team>(id);
        //    }
        //}

        //public static Referee GetRefereeById(int id)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetWithChildren<Referee>(id);
        //    }
        //}

        //public static Player GetPlayerById(int id)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        return db.GetWithChildren<Player>(id);
        //    }
        //}


        #endregion

        #region POST

        //public static int AddLeague(int id, string name, DateTime year, string type, string classname, int rounds, CountriesEnum country)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {

        //        League l = new League();
        //        l.Id = id;
        //        l.Name = name;
        //        l.Year = year;
        //        l.Type = type;
        //        l.ClassName = classname;
        //        l.Rounds = rounds;
        //        l.Country = country;

        //        db.Insert(l);

        //        return l.Id;
        //    }
        //}

        //public static int AddStadium(int id, string name, string address)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        Stadium s = new Stadium();
        //        s.Id = id;
        //        s.Name = name;
        //        s.Address = address;

        //        db.Insert(s);

        //        return s.Id;
        //    }
        //}

        //public static int AddTeam(int id, string name, DateTime year, string coach, string sex, CountriesEnum country, int stadiumId, int leagueId, short get = 0, short scored = 0, short points = 0, short standing = -1)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        Team t = new Team();
        //        t.Id = id;
        //        t.Name = name;
        //        t.Year = year;
        //        t.Coach = coach;
        //        t.Sex = sex;
        //        t.Country = country;
        //        t.Get = get;
        //        t.Scored = scored;
        //        t.Points = points;
        //        t.StadiumId = stadiumId;
        //        t.LeagueId = leagueId;
        //        t.Standing = standing != -1 ? standing : (short)(db.GetAllWithChildren<Team>().Where(t1 => t1.LeagueId == leagueId).Count() + 1);

        //        db.Insert(t);

        //        return t.Id;
        //    }
        //}

        //public static int AddReferee(int id, string name, CountriesEnum country, short number = 0, short penalty = 0)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        Referee r = new Referee();
        //        r.Id = id;
        //        r.Name = name;
        //        r.Number = number;
        //        r.Penalty = penalty;
        //        r.Matches = new List<Match>();
        //        r.Country = country;

        //        db.Insert(r);

        //        return r.Id;
        //    }
        //}

        //public static int AddPlayer(string firstName, string secondName, int regNum, int number, DateTime date)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        Player p = new Player();
        //        p.FirstName = firstName;
        //        p.SecondName = secondName;
        //        p.RegNum = regNum;
        //        p.Number = (short)number;
        //        p.BirthDate = date.Date;
        //        p.Teams = new List<Team>();


        //        db.Insert(p);

        //        return p.RegNum;
        //    }
        //}

        //public static void AddPlayerToTeam(int playerId, int teamId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        var player = db.GetWithChildren<Player>(playerId);
                
        //        var team = db.GetWithChildren<Team>(teamId);

        //        team.Players.Add(player);
        //        player.Teams.Add(team);

        //        AddStatisticsForPlayerInTeam(player, team, db);

        //        db.UpdateWithChildren(team);
        //        db.UpdateWithChildren(player);

        //    }
        //}

        //public static void AddPlayerToMatch(int playerId, int matchId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        var player = db.GetWithChildren<Player>(playerId);
        //        //var matches = db.GetAllWithChildren<Match>();
        //        var match = db.GetWithChildren<Match>(matchId);

        //        if (!(player.Teams.Select(t => t.Id).Contains(match.HomeTeamId) || player.Teams.Select(t => t.Id).Contains(match.AwayTeamId)))
        //        {
        //            throw new Exception("Player cannot be added to match!");
        //        }

        //        match.Players.Add(player);
        //        player.Matches.Add(match);

        //        db.UpdateWithChildren(match);
        //        db.UpdateWithChildren(player);

        //    }
        //}

        //public static void AddRefereeToMatch(int refereeId, int matchId)
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        var referee = db.GetWithChildren<Referee>(refereeId);

        //        var match = db.GetWithChildren<Match>(matchId);

        //        match.Referees.Add(referee);
        //        referee.Matches.Add(match);

        //        db.UpdateWithChildren(match);
        //        db.UpdateWithChildren(referee);

        //    }
        //}

        //private static void AddStatisticsForPlayerInTeam(Player player, Team team, SQLiteConnection db)
        //{

        //    string[] types = new string[] { "G", "A", "P2", "P5", "P10", "PV", "APP" };

        //    foreach (var type in types)
        //    {
        //        Statistic s = new Statistic();
        //        s.Name = type;
        //        s.Number = 0;
        //        s.TeamId = team.Id;
        //        s.PlayerRegNum = player.RegNum;
                

        //        db.Insert(s);

        //    }
        //}

        //public static void AddPlayersAndTeams(Dictionary<int, List<int>> dict)
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        var players = db.GetAllWithChildren<Player>();
        //        var teams = db.GetAllWithChildren<Team>();

        //        foreach (var d in dict)
        //        {
        //            Team team = teams.SingleOrDefault( t => t.Id == d.Key);

        //            foreach (var palyerId in d.Value)
        //            {
        //                Player player = players.SingleOrDefault( p => p.RegNum == palyerId);

        //                player.Teams.Add(team);
        //                team.Players.Add(player);

        //                AddStatisticsForPlayerInTeam(player, team, db);

        //                db.UpdateWithChildren(player);
        //                db.UpdateWithChildren(team);
        //            }

        //        }

        //    }
        //}

        //public static void AddPlayersAndMatches(Dictionary<int, List<int>> dict)
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        var players = db.GetAllWithChildren<Player>();
        //        var matches = db.GetAllWithChildren<Match>();

        //        foreach (var d in dict)
        //        {
        //            Match match = matches.SingleOrDefault(m => m.Id == d.Key);

        //            foreach (var playerId in d.Value)
        //            {
        //                Player player = players.SingleOrDefault(p => p.RegNum== playerId);

        //                player.Matches.Add(match);
        //                match.Players.Add(player);

        //                db.UpdateWithChildren(player);
        //                db.UpdateWithChildren(match);
        //            }
                   
        //        }

        //    }
        //}


        //public static void AddRefereesAndMatches(Dictionary<int, List<int>> dict)
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        var referees = db.GetAllWithChildren<Referee>();
        //        var matches = db.GetAllWithChildren<Match>();

        //        foreach (var d in dict)
        //        {
        //            Match match = matches.SingleOrDefault(m => m.Id == d.Key);

        //            foreach (var refereeId in d.Value)
        //            {
        //                Referee referee = referees.SingleOrDefault(r => r.Id == refereeId);

        //                referee.Matches.Add(match);
        //                match.Referees.Add(referee);

        //                db.UpdateWithChildren(referee);
        //                db.UpdateWithChildren(match);
        //            }
        //        }
        //    }
        //}

        //public static int AddEvent(int id, int matchId, string type, TimeSpan time, int playerId, int evenetMessageId, int teamId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        Event e = new Event();
        //        e.Id = id;
        //        e.MatchId = matchId;
        //        e.PlayerId = playerId;
        //        e.EventMessageId = evenetMessageId;
        //        e.Time = time.ToString();
        //        e.Type = type;
        //        e.TeamId = teamId;

        //        db.Insert(e);

        //        if (playerId != -1 && type != "I" && type != "B")
        //        {
        //            ChangeStatisticFromPlayer(playerId, teamId, type, db, "increase");
        //        }

        //        return e.Id;
        //    }
        //}

        //public static void AddEventMessage(int id, int code, string message)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        EventMessage e = new EventMessage();
        //        e.Id = id;
        //        e.Code = code;
        //        e.Message = message;

        //        db.Insert(e);
        //    }
        //}

        //public static void AddMatch(int id, int homeTeamId, int awayTeamId, short goalsH, short goalsA, short round, StateEnum state, TimeSpan time, DateTime date, int leagueId, int stadiumId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        Match m = new Match();
        //        m.Id = id;
        //        m.HomeTeamId = homeTeamId;
        //        m.AwayTeamId = awayTeamId;
        //        m.GoalsH = goalsH;
        //        m.GoalsA = goalsA;
        //        m.LeagueId = leagueId;
        //        m.Round = round;
        //        m.StadiumId = stadiumId;
        //        m.State = state;
        //        m.Time = time;
        //        m.Date = date;
        //        m.Referees = new List<Referee>();
        //        m.Players = new List<Player>();

        //        db.Insert(m);
        //    }
        //}

        //public static void AddEventMessages(List<EventMessageModel> model)
        //{
        //    foreach (var m in model)
        //    {
        //        AddEventMessage(m.Id,m.Code,m.Message);
        //    }
        //}

        //public static void AddEvents(List<EventModel> model)
        //{
        //    foreach (var m in model)
        //    {
        //        AddEvent(m.Id, m.MatchId, m.Type, m.Time, m.PlayerId, m.EventMessageId, m.TeamId);
        //    }
        //}

        //public static void AddPlayers(List<PlayerModel> model)
        //{
        //    foreach (var m in model)
        //    {
        //        AddPlayer(m.FirstName, m.SecondName, m.RegNum, m.Number, m.BirthDate);
        //    }
        //}

        //public static void AddMatches(List<MatchModel> model)
        //{
        //    foreach (var m in model)
        //    {
        //        AddMatch(m.Id, m.HomeTeamId, m.AwayTeamId, m.GoalsH, m.GoalsA, m.Round, m.State, m.Time, m.Date, m.LeagueId, m.StadiumId);
        //    }
        //}

        //public static void AddLeagues(List<LeagueModel> model)
        //{
        //    foreach (var m in model)
        //    {
        //        AddLeague(m.Id, m.Name, m.Year, m.type, m.ClassName, m.Rounds, m.Country);
        //    }
        //}

        //public static void AddStadiums(List<StadiumModel> model)
        //{
        //    foreach (var m in model)
        //    {
        //        AddStadium(m.Id, m.Name, m.Address);
        //    }
        //}

        //public static void AddReferees(List<RefereeModel> model)
        //{
        //    foreach (var m in model)
        //    {
        //        AddReferee(m.Id, m.Name, m.Country, m.Number,m.Penalty);
        //    }
        //}

        //public static void AddTeams(List<TeamModel> model)
        //{
        //    foreach (var m in model)
        //    {
        //        AddTeam(m.Id, m.Name, m.Year, m.Coach, m.Sex, m.Country, m.StadiumId, m.LeagueId,m.Get, m.Scored, m.Points, m.Standing);
        //    }
        //}

        #endregion

        #region DELETE

        //public static void RemovePlayerFromTeam(int playerId, int teamId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {

        //        var player = db.GetWithChildren<Player>(playerId);

        //        var team = db.GetWithChildren<Team>(teamId);

        //        RemoveStatisticsForPlayerInTeam(player, team, db);

        //        team.Players.Remove(player);
        //        player.Teams.Remove(team);


        //    }
        //}

        //public static void RemovePlayerFromMatch(int playerId, int matchId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        var player = db.GetWithChildren<Player>(playerId);

        //        var match = db.GetWithChildren<Match>(matchId);

        //        if (!(player.Teams.Select(t => t.Id).Contains(match.HomeTeamId) || player.Teams.Select(t => t.Id).Contains(match.AwayTeamId)))
        //            throw new Exception("Player cannot be removed from match!");


        //        match.Players.Remove(player);
        //        player.Matches.Remove(match);

        //    }
        //}

        //private static void RemoveStatisticsForPlayerInTeam(Player player, Team team, SQLiteConnection db)
        //{

        //    var statisctics = db.GetAllWithChildren<Statistic>().Where(s => s.PlayerRegNum == player.RegNum && s.TeamId == team.Id);

        //    foreach (var s in statisctics)
        //    {
        //        db.Delete(s);
        //    }

        //}

        //public static void RemoveEvent(int eventId)
        //{
        //    using (var db = new SQLiteConnection(Platform,DatabasePath))
        //    {
        //        var e = db.GetWithChildren<Event>(eventId);

        //        if (e.Type == "G")
        //        {
        //            int t;

        //            var match = db.GetWithChildren<Match>(e.MatchId);
        //            var homeTeam = db.GetWithChildren<Team>(match.HomeTeamId);
        //            var awayTeam = db.GetWithChildren<Team>(match.AwayTeamId);

        //            if (homeTeam.Players.Select(p => p.RegNum).Contains(e.PlayerId))
        //            {
        //                t = homeTeam.Id;
        //            }
        //            else
        //            {
        //                t = awayTeam.Id;
        //            }

        //            ChangeStatisticFromPlayer(e.PlayerId, t, e.Type, db, "reduce");

        //            var e1 = db.GetAllWithChildren<Event>().Where(ev => ev.MatchId == e.MatchId && ev.Time == e.Time && ev.Type == "A").First();

        //            var match1 = db.GetWithChildren<Match>(e1.MatchId);
        //            var homeTeam1 = db.GetWithChildren<Team>(match1.HomeTeamId);
        //            var awayTeam1 = db.GetWithChildren<Team>(match1.AwayTeamId);


        //            if (homeTeam1.Players.Select(p => p.RegNum).Contains(e1.PlayerId))
        //            {
        //                t = homeTeam1.Id;
        //            }
        //            else
        //            {
        //                t = awayTeam1.Id;
        //            }

        //            db.Delete(e1);
        //        }

        //        db.Delete(e);
        //    }
        //}

        #endregion


        #region PUT

        //private static void ChangeStatisticFromPlayer(int playerId, int teamId, string type, SQLiteConnection db, string direction)
        //{

        //    Statistic stat = db.GetAllWithChildren<Statistic>().Where(s => s.PlayerRegNum == playerId && s.TeamId == teamId && s.Name == type).First();

        //    if (direction == "increase")
        //    {
        //        stat.Number++;
        //    }
        //    else
        //    {
        //        stat.Number--;
        //    }

        //    //db.Insert(stat);
        //    db.Update(stat);
        //    //db.Statistics.Attach(stat);

        //    //var entry = db.Entry(stat);
        //    //entry.Property(e => e.Number).IsModified = true;

        //}

        //public static void UpdateMatchState(int matchId, StateEnum newState)
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        Match match = db.Get<Match>(matchId);
        //        match.State = newState;
        //        db.Update(match);
                
        //    }
        //}

        //public static void UpdateMatchTime(int matchId, TimeSpan newTime)
        //{
        //    using (var db = new SQLiteConnection(Platform, DatabasePath))
        //    {
        //        Match match = db.Get<Match>(matchId);
        //        match.Time = newTime;
        //        db.Update(match);

        //    }
        //}

        #endregion

    }
}
