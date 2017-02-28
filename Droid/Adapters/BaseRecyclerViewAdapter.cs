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

namespace Floorball.Droid.Adapters
{
    public abstract class BaseRecyclerViewAdapter<T> : RecyclerView.Adapter
    {

        public List<T> ListItems { get; set; }

        public event EventHandler<int> ClickedId;
        public event EventHandler<object> ClickedObject;

        public BaseRecyclerViewAdapter(List<T> listItems)
        {
            ListItems = listItems;
        }

        public override int ItemCount
        {
            get
            {
                return ListItems.Count;
            }
        }

        public abstract override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position);

        public abstract override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType);

        protected void OnClickId(int position)
        {
            if (ClickedId != null)
            {
                ClickedId(this, position);
            }
        }

        protected void OnClickObject(object clicked)
        {
            if (ClickedObject != null)
            {
                ClickedObject(this, clicked);
            }
        }
    }
}