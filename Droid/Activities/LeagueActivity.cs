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

namespace Floorball.Droid.Activities
{
    [Activity(Label = "LeagueActivity")]
    public class LeagueActivity : FloorballActivity
    {

        LeaguePageAdapter pagerAdapter;

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
            ViewPager pager = FindViewById<ViewPager>(Resource.Id.pager);
            pagerAdapter = new LeaguePageAdapter(SupportFragmentManager);
            pager.Adapter = pagerAdapter;

            FindViewById<TabLayout>(Resource.Id.tabs).SetupWithViewPager(pager);
        }

        public class LeaguePageAdapter : FragmentPagerAdapter
        {
            public LeaguePageAdapter(Android.Support.V4.App.FragmentManager manager) : base(manager)
            {

            }

            public override int Count
            {
                get
                {
                    return 3;
                }
            }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                switch (position)
                {
                    case 0:

                        return null;
                        //return LeagueMatchesFragment.Instance;
                    case 1:
                        return new LeagueTableFragment();
                    case 2:
                        return new LeagueStatisticsFragment();
                    default:
                        return null;
                }
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                switch (position)
                {
                    case 0:
                        return new Java.Lang.String("Mérkőzések");
                    case 1:
                        return new Java.Lang.String("Tabella");
                    case 2:
                        return new Java.Lang.String("Statisztikák");
                    default:
                        return null;
                }
             
            }

            
        }


    }
}