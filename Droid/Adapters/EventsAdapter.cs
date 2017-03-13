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
using Floorball.Droid.ViewHolders;

namespace Floorball.Droid.Adapters
{
    public class EventsAdapter : BaseRecyclerViewAdapter<MatchEventModel>
    {
        public EventsAdapter(IEnumerable<MatchEventModel> events) : base(events.ToList())
        {

        }

        public override int GetItemViewType(int position)
        {
            return ListItems[position].ViewType;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as EventViewHolder<MatchEventModel>;
            vh.Player.Text = ListItems[position].Player.ShortName;
            vh.Time.Text = UIHelper.GetMatchFullTime(ListItems[position].Time);
            vh.Image.SetImageResource(ListItems[position].ResourceId);

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView;

            if (viewType == 0)
            {
                itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.HomeEventItem, parent, false);
            }
            else
            {
                itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AwayEventItem, parent, false);
            }
            
            return new EventViewHolder<MatchEventModel>(itemView,OnClickObject,this); ;
        }
    }
}