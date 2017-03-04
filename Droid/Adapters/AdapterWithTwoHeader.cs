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
    public abstract class AdapterWithTwoHeader<H1,H2,C> : BaseRecyclerViewAdapter<ListItem>
    {
        public List<H1> MainHeaders { get; set; }
        public List<H2> SubHeaders { get; set; }
        public List<C> Contents { get; set; }

        public AdapterWithTwoHeader() : base(new List<ListItem>())
        {
            MainHeaders = new List<H1>();
            SubHeaders = new List<H2>();
            Contents = new List<C>();
        }

        public override int GetItemViewType(int position)
        {
            return ListItems[position].Type;
        }

        public abstract override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position);
        public abstract override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType);
    }
}