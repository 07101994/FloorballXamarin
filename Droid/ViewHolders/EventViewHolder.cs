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
    public class EventViewHolder<T> : RecyclerView.ViewHolder
    {
        public TextView Player { get; set; }
        public TextView Time { get; set; }
        public ImageView Image { get; set; }

        public EventViewHolder(View itemView, Action<T> listener, BaseRecyclerViewAdapter<T> adapter) : base(itemView)
        {
            Time = itemView.FindViewById<TextView>(Resource.Id.time);
            Image = itemView.FindViewById<ImageView>(Resource.Id.eventImage);
            Player = itemView.FindViewById<TextView>(Resource.Id.playerName);

            itemView.Click += (sender, e) => listener(adapter.ListItems[AdapterPosition]);

        }
    }
}