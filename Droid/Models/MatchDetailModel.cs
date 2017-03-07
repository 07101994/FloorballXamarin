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
    public class MatchDetailModel
    {
        public Match Match { get; set; }
        public League League { get; set; }
        public Stadium Stadium { get; set; }
    }
}