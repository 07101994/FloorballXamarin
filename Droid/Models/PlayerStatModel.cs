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
    public class PlayerStatModel
    {
        public string TeamName { get; set; }
        public string LeagueYear
        {
            get
            {
                return Year.Year.ToString() + " - " + (Year.Year + 1).ToString();
            }
        }
        public string Goals
        {
            get
            {
                return Stats.First(s => s.Name == "G").Number.ToString();
            }
        }
        public string Assists
        {
            get
            {
                return Stats.First(s => s.Name == "A").Number.ToString();
            }
        }
        public string Penalties
        {
            get
            {
                int penaltySum = 0;
                penaltySum += Stats.Where(s => s.Name == "P2").First().Number * 2;
                penaltySum += Stats.Where(s => s.Name == "P5").First().Number * 5;
                int p10 = Stats.Where(s => s.Name == "P10").First().Number * 10;
                penaltySum += p10;

                return penaltySum + " (" + p10 + ") perc";
            }
        }
        public string MatchCount { get; set; }

        public DateTime Year { get; set; }

        public List<Statistic> Stats { get; set; }

    }
}