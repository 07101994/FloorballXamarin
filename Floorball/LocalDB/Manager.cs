using Floorball.LocalDB.Tables;
using Floorball.REST;
using FloorballServer.Models.Floorball;
using SQLite;
using SQLite.Net;
using SQLite.Net.Interop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

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

        public static void CreateDatabase()
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                DropTables(db);

                db.CreateTable<PlayerTeam>();
                db.CreateTable<PlayerTeam>();
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

        private static void DropTables(SQLiteConnection db)
        {
            db.DropTable<PlayerTeam>();
            db.DropTable<PlayerTeam>();
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

        public static void InitDatabaseFromServer()
        {
            //List<LeagueModel> leagues = RESTHelper.GetAllLeague();
            //List<MatchModel> matches = RESTHelper.GetAllMatch();
            //List<TeamModel> teams = RESTHelper.GetAllTeam();
            //List<EventModel> events = RESTHelper.GetAllEvent();
            //List<EventMessageModel> eventMessages = RESTHelper.GetAllEventMessage();
            //List<PlayerModel> players = RESTHelper.GetAllPlayer();
            //List<RefereeModel> referees = RESTHelper.GetAllReferee();
            //List<StadiumModel> stadiums = RESTHelper.GetAllStadium();
            //List<StatisticModel> statistics = RESTHelper.GetAllStatistic();

            AddEventMessages(RESTHelper.GetAllEventMessage());
            AddLeagues(RESTHelper.GetAllLeague());
            AddReferees(RESTHelper.GetAllReferee());
            AddPlayers(RESTHelper.GetAllPlayer());
            AddStadiums(RESTHelper.GetAllStadium());
            AddTeams(RESTHelper.GetAllTeam());
            AddMatches(RESTHelper.GetAllMatch());
            AddPlayersAndTeams(RESTHelper.GetPlayersAndTeams());
            AddPlayersAndMatches(RESTHelper.GetPlayersAndMatches());
            AddRefereesAndMatches(RESTHelper.GetRefereesAndMatches());
            AddEvents(RESTHelper.GetAllEvent());
            

        }

 

        #region GET

        public static List<DateTime> GetAllYear()
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {

                return db.Table<League>().Select(l => l.Year).Distinct().OrderBy(t => t.Year).ToList();

            }
        }

        public static List<League> GetAllLeague()
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Table<League>().ToList();
            }
        }

        public static List<Player> GetAllPlayer()
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Table<Player>().ToList();
            }
        }

        public static List<Team> GetTeamsByLeague(int id)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {

                return db.Table<Team>().Where(t => t.LeagueId == id).ToList();

            }
        }

        public static List<Match> GetMatchesByLeague(int id)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Table<Match>().Where(m => m.LeagueId == id).ToList();
                //return db.Leagues.Include("Matches").Include("Matches.HomeTeam").Include("Matches.AwayTeam").Where(l => l.Id == id).First().Matches.ToList();
            }
        }


        public static List<Team> GetTeamsByYear(DateTime year)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {

                return db.Table<Team>().Where(t => t.Year == year).ToList();

                //var q = from t in db.Teams
                //        where t.Year.Year == year.Year
                //        select t;

                //return q.ToList();
            }
        }

        public static List<Player> GetPlayersByTeam(int id)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {

                return db.Table<Player>().Where(p => p.Teams.Select(t => t.Id).Contains(id)).ToList();

                //var q = (from t in db.Teams
                //         where t.Id == id
                //         select t.Players).First();

                //return q.ToList();
            }
        }

        public static List<Player> GetPlayersByLeague(int leagueId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {

                List<Team> teams = db.Table<Team>().Where(t => t.LeagueId == leagueId).ToList();

                List<Player> players = new List<Player>();

                //IEnumerable<ICollection<Player>> playerCollections = db.Leagues.Include("Teams.Players").Where(l => l.Id == leagueId).First().Teams.Select(t => t.Players);
                foreach (var team in teams)
                {
                    players.AddRange(team.Players);
                }

                return players;
            }
        }

        public static List<Player> GetPlayersByMatch(int id)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Get<Match>(id).Players;

                //return db.Matches.Include("Players").Where(m => m.Id == id).First().Players.ToList();
            }
        }

        public static List<Statistic> GetStatisticsByLeague(int leagueId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {

                List<int> teams = db.Table<Team>().Where(t => t.LeagueId == leagueId).Select(t => t.Id).ToList();

                return db.Table<Statistic>().Where(s => teams.Contains(s.TeamId)).ToList();

                //return db.Statistics.Where(s => s.Team.LeagueId == leagueId).ToList();
            }
        }

        public static Match GetMatchById(int id)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {

                return db.Get<Match>(id);

                //var q = (from m in db.Matches.Include("HomeTeam").Include("AwayTeam").Include("Players").Include("League").Include("HomeTeam.Players").Include("AwayTeam.Players")
                //         where m.Id == id
                //         select m).First();

                //return q;
            }
        }

        public static List<Match> GetActualMatches()
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                string threshold = DateTime.Now.AddDays(3).ToString();

                return db.Table<Match>().Where(m => string.Compare(threshold, m.Date, false) > 0 && string.Compare(m.Date, DateTime.Now.ToString(), false) > 0).ToList();

                //return db.Matches.Where(m => m.Date < threshold && m.Date >= DateTime.Now).ToList();
            }
        }

        public static List<Referee> GetAllReferee()
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Table<Referee>().ToList();
                //   return db.Referees.ToList();
            }
        }

        public static List<Match> GetMatchesByReferee(int refereeId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Table<Referee>().Where(r => r.Id == refereeId).First().Matches;

                //return db.Referees.Include("Matches").Include("Matches.Players").Where(r => r.Id == refereeId).First().Matches.ToList();
            }
        }

        public static List<Stadium> GetAllStadium()
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Table<Stadium>().ToList();
            }
        }

        public static List<Team> GetAllTeam()
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Table<Team>().ToList();
            }
        }

        public static int GetNumberOfRoundsInLeague(int leagueId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Table<League>().Where(l => l.Id == leagueId).First().Rounds;
            }
        }

        public static League GetLeagueById(int leagueId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Get<League>(leagueId);
            }
        }

        public static List<League> GetLeaguesByYear(DateTime year)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Table<League>().Where(l => l.Year == year).ToList();
            }
        }


        public static List<EventMessage> GetEventMessagesByCategory(char catagoryStartNumber)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Table<EventMessage>().ToList().Where(e => e.Code.ToString()[0] == catagoryStartNumber).ToList();
            }
        }

        public static EventMessage GetEventMessageById(int id)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Get<EventMessage>(id);
            }
        }

        public static List<Event> GetEventsByMatch(int matchId)
        {

            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Table<Event>().Where(e => e.MatchId == matchId).ToList();

            }

        }

        public static Event GetEventById(int eventId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Get<Event>(eventId);

                //return db.Events.Include("EventMessage").Include("Player").Where(e => e.Id == eventId).First();
            }
        }

        public static Team GetTeamById(int id)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Get<Team>(id);

                //return db.Teams.Where(t => t.Id == id).First();
            }
        }

        public static Referee GetRefereeById(int id)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Get<Referee>(id);

                //return db.Referees.Where(r => r.Id == id).First();
            }
        }

        public static Player GetPlayerById(int id)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                return db.Get<Player>(id);

                //return db.Players.Where(p => p.RegNum == id).First();
            }
        }


        #endregion

        #region POST

        public static int AddLeague(int id, string name, DateTime year, string type, string classname, int rounds)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {

                League l = new League();
                l.Id = id;
                l.Name = name;
                l.Year = year;
                l.Type = type;
                l.ClassName = classname;
                l.Rounds = rounds;

                db.Insert(l);

                //db.Leagues.Add(l);

                //db.SaveChanges();
                return l.Id;
            }
        }

        public static int AddStadium(int id, string name, string address)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                Stadium s = new Stadium();
                s.Id = id;
                s.Name = name;
                s.Address = address;

                db.Insert(s);

                //db.Stadiums.Add(s);

                //db.SaveChanges();
                return s.Id;
            }
        }

        public static int AddTeam(int id, string name, DateTime year, string coach, string sex, int stadiumId, int leagueId, short get = 0, short scored = 0, short points = 0, short standing = -1)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                Team t = new Team();
                t.Id = id;
                t.Name = name;
                t.Year = year;
                t.Coach = coach;
                t.Sex = sex;
                t.Get = get;
                t.Scored = scored;
                t.Points = points;
                t.StadiumId = stadiumId;
                t.LeagueId = leagueId;
                t.Standing = standing != -1 ? standing : (short)(db.Table<Team>().Where(t1 => t1.LeagueId == leagueId).Count() + 1);

                db.Insert(t);
                //db.Teams.Add(t);

                //db.SaveChanges();
                return t.Id;
            }
        }

        public static int AddReferee(int id, string name, short number = 0, short penalty = 0)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                Referee r = new Referee();
                r.Id = id;
                r.Name = name;
                r.Number = number;
                r.Penalty = penalty;
                r.Matches = new List<Match>();

                db.Insert(r);

                //db.Referees.Add(r);

                //db.SaveChanges();
                return r.Id;
            }
        }

        public static int AddPlayer(string name, int regNum, int number, DateTime date)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                Player p = new Player();
                p.Name = name;
                p.RegNum = regNum;
                p.Number = (short)number;
                p.BirthDate = date.Date;
                p.Teams = new List<Team>();


                db.Insert(p);

                //db.Players.Add(p);

                //db.SaveChanges();
                return p.RegNum;
            }
        }

        public static void AddPlayerToTeam(int playerId, int teamId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                //var player = (from p in db.Players
                //              where p.RegNum == playerId
                //              select p).First();

                var player = db.Get<Player>(playerId);

                //var team = (from t in db.Teams
                //            where t.Id == teamId
                //            select t).First();

                var team = db.Get<Team>(teamId);

                team.Players.Add(player);

                AddStatisticsForPlayerInTeam(player, team, db);

                db.Update(team);
                db.Update(player);
                //db.SaveChanges();

            }
        }

        public static void AddPlayerToMatch(int playerId, int matchId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                var player = db.Get<Player>(playerId);

                //var player = (from p in db.Players.Include("Matches").Include("Teams")
                //              where p.RegNum == playerId
                //              select p).First();

                var match = db.Get<Match>(matchId);

                //var match = (from m in db.Matches.Include("Players")
                //             where m.Id == matchId
                //             select m).First();

                if (!(player.Teams.Select(t => t.Id).Contains(match.HomeTeamId) || player.Teams.Select(t => t.Id).Contains(match.AwayTeamId)))
                    throw new Exception("Player cannot be added to match!");

                match.Players.Add(player);

                //db.SaveChanges();

            }
        }

        private static void AddStatisticsForPlayerInTeam(Player player, Team team, SQLiteConnection db)
        {

            string[] types = new string[] { "G", "A", "P2", "P5", "P10", "PV", "APP" };

            foreach (var type in types)
            {
                Statistic s = new Statistic();
                s.Name = type;
                s.Number = 0;
                s.TeamId = team.Id;
                s.PlayerRegNum = player.RegNum;
                

                db.Insert(s);
                //db.Statistics.Add(s);

            }
        }

        public static void AddPlayersAndTeams(List<List<int>> list)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var players = db.Table<Player>().ToList();
                var teams = db.Table<Team>().ToList();

                foreach (var l in list)
                {
                    Player player = players.Where(p => p.RegNum == l[0]).First();
                    Team team = teams.Where(t => t.Id == l[1]).First();

                    if (player.Teams == null)
                    {
                        player.Teams = new List<Team>();
                    }

                    player.Teams.Add(team);

                    AddStatisticsForPlayerInTeam(player, team, db);

                    db.Update(player);
                    db.Update(team);
                }

            }
        }

        public static void AddPlayersAndMatches(List<List<int>> list)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var players = db.Table<Player>().ToList();
                var matches = db.Table<Match>().ToList();

                foreach (var l in list)
                {
                    Player player = players.Where(p => p.RegNum == l[0]).First();
                    Match match = matches.Where(m => m.Id == l[1]).First();

                    if (player.Matches == null)
                    {
                        player.Matches = new List<Match>();
                    }

                    player.Matches.Add(match);

                    db.Update(player);
                    db.Update(match);
                }

            }
        }


        public static void AddRefereesAndMatches(List<List<int>> list)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var referees = db.Table<Referee>().ToList();
                var matches = db.Table<Match>().ToList();

                foreach (var l in list)
                {
                    Referee referee = referees.Where(r => r.Id == l[0]).First();
                    Match match = matches.Where(m => m.Id == l[1]).First();

                    if (referee.Matches == null)
                    {
                        referee.Matches = new List<Match>();
                    }

                    referee.Matches.Add(match);

                    db.Update(referee);
                    db.Update(match);
                }

            }
        }

        public static int AddEvent(int id, int matchId, string type, TimeSpan time, int playerId, int evenetMessageId, int teamId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                Event e = new Event();
                e.Id = id;
                e.MatchId = matchId;
                e.PlayerId = playerId;
                e.EventMessageId = evenetMessageId;
                e.Time = time.ToString();
                e.Type = type;
                e.TeamId = teamId;

                db.Insert(e);
                //db.Events.Add(e);

                if (playerId != -1 && type != "I" && type != "B")
                {
                    ChangeStatisticFromPlayer(playerId, teamId, type, db, "increase");
                }

                //db.SaveChanges();

                return e.Id;
            }
        }

        public static void AddEventMessage(int id, int code, string message)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                EventMessage e = new EventMessage();
                e.Id = id;
                e.Code = code;
                e.Message = message;

                db.Insert(e);
            }
        }

        public static void AddMatch(int id, int homeTeamId, int awayTeamId, short goalsH, short goalsA, short round, string state, TimeSpan time, string date, int leagueId, int stadiumId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                Match m = new Match();
                m.Id = id;
                m.HomeTeamId = homeTeamId;
                m.AwayTeamId = awayTeamId;
                m.GoalsH = goalsH;
                m.GoalsA = goalsA;
                m.LeagueId = leagueId;
                m.Round = round;
                m.StadiumId = stadiumId;
                m.State = state;
                m.Time = time;
                m.Date = date;
                m.Referees = new List<Referee>();
                m.Players = new List<Player>();

                db.Insert(m);
            }
        }

        public static void AddEventMessages(List<EventMessageModel> model)
        {
            foreach (var m in model)
            {
                AddEventMessage(m.Id,m.Code,m.Message);
            }
        }

        public static void AddEvents(List<EventModel> model)
        {
            foreach (var m in model)
            {
                AddEvent(m.Id, m.MatchId, m.Type, m.Time,/* TimeSpan.ParseExact(m.Time, "h\\h\\:m\\m\\:s\\s", CultureInfo.InvariantCulture),*/ m.PlayerId, m.EventMessageId, m.TeamId);
            }
        }

        public static void AddPlayers(List<PlayerModel> model)
        {
            foreach (var m in model)
            {
                AddPlayer(m.Name, m.RegNum, m.Number, m.BirthDate);
            }
        }

        public static void AddMatches(List<MatchModel> model)
        {
            foreach (var m in model)
            {
                AddMatch(m.Id, m.HomeTeamId, m.AwayTeamId, m.GoalsH, m.GoalsA, m.Round, m.State, m.Time, m.Date.ToString(), m.LeagueId, m.StadiumId);
            }
        }

        public static void AddLeagues(List<LeagueModel> model)
        {
            foreach (var m in model)
            {
                AddLeague(m.Id, m.Name, m.Year, m.type, m.ClassName, m.Rounds);
            }
        }

        public static void AddStadiums(List<StadiumModel> model)
        {
            foreach (var m in model)
            {
                AddStadium(m.Id, m.Name, m.Address);
            }
        }

        public static void AddReferees(List<RefereeModel> model)
        {
            foreach (var m in model)
            {
                AddReferee(m.Id, m.Name,m.Number,m.Penalty);
            }
        }

        public static void AddTeams(List<TeamModel> model)
        {
            foreach (var m in model)
            {
                AddTeam(m.Id, m.Name, m.Year, m.Coach, m.Sex, m.StadiumId, m.LeagueId,m.Get, m.Scored, m.Points, m.Standing);
            }
        }

        #endregion

        #region DELETE

        public static void RemovePlayerFromTeam(int playerId, int teamId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {

                var player = db.Get<Player>(playerId);

                //var player = (from p in db.Players
                //              where p.RegNum == playerId
                //              select p).First();

                var team = db.Get<Team>(teamId);

                //var team = (from t in db.Teams
                //            where t.Id == teamId
                //            select t).First();

                RemoveStatisticsForPlayerInTeam(player, team, db);
                team.Players.Remove(player);

                //db.SaveChanges();

            }
        }

        public static void RemovePlayerFromMatch(int playerId, int matchId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {

                //var player = (from p in db.Players.Include("Matches").Include("Teams")
                //              where p.RegNum == playerId
                //              select p).First();

                var player = db.Get<Player>(playerId);


                //var match = (from m in db.Matches.Include("Players")
                //             where m.Id == matchId
                //             select m).First();

                var match = db.Get<Match>(matchId);

                if (!(player.Teams.Select(t => t.Id).Contains(match.HomeTeamId) || player.Teams.Select(t => t.Id).Contains(match.AwayTeamId)))
                    throw new Exception("Player cannot be removed from match!");


                match.Players.Remove(player);

                //db.SaveChanges();

            }
        }

        private static void RemoveStatisticsForPlayerInTeam(Player player, Team team, SQLiteConnection db)
        {

            var statisctics = db.Table<Statistic>().Where(s => s.PlayerRegNum == player.RegNum && s.TeamId == team.Id).ToList();

            foreach (var s in statisctics)
            {
                //db.Statistics.Remove(s);
                db.Delete(s);
            }

        }

        public static void RemoveEvent(int eventId)
        {
            using (var db = new SQLiteConnection(Platform,DatabasePath))
            {
                //var e = db.Events.Include("Match.HomeTeam.Players").Include("Match.AwayTeam.Players").Where(ev => ev.Id == eventId).First();
                var e = db.Get<Event>(eventId);

                if (e.Type == "G")
                {
                    int t;

                    var match = db.Get<Match>(e.MatchId);
                    var homeTeam = db.Get<Team>(match.HomeTeamId);
                    var awayTeam = db.Get<Team>(match.AwayTeamId);

                    if (homeTeam.Players.Select(p => p.RegNum).Contains(e.PlayerId))
                    {
                        t = homeTeam.Id;
                    }
                    else
                    {
                        t = awayTeam.Id;
                    }

                    ChangeStatisticFromPlayer(e.PlayerId, t, e.Type, db, "reduce");

                    var e1 = db.Table<Event>().Where(ev => ev.MatchId == e.MatchId && ev.Time == e.Time && ev.Type == "A").First();

                    var match1 = db.Get<Match>(e1.MatchId);
                    var homeTeam1 = db.Get<Team>(match1.HomeTeamId);
                    var awayTeam1 = db.Get<Team>(match1.AwayTeamId);


                    if (homeTeam1.Players.Select(p => p.RegNum).Contains(e1.PlayerId))
                    {
                        t = homeTeam1.Id;
                    }
                    else
                    {
                        t = awayTeam1.Id;
                    }

                    //db.Events.Remove(e1);
                    db.Delete(e1);
                }

                db.Delete(e);
                //db.Events.Remove(e);

                //db.SaveChanges();
            }
        }

        #endregion


        #region PUT

        private static void ChangeStatisticFromPlayer(int playerId, int teamId, string type, SQLiteConnection db, string direction)
        {

            Statistic stat = db.Table<Statistic>().Where(s => s.PlayerRegNum == playerId && s.TeamId == teamId && s.Name == type).First();

            if (direction == "increase")
            {
                stat.Number++;
            }
            else
            {
                stat.Number--;
            }

            db.Insert(stat);

            //db.Statistics.Attach(stat);

            //var entry = db.Entry(stat);
            //entry.Property(e => e.Number).IsModified = true;

        }

        #endregion
    }
}
