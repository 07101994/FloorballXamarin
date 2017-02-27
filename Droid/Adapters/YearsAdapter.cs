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
    public class YearsAdapter : RecyclerView.Adapter
    {

        public List<string> Years { get; set; }

        public event EventHandler<int> YearClicked;

        public YearsAdapter(IEnumerable<string> years)
        {
            Years = years.ToList();
        }

        public class ViewHolder : RecyclerView.ViewHolder
        {

            public TextView TextView { get; set; }

            public ViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                TextView = itemView.FindViewById<TextView>(Resource.Id.cardName);

                itemView.Click += (sender, e) => listener(AdapterPosition);

            }

        }

        public override int ItemCount
        {
            get
            {
                return Years.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as ViewHolder;

            vh.TextView.Text = Years[position];

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Card, parent, false);

            var vh = new ViewHolder(itemView, OnClick);

            return vh;
        }

        private void OnClick(int position)
        {
            if (YearClicked != null)
            {
                YearClicked(this, position);
            }
        }

        
    }
}