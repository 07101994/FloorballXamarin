using Floorball;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FloorballServer.Models.Floorball
{
    public class RefereeModel
    {

		public int Id { get; set; }

		public string Name { get; set; }

		public short Number { get; set; }

		public short Penalty { get; set; }

        public CountriesEnum Country { get; set; }
    }
}