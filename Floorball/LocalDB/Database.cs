using FloorballServer.Models.Floorball;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB
{
    public class Database
    {

        public List<EventMessageModel> EventMessages { get; set; }

        public List<LeagueModel> Leagues { get; set; }

        public List<RefereeModel> Referees { get; set; }

        public List<PlayerModel> Players { get; set; }

        public List<StadiumModel> Stadiums { get; set; }

        public List<TeamModel> Teams { get; set; }

        public List<MatchModel> Matches { get; set; }

        public Dictionary<int, List<int>> PlayersAndTeams { get; set; }

        public Dictionary<int, List<int>> PlayersAndMatches { get; set; }

        public Dictionary<int, List<int>> RefereesAndMatches { get; set; }

        public List<EventModel> Events { get; set; }
    }
}
