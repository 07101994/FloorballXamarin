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
using Android.App;

namespace Floorball.Droid.Fragments
{
    public class AppInitFragment : Android.Support.V4.App.DialogFragment
    {

        public static AppInitFragment Instance()
        {
            AppInitFragment fragment = new AppInitFragment();


            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {

            return new AlertDialog.Builder(Activity).SetMessage("Alkalmazás inicializálása!").Create();

        }

        //public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        //{
        //    // Use this to return your custom view for this Fragment
        //    return inflater.Inflate(Resource.Layout.InitFragment, container, false);
        //}
    }
}