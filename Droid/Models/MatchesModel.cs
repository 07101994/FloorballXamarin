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
    public class MatchesModel
    {
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Match> Matches { get; set; }
        public IEnumerable<League> Leagues { get; set; }

        public int TeamId { get; set; }
    }
}