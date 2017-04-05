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
using Android.Media;
using Android.Support.V4.App;
using Android.Graphics;

namespace Floorball.Droid.Utils.Notification.IntentServices
{
    public abstract class BaseIntentService : IntentService
    {
        public UnitOfWork UoW { get; set; }

        public BaseIntentService()
        {
            UoW = new UnitOfWork();
        }

        protected abstract override void OnHandleIntent(Intent intent);

        protected void SendNotification(string title, string body, Type type)
        {
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this,type), PendingIntentFlags.OneShot);

            Android.Net.Uri defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);

            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this)
                .SetContentTitle(title)
                .SetContentText(body)
                .SetStyle(new NotificationCompat.BigTextStyle().BigText(body))
                .SetAutoCancel(false)
                .SetSmallIcon(Resource.Mipmap.ic_launcher)
                .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Mipmap.ic_launcher))
                .SetSound(defaultSoundUri)
                .SetColor(Resource.Color.primary)
                .SetContentIntent(pendingIntent);

            NotificationManager manager = GetSystemService(Context.NotificationService) as NotificationManager;

            manager.Notify(0, notificationBuilder.Build());

        }
    }
}