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
using Android.Support.V4.App;
using FloorballServer.Models.Floorball;
using Floorball.REST;
using Floorball.LocalDB.Tables;
using Floorball.Signalr;
using Microsoft.AspNet.SignalR.Client;
using Android.Support.V7.Widget;
using Android.Animation;
using Android.Graphics.Drawables;
using Floorball.LocalDB;
using Floorball.Droid.Activities;
using System.Threading.Tasks;
using Floorball.Droid.Adapters;
using Newtonsoft.Json;
using Floorball.Droid.Models;
using Floorball.Droid.Utils;

namespace Floorball.Droid.Fragments
{
    public class ActualFragment : MainFragment
    {

        RecyclerView recyclerView;
        ActualAdapter adapter;

        public static ActualFragment Instance(IEnumerable<Match> liveMatches, IEnumerable<Match> soonMatches, IEnumerable<Team> teams, IEnumerable<League> leagues)
        {
            var fragment = new ActualFragment();

            Bundle args = new Bundle();
            args.PutObject("liveMatches", liveMatches);
            args.PutObject("soonMatches",soonMatches);
            args.PutObject("teams",teams);
            args.PutObject("leagues",leagues);
            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            adapter = new ActualAdapter(Arguments.GetObject<IEnumerable<Match>>("liveMatches"),
                Arguments.GetObject<IEnumerable<Match>>("soonMatches"),
                Arguments.GetObject<IEnumerable<Team>>("teams"),
                Arguments.GetObject<IEnumerable<League>>("leagues"),
                Context);
            adapter.ClickedObject += Adapter_ClickedObject;
            
            
            FloorballClient.Instance.MatchStarted += MatchStarted;
            FloorballClient.Instance.MatchEnded += MatchEnded;
            FloorballClient.Instance.NewEventAdded += NewEventAdded;
            FloorballClient.Instance.MatchTimeUpdated += MatchTimeUpdated;

        }

        private void Adapter_ClickedObject(object sender, object e)
        {
            Intent intent = new Intent(Context, typeof(MatchActivity));
            intent.PutExtra("id", (e as LiveMatchModel).MatchId);
            StartActivity(intent);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.RecycleView, container, false);

            //CreateLiveMatches(root);
            //CreateSoonMatches(root);

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            recyclerView.SetAdapter(adapter);

            return root;
        }

        public override void listItemSelected(string s)
        {
            throw new NotImplementedException();
        }

        private void MatchStarted(int matchId)
        {
            throw new NotImplementedException();
        }

        private void MatchEnded(int matchId)
        {
            throw new NotImplementedException();
        }

        private void NewEventAdded(int eventId)
        {

            Event e = UoW.EventRepo.GetEventById(eventId);

            if (e.Type == "G")
            {
                var textView = (Activity.FindViewById(Resource.Id.matchesList1).FindViewWithTag(e.TeamId.ToString() + "score"+e.MatchId.ToString()) as TextView);

                short score = Convert.ToInt16(textView.Text);
                score++;

                Activity.RunOnUiThread(() => {
                    textView.Text = score.ToString();
                });
            }

        }

        private void MatchTimeUpdated(int matchId)
        {

            Match m = UoW.MatchRepo.GetMatchById(matchId);

            string newTime = UIHelper.GetMatchTime(m.Time,m.State);
            var textView = (Activity.FindViewById(Resource.Id.matchesList1).FindViewWithTag(matchId.ToString() + "time") as TextView);

            Activity.RunOnUiThread(() => {
                textView.Text = newTime;
            });
        }

    }
}