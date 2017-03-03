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
using Floorball.Droid.ViewHolders;

namespace Floorball.Droid.Adapters
{
    class RefereeStatsAdapter : BaseRecyclerViewAdapter<RefereeStatModel>
    {

        public RefereeStatsAdapter(List<RefereeStatModel> stats) : base(stats)
        {

        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as StatViewHolder;

            vh.LeagueName.Text = ListItems[position].LeagueName;
            vh.LeagueYear.Text = ListItems[position].LeagueYear;

            vh.StatLabels[0].Text = "Mérkőzésszám: ";
            vh.StatLabels[1].Text = "2 perc: ";
            vh.StatLabels[2].Text = "5 perc: ";
            vh.StatLabels[3].Text = "10 perc: ";
            vh.StatLabels[4].Text = "Végleges: ";

            vh.StatNumbers[0].Text = ListItems[position].NumberOfMatches.ToString();
            vh.StatNumbers[1].Text = ListItems[position].TwoMinutesPenalties.ToString();
            vh.StatNumbers[2].Text = ListItems[position].FiveMinutesPenalties.ToString();
            vh.StatNumbers[3].Text = ListItems[position].TenMinutesPenalties.ToString();
            vh.StatNumbers[4].Text = ListItems[position].FinalPenalties.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Stat, parent, false);

            var vh = new StatViewHolder(itemView,5);

            return vh;
        }
    }
}