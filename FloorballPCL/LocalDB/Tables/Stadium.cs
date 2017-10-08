using SQLite;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    public class Stadium
    {

        [PrimaryKey]//, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
		
        public string PostCode { get; set; }

		public string Country { get; set; }

		public string City { get; set; }

    }
}
