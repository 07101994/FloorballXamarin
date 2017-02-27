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
using Floorball.LocalDB.Tables;
using Floorball.Droid.Models;

namespace Floorball.Droid.Adapters
{
    public class TeamsAdapter : RecyclerView.Adapter
    {

        private List<ListItem> listItems;
        private List<HeaderModel> headers;
        private List<object> contents;

        public event EventHandler<int> Clicked;

        public Context Context { get; set; }

        private class ListItem
        {
            public string Type { get; set; }
            public int Index { get; set; }
        }

        public TeamsAdapter(Context ctx, IEnumerable<List<Team>> teams, List<League> leagues)
        {
            Context = ctx;

            listItems = new List<ListItem>();
            headers = new List<HeaderModel>();
            contents = new List<object>();

            teams.ToList().ForEach(t =>
            {
                listItems.Add(new ListItem { Type = "header", Index = headers.Count });
                var header = new HeaderModel {
                    Title = leagues.Find(l => l.Id == t.First().LeagueId).Name,
                    Country = t.First().Country
                };
                headers.Add(header);

                foreach (var team in t)
                {
                    listItems.Add(new ListItem { Type = "content", Index = contents.Count });
                    contents.Add(team);
                }
            });

        }

        class HeaderViewHolder : RecyclerView.ViewHolder
        {
            public TextView TextView { get; set; }
            public ImageView Flag { get; set; }

            public HeaderViewHolder(View itemView, Action<int> listener) : base(itemView)
            {
                Flag = itemView.FindViewById<ImageView>(Resource.Id.countryFlag);
                TextView = itemView.FindViewById<TextView>(Resource.Id.leagueName);

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

                    vh.TextView.Text = (contents[listItems[position].Index] as Team).Name;

                    break;
                case 1:

                    var vh1 = holder as HeaderViewHolder;

                    int resourceId = Context.Resources.GetIdentifier(headers[listItems[position].Index].Country.ToString().ToLower(), "drawable", Context.PackageName);

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    {
                        vh1.Flag.SetImageDrawable(Context.Resources.GetDrawable(resourceId, Context.ApplicationContext.Theme));
                    }
                    else
                    {
                        vh1.Flag.SetImageDrawable(Context.Resources.GetDrawable(resourceId));
                    }

                    vh1.TextView.Text = headers[listItems[position].Index].Title;

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

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.LeagueNameWithFlag, parent, false);
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
                Clicked(this, (contents[listItems[position].Index] as Team).Id);
            }
        }

    }
}