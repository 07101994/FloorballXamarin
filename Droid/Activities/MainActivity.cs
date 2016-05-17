using Android.App;
using Android.Widget;
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

namespace Floorball.Droid
{
    public interface IListItemSelected
    {
       void ListItemSelected(string s);
    }

	[Activity (Label = "Floorball", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Android.Support.V7.App.AppCompatActivity, IListItemSelected
    {
        public string[] MenuTitles { get; set; }
        public string MenuTitle { get; set; }

        public bool MenuOpened { get; set; }

        MainFragment fragment;

        public string ActivityTitle { get; set; }

        private DrawerLayout drawerLayout;
        private ListView listsView;
        private MyActionBarDrawerToggle ActionBarDrawerToggle { get; set; }

        public IEnumerable<League> Leagues { get; set; }

        public IEnumerable<Match> ActualMatches { get; set; }

        public IEnumerable<Team> ActualTeams { get; set; }

        public IEnumerable<Team> Teams { get; set; }



        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
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
            //TODO:::
            //Updater.Instance.UpdateDataList.Add(RESTHelper.GetUpdates(Updater.Instance.LastSyncDate));
            //TOdo:::

            Updater.Instance.UpdateDatabaseFromServer();
            editor.PutString("LastSyncDate", DateTime.Now.ToString());
            editor.Apply();

            //lastSyncDate = prefs.GetString("LastSyncDate", null);

            ////Leagues = RESTHelper.GetAllLeague();
            ////ActualMatches = RESTHelper.GetActualMatches().OrderBy(a => a.LeagueId).ThenBy(a => a.Date).ToList();
            ////ActualTeams = GetActualTeams(ActualMatches);
            ////Teams = RESTHelper.GetAllTeam();

            Leagues = Manager.GetAllLeague();
            ActualMatches = Manager.GetActualMatches().OrderBy(a => a.LeagueId).ThenBy(a => a.Date).ToList();
            ////ActualMatches = new List<Match>();
            ActualTeams = GetActualTeams(ActualMatches);
            Teams = Manager.GetAllTeam();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            MenuTitles = Resources.GetStringArray(Resource.Array.menu_items);
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            listsView = FindViewById<ListView>(Resource.Id.left_drawer);

            listsView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1,MenuTitles);
            listsView.ItemClick += (sender, args) => { ChangeFragments(args.Position); drawerLayout.CloseDrawers(); ActionBarDrawerToggle.SyncState(); };
            listsView.SetItemChecked(0, true);

            ActionBarDrawerToggle = new MyActionBarDrawerToggle(this, drawerLayout, Resource.String.menu, Resource.String.league);
            drawerLayout.SetDrawerListener(ActionBarDrawerToggle);
            ActionBarDrawerToggle.SyncState();

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetIcon(Resource.Drawable.ic_menu_black_24dp);


            listsView.SetBackgroundResource(Resource.Color.primary_dark);

            MenuOpened = true;

            
        }

        private void ChangeFragments(int position)
        {
            fragment = CreateNewFragment(position);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame,fragment).Commit();

            listsView.SetItemChecked(position, true);
            ActivityTitle = MenuTitles[position];
            drawerLayout.CloseDrawer(listsView);
        }

        private MainFragment CreateNewFragment(int position)
        {
            MainFragment fragment;

            switch (position)
            {
                case 0:
                    fragment = new ActualFragment();
                    //ActivityTitle = Resources.GetString(Resource.String.actual);
                    SupportActionBar.Title = ActivityTitle;

                    break;

                case 1:
                    fragment = new LeaguesFragment();
                    //ActivityTitle = Resources.GetString(Resource.String.league);
                    SupportActionBar.Title = ActivityTitle;

                    break;

                case 2:
                    fragment = new TeamsFragment();
                    //ActivityTitle = Resources.GetString(Resource.String.teams);
                    SupportActionBar.Title = ActivityTitle;

                    break;

                case 3:
                    fragment = new PlayersFragment();
                    //ActivityTitle = Resources.GetString(Resource.String.players);
                    SupportActionBar.Title = ActivityTitle;

                    break;

                case 4:
                    fragment = new RefereesFragment();
                    //ActivityTitle = Resources.GetString(Resource.String.referees);
                    SupportActionBar.Title = ActivityTitle;

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
                SupportActionBar.Title = ActivityTitle;
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

            fragment.listItemSelected(s);


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

    }
}


