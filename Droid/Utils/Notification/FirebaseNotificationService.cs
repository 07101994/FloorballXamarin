using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Common;

namespace Floorball.Droid.Utils.Notification
{
    public class FirebaseNotificationService
    {
        private static FirebaseNotificationService instance;

        public Context Ctx { get; set; }

        private FirebaseNotificationService()
        {
        }

        public static FirebaseNotificationService Instance(Context ctx)
        {
            if (instance == null)
            {
                instance = new FirebaseNotificationService();
            }

            instance.Ctx = ctx;

            return instance;
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(Ctx);
            return resultCode == ConnectionResult.Success;
            
        }

    }
}