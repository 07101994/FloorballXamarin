using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Floorball.LocalDB.Tables;
using Floorball.Droid.Utils;
using Floorball.Droid.Models;

namespace Floorball.Droid.Fragments
{
    public class EventDialogFragment : Android.Support.V4.App.DialogFragment
    {

        public MatchEventModel Events { get; set; }
        public View Root { get; set; }

        public static EventDialogFragment Instance(MatchEventModel ev)
        {
            var fragment = new EventDialogFragment();

            Bundle args = new Bundle();
            args.PutObject("event", ev);

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        { 
            base.OnCreate(savedInstanceState);

            Events = Arguments.GetObject<MatchEventModel>("event");

            //SetStyle(2, 0);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Dialog.SetTitle(UIHelper.GetMatchFullTime(Events.Time));

            Root = inflater.Inflate(Resource.Layout.EventDialog, container, false);

            SetEvent(Root.FindViewById<View>(Resource.Id.firstEvent), Events);

            if (Events.Assist != null)
            {
                SetEvent(Root.FindViewById<View>(Resource.Id.secondEvent), Events.Assist);
            }
            else
            {
                Root.FindViewById<View>(Resource.Id.secondEvent).Visibility = ViewStates.Gone;
            }

            Root.FindViewById<TextView>(Resource.Id.message).Text = Events.EventMessage.Code + " - " + Events.EventMessage.Message;

            return Root;
        }

        //public override Dialog OnCreateDialog(Bundle savedInstanceState)
        //{
        //    Dialog dialog = new Dialog(Activity);
        //    dialog.RequestWindowFeature(WindowFeatures.NoTitle);

        //    Root = LayoutInflater.From(Activity).Inflate(Resource.Layout.EventDialog, container, false);

        //    SetEvent(Root.FindViewById<View>(Resource.Id.firstEvent), Events[0]);

        //    if (Events.Count == 2)
        //    {
        //        SetEvent(Root.FindViewById<View>(Resource.Id.secondEvent), Events[1]);
        //    }
        //    else
        //    {
        //        Root.FindViewById<View>(Resource.Id.secondEvent).Visibility = ViewStates.Gone;
        //    }

        //    Root.FindViewById<TextView>(Resource.Id.message).Text = Events[0].EventMessage.Code + " - " + Events[0].EventMessage.Message;

        //    dialog.SetContentView()


        //    return base.OnCreateDialog(savedInstanceState);
        //}

        private void SetEvent(View view, MatchEventModel e)
        {
            view.FindViewById<TextView>(Resource.Id.playerName).Text = e.Player.Name;
            view.FindViewById<ImageView>(Resource.Id.eventImage).SetImageResource(e.ResourceId);

        }
    }
}