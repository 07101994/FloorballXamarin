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

namespace Floorball.Droid
{
    public interface IListItemSelected
    {
       void ListItemSelected(string s);
    }

	[Activity (Label = "Floorball", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Android.Support.V7.App.AppCompatActivity, IListItemSelected
    {
		//int count = 1;

        public string[] MenuTitles { get; set; }
        public string MenuTitle { get; set; }

        public bool MenuOpened { get; set; }

        MainFragment fragment;

        public string Title { get; set; }

        private DrawerLayout drawerLayout;
        private ListView listsView;
        private MyActionBarDrawerToggle ActionBarDrawerToggle { get; set; }

        protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
            
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

            // Get our button from the layout resource,
            // and attach an event to it
            //Button button = FindViewById<Button> (Resource.Id.myButton);

            //button.Click += delegate {
            //	button.Text = string.Format ("{0} clicks!", count++);
            //};
        }

        private void ChangeFragments(int position)
        {
            fragment = CreateNewFragment(position);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame,fragment).Commit();

            listsView.SetItemChecked(position, true);
            Title = MenuTitles[position];
            drawerLayout.CloseDrawer(listsView);
        }

        private MainFragment CreateNewFragment(int position)
        {
            MainFragment fragment;

            switch (position)
            {
                case 0:
                    fragment = new ActualFragment();
                    Title = Resources.GetString(Resource.String.actual);
                    SupportActionBar.Title = Title;

                    break;

                case 1:
                    fragment = new LeaguesFragment();
                    Title = Resources.GetString(Resource.String.league);
                    SupportActionBar.Title = Title;

                    break;

                case 2:
                    fragment = new TeamsFragment();
                    Title = Resources.GetString(Resource.String.teams);
                    SupportActionBar.Title = Title;

                    break;

                case 3:
                    fragment = new PlayersFragment();
                    Title = Resources.GetString(Resource.String.players);
                    SupportActionBar.Title = Title;

                    break;

                case 4:
                    fragment = new RefereesFragment();
                    Title = Resources.GetString(Resource.String.referees);
                    SupportActionBar.Title = Title;

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
                SupportActionBar.Title = Title;
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

    }
}


