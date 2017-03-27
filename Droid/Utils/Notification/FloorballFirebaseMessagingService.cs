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
using Firebase.Messaging;
using Android.Util;

namespace Floorball.Droid.Utils.Notification
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    class FloorballFirebaseMessagingService : FirebaseMessagingService
    {

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            //Log.Debug("MESSAGING","Message received: " + message.Data["msg"]);
            
        }

    }
}