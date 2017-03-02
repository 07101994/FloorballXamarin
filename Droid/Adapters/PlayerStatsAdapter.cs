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
using Floorball.LocalDB.Tables;
using Floorball.Droid.ViewHolders;

namespace Floorball.Droid.Adapters
{
    public class PlayerStatsAdapter : BaseRecyclerViewAdapter<PlayerStatModel>
    {

        public PlayerStatsAdapter(List<PlayerStatModel> stats) : base(stats)
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var vh = holder as StatViewHolder;

            vh.LeagueName.Text = ListItems[position].TeamName;
            vh.LeagueYear.Text = ListItems[position].LeagueYear;

            vh.StatLabels[0].Text = "Gólok: ";
            vh.StatLabels[1].Text = "Asszisztok: ";
            vh.StatLabels[2].Text = "Kiállítások: ";
            vh.StatLabels[3].Text = "Mérkőzés szám: ";

            vh.StatNumbers[0].Text = ListItems[position].Goals;
            vh.StatNumbers[1].Text = ListItems[position].Assists;
            vh.StatNumbers[2].Text = ListItems[position].Penalties;
            vh.StatNumbers[3].Text = ListItems[position].MatchCount;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Stat, parent, false);

            var vh = new StatViewHolder(itemView);

            return vh;
        }
    }
}