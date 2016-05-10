using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    public class PlayerTeam
    {

        [ForeignKey(typeof(Player))]
        public int PlayerId { get; set; }

        [ForeignKey(typeof(Team))]
        public int TeamId { get; set; }

    }
}
