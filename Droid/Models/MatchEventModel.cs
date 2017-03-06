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

namespace Floorball.Droid.Models
{
    public class MatchEventModel
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public string Player { get; set; }
        public int ResourceId { get; set; }
        public int ViewType { get; set; }
       
    }
}