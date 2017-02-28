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
using Floorball.LocalDB.Tables;
using Android.Support.V7.Widget;
using Floorball.Droid.ViewHolders;

namespace Floorball.Droid.Adapters
{
    class PlayersAdapter : BaseRecyclerViewAdapter<Player>
    {
        public PlayersAdapter(List<Player> players) : base(players)
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PlayerViewHolder<Player> vh = holder as PlayerViewHolder<Player>;
            //vh.Image.SetImageResource(Resource.Drawable.hu);
            vh.Text.Text = ListItems[position].Name;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.PlayerItem, parent, false);

            var vh = new PlayerViewHolder<Player>(itemView, OnClickObject, this);

            return vh;
        }
    }
}