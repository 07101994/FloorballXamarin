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

        public IEnumerable<League> Leagues { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<Match> LiveMatches { get; set; }
        public IEnumerable<Match> SoonMatches { get; set; }

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

            Leagues = Arguments.GetObject<IEnumerable<League>>("leagues");
            Teams = Arguments.GetObject<IEnumerable<Team>>("teams");
            LiveMatches = Arguments.GetObject<IEnumerable<Match>>("liveMatches");
            SoonMatches = Arguments.GetObject<IEnumerable<Match>>("soonMatches");

            adapter = new ActualAdapter(LiveMatches, SoonMatches, Teams, Leagues, Context);
            adapter.ClickedObject += Adapter_ClickedObject;
            
        }

        private void Adapter_ClickedObject(object sender, object e)
        {
            Intent intent = new Intent(Context, typeof(MatchActivity));
            intent.PutExtra("id", (e as LiveMatchModel).MatchId);
            StartActivity(intent);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.ActualFragment, container, false);

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            recyclerView.SetAdapter(adapter);

            return root;
        }

        protected override void MatchStarted(int matchId)
        {
            
        }

        protected override void MatchEnded(int matchId)
        {
            
        }

        protected override void NewEventAdded(int eventId)
        {
            UpdateGoals(eventId, 1);
        }

        protected override void EventDeleted(int eventId)
        {
            UpdateGoals(eventId, -1);
        }

        private void UpdateGoals(int eventId, int count)
        {
            LocalDB.Tables.Event e = UoW.EventRepo.GetEventById(eventId);

            if (e.Type == EventType.G)
            {
                var match = adapter.Contents.FirstOrDefault(c => (c as LiveMatchModel).MatchId == e.MatchId);
                if (match != null)
                {
                    var m = match as LiveMatchModel;
                    m.Time = m.Time < e.Time ? e.Time : m.Time;
                    if (e.TeamId == m.HomeTeamId)
                    {
                        m.HomeScore += count;
                    }
                    else
                    {
                        m.AwayScore += count;
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        adapter.NotifyDataSetChanged();
                    });
                }
            }
        }

        protected override void MatchTimeUpdated(int matchId)
        {

            Match m = UoW.MatchRepo.GetMatchById(matchId);

            var match = adapter.Contents.FirstOrDefault(c => (c as LiveMatchModel).MatchId == matchId) as LiveMatchModel;

            match.Time = m.Time;

            int index = adapter.Contents.FindIndex(c => (c as LiveMatchModel).MatchId == matchId);
            adapter.Contents[index] = match;
            Activity.RunOnUiThread(() =>
            {
                adapter.NotifyDataSetChanged(); ;
            });
        }

        protected override void UpdateEnded()
        {
            base.UpdateEnded();

            var ActualMatches = UoW.MatchRepo.GetActualMatches(Leagues).OrderBy(a => a.LeagueId).ThenBy(a => a.Date).ToList() ?? new List<Match>().OrderBy(a => a.LeagueId).ToList();
            LiveMatches = ActualMatches.Where(m => m.State == StateEnum.Playing);
            SoonMatches = ActualMatches.Where(m => m.State != StateEnum.Playing);
            Teams = GetActualTeams(ActualMatches) ?? new List<Team>();

            Activity.RunOnUiThread(() =>
            {
                adapter = new ActualAdapter(LiveMatches, SoonMatches, Teams, Leagues, Context);
                adapter.ClickedObject += Adapter_ClickedObject;
                View.FindViewById<RecyclerView>(Resource.Id.recyclerView).SetAdapter(adapter);

            });

        }
        

        private List<Team> GetActualTeams(IEnumerable<Match> actualMatches)
        {
            List<Team> teams = new List<Team>();

            foreach (var match in actualMatches)
            {

                teams.Add(UoW.TeamRepo.GetTeamById(match.HomeTeamId));
                teams.Add(UoW.TeamRepo.GetTeamById(match.AwayTeamId));
            }

            return teams;
        }
    }
}