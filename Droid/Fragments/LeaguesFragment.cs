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
            args.PutString("leagues", JsonConvert.SerializeObject(model.Leagues));

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Leagues = JsonConvert.DeserializeObject<List<League>>(Arguments.GetString("leagues"));

            adapter = new LeaguesAdapter(Leagues.GroupBy(l => l.Country).Select(l => l.ToList()));

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.RecycleView, container, false);

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            adapter.Clicked += Adapter_Clicked; ;
            recyclerView.SetAdapter(adapter);

            return root;
        }

        private void Adapter_Clicked(object sender, int leagueId)
        {
            Intent intent = new Intent(Activity, typeof(LeagueActivity));
            intent.PutExtra("league",JsonConvert.SerializeObject(Leagues.Find(l => l.Id == leagueId)));
            StartActivity(intent);
        }

    }
}