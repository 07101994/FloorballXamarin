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
using Floorball.Droid.Activities;

namespace Floorball.Droid.Fragments
{
    public class LeagueTableFragment : Fragment
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
            View root = inflater.Inflate(Resource.Layout.LeagueTableFragment, container, false);

            CreateLeagueTable(root);

            return root;
        }

        private void CreateLeagueTable(View root)
        {
            TableLayout table = root.FindViewById<TableLayout>(Resource.Id.leaguetable);

            foreach (var team in (Activity as LeagueActivity).Teams.OrderBy(t => t.Points))
            {
                ViewGroup newRow = Activity.LayoutInflater.Inflate(Resource.Layout.LeagueTableRow, table, false) as ViewGroup;
                (newRow.GetChildAt(0) as TextView).Text = team.Standing.ToString();
                (newRow.GetChildAt(1) as TextView).Text = team.Name;
                (newRow.GetChildAt(2) as TextView).Text = team.Match.ToString();
                (newRow.GetChildAt(3) as TextView).Text = "2";
                (newRow.GetChildAt(4) as TextView).Text = "4";
                (newRow.GetChildAt(5) as TextView).Text = "3";
                (newRow.GetChildAt(6) as TextView).Text = team.Scored.ToString() + "-" + team.Get.ToString();
                (newRow.GetChildAt(7) as TextView).Text = team.Points.ToString();
                table.AddView(newRow);
            }
            

        }
    }
}