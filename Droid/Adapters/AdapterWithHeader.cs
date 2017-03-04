using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Floorball.Droid.Models;

namespace Floorball.Droid.Adapters
{
    public abstract class AdapterWithHeader<H,C> : BaseRecyclerViewAdapter<ListItem>
    {

        public List<H> Headers;
        public List<C> Contents;

        public AdapterWithHeader() : base(new List<ListItem>())
        {
            Headers = new List<H>();
            Contents = new List<C>();
        }

        public abstract override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position);

        public abstract override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType);

        public override int GetItemViewType(int position)
        {
            return ListItems[position].Type;
        }

    }
}