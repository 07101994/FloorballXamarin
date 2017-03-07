using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Preferences;

namespace Floorball.Droid.Fragments
{
    public class SettingsFragment : PreferenceFragmentCompat { 

        public static SettingsFragment Instance()
        {
            return new SettingsFragment();
        }

        //public override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    // Create your fragment here
        //    //AddPreferencesFromResource(Resource.Xml.Settings);

        //}

        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {
            SetPreferencesFromResource(Resource.Xml.Settings, rootKey);
        }
        
        //public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        //{
        //    // Use this to return your custom view for this Fragment
        //    // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

        //    //return inflater.Inflate(Resource.Layout.Settings, container, false);

        //    return base.OnCreateView(inflater, container, savedInstanceState);
        //}

        
        //public bool OnPreferenceStartScreen(PreferenceFragmentCompat preferenceFragmentCompat, PreferenceScreen preferenceScreen)
        //{
        //    preferenceFragmentCompat.PreferenceScreen = preferenceScreen;
        //    return true;
        //}
    }
}