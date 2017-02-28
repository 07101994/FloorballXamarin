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

namespace Floorball.Droid.ViewHolders
{
    public class LeagueHeaderViewHolder : RecyclerView.ViewHolder
    {
        public TextView TextView { get; set; }
        public ImageView Flag { get; set; }

        public LeagueHeaderViewHolder(View itemView) : base(itemView)
        {
            TextView = itemView.FindViewById<TextView>(Resource.Id.leagueName);
            Flag = itemView.FindViewById<ImageView>(Resource.Id.countryFlag);
        }
    }
}