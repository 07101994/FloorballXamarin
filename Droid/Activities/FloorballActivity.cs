using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.App;
using Floorball.Droid.Fragments;
using System.Threading.Tasks;
using Floorball.Signalr;
using Microsoft.AspNet.SignalR.Client;
using Floorball.LocalDB.Tables;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "FloorballActivity")]
    public abstract class FloorballActivity : AppCompatActivity, IDialogInterfaceOnClickListener
    {
        protected static bool IsStarted { get; set; }

        public UnitOfWork UoW { get; set; }

        protected IEnumerable<League> Leagues { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            IsStarted = false;

        }

        protected override void OnResume()
        {
            base.OnResume();

            Updater.Updater.Instance.UpdateStarted += UpdateStarted;
            Updater.Updater.Instance.UpdateEnded += UpdateEnded;

            //Events with SignalR
            FloorballClient.Instance.MatchStarted += MatchStartedHandler;
            FloorballClient.Instance.MatchEnded += MatchEndedHandler;
            FloorballClient.Instance.NewEventAdded += NewEventAddedHandler;
            FloorballClient.Instance.MatchTimeUpdated += MatchTimeUpdatedHandler;
            FloorballClient.Instance.Connecting += ConnectingHandler;
            FloorballClient.Instance.Connected += ConnectedHandler;
            FloorballClient.Instance.Disconnected += DisconenctedHandler;
            FloorballClient.Instance.Reconnecting += ReconnectingHandler;

            CheckIsSyncing();

        }

        protected override void OnPause()
        {
            base.OnPause();

            Updater.Updater.Instance.UpdateStarted -= UpdateStarted;
            Updater.Updater.Instance.UpdateEnded -= UpdateEnded;

            FloorballClient.Instance.MatchStarted -= MatchStartedHandler;
            FloorballClient.Instance.MatchEnded -= MatchEndedHandler;
            FloorballClient.Instance.NewEventAdded -= NewEventAddedHandler;
            FloorballClient.Instance.MatchTimeUpdated -= MatchTimeUpdatedHandler;
            FloorballClient.Instance.Connecting -= ConnectingHandler;
            FloorballClient.Instance.Connected -= ConnectedHandler;
            FloorballClient.Instance.Disconnected -= DisconenctedHandler;
            FloorballClient.Instance.Reconnecting -= ReconnectingHandler;

        }

        protected virtual void ReconnectingHandler()
        {
            RunOnUiThread(Reconnecting);
        }

        protected virtual void Reconnecting()
        {
            ShowProgressBar(Resources.GetString(Resource.String.reconnecting));
        }

        protected virtual void DisconenctedHandler()
        {
            RunOnUiThread(Disconencted);
        }

        protected virtual void Disconencted()
        {
            HideProgressBar(Resources.GetString(Resource.String.disconnected));
        }

        protected virtual void ConnectedHandler()
        {
            RunOnUiThread(Connected);
            
        }

        protected virtual void Connected()
        {
            if (!Updater.Updater.Instance.IsSyncing)
            {
                HideProgressBar(Resources.GetString(Resource.String.connected));
            }
        }

        protected virtual void ConnectingHandler()
        {
            RunOnUiThread(Connecting);
        }

        protected virtual void Connecting()
        {
            ShowProgressBar(Resources.GetString(Resource.String.connecting));
        }

        private void MatchTimeUpdatedHandler(int id)
        {
            RunOnUiThread(() => MatchTimeUpdated(id));
        }

        protected virtual void MatchTimeUpdated(int id)
        {
            
        }

        private void NewEventAddedHandler(int id)
        {
            RunOnUiThread(() => NewEventAdded(id));
        }

        protected virtual void NewEventAdded(int id)
        {
            
        }

        private void MatchEndedHandler(int id)
        {
            RunOnUiThread(() => MatchEnded(id));
        }

        protected virtual void MatchEnded(int id)
        {
            
        }

        private void MatchStartedHandler(int id)
        {
            RunOnUiThread(() => MatchStarted(id));
            
        }

        protected virtual void MatchStarted(int id)
        {
            
        }

        protected void ShowProgressBar(string text)
        {
            FindViewById<View>(Resource.Id.progressbar).Visibility = ViewStates.Visible;
            FindViewById<TextView>(Resource.Id.notification).Text = text;
        }

        protected async void HideProgressBar(string text)
        {
            FindViewById<View>(Resource.Id.progressbar).Visibility = ViewStates.Visible;
            FindViewById<TextView>(Resource.Id.notification).Text = text;
            await Task.Delay(3000);
            FindViewById<View>(Resource.Id.progressbar).Visibility = ViewStates.Gone;
            IsStarted = false;
        }

        protected virtual void UpdateEnded()
        {
            if (!(FloorballClient.Instance.ConnectionState == ConnectionState.Connecting || FloorballClient.Instance.ConnectionState == ConnectionState.Reconnecting))
            {
                HideProgressBar(Resources.GetString(Resource.String.connected));
            }
        }

        protected virtual void UpdateStarted()
        {
            ShowProgressBar(Resources.GetString(Resource.String.connecting));
        }

        public void OnClick(IDialogInterface dialog, int which)
        {
            FinishAffinity();
        }

        protected ProgressDialog ShowInitializing(string initText){
            //Android.Support.V4.App.DialogFragment initDialog = AppInitFragment.Instance();
            //initDialog.Show(SupportFragmentManager,"initdialog");

            ProgressDialog initDialog = new ProgressDialog(this);
            initDialog.SetMessage(initText);
            initDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            initDialog.Show();

            return initDialog;
        }

        protected void ChangeText(ProgressDialog dialog, string message)
        {
            dialog.SetMessage(message);
        }

        protected void DismisInitializing(ProgressDialog dialog)
        {
            dialog.Dismiss();
        }

        protected void ShowAlertDialog(Exception ex)
        {

            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this);

            builder.SetMessage(ex.Message).SetTitle(Resource.String.errorDialogTitle);
            builder.SetNeutralButton(Resource.String.errorDialogButton, this);

            Android.Support.V7.App.AlertDialog dialog = builder.Create();
            dialog.Show();
        }

        protected virtual void InitToolbar()
        {
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.Title = "";

            FindViewById<TextView>(Resource.Id.toolbarTitle).Text = "Floorball";
        }

        protected void SaveSyncDate(ISharedPreferences prefs, DateTime lastSyncDate)
        {
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("LastSyncDate", lastSyncDate.ToString());
            editor.Apply();
        }

        protected SortedSet<string> GetLeaguesFromSharedPreference(ISharedPreferences prefs)
        {
            SortedSet<string> leagues = new SortedSet<string>();

            foreach (var country in Enum.GetValues(typeof(CountriesEnum)).Cast<CountriesEnum>())
            {
                leagues.UnionWith(prefs.GetStringSet(country.ToString().ToLower(), new List<string>()).ToList());
            }

            return leagues;
        }

        protected virtual void InitProperties()
        {
            UoW = new UnitOfWork();
        }

        protected abstract void InitActivityProperties();

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:

                    Finish();

                    return true;

                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void CheckIsSyncing()
        {
            if (Updater.Updater.Instance.IsSyncing)
            {
                UpdateStarted();
            }
            else
            {
                if (FloorballClient.Instance.ConnectionState == ConnectionState.Connecting)
                {
                    Connecting();
                }
                else
                {
                    if (FloorballClient.Instance.ConnectionState == ConnectionState.Reconnecting)
                    {
                        Reconnecting();
                    }
                    else
                    {
                        if (!IsStarted)
                        {
                            FindViewById<View>(Resource.Id.progressbar).Visibility = ViewStates.Gone;
                        }
                    }
                }
            }
            
        }
    }
}