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
using Floorball.Droid.Activities;
using FloorballServer.Models.Floorball;
using Android.Support.V7.Widget;
using Floorball.LocalDB.Tables;

namespace Floorball.Droid.Fragments
{
    public class LeagueMatchesFragment : Fragment
    {

        public LinearLayout Matches { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View root = inflater.Inflate(Resource.Layout.MatchesFragment, container, false);


            CreateMatches(root);

            return root;
        }

        private void CreateMatches(View root)
        {

            LeagueActivity activity = Activity as LeagueActivity;

            Matches = root.FindViewById<LinearLayout>(Resource.Id.matchesList);
            ViewGroup round;
            ViewGroup matches;
            ViewGroup matchResult;

            for (int i = 0; i < activity.League.Rounds; i++)
            {
                round = Activity.LayoutInflater.Inflate(Resource.Layout.Header, null, false) as ViewGroup;
                round.FindViewById<TextView>(Resource.Id.headerName).Text = (i + 1) + ". forduló";

                Matches.AddView(round);

                List<Match> matchesInRound = activity.Matches.Where(m => m.Round == i + 1).OrderBy(m => m.Date).ThenBy(m => m.Time).ToList();

                int j = 0;

                while (j < matchesInRound.Count)
                {

                    matches = Activity.LayoutInflater.Inflate(Resource.Layout.Matches, null, false) as ViewGroup;
                    matches.FindViewById<TextView>(Resource.Id.matchDate).Text = matchesInRound.ElementAt(j).Date.ToString(); 

                    int k = j;
                    while (k < matchesInRound.Count && matchesInRound.ElementAt(j).Date == matchesInRound.ElementAt(k).Date && matchesInRound.ElementAt(j).Time == matchesInRound.ElementAt(k).Time)
                    {

                        matchResult = Activity.LayoutInflater.Inflate(Resource.Layout.MatchResult, null, false) as ViewGroup;
                        matchResult.FindViewById<TextView>(Resource.Id.homeTeam).Text = activity.Teams.Where(t => t.Id == matchesInRound.ElementAt(k).HomeTeamId).First().Name + " ";
                        matchResult.FindViewById<TextView>(Resource.Id.homeScore).Text = matchesInRound.ElementAt(j).GoalsH.ToString();
                        matchResult.FindViewById<TextView>(Resource.Id.awayScore).Text = matchesInRound.ElementAt(j).GoalsA.ToString();
                        matchResult.FindViewById<TextView>(Resource.Id.awayTeam).Text = " " + activity.Teams.Where(t => t.Id == matchesInRound.ElementAt(k).AwayTeamId).First().Name;

                        matches/*.FindViewById<CardView>(Resource.Id.matches)*/.AddView(matchResult);
                        k++;
                    }
                    Matches.AddView(matches);
                    j = k;
                }



            }


        }

    }
}