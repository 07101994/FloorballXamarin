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

namespace Floorball.Droid.Fragments
{
    public class PlayerListFragment : Fragment
    {

        public List<Player> Players { get; set; }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View root = inflater.Inflate(Resource.Layout.ScrollableCardList, container, false);
            Players = (Activity as TeamActivity).Players;
            CreatePlayers(Players, root);

            return root;
        }

        private void CreatePlayers(List<Player> players,View root)
        {

            ViewGroup playerlist = root.FindViewById<LinearLayout>(Resource.Id.cardlist);

            foreach (var player in players)
            {
                ViewGroup playerView = Activity.LayoutInflater.Inflate(Resource.Layout.Card, playerlist, false) as ViewGroup;

                playerView.FindViewById<TextView>(Resource.Id.cardName).Text = player.Name;
                playerView.Tag = player.RegNum;
                playerView.Click += PlayerViewClick;

                playerlist.AddView(playerView);

            }
        }

        private void PlayerViewClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(Context, typeof(PlayerActivity));
            intent.PutExtra("player", JsonConvert.SerializeObject(Players.Where(p => p.RegNum  == Convert.ToInt32((sender as CardView).Tag.ToString())).First()));
            StartActivity(intent);
        }
    }
}