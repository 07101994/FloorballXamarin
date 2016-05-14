using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Floorball.Droid.Fragments;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.App;
using FloorballServer.Models.Floorball;
using Newtonsoft.Json;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "TeamActivity")]
    public class TeamActivity : AppCompatActivity
    {

        TeamPageAdapter pagerAdapter;

        public Team Team { get; set; }

        public List<Player> Players { get; set; }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TeamActivity);

            ViewPager pager = FindViewById<ViewPager>(Resource.Id.teamPager);
            pagerAdapter = new TeamPageAdapter(SupportFragmentManager);
            pager.Adapter = pagerAdapter;

            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.teamtabs);
            tabs.SetupWithViewPager(pager);

            Team =  JsonConvert.DeserializeObject<Team>(Intent.GetStringExtra("team"));
            Players = Manager.GetPlayersByTeam(Team.Id).ToList();


            SupportActionBar.Title = Team.Name;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            FindViewById<TextView>(Resource.Id.coachName).Text = Team.Coach;
            FindViewById<TextView>(Resource.Id.stadiumName).Text = Manager.GetStadiumById(Team.StadiumId).Name;


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


        public class TeamPageAdapter : Android.Support.V4.App.FragmentPagerAdapter
        {
            public TeamPageAdapter(Android.Support.V4.App.FragmentManager manager) : base(manager)
            {

            }

            public override int Count
            {
                get
                {
                    return 2;
                }
            }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                switch (position)
                {
                    case 0:
                        return new PlayerListFragment();
                    case 1:
                        return new MatchListFragment();
                    default:
                        return null;
                }
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                switch (position)
                {
                    case 0:
                        return new Java.Lang.String("Játékosok");
                    case 1:
                        return new Java.Lang.String("Mérkőzések");
                    default:
                        return null;
                }

            }


        }
    }
}