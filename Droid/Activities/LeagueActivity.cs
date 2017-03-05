using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.App;
using Floorball.Droid.Fragments;
using Android.Support.V4.View;
using Java.Lang;
using Android.Support.Design.Widget;
using FloorballServer.Models.Floorball;
using Floorball.REST;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using Newtonsoft.Json;
using Floorball.Droid.Models;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "LeagueActivity")]
    public class LeagueActivity : FloorballActivity
    {

        public League League { get; set; }

        public IEnumerable<Team> Teams { get; set; }

        public IEnumerable<Match> Matches { get; set; }

        public IEnumerable<Statistic> Statistics { get; set; }

        public IEnumerable<PlayerStatisticsModel> PlayerStatistics { get; set; }

        public IEnumerable<Player> Players { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.League);

            //Initialize the toolbar
            InitToolbar();

            //Init activity properties
            InitActivityProperties();

            //Initialize properties
            InitProperties();

            //Attach tabbedfragment
            if (savedInstanceState == null)
            {

                var tabModels = new List<TabbedViewPagerModel>();
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.LeagueMatches, TabTitle = "Mérkőzések", Data = new MatchesModel { Teams = Teams, Matches = Matches, Leagues = new List<League> { League } } });
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.Table, TabTitle = "Tabella", Data = null });
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.Stats, TabTitle = "Statisztikák", Data = null });

                Android.Support.V4.App.Fragment fr = TabbedViewPagerFragment.Instance(tabModels);
                Android.Support.V4.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
                ft.Add(Resource.Id.content_frame, fr).Commit();
            }

            FindViewById<TextView>(Resource.Id.leagueName).Text = League.Name;
        }

        protected override void InitProperties()
        {
            base.InitProperties();

            League = JsonConvert.DeserializeObject<League>(Intent.GetStringExtra("league"));
            Teams = UoW.TeamRepo.GetTeamsByLeague(League.Id);
            Matches = UoW.MatchRepo.GetMatchesByLeague(League.Id);
            Statistics = UoW.StatiscticRepo.GetStatisticsByLeague(League.Id);
            PlayerStatistics = PlayerStatisticsMaker.CreatePlayerStatistics(Statistics);
            Players = UoW.PlayerRepo.GetPlayersByLeague(League.Id);
        }

        protected override void InitActivityProperties()
        {

        }

    }
}