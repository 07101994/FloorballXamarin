using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Floorball.LocalDB.Tables;
using Floorball.LocalDB;
using Floorball.Droid.Adapters;
using Newtonsoft.Json;
using Floorball.Droid.Activities;

namespace Floorball.Droid.Fragments
{
    public class LeaguePagerFragment : PagerFragment
    {

        public ListView LeaguesListView { get; set; }

        public IEnumerable<League> Leagues { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (PageCount == 0)
            {
                Leagues = (Activity as MainActivity).Leagues.Where(l => l.Year.Year == Year);
            }
            else
            {
                Leagues = (Activity as MainActivity).Leagues.Where(l => l.Year.Year == Year);
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.LeaguesPageFragment, container, false);

            LeaguesListView = root.FindViewById<ListView>(Resource.Id.leaguesList);

            try
            {
                //TODO nemre szűrés

                LeaguesListView.Adapter = new LeaguesAdapter(Context, Leagues.ToList());
                LeaguesListView.ItemClick += (e, p) =>
                {

                    Intent intent = new Intent(Context, typeof(LeagueActivity));
                    intent.PutExtra("league", JsonConvert.SerializeObject(Leagues.ElementAt(p.Position)));
                    StartActivity(intent);
                };

            }
            catch (Java.Lang.Exception)
            {

            }

            return root;
        }


        public override void YearUpdated(int year)
        {
            base.YearUpdated(year);

            Leagues = (Activity as MainActivity).Leagues.Where(l => l.Year.Year.ToString() == Year.ToString()).ToList();
            LeaguesListView.Adapter = new LeaguesAdapter(Context, Leagues.ToList());

        }

    }
}