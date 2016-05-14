using SQLite;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    public class Team
    {
        [PrimaryKey]//, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Year { get; set; }

        public string Coach { get; set; }

        public short Points { get; set; }

        public short Standing { get; set; }

        public int TeamId { get; set; }

        public short Match { get; set; }

        public short Scored { get; set; }

        public short Get { get; set; }

        public string Sex { get; set; }

        //Relationships

        [ForeignKey(typeof(Stadium))]
        public int StadiumId { get; set; }

        [ForeignKey(typeof(League))]
        public int LeagueId { get; set; }

        [ManyToMany(typeof(PlayerTeam))]
        public List<Player> Players { get; set; }


    }
}
