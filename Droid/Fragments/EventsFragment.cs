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
using Floorball.Droid.Models;
using Newtonsoft.Json;
using Android.Support.V7.Widget;
using Floorball.Droid.Adapters;
using Android.Support.V4.App;
using Floorball.LocalDB.Tables;
using Android.Graphics;
using System.IO;
using Floorball.Droid.Activities;
using Floorball.Droid.Utils;

namespace Floorball.Droid.Fragments
{
    public class EventsFragment : MainFragment
    {
        RecyclerView recyclerView;
        EventsAdapter adapter;

        public Match Match { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public List<MatchEventModel> Events { get; set; }

        public static EventsFragment Instance(IEnumerable<MatchEventModel> events, Match match, Team homeTeam, Team awayTeam)
        {
            var fragment = new EventsFragment();

            Bundle args = new Bundle();
            args.PutObject("events", events);
            args.PutObject("match", match);
            args.PutObject("homeTeam", homeTeam);
            args.PutObject("awayTeam", awayTeam);

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            Events = Arguments.GetObject<IEnumerable<MatchEventModel>>("events").ToList();
            Match = Arguments.GetObject<Match>("match");
            HomeTeam = Arguments.GetObject<Team>("homeTeam");
            AwayTeam = Arguments.GetObject<Team>("awayTeam");
            adapter = new EventsAdapter(Events);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View root = inflater.Inflate(Resource.Layout.Events, container, false);

            root = InitView(root);

            return root;
        }

        private View InitView(View root)
        {
            root.FindViewById<TextView>(Resource.Id.homeTeamName).Text = HomeTeam.Name;
            root.FindViewById<TextView>(Resource.Id.awayTeamName).Text = AwayTeam.Name;

            root.FindViewById<TextView>(Resource.Id.homeTeamScore).Text = Match.GoalsH.ToString();
            root.FindViewById<TextView>(Resource.Id.awayTeamScore).Text = Match.GoalsA.ToString();

            root.FindViewById<TextView>(Resource.Id.actualTime).Text = UIHelper.GetMatchFullTime(Match.Time);

            SetTeamImage(HomeTeam, root.FindViewById<ImageView>(Resource.Id.homeTeamImage));
            SetTeamImage(AwayTeam, root.FindViewById<ImageView>(Resource.Id.awayTeamImage));

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            recyclerView.SetAdapter(adapter);

            return root;
        }

        

        protected override void MatchTimeUpdated(int matchId)
        {
            if (matchId == Match.Id)
            {
                Match m = UoW.MatchRepo.GetMatchById(matchId);
                Match.Time = m.Time;
                Activity.RunOnUiThread(() =>
                {
                    View.FindViewById<TextView>(Resource.Id.actualTime).Text = UIHelper.GetMatchFullTime(Match.Time);
                });
            }
        }

        protected override void NewEventAdded(int eventId)
        {
            base.NewEventAdded(eventId);

            Event e = UoW.EventRepo.GetEventById(eventId);

            if (e.Type != "A")
            {
                var eventModel = new MatchEventModel { Id = e.Id };

                eventModel.Player = UoW.PlayerRepo.GetPlayerById(e.PlayerId).ShortName;

                Match.Time = Match.Time < e.Time ? e.Time : Match.Time;

                Activity.RunOnUiThread(() =>
                {
                    View.FindViewById<TextView>(Resource.Id.actualTime).Text = UIHelper.GetMatchFullTime(Match.Time);
                });

                if (e.TeamId == HomeTeam.Id)
                {
                    eventModel.ViewType = 0;
                }
                else
                {
                    eventModel.ViewType = 1;
                }

                if (e.Type == "P2" || e.Type == "P10")
                {
                    eventModel.ResourceId = Resource.Drawable.ic_numeric_2_box_grey600_24dp;
                    
                }
                else
                {
                    if (e.Type == "P5")
                    {
                        eventModel.ResourceId = Resource.Drawable.ic_numeric_2_box_grey600_24dp;
                    }
                    else
                    {
                        if (e.Type == "G")
                        {
                            eventModel.IsGoal = true;
                            if (e.TeamId == HomeTeam.Id)
                            {
                                Match.GoalsH++;
                                Activity.RunOnUiThread(() =>
                                {
                                    View.FindViewById<TextView>(Resource.Id.homeTeamScore).Text = Match.GoalsH.ToString();
                                });
                            }
                            else
                            {
                                Match.GoalsA++;
                                Activity.RunOnUiThread(() =>
                                {
                                    View.FindViewById<TextView>(Resource.Id.awayTeamScore).Text = Match.GoalsA.ToString();
                                });
                            }
                            eventModel.ResourceId = Resource.Drawable.ball;
                        }
                    }
                }

                eventModel.Time = e.Time;
                Events.Add(eventModel);
                Events = Events.OrderByDescending(ev => ev.Time).ToList();
                Activity.RunOnUiThread(() =>
                {
                    adapter.Swap(Events.ToList());
                });
            }
        }

        protected override void EventDeleted(int eventId)
        {
            base.EventDeleted(eventId);

            var removed = Events.FirstOrDefault(e => e.Id == eventId);

            if (removed != null)
            {
                if (removed.IsGoal)
                {
                    if (removed.ViewType == 0)
                    {
                        Match.GoalsH++;
                        Activity.RunOnUiThread(() =>
                        {
                            View.FindViewById<TextView>(Resource.Id.homeTeamScore).Text = Match.GoalsH.ToString();
                        });
                    }
                    else
                    {
                        Match.GoalsA++;
                        Activity.RunOnUiThread(() =>
                        {
                            View.FindViewById<TextView>(Resource.Id.awayTeamScore).Text = Match.GoalsA.ToString();
                        });
                    }
                }
                
                Activity.RunOnUiThread(() => { Events.Remove(removed); });
            }

        }

        protected override void UpdateEnded()
        {
            base.UpdateEnded();

            var events = UoW.EventRepo.GetEventsByMatch(Match.Id).OrderByDescending(e => e.Time);
            Match = UoW.MatchRepo.GetMatchById(Match.Id);
            HomeTeam = UoW.TeamRepo.GetTeamById(Match.HomeTeamId);
            AwayTeam = UoW.TeamRepo.GetTeamById(Match.AwayTeamId);
            Events = CreateEvents(events);

            Activity.RunOnUiThread(() => InitView(View));

        }

        private List<MatchEventModel> CreateEvents(IEnumerable<Event> events)
        {
            var eventmodels = new List<MatchEventModel>();

            foreach (var e in events)
            {
                if (e.Type != "A")
                {
                    var eventModel = new MatchEventModel { Id = e.Id };

                    if (e.TeamId == HomeTeam.Id)
                    {
                        eventModel.Player = HomeTeam.Players.Where(p => p.RegNum == e.PlayerId).First().ShortName;
                        eventModel.ViewType = 0;
                    }
                    else
                    {
                        eventModel.Player = AwayTeam.Players.Where(p => p.RegNum == e.PlayerId).First().ShortName;
                        eventModel.ViewType = 1;
                    }


                    if (e.Type == "P2" || e.Type == "P10")
                    {
                        eventModel.ResourceId = Resource.Drawable.ic_numeric_2_box_grey600_24dp;
                    }
                    else
                    {
                        if (e.Type == "P5")
                        {
                            eventModel.ResourceId = Resource.Drawable.ic_numeric_2_box_grey600_24dp;
                        }
                        else
                        {
                            if (e.Type == "G")
                            {
                                eventModel.ResourceId = Resource.Drawable.ball;
                                eventModel.IsGoal = true;
                            }
                        }
                    }

                    eventModel.Time = e.Time;
                    eventmodels.Add(eventModel);
                }
            }

            return eventmodels;
        }

    }
}