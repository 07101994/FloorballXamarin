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
using Floorball.Droid.Activities;
using Floorball.Droid.Utils.Notification.IntentServices;

namespace Floorball.Droid.Utils.Notification
{
    [Service(Enabled =  true, Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    class FloorballFirebaseMessagingService : FirebaseMessagingService
    {

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);

            StartService(CreateIntent(message.Data));
            

        }

        private Intent CreateIntent(IDictionary<string,string> message)
        {
            Intent intent = null;

            switch (message["messageType"])
            {
                case "newEvent":

                    intent = new Intent(this, typeof(EventIntentService));
                    intent.PutObject("entity", message["entity"]);
                    intent.PutObject("bodyArgs", message["titleArgs"]);
                    intent.PutObject("titleArgs", message["bodyArgs"]);

                    break;
                default:
                    break;
            }

            return intent;
        }
    }
}