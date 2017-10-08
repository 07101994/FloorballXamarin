﻿using SQLite;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    public class League
    {

        [PrimaryKey]//, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Year { get; set; }

        public LeagueTypeEnum Type { get; set; }

        public ClassEnum Class { get; set; }

        public short Rounds { get; set; }

        public CountriesEnum Country { get; set; }

		public GenderEnum Gender { get; set; }

    }
}
