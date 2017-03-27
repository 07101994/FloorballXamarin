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
using Floorball.LocalDB.Tables;
using Floorball.Droid.Utils;
using Android.Support.V7.Preferences;
using Android.Support.V14.Preferences;
using Floorball.Droid.Activities;

namespace Floorball.Droid.Fragments
{
    public class ChooseLeaguesSettingsFragment : PreferenceFragmentCompat, ISharedPreferencesOnSharedPreferenceChangeListener
    {

        public IEnumerable<League> Leagues { get; set; }

        public static List<string> countries = new List<string>{ "HU", "SE", "FL", "SW", "CZ" };

        public static ChooseLeaguesSettingsFragment Instance(IEnumerable<League> leagues)
        {
            var fragment = new ChooseLeaguesSettingsFragment();
            Bundle args = new Bundle();
            args.PutObject("leagues", leagues);

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var UoW = new UnitOfWork();
            Leagues = UoW.LeagueRepo.GetAllLeague(); 

            var countriesCategory = PreferenceScreen.FindPreference("countriesCategory") as PreferenceCategory;

            foreach (var country in countries)
            {
                MultiSelectListPreference listPreference = new MultiSelectListPreference(Activity);
                listPreference.Title = Resources.GetString(Resources.GetIdentifier(country.ToLower(), "string", "com.resitomi.floorball"));
                listPreference.Key = country.ToLower();

                var listItems = Leagues.Where(l => l.Country.ToString() == country);
                listPreference.SetEntryValues(listItems.Select(l => l.Id.ToString()).ToArray());
                listPreference.SetEntries(listItems.Select(l => l.Name).ToArray());

                countriesCategory.AddPreference(listPreference);
            }

            var pref = PreferenceManager.GetDefaultSharedPreferences(Activity);
        }

        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {

            SetPreferencesFromResource(Resource.Xml.Settings, rootKey);

        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
           

        }

        


    }
}