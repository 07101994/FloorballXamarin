using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V4.App;
using Floorball.Droid.Fragments;
using Android.Content.Res;
using Android.Views;
using Android.Support.V4.View;
using System;
using System.Collections.Generic;
using FloorballServer.Models.Floorball;
using Floorball.REST;
using System.Linq;
using Android.Content;
using Android.Preferences;
using System.Globalization;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using Android.Support.V7.Widget;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;

namespace Floorball.Droid
{
    public interface IListItemSelected
    {
       void ListItemSelected(string s);
    }

    [Activity(Label = "Floorball", MainLauncher = true, Icon = "@mipmap/ball")]
    //[Activity(Label = "Floorball")]
    public class MainActivity : AppCompatActivity, IListItemSelected, ISharedPreferencesOnSharedPreferenceChangeListener
    {
        public string[] MenuTitles { get; set; }
        public string MenuTitle { get; set; }

        public bool MenuOpened { get; set; }

        Android.Support.V4.App.Fragment fragment;

        public string ActivityTitle { get; set; }

        private DrawerLayout drawerLayout;

        private MyActionBarDrawerToggle ActionBarDrawerToggle { get; set; }

        public IEnumerable<League> Leagues { get; set; }

        public IEnumerable<Match> ActualMatches { get; set; }

        public IEnumerable<Team> ActualTeams { get; set; }

        public IEnumerable<Team> Teams { get; set; }

        public SortedSet<CountriesEnum> Countries { get; set; }

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            Countries = GetCountriesFromSharedPreference(prefs);

            ISharedPreferencesEditor editor = prefs.Edit();
            string lastSyncDate = prefs.GetString("LastSyncDate", null);
            if (lastSyncDate == null)
            {
                //Első alkalmazás futás
                lastSyncDate = DateTime.Now.ToString();
                Manager.CreateDatabase();
                Manager.InitDatabaseFromServer();
            }

            Updater.Instance.LastSyncDate = DateTime.Parse(lastSyncDate);// Exact(lastSyncDate, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);

            Updater.Instance.UpdateDatabaseFromServer();
            editor.PutString("LastSyncDate", DateTime.Now.ToString());
            editor.Apply();

            Leagues = Manager.GetAllLeague().Where(l => Countries.Contains(l.Country));
            if (Countries.Count > 0)
            {
                ActualMatches = Manager.GetActualMatches().OrderBy(a => a.LeagueId).ThenBy(a => a.Date).ToList();
                //ActualMatches = Manager.GetAllMatch();
                ActualTeams = GetActualTeams(ActualMatches);
            }
            else
            {
                ActualMatches = new List<Match>();
                ActualTeams = new List<Team>();
            }
            Teams = Manager.GetAllTeam().Where(t => Countries.Contains(t.Country));

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            MenuTitles = Resources.GetStringArray(Resource.Array.menu_items);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            ActionBarDrawerToggle = new MyActionBarDrawerToggle(this, drawerLayout, Resource.String.menu, Resource.String.league);
            drawerLayout.AddDrawerListener(ActionBarDrawerToggle);
            drawerLayout.SetStatusBarBackgroundColor(Resource.Color.primary_dark);
            ActionBarDrawerToggle.SyncState();

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.Title = "";
            FindViewById<TextView>(Resource.Id.toolbarTitle).Text = "Floorball";

            FindViewById<NavigationView>(Resource.Id.drawerNavigationView).NavigationItemSelected += NavigationDrawerItemSelected;

            ChangeFragments(0);

            MenuOpened = true;


        }

        private SortedSet<CountriesEnum> GetCountriesFromSharedPreference(ISharedPreferences prefs)
        {
            SortedSet<CountriesEnum> countries = new SortedSet<CountriesEnum>();
            
            if (prefs.GetBoolean(Resources.GetString(Resource.String.hungary),false))
            {
                countries.Add(CountriesEnum.HU);
            }

            if (prefs.GetBoolean(Resources.GetString(Resource.String.sweden), false))
            {
                countries.Add(CountriesEnum.SE);
            }

            if (prefs.GetBoolean(Resources.GetString(Resource.String.finnland), false))
            {
                countries.Add(CountriesEnum.FL);
            }

            if (prefs.GetBoolean(Resources.GetString(Resource.String.switzerland), false))
            {
                countries.Add(CountriesEnum.SW);
            }

            if (prefs.GetBoolean(Resources.GetString(Resource.String.czech), false))
            {
                countries.Add(CountriesEnum.CZ);
            }

            return countries;
        }

        private void NavigationDrawerItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            switch (e.MenuItem.ItemId)
            {

                case Resource.Id.actualmenuitem:

                    ChangeFragments(0);
                    drawerLayout.CloseDrawers();
                    break;

                case Resource.Id.leaguemenuitem:

                    ChangeFragments(1);
                    drawerLayout.CloseDrawers();
                    break;

                case Resource.Id.teamsmenuitem:

                    ChangeFragments(2);
                    drawerLayout.CloseDrawers();
                    break;

                case Resource.Id.playersmenuitem:

                    ChangeFragments(3);
                    drawerLayout.CloseDrawers();
                    break;

                case Resource.Id.refereesmenuitem:

                    ChangeFragments(4);
                    drawerLayout.CloseDrawers();
                    break;

                case Resource.Id.settingsmenuitem:

                    ChangeFragments(5);
                    drawerLayout.CloseDrawers();
                    break;

                default:
                    break;
            }
        }

        private void ChangeFragments(int position)
        {
            fragment = CreateNewFragment(position);
            //SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();
        }

        private Android.Support.V4.App.Fragment CreateNewFragment(int position)
        {
            Android.Support.V4.App.Fragment fragment;

            switch (position)
            {
                case 0:
                    fragment = ActualFragment.Instance();
                    break;

                case 1:
                    fragment = LeaguesFragment.Instance();
                    break;

                case 2:
                    fragment = TeamsFragment.Instance();
                    break;

                case 3:
                    fragment = PlayersFragment.Instance();
                    break;

                case 4:
                    fragment = RefereesFragment.Instance();
                    break;

                case 5:
                    fragment = SettingsFragment.Instance();

                    break;

                default:
                    fragment = null;
                    break;
            }

            return fragment;
        }

        public override void OnPostCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnPostCreate(savedInstanceState, persistentState);
            ActionBarDrawerToggle.SyncState();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            ActionBarDrawerToggle.OnConfigurationChanged(newConfig);
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (ActionBarDrawerToggle.OnOptionsItemSelected(item))
            {
                //Console.WriteLine("Klikk.");
                //if (MenuOpened)
                //{
                //    SupportActionBar.SetTitle(Resource.String.menu);
                //    MenuOpened = false;
                //}
                //else
                //{
                //    SupportActionBar.SetTitle(Resource.String.league);
                //    MenuOpened = true;
                //}
                return true;
                //if (!HomeEnabled)
                //{
                //    return false;
                //}
                //else
                //{
                //    //drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
                //    //SupportActionBar.SetHomeButtonEnabled(false);
                //    //SupportActionBar.SetDisplayHomeAsUpEnabled(false);
                //    HomeEnabled = false;
                //    return true;
                //}
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            if (ActionBarDrawerToggle.IsMoving)
            {
                //SupportActionBar.Title = ActivityTitle;
                FindViewById<TextView>(Resource.Id.toolbarTitle).Text = ActivityTitle;
                drawerLayout.CloseDrawers();
            }
            else
            {
                if (drawerLayout.IsDrawerOpen(GravityCompat.Start))
                {
                    //SupportActionBar.SetTitle(Resource.String.league);
                    drawerLayout.CloseDrawers();
                }
                else
                {
                    base.OnBackPressed();
                }
            }


        }

        public void ListItemSelected(string s)
        {

            //fragment.listItemSelected(s);


        }

        private List<Team> GetActualTeams(IEnumerable<Match> actualMatches)
        {
            List<Team> teams = new List<Team>();

            foreach (var match in actualMatches)
            {

                teams.Add(Manager.GetTeamById(match.HomeTeamId));
                teams.Add(Manager.GetTeamById(match.AwayTeamId));
            }

            return teams;
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            if (Countries.Select(c => c.ToFriendlyString()).Contains(key))
            {
                if (sharedPreferences.GetBoolean(key,false))
                {
                    Countries.Add(key.ToEnum<CountriesEnum>());
                }
            }
        }
    }
}


