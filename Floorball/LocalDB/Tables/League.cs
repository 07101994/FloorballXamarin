using SQLite;
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

        public string Type { get; set; }

        public string ClassName { get; set; }

        public int Rounds { get; set; }
    }
}
