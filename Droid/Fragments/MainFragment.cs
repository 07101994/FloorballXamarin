using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.App;
using Android.Support.V4.App;
using Floorball.Signalr;

namespace Floorball.Droid.Fragments
{
    public abstract class MainFragment : Android.Support.V4.App.Fragment
    {
        public UnitOfWork UoW { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            UoW = new UnitOfWork();

        }

        public override void OnStart()
        {
            base.OnStart();

            //Events with SignalR
            //FloorballClient.Instance.MatchStarted += MatchStarted;
            //FloorballClient.Instance.MatchEnded += MatchEnded;
            //FloorballClient.Instance.NewEventAdded += NewEventAdded;
            //FloorballClient.Instance.EventDeleted += EventDeleted;
            //FloorballClient.Instance.MatchTimeUpdated += MatchTimeUpdated;
        }


        public override void OnStop()
        {
            base.OnStop();

            //FloorballClient.Instance.MatchStarted -= MatchStarted;
            //FloorballClient.Instance.MatchEnded -= MatchEnded;
            //FloorballClient.Instance.NewEventAdded -= NewEventAdded;
            //FloorballClient.Instance.EventDeleted -= EventDeleted;
            //FloorballClient.Instance.MatchTimeUpdated -= MatchTimeUpdated;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Updater.Updater.Instance.UpdateStarted += UpdateStarted;
            Updater.Updater.Instance.UpdateEnded += UpdateEnded;

            FloorballClient.Instance.MatchStarted += MatchStarted;
            FloorballClient.Instance.MatchEnded += MatchEnded;
            FloorballClient.Instance.NewEventAdded += NewEventAdded;
            FloorballClient.Instance.EventDeleted += EventDeleted;
            FloorballClient.Instance.MatchTimeUpdated += MatchTimeUpdated;

        }

        protected virtual void UpdateEnded()
        {
            
        }

        protected virtual void UpdateStarted()
        {
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();

            Updater.Updater.Instance.UpdateStarted -= UpdateStarted;
            Updater.Updater.Instance.UpdateEnded -= UpdateEnded;

            FloorballClient.Instance.MatchStarted -= MatchStarted;
            FloorballClient.Instance.MatchEnded -= MatchEnded;
            FloorballClient.Instance.NewEventAdded -= NewEventAdded;
            FloorballClient.Instance.EventDeleted -= EventDeleted;
            FloorballClient.Instance.MatchTimeUpdated -= MatchTimeUpdated;
        }

        protected virtual void MatchStarted(int matchId) { }
        protected virtual void MatchEnded(int matchId) { }
        protected virtual void NewEventAdded(int eventId) { }
        protected virtual void MatchTimeUpdated(int matchId) { }
        protected virtual void EventDeleted(int eventId) { }
    }
}