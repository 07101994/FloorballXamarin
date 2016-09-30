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
using Floorball.REST;
using FloorballServer.Models.Floorball;
using Android.Support.V4.View;
using Floorball.Droid.Activities;
using Newtonsoft.Json;
using Android.Support.V7.Widget;
using Floorball.LocalDB.Tables;
using Floorball.LocalDB;

namespace Floorball.Droid.Fragments
{
    public class TeamsPageFragment : PagerFragment
    {

        public IEnumerable<Team> Teams { get; set; }

        public IEnumerable<League> Leagues { get; set; }

        public LinearLayout TeamsLayout { get; set; }

        List<Team> actualTeams;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

            Teams = (Activity as MainActivity).Teams.Where(t => t.Year.Year == Year);
            Leagues = (Activity as MainActivity).Leagues.Where(l => l.Year.Year == Year);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View root = inflater.Inflate(Resource.Layout.TeamsPageFragment, container, false);

            CreateTeams(root);

            return root;
        }

        public void CreateTeams(View root)
        {
            
            if (PageCount == 0)
            {
                
                actualTeams = Teams.Where(t => t.Sex == "férfi").OrderBy(t => t.LeagueId).ToList();
            }
            else
            {
                actualTeams = Teams.Where(t => t.Sex == "női").OrderBy(t => t.LeagueId).ToList();
            }

            MainActivity activity = Activity as MainActivity;

            TeamsLayout = root.FindViewById<LinearLayout>(Resource.Id.cardlist);
            ViewGroup header;
            ViewGroup team;

            int i = 0;
            while (i < actualTeams.Count)
            {

                int leagueId = actualTeams.ElementAt(i).LeagueId;

                header = Activity.LayoutInflater.Inflate(Resource.Layout.Header, null, false) as ViewGroup;
                header.FindViewById<TextView>(Resource.Id.headerName).Text = Leagues.Where(l => l.Id == leagueId).First().Name;

                TeamsLayout.AddView(header);

                int j = i;
                while (j < actualTeams.Count && actualTeams.ElementAt(j).LeagueId == leagueId)
                {

                    team = Activity.LayoutInflater.Inflate(Resource.Layout.Card, TeamsLayout, false) as ViewGroup;
                    team.FindViewById<TextView>(Resource.Id.cardName).Text = actualTeams.ElementAt(j).Name;
                    team.Click += TeamClick;
                    team.Tag = j.ToString();

                    TeamsLayout.AddView(team);

                    j++;
                }

                i = j;
            }

        }

        private void RemoveTeams(View root)
        {
            root.FindViewById<LinearLayout>(Resource.Id.cardlist).RemoveAllViews();
        }

        public override void YearUpdated(int year)
        {
            base.YearUpdated(year);

            Teams = (Activity as MainActivity).Teams.Where(t => t.Year.Year == year);
            Leagues = (Activity as MainActivity).Leagues.Where(l => l.Year.Year == year);
            RemoveTeams(View);
            CreateTeams(View);
        }

        private void TeamClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(Context, typeof(TeamActivity));
            intent.PutExtra("team", JsonConvert.SerializeObject(actualTeams.ElementAt(Convert.ToInt32((sender as CardView).Tag.ToString()))));
            StartActivity(intent);
           
        }
    }
}