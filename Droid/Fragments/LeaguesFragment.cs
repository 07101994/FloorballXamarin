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

namespace Floorball.Droid.Fragments
{
    public class LeaguesFragment : MainFragment
    {
        ListView leaguesListView;
        List<string> years;

        public IEnumerable<League> Leagues { get; set; }

        public IEnumerable<League> ActualLeagues { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View root = inflater.Inflate(Resource.Layout.LeaugesFragment, container, false);
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return root;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnStart()
        {
            base.OnStart();

            leaguesListView = Activity.FindViewById<ListView>(Resource.Id.leaguesList);

            try
            {
                Button button = Activity.FindViewById<Button>(Resource.Id.yearsbutton);
                //years = RESTHelper.GetAllYear();
                years = Manager.GetAllYear().Select(y => y.Year.ToString()).ToList();
                button.Text = years.First();
                //years = new List<string>(new string[] { "2011","2012","2013","2014","2015"});

                button.Click += delegate
                {

                    ListDialogFragment listDialogFragment = new ListDialogFragment(years);
                    listDialogFragment.Show(Activity.SupportFragmentManager, "listdialog");

                };

                //Leagues = RESTHelper.GetAllLeague();
                Leagues = Manager.GetAllLeague();
                ActualLeagues = Leagues.Where(l => l.Year.Year.ToString() == button.Text).ToList();
                leaguesListView.Adapter = new LeaguesAdapter(Context, ActualLeagues.ToList());
                leaguesListView.ItemClick += (e, p) => {

                    Intent intent = new Intent(Context,typeof(LeagueActivity));
                    intent.PutExtra("league",JsonConvert.SerializeObject(ActualLeagues.ElementAt(p.Position)));
                    StartActivity(intent);
                };

            } catch (Java.Lang.Exception e)
            {

            }
            
        }

        public override void listItemSelected(string newYear)
        {
            Activity.FindViewById<Button>(Resource.Id.yearsbutton).Text = newYear;
            ActualLeagues = Leagues.Where(l => l.Year.Year.ToString() == newYear).ToList();
            leaguesListView.Adapter = new LeaguesAdapter(Context, ActualLeagues.ToList());
        }

        

    }
}