﻿using System;
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
using Floorball.Droid.Adapters;
using FloorballServer.Models.Floorball;
using Floorball.REST;
using Floorball.Droid.Activities;
using System.Globalization;
using Java.Lang;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using Newtonsoft.Json;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Floorball.Droid.Models;
using Floorball.Droid.Utils;

namespace Floorball.Droid.Fragments
{
    public class LeaguesFragment : Fragment
    {

        public List<League> Leagues { get; set; }

        RecyclerView recyclerView;
        LeaguesAdapter adapter;

        public static LeaguesFragment Instance(LeaguesModel model)
        {
            var fragment = new LeaguesFragment();

            Bundle args = new Bundle();
            args.PutObject("leagues", model.Leagues);

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Leagues = Arguments.GetObject<List<League>>("leagues");

            adapter = new LeaguesAdapter(Activity,Leagues.GroupBy(l => l.Country).Select(l => l.ToList()));

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.RecycleView, container, false);

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            adapter.ClickedId += Adapter_Clicked;
            recyclerView.SetAdapter(adapter);

            return root;
        }

        private void Adapter_Clicked(object sender, int leagueId)
        {
            Intent intent = new Intent(Activity, typeof(LeagueActivity));
            intent.PutObject("league",Leagues.Find(l => l.Id == leagueId));
            StartActivity(intent);
        }

    }
}