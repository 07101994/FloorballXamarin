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

namespace Floorball.Droid.Activities
{
    [Activity(Label = "FloorballActivity")]
    public class FloorballActivity : AppCompatActivity, IDialogInterfaceOnClickListener
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public void OnClick(IDialogInterface dialog, int which)
        {
            throw new NotImplementedException();
        }

        protected void ShowInitializing()
        {
            throw new NotImplementedException();
        }

        protected void ShowUpdating()
        {
            throw new NotImplementedException();
        }

        protected void ShowAlertDialog(Exception ex)
        {

            Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this);

            builder.SetMessage(ex.Message).SetTitle(Resource.String.errorDialogTitle);
            builder.SetNeutralButton(Resource.String.errorDialogButton, this);

            Android.Support.V7.App.AlertDialog dialog = builder.Create();
        }
    }
}