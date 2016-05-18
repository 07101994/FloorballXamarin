using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Floorball.LocalDB.Tables;
using Newtonsoft.Json;
using Floorball.LocalDB;
using Android.App;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Floorball.Droid.Activities;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "PlayerActivity")]
    public class PlayerActivity : AppCompatActivity
    {

        public Player Player { get; set; }

        public IEnumerable<Statistic> Statistics { get; set; }

        private IEnumerable<Team> Teams { get; set; }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Empty);

            Player = JsonConvert.DeserializeObject<Player>(Intent.GetStringExtra("player"));
            Statistics = Manager.GetStatisticsByPlayer(Player.RegNum);
            Teams = Statistics.Select(s => Manager.GetTeamById(s.TeamId)).GroupBy(t => t.Id).Select(g => g.First()).OrderByDescending(t => t.Year).ToList();
            int matchCount = Manager.GetMatchesByPlayer(Player.RegNum).Count();

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            FindViewById<TextView>(Resource.Id.toolbarTitle).Text = "Floorball";
            //SupportActionBar.SetHomeButtonEnabled(true);

            CreatePlayerStat(Teams, Statistics,matchCount, FindViewById<LinearLayout>(Resource.Id.linearlayout));

        }

        private void CreatePlayerStat(IEnumerable<Team> Teams, IEnumerable<Statistic> statistics, int matchCount, LinearLayout container)
        {

            foreach (var team in Teams)
            {
                ViewGroup stat = LayoutInflater.Inflate(Resource.Layout.Stat, container, false) as ViewGroup;
                stat.FindViewById<TextView>(Resource.Id.headerName).Text = team.Name + " (" + team.Year.Year + "-" + (team.Year.Year+1) + ")";

                LinearLayout statCard = stat.FindViewById<LinearLayout>(Resource.Id.statCard);

                ViewGroup goals = LayoutInflater.Inflate(Resource.Layout.StatLine,statCard,false) as ViewGroup;
                goals.FindViewById<TextView>(Resource.Id.statLabel).Text = "Gólok: ";
                goals.FindViewById<TextView>(Resource.Id.statNumber).Text = statistics.Where(s => s.TeamId == team.Id && s.Name == "G").First().Number.ToString();
                statCard.AddView(goals);


                ViewGroup assists = LayoutInflater.Inflate(Resource.Layout.StatLine, statCard, false) as ViewGroup;
                assists.FindViewById<TextView>(Resource.Id.statLabel).Text = "Asszisztok: ";
                assists.FindViewById<TextView>(Resource.Id.statNumber).Text = statistics.Where(s => s.TeamId == team.Id && s.Name == "A").First().Number.ToString();
                statCard.AddView(assists);

                ViewGroup penalties = LayoutInflater.Inflate(Resource.Layout.StatLine, statCard, false) as ViewGroup;
                penalties.FindViewById<TextView>(Resource.Id.statLabel).Text = "Kiállítások: ";
                int penaltySum = 0;
                penaltySum += statistics.Where(s => s.TeamId == team.Id && s.Name == "P2").First().Number * 2;
                penaltySum += statistics.Where(s => s.TeamId == team.Id && s.Name == "P5").First().Number * 5;
                int p10 = statistics.Where(s => s.TeamId == team.Id && s.Name == "P10").First().Number * 10;
                penaltySum += p10;
                penalties.FindViewById<TextView>(Resource.Id.statNumber).Text = penaltySum.ToString() + " (" + p10 + ")";
                statCard.AddView(penalties);

                ViewGroup matches = LayoutInflater.Inflate(Resource.Layout.StatLine, statCard, false) as ViewGroup;
                matches.FindViewById<TextView>(Resource.Id.statLabel).Text = "Mérkőzés szám: ";
                matches.FindViewById<TextView>(Resource.Id.statNumber).Text = matchCount.ToString();
                statCard.AddView(matches);

                container.AddView(stat);
            }

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:

                    Finish();

                    return true;

                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }


    }
}