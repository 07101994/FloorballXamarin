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
using Floorball.LocalDB.Tables;
using Android.Content.Res;

namespace Floorball.Droid.Adapters
{
    class LeaguesAdapter : ArrayAdapter<League>
    {

        //private Context Context { get; set; }

        public LeaguesAdapter(Context context , List<League> leagues) : base(context,0,leagues)
        {
            //Context = context;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            League league = GetItem(position);
            if (convertView == null)
            {
                convertView = LayoutInflater.From(Context).Inflate(Resource.Layout.LeagueItem, parent, false);
            }

            convertView.FindViewById<TextView>(Resource.Id.leagueName).Text = league.Name;

            int resourceId = Context.Resources.GetIdentifier(league.Country.ToFriendlyString().ToLower(), "drawable", Context.PackageName);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                //leagueNameView.FindViewById<ImageView>(Resource.Id.countryFlag).SetImageDrawable(Resources.GetDrawable(Resource.Drawable.HU, activity.ApplicationContext.Theme));
                convertView.FindViewById<ImageView>(Resource.Id.countryFlag).SetImageDrawable(Context.Resources.GetDrawable(resourceId, Context.ApplicationContext.Theme));
            }
            else
            {
                convertView.FindViewById<ImageView>(Resource.Id.countryFlag).SetImageDrawable(Context.GetDrawable(resourceId));
            }

            return convertView;
        }


    }
}