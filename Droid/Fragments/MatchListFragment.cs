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
using Floorball.LocalDB.Tables;
using Floorball.Droid.Activities;
using Floorball.LocalDB;

namespace Floorball.Droid.Fragments
{
    public class MatchListFragment : Fragment
    {

        public UnitOfWork UoW { get; set; }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            UoW = new UnitOfWork();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View root = inflater.Inflate(Resource.Layout.MatchesFragment, container, false);

            //CreateMatches(root.FindViewById<LinearLayout>(Resource.Id.matchesList));

            return root;
        }

        private void CreateMatches(LinearLayout container)
        {

            TeamActivity activity = Activity as TeamActivity;

            ViewGroup header;
            ViewGroup matches;
            ViewGroup matchResult;

            int i = 0;

            while (i < activity.Matches.Count)
            {
                header = Activity.LayoutInflater.Inflate(Resource.Layout.Header, null, false) as ViewGroup;
                Match actual = activity.Matches.ElementAt(i);
                League league = UoW.LeagueRepo.GetLeagueById(actual.LeagueId);
                header.FindViewById<TextView>(Resource.Id.headerName).Text = league.Name;

                container.AddView(header);

                int j = i;

                while (j < activity.Matches.Count && activity.Matches.ElementAt(j).LeagueId == actual.LeagueId)
                {
                    matches = Activity.LayoutInflater.Inflate(Resource.Layout.Matches, null, false) as ViewGroup;
                    matches.FindViewById<TextView>(Resource.Id.matchDate).Text = activity.Matches.ElementAt(j).Date.ToString();

                    matchResult = Activity.LayoutInflater.Inflate(Resource.Layout.MatchResult, null, false) as ViewGroup;
                    if (activity.Team.Id == activity.Matches.ElementAt(j).HomeTeamId)
                    {
                        matchResult.FindViewById<TextView>(Resource.Id.homeTeam).Text = activity.Team.Name + " ";
                        matchResult.FindViewById<TextView>(Resource.Id.homeScore).Text = activity.Matches.ElementAt(j).GoalsH.ToString();
                        matchResult.FindViewById<TextView>(Resource.Id.awayScore).Text = activity.Matches.ElementAt(j).GoalsA.ToString();
                        matchResult.FindViewById<TextView>(Resource.Id.awayTeam).Text = " " + UoW.TeamRepo.GetTeamById(activity.Matches.ElementAt(j).AwayTeamId).Name;

                    }
                    else
                    {
                        matchResult.FindViewById<TextView>(Resource.Id.homeTeam).Text = UoW.TeamRepo.GetTeamById(activity.Matches.ElementAt(j).HomeTeamId).Name + " ";
                        matchResult.FindViewById<TextView>(Resource.Id.homeScore).Text = activity.Matches.ElementAt(j).GoalsH.ToString();
                        matchResult.FindViewById<TextView>(Resource.Id.awayScore).Text = activity.Matches.ElementAt(j).GoalsA.ToString();
                        matchResult.FindViewById<TextView>(Resource.Id.awayTeam).Text = " " + activity.Team.Name;

                    }

                    matches.AddView(matchResult);

                    j++;
                    container.AddView(matches);
                }

                i = j;
            }


        }

    }
}