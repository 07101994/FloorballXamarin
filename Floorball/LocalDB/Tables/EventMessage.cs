using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    public class EventMessage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

    }
}
