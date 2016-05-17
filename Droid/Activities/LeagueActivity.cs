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
    public class LeagueActivity : Android.Support.V7.App.AppCompatActivity
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

            ViewPager pager = FindViewById<ViewPager>(Resource.Id.leaguePager);
            pagerAdapter = new LeaguePageAdapter(SupportFragmentManager);
            pager.Adapter = pagerAdapter;

            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.leaguetabs);
            tabs.SetupWithViewPager(pager);

            League = JsonConvert.DeserializeObject<League>(Intent.GetStringExtra("league"));

            SupportActionBar.Title = League.Name;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            Teams = Manager.GetTeamsByLeague(League.Id);

            Matches = Manager.GetMatchesByLeague(League.Id);

            Statistics = Manager.GetStatisticsByLeague(League.Id);

            PlayerStatistics = PlayerStatisticsMaker.CreatePlayerStatistics(Statistics);

            Players = Manager.GetPlayersByLeague(League.Id);
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
                        return new LeagueMatchesFragment();
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