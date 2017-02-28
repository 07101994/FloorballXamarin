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
using Android.Support.V7.Widget;
using Floorball.Droid.Adapters;

namespace Floorball.Droid.ViewHolders
{
    public class PlayerViewHolder<T> : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }

        public TextView Text { get; set; }

        public PlayerViewHolder(View itemView, Action<T> listener, BaseRecyclerViewAdapter<T> adapter) : base(itemView)
            {
            Image = itemView.FindViewById<ImageView>(Resource.Id.playerTeamImage);
            Text = itemView.FindViewById<TextView>(Resource.Id.playerName);

            itemView.Click += (sender, e) => listener(adapter.ListItems[AdapterPosition]);

        }

    }
}