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
    public class HeaderViewHolder : RecyclerView.ViewHolder
    {
        public TextView Header { get; set; }

        public HeaderViewHolder(View itemView) : base(itemView)
        {
            Header = itemView.FindViewById<TextView>(Resource.Id.headerName);
        }

    }
}