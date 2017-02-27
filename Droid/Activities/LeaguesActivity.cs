using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Floorball.Droid.Adapters;
using Floorball.LocalDB.Tables;
using Android.Support.V4.App;
using Android.App;
using Floorball.Droid.Models;
using Floorball.Droid.Fragments;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "LeaguesActivity")]
    public class LeaguesActivity : FloorballActivity
    {

        public string YearString { get; set; }

        public DateTime Year { get; set; }

        protected override void InitActivityProperties()
        {

            FindViewById<TextView>(Resource.Id.year).Text = YearString;
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Leagues);

            //Initilalize toolbar
            InitToolbar();

            //Init properties
            InitProperties();

            //Init activity properties
            InitActivityProperties();

            //Attach tabbedfragment
            if (savedInstanceState == null)
            {
                IEnumerable<League> leagues = UoW.LeagueRepo.GetLeaguesByYear(Year);

                var tabModels = new List<TabbedViewPagerModel>();
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.Leagues, TabTitle = "férfi", Data = new LeaguesModel { Leagues = leagues.Where(l => l.Sex == "ferfi").ToList() } });
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.Leagues, TabTitle = "női", Data = new LeaguesModel { Leagues = leagues.Where(l => l.Sex == "noi").ToList() } });

                Android.Support.V4.App.Fragment fr = TabbedViewPagerFragment.Instance(tabModels);
                Android.Support.V4.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
                ft.Add(Resource.Id.content_frame, fr).Commit();
            }
        }

        protected override void InitProperties()
        {
            base.InitProperties();

            Year = new DateTime(Convert.ToInt16(Intent.GetStringExtra("year")), 1, 1);
            YearString = Year.Year + " - " + (Year.Year + 1); 

        }
    }
}