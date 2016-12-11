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

namespace Floorball.Droid.Activities
{
    [Activity(Label = "FloorballActivity")]
    public abstract class FloorballActivity : AppCompatActivity, IDialogInterfaceOnClickListener
    {

        public UnitOfWork UoW { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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

        protected void ShowUpdating()
        {
            FindViewById<TextView>(Resource.Id.notification).Visibility = ViewStates.Visible;
            FindViewById<TextView>(Resource.Id.notification).Text = "Frissítés folyamatban..";
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

        protected SortedSet<CountriesEnum> GetCountriesFromSharedPreference(ISharedPreferences prefs)
        {
            SortedSet<CountriesEnum> countries = new SortedSet<CountriesEnum>();

            if (prefs.GetBoolean(Resources.GetString(Resource.String.hungary), false))
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

        protected virtual void InitProperties()
        {
            UoW = new UnitOfWork();
        }

        protected abstract void InitActivityProperties();

    }
}