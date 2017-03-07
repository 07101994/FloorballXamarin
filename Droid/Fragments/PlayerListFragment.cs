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
using Newtonsoft.Json;
using Android.Support.V7.Widget;
using Floorball.Droid.Adapters;
using Floorball.Droid.Utils;

namespace Floorball.Droid.Fragments
{
    public class PlayerListFragment : Fragment
    {
        RecyclerView recyclerView;
        ListAdapter adapter;

        public static PlayerListFragment Instance(List<string> players)
        {
            var fragment = new PlayerListFragment();

            Bundle args = new Bundle();
            args.PutObject("players", players);
            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //adapter = new PlayersAdapter(Arguments.GetObject<List<Player>>("players"));
            //adapter = new ListAdapter(Arguments.GetObject<List<string>>("players"));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.RecycleView, container, false);

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            adapter.ClickedObject += Adapter_ClickedObject;
            recyclerView.SetAdapter(adapter);

            return root;
        }

        private void Adapter_ClickedObject(object sender, object e)
        {
            Intent intent = new Intent(Context, typeof(PlayerActivity));
            intent.PutObject("player", e);
            StartActivity(intent);
        }

    }
}