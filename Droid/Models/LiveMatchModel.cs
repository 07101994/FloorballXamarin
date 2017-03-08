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
    public class LiveMatchModel : LiveMatchModelBase
    {
        public int MatchId { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string HomeScoreText
        {
            get
            {
                return HomeScore.ToString();
            }
        }
        public string AwayScoreText
        {
            get
            {
                return AwayScore.ToString();
            }
        }
        public StateEnum State { get; set; }

        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}