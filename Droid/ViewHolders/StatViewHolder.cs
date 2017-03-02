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

namespace Floorball.Droid.ViewHolders
{
    public class StatViewHolder : RecyclerView.ViewHolder
    {

        public TextView LeagueName { get; set; }
        public TextView LeagueYear { get; set; }

        public List<TextView> StatLabels { get; set; }
        public List<TextView> StatNumbers { get; set; }

        public StatViewHolder(View itemView) : base(itemView) 
        {
            LeagueName =  itemView.FindViewById<TextView>(Resource.Id.leagueName);
            LeagueYear =  itemView.FindViewById<TextView>(Resource.Id.leagueYear);

            StatLabels = new List<TextView>();
            StatNumbers = new List<TextView>();

            LinearLayout statCard = itemView.FindViewById<LinearLayout>(Resource.Id.statCard);

            AddStatLine(statCard.FindViewById<View>(Resource.Id.goalStat));
            AddStatLine(statCard.FindViewById<View>(Resource.Id.assistStat));
            AddStatLine(statCard.FindViewById<View>(Resource.Id.penaltyStat));
            AddStatLine(statCard.FindViewById<View>(Resource.Id.matchStat));

        }

        private void AddStatLine(View view)
        {

            StatLabels.Add(view.FindViewById<TextView>(Resource.Id.statLabel));
            StatNumbers.Add(view.FindViewById<TextView>(Resource.Id.statNumber));

        }
    }
}