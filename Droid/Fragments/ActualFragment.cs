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
using FloorballServer.Models.Floorball;
using Floorball.REST;
using Floorball.LocalDB.Tables;

namespace Floorball.Droid.Fragments
{
    public class ActualFragment : MainFragment
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

            View root = inflater.Inflate(Resource.Layout.LeagueMatchesFragment, container, false);

            CreateMatches(root);

            return root;
        }

        private void CreateMatches(View root)
        {

            MainActivity activity = Activity as MainActivity;

            Matches = root.FindViewById<LinearLayout>(Resource.Id.leagueMatches);
            ViewGroup header;
            ViewGroup matches;
            ViewGroup matchResult;

            int i = 0;

            while (i < activity.ActualMatches.Count)
            {
            
                header = Activity.LayoutInflater.Inflate(Resource.Layout.Round, null, false) as ViewGroup;
                Match actual = activity.ActualMatches.ElementAt(i);
                int leagueId = actual.LeagueId;
                header.FindViewById<TextView>(Resource.Id.round).Text = activity.Leagues.Find(l => l.Id == leagueId).Name;

                Matches.AddView(header);

                int j = i;

                while (j < activity.ActualMatches.Count)
                {
                    matches = Activity.LayoutInflater.Inflate(Resource.Layout.Matches, null, false) as ViewGroup;
                    matches.FindViewById<TextView>(Resource.Id.matchDate).Text = actual.Date.ToString();

                    int k = j;

                    while (k < activity.ActualMatches.Count && activity.ActualMatches.ElementAt(k).LeagueId == actual.LeagueId && activity.ActualMatches.ElementAt(k).Date == actual.Date)
                    {

                        matchResult = Activity.LayoutInflater.Inflate(Resource.Layout.MatchResult, null, false) as ViewGroup;
                        matchResult.FindViewById<TextView>(Resource.Id.homeTeam).Text = activity.ActualTeams.Where(t => t.Id == activity.ActualMatches.ElementAt(k).HomeTeamId).First().Name + " ";
                        matchResult.FindViewById<TextView>(Resource.Id.homeScore).Text = activity.ActualMatches.ElementAt(k).GoalsH.ToString();
                        matchResult.FindViewById<TextView>(Resource.Id.awayScore).Text = activity.ActualMatches.ElementAt(k).GoalsA.ToString();
                        matchResult.FindViewById<TextView>(Resource.Id.awayTeam).Text = " " + activity.ActualTeams.Where(t => t.Id == activity.ActualMatches.ElementAt(k).AwayTeamId).First().Name;

                        matches.AddView(matchResult);
                        
                        k++;
                    }

                    j = k;
                    Matches.AddView(matches);
                }

                i = j;
            }


        }

        public override void listItemSelected(string s)
        {
            throw new NotImplementedException();
        }
    }
}