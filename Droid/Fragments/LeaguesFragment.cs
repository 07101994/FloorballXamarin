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
    public class LeaguesFragment : MainFragment
    {
        ListView leaguesListView;
        List<string> years;

        public int ActualFragmentIndex { get; set; }

        public ViewPager YearViewPager { get; set; }

        private YearFragmentStatePageAdapter YearViewPagerAdapter { get; set; }

        public IEnumerable<League> Leagues { get; set; }

        public IEnumerable<League> ActualLeagues { get; set; }

        public static LeaguesFragment Instance()
        {
            return new LeaguesFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            years = Manager.GetAllYear().Select(y => y.Year.ToString()).ToList();
            ActualFragmentIndex = 0;
            

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View root = inflater.Inflate(Resource.Layout.LeaugesFragment, container, false);

            YearViewPager = root.FindViewById<ViewPager>(Resource.Id.yearPager);
            YearViewPager.Adapter = new YearFragmentStatePageAdapter(Activity.SupportFragmentManager, years);
            YearViewPager.PageSelected += YearViewPager_PageSelected;

            root.FindViewById<ImageView>(Resource.Id.rightArrow).Click += RightArrowClicked;
            root.FindViewById<ImageView>(Resource.Id.leftArrow).Click += LeftArrowClicked;

            leaguesListView = root.FindViewById<ListView>(Resource.Id.leaguesList);

            try
            {
                Leagues = Manager.GetAllLeague();
                ActualLeagues = Leagues.Where(l => l.Year.Year.ToString() == years.ElementAt(ActualFragmentIndex)).ToList();
                leaguesListView.Adapter = new LeaguesAdapter(Context, ActualLeagues.ToList());
                leaguesListView.ItemClick += (e, p) => {

                    Intent intent = new Intent(Context, typeof(LeagueActivity));
                    intent.PutExtra("league", JsonConvert.SerializeObject(ActualLeagues.ElementAt(p.Position)));
                    StartActivity(intent);
                };

            }
            catch (Java.Lang.Exception)
            {

            }

            return root;
        }

        private void YearViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            ActualFragmentIndex = e.Position;
            ActualLeagues = Leagues.Where(l => l.Year.Year.ToString() == years.ElementAt(ActualFragmentIndex)).ToList();
            leaguesListView.Adapter = new LeaguesAdapter(Context, ActualLeagues.ToList());
        }

        private void LeftArrowClicked(object sender, EventArgs e)
        {
            if (ActualFragmentIndex > 0)
            {
                //ActualFragmentIndex--;
                YearViewPager.CurrentItem = ActualFragmentIndex - 1;

                //ActualLeagues = Leagues.Where(l => l.Year.Year.ToString() == years.ElementAt(ActualFragmentIndex)).ToList();
                //leaguesListView.Adapter = new LeaguesAdapter(Context, ActualLeagues.ToList());
            }
        }

        private void RightArrowClicked(object sender, EventArgs e)
        {
            if (ActualFragmentIndex < years.Count - 1)
            {
                //ActualFragmentIndex++;
                YearViewPager.CurrentItem = ActualFragmentIndex + 1;

                //ActualLeagues = Leagues.Where(l => l.Year.Year.ToString() == years.ElementAt(ActualFragmentIndex)).ToList();
                //leaguesListView.Adapter = new LeaguesAdapter(Context, ActualLeagues.ToList());
            }
        }

        public override void listItemSelected(string newYear)
        {
            //Activity.FindViewById<Button>(Resource.Id.yearsbutton).Text = newYear;
            //ActualLeagues = Leagues.Where(l => l.Year.Year.ToString() == newYear).ToList();
            //leaguesListView.Adapter = new LeaguesAdapter(Context, ActualLeagues.ToList());
        }

    }
}