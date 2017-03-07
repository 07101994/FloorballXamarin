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
using Android.Support.V4.App;

namespace Floorball.Droid.Fragments
{
    public class MatchDetailFragment : Fragment
    {
        public Match Match { get; set; }
        public League League { get; set; }
        public Stadium Stadium { get; set; }

        public static MatchDetailFragment Instance(League league, Match match, Stadium stadium)
        {
            var fragment = new MatchDetailFragment();
            Bundle args = new Bundle();
            args.PutObject("league", league);
            args.PutObject("match", match);
            args.PutObject("stadium", stadium);

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            Match = Arguments.GetObject<Match>("match");
            League = Arguments.GetObject<League>("league");
            Stadium = Arguments.GetObject<Stadium>("stadium");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View root = inflater.Inflate(Resource.Layout.MatchDetail, container, false);

            root.FindViewById<TextView>(Resource.Id.leagueName).Text = League.Name + " " + Match.Round.ToString() + ". forduló";
            root.FindViewById<TextView>(Resource.Id.date).Text = Match.Date.ToShortDateString();
            root.FindViewById<TextView>(Resource.Id.stadium).Text = Stadium.Name;

            return root;
        }
    }
}