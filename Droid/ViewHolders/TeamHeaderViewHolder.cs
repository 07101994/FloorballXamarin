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
    public class TeamHeaderViewHolder : RecyclerView.ViewHolder
    {
        public TextView TextView { get; set; }
        public ImageView Flag { get; set; }

        public TeamHeaderViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            Flag = itemView.FindViewById<ImageView>(Resource.Id.countryFlag);
            TextView = itemView.FindViewById<TextView>(Resource.Id.leagueName);

        }

    }
}