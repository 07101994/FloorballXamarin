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

        public override event EventHandler<object> ClickedObject;
        public override event EventHandler<int> ClickedId;

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

        protected override void OnClickId(int position)
        {
            if (ClickedId != null)
            {
                ClickedId(this, ListItems[position].Index);
            }
        }

        protected override void OnClickObject(object clicked)
        {
            if (ClickedObject != null)
            {
                ClickedObject(this, Contents[(clicked as ListItem).Index]);
            }
        }
    }
}