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
using Floorball.Droid.Adapters;
using Floorball.Droid.Models;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "PlayerActivity")]
    public class PlayerActivity : FloorballActivity
    {

        public Player Player { get; set; }

        public IEnumerable<Statistic> Statistics { get; set; }

        private IEnumerable<Team> Teams { get; set; }

        RecyclerView recyclerView;
        PlayerStatsAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PlayerStat);

            //Initialize toolbar
            InitToolbar();

            //Initialize properties
            InitProperties();

            FindViewById<TextView>(Resource.Id.playerName).Text = Player.Name;
            FindViewById<TextView>(Resource.Id.birthDate).Text = Player.BirthDate.ToShortDateString();
            FindViewById<TextView>(Resource.Id.regNum).Text = Player.RegNum.ToString();

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            adapter = new PlayerStatsAdapter(CreateStatModels());
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerView.SetAdapter(adapter);

        }

        private List<PlayerStatModel> CreateStatModels()
        {
            List<PlayerStatModel> stats = new List<PlayerStatModel>();

            foreach (var stat in UoW.StatiscticRepo.GetStatisticsByPlayer(Player.RegNum).GroupBy(s => s.TeamId).Select(s => s.ToList()))
            {
                var team = Teams.First(t => stat.First().TeamId == t.Id);

                stats.Add(new PlayerStatModel
                {
                    Stats = stat,
                    TeamName = team.Name,
                    Year = team.Year,
                    MatchCount = Player.Matches.Count().ToString()
                });
            }

            return stats;
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

        protected override void InitProperties()
        {
            base.InitProperties();

            Player = JsonConvert.DeserializeObject<Player>(Intent.GetStringExtra("player"));
            Statistics = UoW.StatiscticRepo.GetStatisticsByPlayer(Player.RegNum);
            Teams = Statistics.Select(s => UoW.TeamRepo.GetTeamById(s.TeamId)).GroupBy(t => t.Id).Select(g => g.First()).OrderByDescending(t => t.Year).ToList();
        }

        protected override void InitActivityProperties()
        {
            throw new NotImplementedException();
        }
    }
}