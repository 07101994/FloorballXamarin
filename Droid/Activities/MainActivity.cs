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
using Floorball.Updater;
using System.Linq;
using Android.Content;
using System.Globalization;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using Android.Support.V7.Widget;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using System.Threading.Tasks;
using Floorball.Droid.Utils;
using Floorball.Signalr;
using Microsoft.AspNet.SignalR.Client;
using Floorball.Droid.Models;
using Firebase.Iid;
using Floorball.Droid.Utils.Notification;
using Android.Support.V7.Preferences;
using Floorball.Droid.Utils.Notification.IntentServices;

namespace Floorball.Droid.Activities
{

    [Activity(Label = "Floorball", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
    //[Activity(Label = "Floorball")]
    public class MainActivity : FloorballActivity, ISharedPreferencesOnSharedPreferenceChangeListener, PreferenceFragmentCompat.IOnPreferenceStartScreenCallback
    {
        public string[] MenuTitles { get; set; }

        public string MenuTitle { get; set; }

        public bool MenuOpened { get; set; }

        Android.Support.V4.App.Fragment fragment;

        public string ActivityTitle { get; set; }

        private DrawerLayout drawerLayout;

        private MyActionBarDrawerToggle ActionBarDrawerToggle { get; set; }

        public List<Match> ActualMatches { get; set; }

        public IEnumerable<Team> ActualTeams { get; set; }

        public IEnumerable<Team> Teams { get; set; }

        public SortedSet<string> LeagueIds { get; set; }

        private DateTime lastSyncDate;
        private ISharedPreferences prefs;

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
            
            prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            prefs.RegisterOnSharedPreferenceChangeListener(this);
            lastSyncDate = DateTime.Parse(prefs.GetString("LastSyncDate", "1900-12-12"));
            //lastSyncDate = new DateTime(1900,12,12);

            //Init country from preference
            LeagueIds = GetLeaguesFromSharedPreference(prefs);

            //Set content view
            SetContentView(Resource.Layout.Main);
            
            //Initialize the toolbar
            InitToolbar();

            //Initialize the drawerlayout
            InitDrawerlayout();

            //Get updates and connect to signalr
            Init();

            //Check play services
            if (FirebaseNotificationService.Instance(this).IsPlayServicesAvailable())
            {
                if (FirebaseInstanceId.Instance.Token != null)
                {
                    Log.Debug("Refreshed Token:", FirebaseInstanceId.Instance.Token);
                }
            }
            StartService(new Intent(this, typeof(EventIntentService)));

        }

        private async void Init()
        {
            try
            {
                Task<DateTime> lastSyncDateTask;

                if (IsFirstLaunch(lastSyncDate))
                {
                    //Init the whole local DB
                    lastSyncDateTask = Manager.Instance.InitLocalDatabase();

                    //Show app initializing
                    var dialog = ShowInitializing(Resources.GetString(Resource.String.initDownload));

                    //Initializing finished
                    lastSyncDate = await lastSyncDateTask;
                    Updater.Updater.Instance.LastSyncDate = lastSyncDate;

                    //change init text
                    //ChangeText(dialog, Resources.GetString(Resource.String.initPrepare));

                    //Initialize properties
                    InitProperties();

                    //dismiss app initializing
                    DismisInitializing(dialog);

                    //Change to first (actual fragment)
                    ChangeFragments(0);

                }
                else
                {
                    //Initialize properties
                    InitProperties();

                    //Change to first (actual fragment)
                    ChangeFragments(0);

                    try
                    {
						//Get updates from server
						GetUpdates();
					}
                    catch (Exception)
                    {
                    }
                }

                //Save to sharedpreference
                SaveSyncDate(prefs, lastSyncDate);

                //Connect to signalr
                ConnectToSignalRServer();

            }
            catch (Exception ex)
            {
                ShowAlertDialog(ex);
            }
        }

        private async void GetUpdates()
        {
            //Check is there any remote database updates and update local DB
            await Updater.Updater.Instance.UpdateDatabaseFromServer(lastSyncDate);

            lastSyncDate = Updater.Updater.Instance.LastSyncDate;
           
        }

        private void InitDrawerlayout()
        {
            MenuTitles = Resources.GetStringArray(Resource.Array.menu_items);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            ActionBarDrawerToggle = new MyActionBarDrawerToggle(this, drawerLayout, Resource.String.menu, Resource.String.league);
            drawerLayout.AddDrawerListener(ActionBarDrawerToggle);
            drawerLayout.SetStatusBarBackgroundColor(Resource.Color.primary_dark);

            ActionBarDrawerToggle.SyncState();

            FindViewById<NavigationView>(Resource.Id.drawerNavigationView).NavigationItemSelected += NavigationDrawerItemSelected;

            MenuOpened = true;
        }
        
        private bool IsFirstLaunch(DateTime lastSyncDate)
        {
            return lastSyncDate.CompareTo(new DateTime(1900,12,12)) == 0;
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
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, fragment).Commit();
        }

        private Android.Support.V4.App.Fragment CreateNewFragment(int position)
        {
            Android.Support.V4.App.Fragment fragment;

            switch (position)
            {
                case 0:
                    fragment = ActualFragment.Instance(ActualMatches.Where(m => m.State == StateEnum.Playing), ActualMatches.Where(m => m.State != StateEnum.Playing),ActualTeams, Leagues);
                    break;

                case 1:
                    fragment = Fragments.ListFragment.Instance(UoW.LeagueRepo.GetAllYear().Select(l => new ListModel { Text = l.ToString(), Object = l}),"leagues", Resources.GetString(Resource.String.leagueSeasons));
                    break;

                case 2:
                    fragment = Fragments.ListFragment.Instance(UoW.LeagueRepo.GetAllYear().Select(l => new ListModel { Text = l.ToString(), Object = l }),"teams", Resources.GetString(Resource.String.teamSeasons));
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

        private async void ConnectToSignalRServer()
        {
            if (ActualMatches.Where(m => m.State == StateEnum.Playing).Count() > 0 && FloorballClient.Instance.ConnectionState == ConnectionState.Disconnected)
            {
                try
                {
                    if (!(await FloorballClient.Instance.Connect(LeagueIds)))
                    {
                        IsStarted = true;
                        RunOnUiThread(() => HideProgressBar("Nincs csatlakozva!"));
                    }
                }
                catch (Exception)
                {
                    RunOnUiThread(() => HideProgressBar("Nincs csatlakozva!"));
                }
            }
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
                return true;
            }

            SupportFragmentManager.PopBackStackImmediate();

            SetDrawerState(true);

            return false;
        }

        public override void OnBackPressed()
        {
            if (ActionBarDrawerToggle.IsMoving)
            {
                FindViewById<TextView>(Resource.Id.toolbarTitle).Text = ActivityTitle;
                drawerLayout.CloseDrawers();
            }
            else
            {
                if (drawerLayout.IsDrawerOpen(GravityCompat.Start))
                {
                    drawerLayout.CloseDrawers();
                }
                else
                {
                    SetDrawerState(true);

                    base.OnBackPressed();
                }
            }


        }

        private List<Team> GetActualTeams(IEnumerable<Match> actualMatches)
        {
            List<Team> teams = new List<Team>();

            foreach (var match in actualMatches)
            {

                teams.Add(UoW.TeamRepo.GetTeamById(match.HomeTeamId));
                teams.Add(UoW.TeamRepo.GetTeamById(match.AwayTeamId));
            }

            return teams;
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            LeagueIds = GetLeaguesFromSharedPreference(prefs);
            InitProperties();
        }

        protected override void InitProperties()
        {
            base.InitProperties();

            var leagueIds = LeagueIds.Select(l => Convert.ToInt32(l)).ToList();

            //Initialize properties from database
            Leagues = UoW.LeagueRepo.GetLeaguesByIds(leagueIds) ?? new List<League>();
            Teams = UoW.TeamRepo.GetTeamsByLeagues(leagueIds) ?? new List<Team>();
            ActualMatches = UoW.MatchRepo.GetActualMatches(Leagues).OrderBy(a => a.LeagueId).ThenBy(a => a.Date).ToList() ?? new List<Match>().OrderBy(a => a.LeagueId).ToList();
            ActualTeams = GetActualTeams(ActualMatches) ?? new List<Team>();
        }

        protected override void InitActivityProperties()
        {
            throw new NotImplementedException();
        }

        protected override void NewEventAdded(int id)
        {
            base.NewEventAdded(id);

            var e = UoW.EventRepo.GetEventById(id);
            
            var match = ActualMatches.FirstOrDefault(m => m.Id == e.MatchId);
            if (match != null)
            {
                if (e.Type == EventType.G)
                {
                    if (e.TeamId == match.HomeTeamId)
                    {
                        match.ScoreH++;
                    }
                    else
                    {
                        match.ScoreA++;
                    }
                }
                match.Time = match.Time < e.Time ? e.Time : match.Time;
            }
        }

        protected override void MatchTimeUpdated(int id)
        {
            base.MatchTimeUpdated(id);

            var m = UoW.MatchRepo.GetMatchById(id);
            var match = ActualMatches.FirstOrDefault(m1 => m1.Id == id);
            if (match != null)
            {
                match.Time = m.Time;
            }
        }

        public bool OnPreferenceStartScreen(PreferenceFragmentCompat caller, PreferenceScreen pref)
        {

            var ft = SupportFragmentManager.BeginTransaction();

            PreferenceFragmentCompat fragment;

            if (pref.Key == "leaguesSettings")
            {
                fragment = ChooseLeaguesSettingsFragment.Instance(Leagues);
            }
            else
            {
                fragment = ChooseNotificationsSettingsFragment.Instance();
            }

            Bundle args = new Bundle();

            args.PutString(PreferenceFragmentCompat.ArgPreferenceRoot, pref.Key);
            fragment.Arguments = args;
            ft.Replace(Resource.Id.content_frame, fragment, pref.Key).AddToBackStack(null).Commit();

            SetDrawerState(false);

            return true;

        }

        private void SetDrawerState(bool isEnabled)
        {
            if (isEnabled)
            {
                SupportActionBar.SetHomeButtonEnabled(false);
                drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
                ActionBarDrawerToggle.OnDrawerStateChanged(DrawerLayout.LockModeUnlocked);
                ActionBarDrawerToggle.DrawerIndicatorEnabled = true;
                ActionBarDrawerToggle.SyncState();
            }
            else
            {
                SupportActionBar.SetHomeButtonEnabled(true);
                drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
                ActionBarDrawerToggle.OnDrawerStateChanged(DrawerLayout.LockModeLockedClosed);
                ActionBarDrawerToggle.DrawerIndicatorEnabled = false;
                ActionBarDrawerToggle.SyncState();
            }
        }

        
    }
}


