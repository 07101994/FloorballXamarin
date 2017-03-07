using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Floorball.LocalDB.Tables;

namespace Floorball.Droid.Models
{
    public class LeagueStatModel
    {
        public IEnumerable<PlayerStatisticsModel> Stats { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<Team> Teams { get; set; }
    }
}