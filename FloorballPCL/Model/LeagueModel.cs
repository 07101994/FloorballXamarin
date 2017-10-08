﻿using Floorball;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FloorballServer.Models.Floorball
{
    public class LeagueModel
    {

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