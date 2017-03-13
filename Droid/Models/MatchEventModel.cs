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
    public class MatchEventModel
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public Player Player { get; set; }
        public EventMessage EventMessage { get; set; }
        public MatchEventModel Assist { get; set; }
        public int ResourceId { get; set; }
        public int ViewType { get; set; }
        public bool IsGoal { get; set; }

    }
}