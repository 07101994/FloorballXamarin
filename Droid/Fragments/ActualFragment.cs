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
using Floorball.Signalr;
using Microsoft.AspNet.SignalR.Client;
using Android.Support.V7.Widget;

namespace Floorball.Droid.Fragments
{
    public class ActualFragment : MainFragment
    {

        public static ActualFragment Instance()
        {
            return new ActualFragment();
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            FloorballClient.Instance.MatchStarted += MatchStarted;
            FloorballClient.Instance.MatchEnded += MatchEnded;
            FloorballClient.Instance.NewEventAdded += NewEventAdded;

        }

      

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View root = inflater.Inflate(Resource.Layout.ActualFragment, container, false);
            
            CreateMatches(root);

            return root;
        }

        private void CreateMatches(View root)
        {

            LinearLayout container = root.FindViewById(Resource.Id.first).FindViewById<LinearLayout>(Resource.Id.matchesList);

            MainActivity activity = Activity as MainActivity;

            int i = 0;


            while (i < activity.ActualMatches.Count())
            {

                Match actualMatch = activity.ActualMatches.ElementAt(i);
                League actualLeague = activity.Leagues.ToList().Find(l => l.Id == actualMatch.LeagueId);

                //Create league name and country flag
                CreateLeagueNameAndFlag(actualMatch,actualLeague, container);

                int j = i;

                //While int the same league
                while (j < activity.ActualMatches.Count() && activity.ActualMatches.ElementAt(j).LeagueId ==  actualMatch.LeagueId)
                {

                    CreateMatchTile(actualMatch, container);

                    actualMatch = activity.ActualMatches.ElementAt(j);

                    j++;
                }


                i = j;

            }

            //ViewGroup header;
            //ViewGroup matches;
            //ViewGroup matchResult;

            //while (i < activity.ActualMatches.Count())
            //{

            //    header = Activity.LayoutInflater.Inflate(Resource.Layout.Header, null, false) as ViewGroup;
            //    Match actual = activity.ActualMatches.ElementAt(i);
            //    int leagueId = actual.LeagueId;
            //    header.FindViewById<TextView>(Resource.Id.headerName).Text = activity.Leagues.ToList().Find(l => l.Id == leagueId).Name;

            //    container.AddView(header);

            //    int j = i;

            //    while (j < activity.ActualMatches.Count())
            //    {
            //        matches = Activity.LayoutInflater.Inflate(Resource.Layout.Matches, null, false) as ViewGroup;
            //        matches.FindViewById<TextView>(Resource.Id.matchDate).Text = actual.Date.ToString();

            //        int k = j;

            //        while (k < activity.ActualMatches.Count() && activity.ActualMatches.ElementAt(k).LeagueId == actual.LeagueId && activity.ActualMatches.ElementAt(k).Date == actual.Date)
            //        {

            //            matchResult = Activity.LayoutInflater.Inflate(Resource.Layout.MatchResult, null, false) as ViewGroup;
            //            matchResult.FindViewById<TextView>(Resource.Id.homeTeam).Text = activity.ActualTeams.Where(t => t.Id == activity.ActualMatches.ElementAt(k).HomeTeamId).First().Name + " ";
            //            matchResult.FindViewById<TextView>(Resource.Id.homeScore).Text = activity.ActualMatches.ElementAt(k).GoalsH.ToString();
            //            matchResult.FindViewById<TextView>(Resource.Id.awayScore).Text = activity.ActualMatches.ElementAt(k).GoalsA.ToString();
            //            matchResult.FindViewById<TextView>(Resource.Id.awayTeam).Text = " " + activity.ActualTeams.Where(t => t.Id == activity.ActualMatches.ElementAt(k).AwayTeamId).First().Name;

            //            matches.AddView(matchResult);

            //            k++;
            //        }

            //        actual = activity.ActualMatches.ElementAt(k);

            //        j = k;
            //        container.AddView(matches);
            //    }

            //    i = j;
            //}

            if (i == 0)
            {
                //TextView t = View.FindViewById<TextView>(Resource.Id.noActualMatches);
                root.FindViewById<TextView>(Resource.Id.noActualMatches).Visibility = ViewStates.Visible;
            }
            
            //Connect to siqnalr server
            if (activity.ActualMatches.Where(m => m.State == StateEnum.Playing).Count() > 0 &&  FloorballClient.Instance.ConnectionState == ConnectionState.Disconnected)
            {
                //FloorballClient.Instance.Connect(activity.Countries);
            }


        }

        private void CreateMatchTile(Match actualMatch, LinearLayout container)
        {

            MainActivity activity = Activity as MainActivity;
            Team homeTeam = activity.ActualTeams.Where(t => t.Id == actualMatch.HomeTeamId).First();
            Team awayTeam = activity.ActualTeams.Where(t => t.Id == actualMatch.AwayTeamId).First();

            CardView matchTile = activity.LayoutInflater.Inflate(Resource.Layout.ActualTile, container, false) as CardView;

            matchTile.FindViewById<TextView>(Resource.Id.actualDate).Text = actualMatch.Date.ToString();
            matchTile.FindViewById<TextView>(Resource.Id.time).Text = GetMatchTime(actualMatch.Time, actualMatch.State);
            matchTile.FindViewById<TextView>(Resource.Id.homeTeamName).Text = homeTeam.Name;
            matchTile.FindViewById<TextView>(Resource.Id.awayTeamName).Text = awayTeam.Name;
            matchTile.FindViewById<TextView>(Resource.Id.homeTeamScore).Text = actualMatch.GoalsH.ToString();
            matchTile.FindViewById<TextView>(Resource.Id.awayTeamScore).Text = actualMatch.GoalsA.ToString();

            container.AddView(matchTile);

        }

        private string GetMatchTime(TimeSpan time, StateEnum state)
        {

            if (state == StateEnum.Ended || state == StateEnum.Playing)
            {
                return time.Hours == 1 ? "60:00" : time.Minutes + ":" + time.Seconds;
            }

            return "";

        }

        private void CreateLeagueNameAndFlag(Match actualMatch, League actualLeague, ViewGroup container)
        {

            MainActivity activity = Activity as MainActivity;

            int resourceId = activity.Resources.GetIdentifier(actualLeague.Country.ToFriendlyString().ToLower(), "drawable", activity.PackageName);

            ViewGroup leagueNameView = activity.LayoutInflater.Inflate(Resource.Layout.LeagueNameWithFlag, null, false) as ViewGroup;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                //leagueNameView.FindViewById<ImageView>(Resource.Id.countryFlag).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.HU, activity.ApplicationContext.Theme));
                leagueNameView.FindViewById<ImageView>(Resource.Id.countryFlag).SetImageDrawable(Resources.GetDrawable(resourceId, activity.ApplicationContext.Theme));
            }
            else
            {
                leagueNameView.FindViewById<ImageView>(Resource.Id.countryFlag).SetImageDrawable(Resources.GetDrawable(resourceId));
            }

            leagueNameView.FindViewById<TextView>(Resource.Id.leagueName).Text = actualLeague.Name + " " + actualMatch.Round + ". forduló";

            container.AddView(leagueNameView);

        }

        public override void listItemSelected(string s)
        {
            throw new NotImplementedException();
        }

        private void MatchStarted(int matchId)
        {
            throw new NotImplementedException();
        }

        private void MatchEnded(int matchId)
        {
            throw new NotImplementedException();
        }

        private void NewEventAdded(int eventId)
        {
            throw new NotImplementedException();
        }

    }
}