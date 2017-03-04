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
using Floorball.Droid.ViewHolders;

namespace Floorball.Droid.Adapters
{
    public class TeamsAdapter : AdapterWithHeader<HeaderModel,object>
    {

        public Context Context { get; set; }

        public TeamsAdapter(Context ctx, IEnumerable<List<Team>> teams, List<League> leagues)
        {
            Context = ctx;

            teams.ToList().ForEach(t =>
            {
                ListItems.Add(new ListItem { Type = 0, Index = Headers.Count });
                var header = new HeaderModel {
                    Title = leagues.Find(l => l.Id == t.First().LeagueId).Name,
                    Country = t.First().Country
                };
                Headers.Add(header);

                foreach (var team in t)
                {
                    ListItems.Add(new ListItem { Type = 1, Index = Contents.Count });
                    Contents.Add(team);
                }
            });

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            switch (holder.ItemViewType)
            {
                case 0:

                    var vh1 = holder as TeamHeaderViewHolder;

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
                case 1:

                    var vh = holder as TeamContentViewHolder;

                    vh.TextView.Text = (Contents[ListItems[position].Index] as Team).Name;

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

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.LeagueNameWithFlag, parent, false);
                    vh = new TeamHeaderViewHolder(itemView, OnClickId);

                    break;
                case 1:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Card, parent, false);
                    vh = new TeamContentViewHolder(itemView, OnClickId, this);

                    break;
                default:
                    break;
            }

            return vh;
        }

    }
}