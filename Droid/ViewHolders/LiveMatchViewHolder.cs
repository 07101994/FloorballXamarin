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
    public class LiveMatchViewHolder : RecyclerView.ViewHolder
    {
        public TextView Date { get; set; }
        public TextView Time { get; set; }
        public TextView HomeTeam { get; set; }
        public TextView AwayTeam { get; set; }
        public TextView HomeScore { get; set; }
        public TextView AwayScore { get; set; }
        public View Progress { get; set; }

        public LiveMatchViewHolder(View itemView, Action<object> listener, ActualAdapter adapter) : base(itemView)
        {

            Date = itemView.FindViewById<TextView>(Resource.Id.actualDate);
            Time = itemView.FindViewById<TextView>(Resource.Id.time);
            HomeTeam = itemView.FindViewById<TextView>(Resource.Id.homeTeamName);
            AwayTeam = itemView.FindViewById<TextView>(Resource.Id.awayTeamName);
            HomeScore = itemView.FindViewById<TextView>(Resource.Id.homeTeamScore);
            AwayScore = itemView.FindViewById<TextView>(Resource.Id.awayTeamScore);
            Progress = itemView.FindViewById<View>(Resource.Id.actualProgress);

            itemView.Click += (sender, e) => listener(adapter.ListItems[AdapterPosition]);

        }
    }
}