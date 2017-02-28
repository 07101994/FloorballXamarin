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
    public class YearViewHolder : RecyclerView.ViewHolder
    {

        public TextView TextView { get; set; }

        public YearViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
            TextView = itemView.FindViewById<TextView>(Resource.Id.cardName);

            itemView.Click += (sender, e) => listener(AdapterPosition);

        }

    }
}