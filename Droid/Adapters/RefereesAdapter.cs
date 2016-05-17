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
    class RefereesAdapter : ArrayAdapter<Referee>
    {

        public RefereesAdapter(Context context, List<Referee> referees) : base(context,0,referees)
        {

        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            Referee model = GetItem(position);
            if (convertView == null)
            {
                convertView = LayoutInflater.From(Context).Inflate(Resource.Layout.RefereeItem, parent, false);
            }

            TextView name = convertView.FindViewById<TextView>(Resource.Id.refereeName);
            name.Text = model.Name;

            return convertView;
        }


    }
}