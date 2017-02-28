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
using Floorball.LocalDB.Tables;

namespace Floorball.Droid.ViewHolders
{
    public class TeamContentViewHolder : RecyclerView.ViewHolder
    {
        public TextView TextView { get; set; }

        public TeamContentViewHolder(View itemView, Action<int> listener, TeamsAdapter adapter) : base(itemView)
            {
            TextView = itemView.FindViewById<TextView>(Resource.Id.cardName);

            itemView.Click += (sender, e) => listener((adapter.Contents[adapter.ListItems[AdapterPosition].Index] as Team).Id);

        }
    }
}