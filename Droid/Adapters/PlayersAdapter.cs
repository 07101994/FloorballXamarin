using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Floorball.LocalDB.Tables;
using Android.Support.V7.Widget;

namespace Floorball.Droid.Adapters
{
    class PlayersAdapter : RecyclerView.Adapter
    {

        public List<Player> Players { get; set; }

        public event EventHandler<Player> Clicked;

        public PlayersAdapter(List<Player> players)
        {
            Players = players;
        }

        public override int ItemCount
        {
            get
            {
                return Players.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolder vh = holder as ViewHolder;
            //vh.Image.SetImageResource(Resource.Drawable.hu);
            vh.Text.Text = Players[position].Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.PlayerItem, parent, false);

            var vh = new ViewHolder(itemView, OnClick);

            return vh;
        }

        public void Swap(List<Player> players)
        {
            Players.Clear();
            Players.AddRange(players);
            NotifyDataSetChanged();
        }

        private class ViewHolder : RecyclerView.ViewHolder
        {

            public ImageView Image { get; set; }

            public TextView Text { get; set; }

            public ViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                Image = itemView.FindViewById<ImageView>(Resource.Id.playerTeamImage);
                Text = itemView.FindViewById<TextView>(Resource.Id.playerName);

                itemView.Click += (sender, e) => listener(AdapterPosition);

            }
        }

        private void OnClick(int position)
        {
            if (Clicked != null)
            {
                Clicked(this, Players[position]);
            }
        }

    }
}