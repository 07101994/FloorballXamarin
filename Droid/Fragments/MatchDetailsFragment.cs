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
using Floorball.LocalDB.Tables;
using Floorball.Droid.Utils;
using Floorball.Droid.Adapters;
using Android.Support.V4.App;
using Floorball.Droid.Models;

namespace Floorball.Droid.Fragments
{
    public class MatchDetailsFragment : MainFragment
    {
        public Match Match { get; set; }
        public League League { get; set; }
        public Stadium Stadium { get; set; }
        public IEnumerable<Referee> Referees { get; set; }

        public static MatchDetailsFragment Instance(Match match, League league, Stadium stadium)
        {
            var fragment = new MatchDetailsFragment();

            Bundle args = new Bundle();
            args.PutObject("match", match);
            args.PutObject("league", league);
            args.PutObject("stadium", stadium);

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Match = Arguments.GetObject<Match>("match");
            League = Arguments.GetObject<League>("league");
            Stadium = Arguments.GetObject<Stadium>("stadium");
            Referees = Match.Referees;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.MatchDetail, container, false);

            root.FindViewById<TextView>(Resource.Id.leagueName).Text = League.Name + " " + Match.Round.ToString() + ". forduló";

            int resourceId = Context.Resources.GetIdentifier(League.Country.ToString().ToLower(), "drawable", Context.PackageName);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                root.FindViewById<ImageView>(Resource.Id.countryFlag).SetImageDrawable(Context.Resources.GetDrawable(resourceId, Context.ApplicationContext.Theme));
            }
            else
            {
                root.FindViewById<ImageView>(Resource.Id.countryFlag).SetImageDrawable(Context.Resources.GetDrawable(resourceId));
            }

            root.FindViewById<TextView>(Resource.Id.date).Text = Match.Date.ToShortDateString();
            root.FindViewById<TextView>(Resource.Id.stadiumName).Text = Stadium.Name;
            root.FindViewById<TextView>(Resource.Id.stadiumAddress).Text = Stadium.Address;

            FragmentManager fm = Activity.SupportFragmentManager;
            FragmentTransaction ft = fm.BeginTransaction();

            Fragment fragment = ListFragment.Instance(Referees.Select(r => new ListModel { Text = r.Name, Object = r }), "referees");
            ft.Add(Resource.Id.refereees_container, fragment);
            ft.Commit();

            return root;
        }
    }
}