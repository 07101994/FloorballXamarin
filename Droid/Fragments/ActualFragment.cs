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
using Android.Animation;
using Android.Graphics.Drawables;

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
            
            CreateLiveMatches(root);
            CreateSoonMatches(root);
            
            return root;
        }

        private void CreateSoonMatches(View root)
        {
            LinearLayout container = root.FindViewById<LinearLayout>(Resource.Id.matchesList2);

            MainActivity activity = Activity as MainActivity;

            int i = 0;


            while (i < activity.ActualMatches.Count())
            {

                Match actualMatch = activity.ActualMatches.ElementAt(i);
                League actualLeague = activity.Leagues.ToList().Find(l => l.Id == actualMatch.LeagueId);

                //Create league name and country flag
                CreateLeagueNameAndFlag(actualMatch, actualLeague, container);

                int j = i;

                //While int the same league
                while (j < activity.ActualMatches.Count() && activity.ActualMatches.ElementAt(j).LeagueId == actualMatch.LeagueId)
                {

                    CreateMatchTile(activity.ActualMatches.ElementAt(j), container);

                    actualMatch = activity.ActualMatches.ElementAt(j);

                    j++;
                }


                i = j;

            }

            if (i == 0)
            {
                root.FindViewById<TextView>(Resource.Id.noActualMatches).Visibility = ViewStates.Visible;
            }

        }

        private void CreateLiveMatches(View root)
        {

            LinearLayout container = root.FindViewById<LinearLayout>(Resource.Id.matchesList1);

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

                    CreateMatchTile(activity.ActualMatches.ElementAt(j), container);

                    actualMatch = activity.ActualMatches.ElementAt(j);

                    j++;
                }


                i = j;

            }

            if (i == 0)
            {
                root.FindViewById<TextView>(Resource.Id.noActualLiveMatches).Visibility = ViewStates.Visible;
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

            if (actualMatch.State == StateEnum.Playing)
            {
                MakeAnimation(matchTile.FindViewById<View>(Resource.Id.actualProgress));
            }

            container.AddView(matchTile);

        }

        private void MakeAnimation(View view)
        {
            int colorFrom = Resources.GetColor(Resource.Color.green);
            int colotTo = Resources.GetColor(Resource.Color.red);
            ValueAnimator colorAnimation = ValueAnimator.OfObject(new ArgbEvaluator(), colorFrom, colotTo);
            colorAnimation.SetDuration(1000);
            colorAnimation.RepeatCount = ValueAnimator.Infinite;
            colorAnimation.RepeatMode = ValueAnimatorRepeatMode.Reverse;
            colorAnimation.Update += delegate
            {
                view.SetBackgroundColor(new Android.Graphics.Color(Convert.ToInt32(colorAnimation.AnimatedValue)));
            };
            colorAnimation.Start();
        }

        private string GetMatchTime(TimeSpan time, StateEnum state)
        {
            if (state == StateEnum.Ended || state == StateEnum.Playing)
            {
                if (time.Hours == 1)
                {
                    return "3.\n60:00";
                }

                return GetPeriod(time)+".\n"+GetTimeInPeriod(time);
            }

            return "";
        }

        private string GetTimeInPeriod(TimeSpan time)
        {
            string str = "";

            int minutes = time.Minutes % 20;
            if (minutes < 10)
            {
                str += "0" + minutes;
            }
            else
            {
                str += minutes;
            }

            int seconds = time.Seconds;
            if (seconds < 10)
            {
                str += "0" + seconds;
            }
            else
            {
                str += seconds;
            }

            return str;
        }

        private string GetPeriod(TimeSpan time)
        {
            return (time.Minutes / 20 + 1).ToString();
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

            leagueNameView.FindViewById<TextView>(Resource.Id.leagueName).Text = actualLeague.Name + " " + actualMatch.Round + ". fordul�";

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