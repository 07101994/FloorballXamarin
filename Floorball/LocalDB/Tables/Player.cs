﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    public class Player
    {
        //[PrimaryKey, AutoIncrement]
        public int RegNum { get; set; }

        public string Name { get; set; }

        public short Number { get; set; }

        public DateTime BirthDate { get; set; }

        //Relationships

        [ManyToMany(typeof(PlayerMatch))]
        public List<Match> Matches { get; set; }

        [ManyToMany(typeof(PlayerTeam))]
        public List<Team> Teams { get; set; }
    }
}
