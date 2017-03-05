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
using Floorball.Droid.Models;
using FloorballServer.Models.Floorball;
using Floorball.Droid.ViewHolders;
using Floorball.LocalDB.Tables;

namespace Floorball.Droid.Adapters
{
    public class MatchesAdapter : AdapterWithTwoHeader<HeaderModel, HeaderModel, MatchResultModel>
    {

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            switch (holder.ItemViewType)
            {
                case 0:

                    var vh = holder as HeaderViewHolder;
                    vh.Header.Text = MainHeaders[ListItems[position].Index].Title;

                    break;
                case 1:

                    var vh1 = holder as HeaderViewHolder;
                    vh1.Header.Text = SubHeaders[ListItems[position].Index].Title;

                    break;
                case 2:

                    var vh2 = holder as MatchViewHolder<MatchResultModel>;
                    vh2.HomeTeam.Text = Contents[ListItems[position].Index].HomeTeam;
                    vh2.AwayTeam.Text = Contents[ListItems[position].Index].AwayTeam;
                    vh2.HomeScore.Text = Contents[ListItems[position].Index].HomeScore;
                    vh2.AwayScore.Text = Contents[ListItems[position].Index].AwayScore;

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

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Header, parent, false);
                    vh = new HeaderViewHolder(itemView);

                    break;
                case 1:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SubHeader, parent, false);
                    vh = new HeaderViewHolder(itemView);

                    break;
                case 2:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.MatchResult, parent, false);
                    vh = new MatchViewHolder<MatchResultModel>(itemView, OnClickObject, this);

                    break;
                default:
                    break;
            }

            return vh;
        }
    }
}