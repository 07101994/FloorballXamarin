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
using FloorballServer.Models.Floorball;

namespace Floorball.Droid.Adapters
{
    class LeaguesAdapter : ArrayAdapter<LeagueModel>
    {

        public LeaguesAdapter(Context context , List<LeagueModel> leagues) : base(context,0,leagues)
        {

        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            LeagueModel model = GetItem(position);
            if (convertView == null)
            {
                convertView = LayoutInflater.From(Context).Inflate(Resource.Layout.LeagueItem, parent, false);
            }

            TextView name = convertView.FindViewById<TextView>(Resource.Id.leagueName);
            name.Text = model.Name;

            return convertView;
        }


    }
}