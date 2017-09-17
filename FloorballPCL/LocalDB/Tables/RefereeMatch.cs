using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Floorball.LocalDB.Tables
{
    class RefereeMatch
    {

        [ForeignKey(typeof(Referee))]
        public int RefereeId { get; set; }

        [ForeignKey(typeof(Match))]
        public int MatchId { get; set; }

    }
}
