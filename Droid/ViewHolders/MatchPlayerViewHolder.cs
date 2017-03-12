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
    public class MatchPlayerViewHolder : RecyclerView.ViewHolder
    {

        public StatViews HomeStat { get; set; }
        public StatViews AwayStat { get; set; }
        public TextView Number { get; set; }

        public MatchPlayerViewHolder(View listItem) : base (listItem)
        {

            HomeStat = CreateStatViews(listItem.FindViewById<View>(Resource.Id.homePlayerStat));
            AwayStat = CreateStatViews(listItem.FindViewById<View>(Resource.Id.awayPlayerStat));
            Number = listItem.FindViewById<TextView>(Resource.Id.number);
        }

        private StatViews CreateStatViews(View view)
        {
            StatViews views = new StatViews();

            views.Root = view;
            views.PlayerName = view.FindViewById<TextView>(Resource.Id.playerName);
            views.GoalImage = view.FindViewById<View>(Resource.Id.goalStat).FindViewById<ImageView>(Resource.Id.statIcon);
            views.GoalText = view.FindViewById<View>(Resource.Id.goalStat).FindViewById<TextView>(Resource.Id.statNumber);
            views.AssistImage = view.FindViewById<View>(Resource.Id.assistStat).FindViewById<ImageView>(Resource.Id.statIcon);
            views.AssistText = view.FindViewById<View>(Resource.Id.assistStat).FindViewById<TextView>(Resource.Id.statNumber);
            views.P2Image = view.FindViewById<View>(Resource.Id.P2Stat).FindViewById<ImageView>(Resource.Id.statIcon);
            views.P2Text = view.FindViewById<View>(Resource.Id.P2Stat).FindViewById<TextView>(Resource.Id.statNumber);
            views.P5Image = view.FindViewById<View>(Resource.Id.P5Stat).FindViewById<ImageView>(Resource.Id.statIcon);
            views.P5Text = view.FindViewById<View>(Resource.Id.P5Stat).FindViewById<TextView>(Resource.Id.statNumber);
            views.P10Image = view.FindViewById<View>(Resource.Id.P10Stat).FindViewById<ImageView>(Resource.Id.statIcon);
            views.P10Text = view.FindViewById<View>(Resource.Id.P10Stat).FindViewById<TextView>(Resource.Id.statNumber);
            views.PVImage = view.FindViewById<View>(Resource.Id.PVStat).FindViewById<ImageView>(Resource.Id.statIcon);
            views.PVText = view.FindViewById<View>(Resource.Id.PVStat).FindViewById<TextView>(Resource.Id.statNumber);

            return views;
        }

        public class StatViews
        {
            public View Root { get; set; }
            public TextView PlayerName { get; set; }
            public ImageView GoalImage { get; set; }
            public ImageView AssistImage { get; set; }
            public ImageView P2Image { get; set; }
            public ImageView P5Image { get; set; }
            public ImageView P10Image { get; set; }
            public ImageView PVImage { get; set; }
            public TextView GoalText { get; set; }
            public TextView AssistText { get; set; }
            public TextView P2Text { get; set; }
            public TextView P5Text { get; set; }
            public TextView P10Text { get; set; }
            public TextView PVText { get; set; }

        }

    }
}