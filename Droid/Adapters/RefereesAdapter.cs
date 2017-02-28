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
using Floorball.LocalDB.Tables;
using Android.Support.V7.Widget;

namespace Floorball.Droid.Adapters
{
    class RefereesAdapter : RecyclerView.Adapter
    {

        public List<Referee> Referees { get; set; }

        public event EventHandler<Referee> Clicked;

        public RefereesAdapter(List<Referee> referees)
        {
            Referees = referees;
        }

        public override int ItemCount
        {
            get
            {
                return Referees.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolder vh = holder as ViewHolder;
            //vh.Image.SetImageResource(Resource.Drawable.hu);
            vh.Text.Text = Referees[position].Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.PlayerItem, parent, false);

            var vh = new ViewHolder(itemView, OnClick);

            return vh;
        }

        public void Swap(List<Referee> referees)
        {
            Referees.Clear();
            Referees.AddRange(referees);
            NotifyDataSetChanged();
        }

        private class ViewHolder : RecyclerView.ViewHolder
        {

            public ImageView Image { get; set; }

            public TextView Text { get; set; }

            public ViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                Image = itemView.FindViewById<ImageView>(Resource.Id.playerTeamImage);
                Text = itemView.FindViewById<TextView>(Resource.Id.playerName);

                itemView.Click += (sender, e) => listener(AdapterPosition);

            }
        }

        private void OnClick(int position)
        {
            if (Clicked != null)
            {
                Clicked(this, Referees[position]);
            }
        }


    }
}