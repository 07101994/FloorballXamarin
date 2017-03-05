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
    public class MatchViewHolder<T> : RecyclerView.ViewHolder
    {
        public TextView HomeTeam { get; set; }
        public TextView AwayTeam { get; set; }
        public TextView HomeScore { get; set; }
        public TextView AwayScore { get; set; }

        public MatchViewHolder(View itemView, Action<object> listener, MatchesAdapter adapter) : base(itemView)
        {
            HomeTeam = itemView.FindViewById<TextView>(Resource.Id.homeTeam);
            HomeScore = itemView.FindViewById<TextView>(Resource.Id.homeScore);
            AwayTeam = itemView.FindViewById<TextView>(Resource.Id.awayTeam);
            AwayScore = itemView.FindViewById<TextView>(Resource.Id.awayScore);

            itemView.Click += (sender, e) => listener(adapter.ListItems[AdapterPosition]);

        }

    }
}