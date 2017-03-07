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
using Java.Lang;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Floorball.Droid.Adapters;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using Android.Support.V7.Widget;
using Newtonsoft.Json;
using Floorball.Droid.Activities;
using Floorball.Droid.Utils;

namespace Floorball.Droid.Fragments
{
    public class TeamsFragment : Fragment
    {

        public List<Team> Teams { get; set; }
        public List<League> Leagues { get; set; }

        RecyclerView recyclerView;
        TeamsAdapter adapter;

        public static TeamsFragment Instance(List<Team> teams, List<League> leagues)
        {
            var fragment = new TeamsFragment();

            Bundle args = new Bundle();
            args.PutObject("teams", teams);
            args.PutObject("leagues", leagues);
            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Teams = Arguments.GetObject<List<Team>>("teams");
            Leagues = Arguments.GetObject<List<League>>("leagues");

            adapter = new TeamsAdapter(Activity,Teams.GroupBy(t => t.LeagueId).Select(t => t.ToList()), Leagues);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.RecycleView, container, false);

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            adapter.ClickedId += Adapter_Clicked; ;
            recyclerView.SetAdapter(adapter);

            return root;
        }

        private void Adapter_Clicked(object sender, int teamId)
        {
            Intent intent = new Intent(Activity, typeof(TeamActivity));
            intent.PutObject("team", Teams.Find(t => t.Id == teamId));
            StartActivity(intent);
        }

    }
}