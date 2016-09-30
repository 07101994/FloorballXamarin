using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Floorball.Droid.Adapters;
using FloorballServer.Models.Floorball;
using Floorball.REST;
using Floorball.Droid.Activities;
using System.Globalization;
using Java.Lang;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using Newtonsoft.Json;
using Android.Support.V4.View;

namespace Floorball.Droid.Fragments
{
    public class LeaguesFragment : ViewPagerWithTabs
    {

        public IEnumerable<League> Leagues { get; set; }

        public IEnumerable<League> ActualLeagues { get; set; }

        public static LeaguesFragment Instance()
        {
            return new LeaguesFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            PagerType = PagerFragmentType.Leagues;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = base.OnCreateView(inflater, container, savedInstanceState);

            root.FindViewById<TextView>(Resource.Id.title).Text = Resources.GetString(Resource.String.league);

            //leaguesListView = root.FindViewById<ListView>(Resource.Id.leaguesList);

            //try
            //{
            //    Leagues = Manager.GetAllLeague();
            //    ActualLeagues = Leagues.Where(l => l.Year.Year.ToString() == years.ElementAt(ActualFragmentIndex)).ToList();
            //    leaguesListView.Adapter = new LeaguesAdapter(Context, ActualLeagues.ToList());
            //    leaguesListView.ItemClick += (e, p) => {

            //        Intent intent = new Intent(Context, typeof(LeagueActivity));
            //        intent.PutExtra("league", JsonConvert.SerializeObject(ActualLeagues.ElementAt(p.Position)));
            //        StartActivity(intent);
            //    };

            //}
            //catch (Java.Lang.Exception)
            //{

            //}

            return root;
        }

        //public override void YearViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        //{
        //    base.YearViewPager_PageSelected(sender, e);

        //    ActualLeagues = Leagues.Where(l => l.Year.Year.ToString() == years.ElementAt(ActualFragmentIndex)).ToList();
        //    leaguesListView.Adapter = new LeaguesAdapter(Context, ActualLeagues.ToList());

        //}

        public override void listItemSelected(string newYear)
        {
            //Activity.FindViewById<Button>(Resource.Id.yearsbutton).Text = newYear;
            //ActualLeagues = Leagues.Where(l => l.Year.Year.ToString() == newYear).ToList();
            //leaguesListView.Adapter = new LeaguesAdapter(Context, ActualLeagues.ToList());
        }

    }
}