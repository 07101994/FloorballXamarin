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
using FloorballServer.Models.Floorball;
using Floorball.LocalDB.Tables;
using Android.Content.Res;
using Android.Support.V7.Widget;

namespace Floorball.Droid.Adapters
{
    class LeaguesAdapter : RecyclerView.Adapter
    {
        private List<ListItem> listItems;
        private List<string> headers;
        private List<object> contents;

        public event EventHandler<int> Clicked;

        private class ListItem
        {
            public string Type { get; set; }
            public int Index { get; set; }
        }

        public LeaguesAdapter(IEnumerable<List<League>> leagues)
        {

            listItems = new List<ListItem>();
            headers = new List<string>();
            contents = new List<object>();

            leagues.ToList().ForEach(l =>
            {
                listItems.Add(new ListItem { Type = "header", Index = headers.Count });
                headers.Add(l.First().Country.ToFriendlyString());

                foreach (var value in l)
                {
                    listItems.Add(new ListItem { Type = "content", Index = contents.Count });
                    contents.Add(value);
                }
            });

        }

        class HeaderViewHolder : RecyclerView.ViewHolder
        {
            public TextView TextView { get; set; }

            public HeaderViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                TextView = itemView.FindViewById<TextView>(Resource.Id.headerName);
            }
        }

        class ViewHolder : RecyclerView.ViewHolder
        {
            public TextView TextView { get; set; }

            public ViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                TextView = itemView.FindViewById<TextView>(Resource.Id.cardName);

                itemView.Click += (sender, e) => listener(AdapterPosition);
                
            }
        }

        public override int GetItemViewType(int position)
        {
            return Convert.ToInt16(listItems[position].Type == "header");
        }

        

        public override int ItemCount
        {
            get
            {
                return listItems.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            switch (holder.ItemViewType)
            {
                case 0:
                    var vh = holder as ViewHolder;

                    vh.TextView.Text = (contents[listItems[position].Index] as League).Name;

                    break;
                case 1:

                    var vh1 = holder as HeaderViewHolder;

                    vh1.TextView.Text = headers[listItems[position].Index];

                    break;
                default:
                    break;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView;
            RecyclerView.ViewHolder vh = null;

            switch (viewType)
            {
                case 0:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Card, parent, false);
                    vh = new ViewHolder(itemView, OnClick);

                    break;
                case 1:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Header, parent, false);
                    vh = new HeaderViewHolder(itemView, OnClick);

                    break;
                default:
                    break;
            }

            return vh;
        }

        private void OnClick(int position)
        {
            if (Clicked != null)
            {
                Clicked(this, (contents[listItems[position].Index] as League).Id);
            }
        }
    }
}