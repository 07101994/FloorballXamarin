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
using FloorballServer.Models.Floorball;
using Newtonsoft.Json;
using Floorball.Droid.Activities;

namespace Floorball.Droid.Utils.Notification.IntentServices
{
    [Service(Exported = true, Enabled = true)]
    public class EventIntentService : BaseIntentService
    {

        protected override void OnHandleIntent(Intent intent)
        {
            //EventModel e = intent.GetObject<EventModel>("entity");

            ////add event to database
            //UoW.EventRepo.AddEvent(e.Id, e.MatchId, e.Type, e.Time, e.PlayerId, e.EventMessageId, e.TeamId);

            //send notification
            if (FloorballAppication.IsInBackround)
            {
                ////get notification params
                //string notificatinTitle = Resources.GetString(Resource.String.homeGoalBody, intent.GetObject<List<string>>("titleArgs").ToJavaArray());
                //string notificationBody = Resources.GetString(Resource.String.homeGoalBody, intent.GetObject<List<string>>("bodyArgs").ToJavaArray());

                //SendNotification(notificatinTitle, notificationBody, typeof(MatchActivity));
                SendNotification("Értesítése érkezett", "Ez egy nagyon hosszú értesítése, amire ha rányomsz, akkor látszódni fog az egész.", typeof(MatchActivity));
            }

        }

    }
}