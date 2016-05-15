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

namespace Floorball.Droid.Fragments
{
    public class PlayersFragment : MainFragment
    {
        ListView playerListView;

        public IEnumerable<Player> Players { get; set; }

        public IEnumerable<Player> ActualPlayers { get; set; }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View root = inflater.Inflate(Resource.Layout.PlayersFragment,container,false);

            return root;
        }

        public override void OnStart()
        {
            base.OnStart();

            Players = Manager.GetAllPlayer().OrderBy(p => p.Name).ToList();
            ActualPlayers = Players;

            playerListView = Activity.FindViewById<ListView>(Resource.Id.playersList);
            playerListView.Adapter = new PlayersAdapter(Context, ActualPlayers.ToList());
            playerListView.ItemClick += PlayerListViewItemClick;

            EditText searchBox = Activity.FindViewById<EditText>(Resource.Id.playerSearch);
            searchBox.TextChanged += SearchBoxTextChanged;
            
        }

        private void SearchBoxTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {

            if (e.Text.ToString() == "")
            {
                ActualPlayers = Players;
                playerListView.Adapter = new PlayersAdapter(Context, ActualPlayers.ToList());
            } else
            {
                ActualPlayers = Players.Where(p => p.Name.ToLower().Contains(e.Text.ToString().ToLower()));
                playerListView.Adapter = new PlayersAdapter(Context,ActualPlayers.ToList());
            }

        }

        private void PlayerListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(Context,typeof(PlayerActivity));
            intent.PutExtra("player", JsonConvert.SerializeObject(ActualPlayers.ElementAt(e.Position)));
            StartActivity(intent);

        }

        public override void listItemSelected(string s)
        {
            throw new NotImplementedException();
        }
    }
}