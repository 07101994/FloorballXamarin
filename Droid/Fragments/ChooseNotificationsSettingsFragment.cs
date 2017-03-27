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
using Android.Support.V14.Preferences;
using Floorball.LocalDB.Tables;

namespace Floorball.Droid.Fragments
{
    public class ChooseNotificationsSettingsFragment : PreferenceFragmentCompat
    {

        public IEnumerable<League> Leagues { get; set; }


        public static ChooseNotificationsSettingsFragment Instance()
        {
            var fragment = new ChooseNotificationsSettingsFragment();

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var UoW = new UnitOfWork();
            Leagues = UoW.LeagueRepo.GetAllLeague();

            var pref = PreferenceManager.GetDefaultSharedPreferences(Activity);

            var allLeague = new List<string>();

            foreach (var country in ChooseLeaguesSettingsFragment.countries)
            {
                var leagues = pref.GetStringSet(country.ToLower(), null);
                
                if (pref.GetStringSet(country.ToLower(),null) != null)
                {
                    allLeague.AddRange(leagues);
                }
            }


            var filteredLeagues = Leagues.ToList();
            filteredLeagues.RemoveAll(l => !allLeague.Contains(l.Id.ToString()));

            UpdateNotification("starts", filteredLeagues);
            UpdateNotification("event", filteredLeagues);
            UpdateNotification("today", filteredLeagues);

        }

        public override void OnCreatePreferences(Bundle savedInstanceState, string rootKey)
        {
            SetPreferencesFromResource(Resource.Xml.Settings, rootKey);

        }

        private void UpdateNotification(string key, List<League> filteredLeagues)
        {
            var startsPreference = PreferenceScreen.FindPreference(key) as MultiSelectListPreference;
            startsPreference.SetEntryValues(filteredLeagues.Select(l => l.Id.ToString()).ToArray());
            startsPreference.SetEntries(filteredLeagues.Select(l => l.Name).ToArray());
        }

    }
}