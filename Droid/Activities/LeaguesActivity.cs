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
using Floorball.Droid.Utils;

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
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.Leagues, TabTitle = Resources.GetString(Resource.String.men), Data = new LeaguesModel { Leagues = leagues.Where(l => l.Sex == "ferfi").ToList() } });
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.Leagues, TabTitle = Resources.GetString(Resource.String.women), Data = new LeaguesModel { Leagues = leagues.Where(l => l.Sex == "noi").ToList() } });

                Android.Support.V4.App.Fragment fr = TabbedViewPagerFragment.Instance(tabModels);
                Android.Support.V4.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
                ft.Add(Resource.Id.content_frame, fr).Commit();
            }
        }

        protected override void InitProperties()
        {
            base.InitProperties();

            Year = Intent.GetObject<DateTime>("year");
            YearString = Year.Year + " - " + (Year.Year + 1); 

        }
    }
}