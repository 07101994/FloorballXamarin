using SQLite;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    public class Referee
    {

        [PrimaryKey]//, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public short Number { get; set; }

        public short Penalty { get; set; }

        //Relationship
        [ManyToMany(typeof(RefereeMatch))]
        public List<Match> Matches { get; set; }

    }
}
