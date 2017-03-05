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

namespace Floorball.Droid.Fragments
{
    public class PlayerListFragment : Fragment
    {
        public List<Player> Players { get; set; }

        RecyclerView recyclerView;
        PlayersAdapter adapter;

        public static PlayerListFragment Instance(List<Player> players)
        {
            var fragment = new PlayerListFragment();

            Bundle args = new Bundle();
            args.PutString("players", JsonConvert.SerializeObject(players));
            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Players = JsonConvert.DeserializeObject<List<Player>>(Arguments.GetString("players"));
            adapter = new PlayersAdapter(Players);
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
            intent.PutExtra("player", JsonConvert.SerializeObject(e));
            StartActivity(intent);
        }

    }
}