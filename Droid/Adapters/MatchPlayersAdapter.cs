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
using Floorball.LocalDB.Tables;
using Floorball.Droid.ViewHolders;

namespace Floorball.Droid.Adapters
{
    public class MatchPlayersAdapter : BaseRecyclerViewAdapter<MatchPlayerItemModel>
    {

        public MatchPlayersAdapter(IEnumerable<Player> homePlayers, IEnumerable<Player> awayPlayers, IEnumerable<LocalDB.Tables.Event> events) : base(new List<MatchPlayerItemModel>()) 
        {
            List<PlayerWithEventsModel> homePlayersWithEvents = CreatePlayerEventsModel(homePlayers, events);
            List<PlayerWithEventsModel> awayPlayersWithEvents = CreatePlayerEventsModel(awayPlayers, events);

            ListItems = homePlayersWithEvents.Zip(awayPlayersWithEvents, (h, a) => new MatchPlayerItemModel { HomePlayer = h, AwayPlayer = a }).ToList();

            if (homePlayersWithEvents.Count < awayPlayersWithEvents.Count)
            {
                for (int i = homePlayersWithEvents.Count; i < awayPlayersWithEvents.Count; i++)
                {
                    ListItems.Add(new MatchPlayerItemModel { AwayPlayer = awayPlayersWithEvents[i] });
                }
            }
            else
            {
                for (int i = awayPlayersWithEvents.Count; i < homePlayersWithEvents.Count; i++)
                {
                    ListItems.Add(new MatchPlayerItemModel { HomePlayer = homePlayersWithEvents[i] });
                }
            }

        }

        private List<PlayerWithEventsModel> CreatePlayerEventsModel(IEnumerable<Player> players, IEnumerable<LocalDB.Tables.Event> events)
        {
            List<PlayerWithEventsModel> model = new List<PlayerWithEventsModel>();

            foreach (var player in players)
            {
                model.Add(new PlayerWithEventsModel { Name = player.ShortName, Events = events.Where(e => e.PlayerId == player.RegNum) });
            }

            return model.OrderBy(m => m.Goals).ThenBy(m => m.Assists).ThenBy(m => m.P2).ThenBy(m => m.P5).ThenBy(m => m.P10).ThenBy(m => m.PV).ThenBy(m => m.Number).ToList();
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            
            var vh = holder as MatchPlayerViewHolder;

            if (ListItems[position].HomePlayer != null)
            {
                vh.HomeStat = BindStats(vh.HomeStat, ListItems[position].HomePlayer);
            }
            else
            {
                vh.HomeStat.Root.Visibility = ViewStates.Invisible;
            }

            if (ListItems[position].AwayPlayer != null)
            {
                vh.AwayStat = BindStats(vh.AwayStat, ListItems[position].AwayPlayer);
            }
            else
            {
                vh.AwayStat.Root.Visibility = ViewStates.Invisible;
            }

            vh.Number.Text = (position+1).ToString();

        }

        private MatchPlayerViewHolder.StatViews BindStats(MatchPlayerViewHolder.StatViews stat, PlayerWithEventsModel player)
        {

            stat.PlayerName.Text = player.Name;
            
            if (player.Goals != 0)
            {
                stat.GoalImage.SetBackgroundResource(Resource.Drawable.ball);
                stat.GoalText.Text = player.Goals.ToString();
            }

            if (player.Assists != 0)
            {
                stat.AssistImage.SetBackgroundResource(Resource.Drawable.ball);
                stat.AssistText.Text = player.Assists.ToString();
            }

            if (player.P2 != 0)
            {
                stat.P2Image.SetBackgroundResource(Resource.Drawable.ic_numeric_2_box_grey600_24dp);
                stat.P2Text.Text = player.P2.ToString();
            }

            if (player.P5 != 0)
            {
                stat.P5Image.SetBackgroundResource(Resource.Drawable.ic_numeric_2_box_grey600_24dp);
                stat.P5Text.Text = player.P5.ToString();
            }

            if (player.P10 != 0)
            {
                stat.P10Image.SetBackgroundResource(Resource.Drawable.ic_numeric_2_box_grey600_24dp);
                stat.P10Text.Text = player.P10.ToString();
            }

            if (player.PV != 0)
            {
                stat.PVImage.SetBackgroundResource(Resource.Drawable.ic_numeric_2_box_grey600_24dp);
                stat.PVText.Text = player.PV.ToString();
            }


            return stat;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.MatchPlayerItem, parent, false);

            return new MatchPlayerViewHolder(itemView);
        }
    }
}