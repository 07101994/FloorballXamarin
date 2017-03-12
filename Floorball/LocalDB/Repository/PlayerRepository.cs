using Floorball.LocalDB.Tables;
using FloorballServer.Models.Floorball;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Floorball.LocalDB.Repository
{
    public class PlayerRepository : Repository
    {

        #region GET

        public IEnumerable<Player> GetAllPlayer()
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Player>();
            }
        }

        public IEnumerable<Player> GetPlayersByTeam(int id)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetAllWithChildren<Player>().Where(p => p.Teams.Select(t => t.Id).Contains(id));
            }
        }

        public IEnumerable<Player> GetPlayersByLeague(int leagueId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {

                IEnumerable<Team> teams = db.GetAllWithChildren<Team>().Where(t => t.LeagueId == leagueId);

                List<Player> players = new List<Player>();

                foreach (var team in teams)
                {
                    players.AddRange(team.Players);
                }

                return players;
            }
        }

        public IEnumerable<Player> GetPlayersByMatch(int id)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetWithChildren<Match>(id).Players;
            }
        }

        public Player GetPlayerById(int id)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                return db.GetWithChildren<Player>(id);
            }
        }

        #endregion

        #region POST

        public int AddPlayer(string firstName, string secondName, int regNum, short number, DateTime date)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Player p = new Player();
                p.FirstName = firstName;
                p.SecondName = secondName;
                p.RegNum = regNum;
                p.Number = number;
                p.BirthDate = date.Date;
                p.Teams = new List<Team>();

                db.Insert(p);

                return p.RegNum;
            }
        }

        public void AddPlayers(List<PlayerModel> model)
        {
            foreach (var m in model)
            {
                try
                {
                    AddPlayer(m.FirstName, m.SecondName, m.RegNum, m.Number, m.BirthDate);
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion

        #region PUT

        public void AddPlayerToTeam(int playerId, int teamId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var player = db.GetWithChildren<Player>(playerId);

                var team = db.GetWithChildren<Team>(teamId);

                team.Players.Add(player);
                player.Teams.Add(team);

                AddStatisticsForPlayerInTeam(player, team, db);

                db.UpdateWithChildren(team);
                db.UpdateWithChildren(player);

            }
        }


        private void AddStatisticsForPlayerInTeam(Player player, Team team, SQLiteConnection db)
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

            }
        }

        public void AddPlayerToMatch(int playerId, int matchId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var player = db.GetWithChildren<Player>(playerId);
                //var matches = db.GetAllWithChildren<Match>();
                var match = db.GetWithChildren<Match>(matchId);

                if (!(player.Teams.Select(t => t.Id).Contains(match.HomeTeamId) || player.Teams.Select(t => t.Id).Contains(match.AwayTeamId)))
                {
                    throw new Exception("Player cannot be added to match!");
                }

                match.Players.Add(player);
                player.Matches.Add(match);

                db.UpdateWithChildren(match);
                db.UpdateWithChildren(player);

            }
        }


        public void AddPlayersAndTeams(Dictionary<int, List<int>> dict)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var players = db.GetAllWithChildren<Player>();
                var teams = db.GetAllWithChildren<Team>();

                foreach (var d in dict)
                {
                    Team team = teams.FirstOrDefault(t => t.Id == d.Key);

                    foreach (var palyerId in d.Value)
                    {
                        Player player = players.FirstOrDefault(p => p.RegNum == palyerId);

                        player.Teams.Add(team);
                        team.Players.Add(player);

                        AddStatisticsForPlayerInTeam(player, team, db);

                        db.UpdateWithChildren(player);
                        db.UpdateWithChildren(team);
                    }

                }

            }
        }

        public void AddPlayersAndMatches(Dictionary<int, List<int>> dict)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var players = db.GetAllWithChildren<Player>();
                var matches = db.GetAllWithChildren<Match>();

                foreach (var d in dict)
                {
                    Match match = matches.FirstOrDefault(m => m.Id == d.Key);

                    foreach (var playerId in d.Value)
                    {
                        Player player = players.FirstOrDefault(p => p.RegNum == playerId);

                        player.Matches.Add(match);
                        match.Players.Add(player);

                        db.UpdateWithChildren(player);
                        db.UpdateWithChildren(match);
                    }

                }

            }
        }

        public int UpdatePlayer(Player player)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                Player p = db.Find<Player>(player.RegNum);

                p.FirstName = player.FirstName;
                p.SecondName = player.SecondName;
                p.Number = player.Number;
                p.BirthDate = player.BirthDate;

                db.Update(p);

                return p.RegNum;
            }
        }

        #endregion


        #region DELETE

        public void RemovePlayerFromTeam(int playerId, int teamId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {

                var player = db.GetWithChildren<Player>(playerId);

                var team = db.GetWithChildren<Team>(teamId);

                RemoveStatisticsForPlayerInTeam(player, team, db);

                team.Players.Remove(player);
                player.Teams.Remove(team);


            }
        }

        public void RemovePlayerFromMatch(int playerId, int matchId)
        {
            using (var db = new SQLiteConnection(Platform, DatabasePath))
            {
                var player = db.GetWithChildren<Player>(playerId);

                var match = db.GetWithChildren<Match>(matchId);

                if (!(player.Teams.Select(t => t.Id).Contains(match.HomeTeamId) || player.Teams.Select(t => t.Id).Contains(match.AwayTeamId)))
                    throw new Exception("Player cannot be removed from match!");


                match.Players.Remove(player);
                player.Matches.Remove(match);

            }
        }

        private void RemoveStatisticsForPlayerInTeam(Player player, Team team, SQLiteConnection db)
        {

            var statisctics = db.GetAllWithChildren<Statistic>().Where(s => s.PlayerRegNum == player.RegNum && s.TeamId == team.Id);

            foreach (var s in statisctics)
            {
                db.Delete(s);
            }

        }

        #endregion

    }

}
