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
using Floorball.LocalDB;
using Floorball.Droid.Adapters;
using Floorball.Droid.Activities;
using Newtonsoft.Json;
using Android.Support.V7.Widget;
using Floorball.Droid.Utils;
using System.Threading.Tasks;

namespace Floorball.Droid.Fragments
{
    public class PlayersFragment : MainFragment
    {

        RecyclerView recyclerView;
        PlayersAdapter adapter;

        public IEnumerable<Player> Players { get; set; }

        public IEnumerable<Player> ActualPlayers { get; set; }

        public static PlayersFragment Instance()
        {
            return new PlayersFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            Players = UoW.PlayerRepo.GetAllPlayer().OrderBy(p => p.Name).ToList();
            ActualPlayers = Players;

            adapter = new PlayersAdapter(ActualPlayers.ToList());
            adapter.ClickedObject += Adapter_Clicked;
        }

        private void Adapter_Clicked(object sender, object player)
        {
            Intent intent = new Intent(Context, typeof(PlayerActivity));
            intent.PutObject("player", player);
            StartActivity(intent);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.SearchListFragment,container,false);

            root.FindViewById<TextView>(Resource.Id.fragmentName).Text = Resources.GetString(Resource.String.players);
            EditText searchBox = root.FindViewById<EditText>(Resource.Id.playerSearch);
            searchBox.TextChanged += SearchBoxTextChanged;

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            recyclerView.SetAdapter(adapter);
            

            return root;
        }

        private void SearchBoxTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {

            if (e.Text.ToString() == "")
            {
                ActualPlayers = Players;
            } else
            {
                ActualPlayers = Players.Where(p => p.Name.ToLower().Contains(e.Text.ToString().ToLower()));
            }

            adapter.Swap(ActualPlayers.ToList());

        }

        protected override void UpdateStarted()
        {
            View.FindViewById<View>(Resource.Id.progressbar).Visibility = ViewStates.Visible;
            View.FindViewById<TextView>(Resource.Id.notification).Text = "Frissítés folyamatban..";
        }

        protected async override void UpdateEnded()
        {
            View.FindViewById<TextView>(Resource.Id.notification).Text = "Frissítve";
            await Task.Delay(3000);
            View.FindViewById<View>(Resource.Id.progressbar).Visibility = ViewStates.Gone;
        }

    }
}