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
using Floorball.LocalDB.Tables;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "TeamsActivity")]
    public class TeamsActivity : FloorballActivity
    {
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            // Create your application here
            SetContentView(Resource.Layout.Teams);

            //Initilalize toolbar
            InitToolbar();

            //Init properties
            InitProperties();

            //Init activity properties
            InitActivityProperties();

            //Attach tabbedfragment
            if (savedInstanceState == null)
            {
                IEnumerable<Team> teams = UoW.TeamRepo.GetTeamsByYear(Year);
                IEnumerable<League> leagues = UoW.LeagueRepo.GetAllLeague();

                var tabModels = new List<TabbedViewPagerModel>();
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.Teams, TabTitle = "férfi", Data = new TeamsModel { Teams = teams.Where(t => t.Sex == "ferfi").ToList(), Leagues = leagues.ToList() } });
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.Teams, TabTitle = "női", Data = new TeamsModel { Teams = teams.Where(l => l.Sex == "noi").ToList(), Leagues = leagues.ToList() } } );

                Android.Support.V4.App.Fragment fr = TabbedViewPagerFragment.Instance(tabModels);
                Android.Support.V4.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
                ft.Add(Resource.Id.content_frame, fr).Commit();
            }
        }

        public string YearString { get; set; }

        public DateTime Year { get; set; }

        protected override void InitProperties()
        {
            base.InitProperties();

            Year = new DateTime(Convert.ToInt16(Intent.GetStringExtra("year")), 1, 1);
            YearString = Year.Year + " - " + (Year.Year + 1);

        }

        protected override void InitActivityProperties()
        {

            FindViewById<TextView>(Resource.Id.year).Text = YearString;
        }
    }
}