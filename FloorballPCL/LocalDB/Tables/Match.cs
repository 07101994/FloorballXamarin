using SQLite;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    public class Match
    {

        [PrimaryKey]//, AutoIncrement]
        public int Id { get; set; }

        //public string Date { get; set; }
        public DateTime Date { get; set; }

        public short Round { get; set; }

        public StateEnum State { get; set; }

        public short GoalsH { get; set; }

        public short GoalsA { get; set; }

        public TimeSpan Time { get; set; }

        //Relationships

        [ForeignKey(typeof(Team))]
        public int HomeTeamId { get; set; }

        [ForeignKey(typeof(Team))]
        public int AwayTeamId { get; set; }

        [ForeignKey(typeof(League))]
        public int LeagueId { get; set; }

        [ForeignKey(typeof(Stadium))]
        public int StadiumId { get; set; }

        [ManyToMany(typeof(PlayerMatch))]
        public List<Player> Players { get; set; }

        [ManyToMany(typeof(RefereeMatch))]
        public List<Referee> Referees { get; set; }

    }
}
