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

namespace Floorball.Droid.Fragments
{
    public class LeagueStatisticsFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View root = inflater.Inflate(Resource.Layout.LeagueStatisticsFragment, container, false);

            CreateStatisticsTable(root);

            return root;
        }

        private void CreateStatisticsTable(View root)
        {
            TableLayout table = root.FindViewById<TableLayout>(Resource.Id.statisticstable);
            ViewGroup newRow = Activity.LayoutInflater.Inflate(Resource.Layout.StatisticsTableRow, table, false) as ViewGroup;
            LeagueActivity activity = Activity as LeagueActivity;
            var n = activity.Players;
            int i = 1;
            foreach (var stat in (Activity as LeagueActivity).PlayerStatistics.OrderByDescending(s => s.Points))
            {
                newRow = Activity.LayoutInflater.Inflate(Resource.Layout.StatisticsTableRow, table, false) as ViewGroup;
                (newRow.GetChildAt(0) as TextView).Text = (i++).ToString();
                (newRow.GetChildAt(1) as TextView).Text = activity.Players.Where(p => p.RegNum == stat.PlayerId).First().Name;
                (newRow.GetChildAt(2) as TextView).Text = activity.Teams.Where(t => t.Id == stat.TeamId).First().Name; 
                (newRow.GetChildAt(3) as TextView).Text = stat.Goals.ToString();
                (newRow.GetChildAt(4) as TextView).Text = stat.Assists.ToString();
                (newRow.GetChildAt(5) as TextView).Text = stat.Points.ToString();
                (newRow.GetChildAt(6) as TextView).Text = stat.Penalties;
                table.AddView(newRow);
            }

        }

    }

   

}