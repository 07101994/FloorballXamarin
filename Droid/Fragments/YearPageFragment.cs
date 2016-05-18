using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

namespace Floorball.Droid.Fragments
{
    public class YearPageFragment : Fragment
    {

        public string Year { get; set; }

        public static YearPageFragment Instance(string year)
        {
            YearPageFragment fragment = new YearPageFragment();

            Bundle bundle = new Bundle();
            bundle.PutString("year",year);
            fragment.Arguments = bundle;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

            Bundle bundle = Arguments;
            Year = bundle.GetString("year");

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View root = inflater.Inflate(Resource.Layout.YearViewPagerContent, container, false);

            root.FindViewById<TextView>(Resource.Id.year).Text = Year + " - " + (Convert.ToInt32(Year) + 1); 

            return root;
        }
    }
}