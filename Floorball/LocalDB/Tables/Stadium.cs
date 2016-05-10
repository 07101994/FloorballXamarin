using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    public class Stadium
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
