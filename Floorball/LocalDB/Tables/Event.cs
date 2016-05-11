using SQLite;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    public class Event
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Type { get; set; }

        public string Time { get; set; }

        //Relationships

        [ForeignKey(typeof(Match))]
        public int MatchId { get; set; }

        [ForeignKey(typeof(Player))]
        public int PlayerId { get; set; }

        [ForeignKey(typeof(EventMessage))]
        public int EventMessageId { get; set; }

        [ForeignKey(typeof(Team))]
        public int TeamId { get; set; }
    }
}
