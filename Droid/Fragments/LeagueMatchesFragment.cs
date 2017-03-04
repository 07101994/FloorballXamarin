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
using Floorball.Droid.Adapters;

namespace Floorball.Droid.Fragments
{
    public class LeagueMatchesFragment : Fragment
    {

        RecyclerView recyclerView;
        MatchesAdapter adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            var activity = Activity as LeagueActivity;
            adapter = new MatchesAdapter(activity.Teams.ToList(),activity.Matches.ToList(),activity.League.Rounds);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.MatchesFragment, container, false);

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            recyclerView.SetAdapter(adapter);

            return root;
        }
       
    }
}