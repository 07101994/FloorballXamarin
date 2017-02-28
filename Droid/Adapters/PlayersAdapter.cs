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

namespace Floorball.Droid.Adapters
{
    class PlayersAdapter : ArrayAdapter<Player>
    {
        public PlayersAdapter(Context context, List<Player> players) : base(context,0,players)
        {

        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            Player model = GetItem(position);
            if (convertView == null)
            {
                //convertView = LayoutInflater.From(Context).Inflate(Resource.Layout.PlayerItem, parent, false);
            }

            TextView name = convertView.FindViewById<TextView>(Resource.Id.playerName);
            name.Text = model.Name;

            //ImageView image = convertView.FindViewById<ImageView>(Resource.Id.playerTeamImage);
            //image.SetBackgroundResource(Resource.Drawable.phoenix);

            return convertView;
        }


    }
}