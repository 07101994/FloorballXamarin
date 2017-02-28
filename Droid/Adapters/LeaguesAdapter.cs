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
using Floorball.Droid.Models;
using Floorball.Droid.ViewHolders;

namespace Floorball.Droid.Adapters
{
    public class LeaguesAdapter : AdapterWithHeader<HeaderModel,object>
    {

        public Context Context { get; set; }

        public LeaguesAdapter(Context ctx, IEnumerable<List<League>> leagues)
        {
            Context = ctx;

            leagues.ToList().ForEach(l =>
            {
                ListItems.Add(new ListItem { Type = "header", Index = Headers.Count });
                var header = new HeaderModel { Country = l.First().Country, Title = l.First().Country.ToFriendlyString() };
                Headers.Add(header);

                foreach (var value in l)
                {
                    ListItems.Add(new ListItem { Type = "content", Index = Contents.Count });
                    Contents.Add(value);
                }
            });

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            switch (holder.ItemViewType)
            {
                case 0:
                    var vh = holder as LeagueContentViewHolder;

                    vh.TextView.Text = (Contents[ListItems[position].Index] as League).Name;
                    
                    break;
                case 1:

                    var vh1 = holder as LeagueHeaderViewHolder;

                    int resourceId = Context.Resources.GetIdentifier(Headers[ListItems[position].Index].Country.ToString().ToLower(), "drawable", Context.PackageName);

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    {
                        vh1.Flag.SetImageDrawable(Context.Resources.GetDrawable(resourceId, Context.ApplicationContext.Theme));
                    }
                    else
                    {
                        vh1.Flag.SetImageDrawable(Context.Resources.GetDrawable(resourceId));
                    }

                    vh1.TextView.Text = Headers[ListItems[position].Index].Title;

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
                    vh = new LeagueContentViewHolder(itemView, OnClickId, this);

                    break;
                case 1:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.LeagueNameWithFlag, parent, false);
                    vh = new LeagueHeaderViewHolder(itemView);

                    break;
                default:
                    break;
            }

            return vh;
        }

       
    }
}