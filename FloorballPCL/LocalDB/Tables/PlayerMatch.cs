using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    class PlayerMatch
    {

        [ForeignKey(typeof(Player))]
        public int PlayerId { get; set; }

        [ForeignKey(typeof(Match))]
        public int MatchId { get; set; }


    }
}
