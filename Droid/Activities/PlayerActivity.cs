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
            //TeamYear = Statistics.Select(s => new { s.TeamId, Manager.GetTeamById(s.TeamId).Year.Year }).OrderByDescending(t => t.Year).ToDictionary(t => t.TeamId, t => t.Year);
            Teams = Statistics.Select(s => Manager.GetTeamById(s.TeamId)).GroupBy(t => t.Id).Select(g => g.First()).OrderByDescending(t => t.Year).ToList();
            int matchCount = Manager.GetMatchesByPlayer(Player.RegNum).Count();


            SupportActionBar.Title = Player.Name;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            CreatePlayerStat(Teams, Statistics,matchCount, FindViewById<LinearLayout>(Resource.Id.linearlayout));

        }

        private void CreatePlayerStat(IEnumerable<Team> Teams, IEnumerable<Statistic> statistics, int matchCount, LinearLayout container)
        {

            foreach (var team in Teams)
            {
                ViewGroup statView = LayoutInflater.Inflate(Resource.Layout.Stat, container, false) as ViewGroup;

                statView.FindViewById<TextView>(Resource.Id.headerName).Text = team.Name + " (" + team.Year.Year + "-" + (team.Year.Year+1) + ")";

                ViewGroup card = statView.FindViewById<LinearLayout>(Resource.Id.statLinearLayout);

                ViewGroup goals = LayoutInflater.Inflate(Resource.Layout.StatLine,card,false) as ViewGroup;
                goals.FindViewById<TextView>(Resource.Id.statLabel).Text = "Gólok: ";
                goals.FindViewById<TextView>(Resource.Id.statNumber).Text = statistics.Where(s => s.TeamId == team.Id && s.Name == "G").First().Number.ToString();
                card.AddView(goals);


                ViewGroup assists = LayoutInflater.Inflate(Resource.Layout.StatLine, card, false) as ViewGroup;
                goals.FindViewById<TextView>(Resource.Id.statLabel).Text = "Asszisztok: ";
                goals.FindViewById<TextView>(Resource.Id.statNumber).Text = statistics.Where(s => s.TeamId == team.Id && s.Name == "A").First().Number.ToString();
                card.AddView(assists);

                //ViewGroup penalties = LayoutInflater.Inflate(Resource.Layout.StatLine, null, false) as ViewGroup;
                //goals.FindViewById<TextView>(Resource.Id.statLabel).Text = "Kiállítások: ";
                //int penaltySum = 0;
                //penaltySum += statistics.Where(s => s.TeamId == team.Id && s.Name == "P2").First().Number * 2;
                //penaltySum += statistics.Where(s => s.TeamId == team.Id && s.Name == "P5").First().Number * 5;
                //int p10 = statistics.Where(s => s.TeamId == team.Id && s.Name == "P10").First().Number * 10;
                //penaltySum += p10;
                //goals.FindViewById<TextView>(Resource.Id.statNumber).Text = penaltySum.ToString() + " (" + p10 + ")";
                //card.AddView(penalties);

                //ViewGroup matches = LayoutInflater.Inflate(Resource.Layout.StatLine, card, false) as ViewGroup;
                //goals.FindViewById<TextView>(Resource.Id.statLabel).Text = "Mérkőzés szám: ";
                //goals.FindViewById<TextView>(Resource.Id.statNumber).Text = matchCount.ToString();
                //card.AddView(matches);

                container.AddView(statView);
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