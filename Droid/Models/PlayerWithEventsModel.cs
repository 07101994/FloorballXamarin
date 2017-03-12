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
    public class PlayerWithEventsModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public IEnumerable<Event> Events { get; set; }

        public int Goals
        {
            get
            {
                return Events.Count(e => e.Type == "G");
            }
        }

        public int Assists
        {
            get
            {
                return Events.Count(e => e.Type == "A");
            }
        }

        public int P2
        {
            get
            {
                return Events.Count(e => e.Type == "P2");
            }
        }

        public int P5
        {
            get
            {
                return Events.Count(e => e.Type == "P5");
            }
        }

        public int P10
        {
            get
            {
                return Events.Count(e => e.Type == "P10");
            }
        }

        public int PV
        {
            get
            {
                return Events.Count(e => e.Type == "PV");
            }
        }

    }
}