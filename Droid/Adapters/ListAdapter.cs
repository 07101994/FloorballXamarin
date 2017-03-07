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
using Floorball.Droid.ViewHolders;
using Floorball.Droid.Models;

namespace Floorball.Droid.Adapters
{
    public class ListAdapter : BaseRecyclerViewAdapter<ListModel>
    {

        public ListAdapter(IEnumerable<ListModel> items) : base(items.ToList())
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as YearViewHolder;

            vh.TextView.Text = ListItems[position].Text;

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Card, parent, false);

            var vh = new YearViewHolder(itemView, OnClickObject, this);

            return vh;
        }

        
    }
}