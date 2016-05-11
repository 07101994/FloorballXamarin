using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    public class Statistic
    {

        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public short Number { get; set; }

        //Relationships

        [ForeignKey(typeof(Player))]
        public int PlayerRegNum { get; set; }

        [ForeignKey(typeof(Team))]
        public int TeamId { get; set; }
    }
}
