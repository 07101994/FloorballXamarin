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
    public class MatchPlayersModel
    {
        public PlayerWithEventsModel HomePlayer { get; set; }
        public PlayerWithEventsModel AwayPlayer { get; set; }
    }
}