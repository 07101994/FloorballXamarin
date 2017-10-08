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
using Floorball.LocalDB.Tables;
using Newtonsoft.Json;
using Floorball.LocalDB;
using Android.Support.V7.App;
using Android.Support.V4.App;
using Android.Graphics;
using System.IO;
using Floorball.Droid.Models;
using Floorball.Droid.Fragments;
using Floorball.Util;
using Floorball.Droid.Utils;
using Floorball.Signalr;
using FloorballServer.Models.Floorball;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "MatchActivity")]
    [IntentFilter(new[] { "Activity_Match" }, Categories = new[] { "android.intent.category.DEFAULT" })]
    public class MatchActivity : FloorballActivity
    {

        public Match Match { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }
        
        public IEnumerable<Player> HomeTeamPlayers { get; set; }

        public IEnumerable<Player> AwayTeamPlayers { get; set; }

        public League League { get; set; }

        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<Referee> Referees { get; set; }

        public Stadium Stadium { get; set; }

        public List<EventMessage> EventMessages { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            // Create your application here
            SetContentView(Resource.Layout.MatchActivity);

            //Initilalize toolbar
            InitToolbar();

            //Initialize properties
            InitProperties();

            //Initialize activity properties
            InitActivityProperties();

            //Attach tabbedfragment
            if (savedInstanceState == null)
            {
                var tabModels = new List<TabbedViewPagerModel>();
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.Events, TabTitle = Resources.GetString(Resource.String.matchEvents), Data = new MatchEvents { Events = CreateEvents(), Match = Match, HomeTeam = HomeTeam, AwayTeam = AwayTeam } });
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.MatccPlayers, TabTitle = Resources.GetString(Resource.String.matchPlayers), Data = new MatchPlayersModel { Events = Events, HomeTeam = HomeTeam, AwayTeam = AwayTeam, Match = Match } });
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.MatchDetail, TabTitle = Resources.GetString(Resource.String.matchDetails), Data = new MatchDetailModel { Match = Match, League = League, Stadium = Stadium } });

                Android.Support.V4.App.Fragment fr = TabbedViewPagerFragment.Instance(tabModels);
                Android.Support.V4.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
                ft.Add(Resource.Id.content_frame, fr).Commit();
            }
        }

        private List<MatchEventModel> CreateEvents()
        {
            var events = new List<MatchEventModel>();

            foreach (var e in Events)
            {
                if (e.Type != EventType.A)
                {
                    var eventModel = new MatchEventModel { Id = e.Id};

                    if (e.TeamId == HomeTeam.Id)
                    {
                        eventModel.Player = HomeTeamPlayers.Where(p => p.Id == e.PlayerId).First();
                        eventModel.ViewType = 0;
                    }
                    else
                    {
                        eventModel.Player = AwayTeamPlayers.Where(p => p.Id == e.PlayerId).First();
                        eventModel.ViewType = 1;
                    }


                    if (e.Type == EventType.P2 || e.Type == EventType.P10)
                    {
                        eventModel.ResourceId = Resource.Drawable.ic_numeric_2_box_grey600_24dp;
                    }
                    else
                    {
                        if (e.Type == EventType.P5)
                        {
                            eventModel.ResourceId = Resource.Drawable.ic_numeric_2_box_grey600_24dp;
                        }
                        else
                        {
                            if (e.Type == EventType.G)
                            {
                                eventModel.ResourceId = Resource.Drawable.ball;
                                eventModel.IsGoal = true;

                                var assist = Events.FirstOrDefault(a => a.Time == e.Time && a.Type == EventType.A);
                                if (assist != null)
                                {
                                    var player = assist.TeamId == HomeTeam.Id ? HomeTeamPlayers.Where(p => p.Id == e.PlayerId).First() : AwayTeamPlayers.Where(p => p.Id == e.PlayerId).First();
                                    eventModel.Assist = new MatchEventModel { ResourceId = Resource.Drawable.ball , Player = player };
                                }
                            }
                        }
                    }

                    eventModel.Time = e.Time;
                    eventModel.EventMessage = EventMessages.First(em => em.Id == e.EventMessageId);
                    events.Add(eventModel);
                }
            }

            return events;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:

                    Finish();
                    return true;

                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void InitProperties()
        {
            base.InitProperties();

            EventModel e = Intent.GetObject<EventModel>("entity");

            if (e != null)
            {
                FloorballClient.Instance.AddEventToMatch(e);
                Match = UoW.MatchRepo.GetMatchById(e.MatchId);
            }
            else
            {
                Match = UoW.MatchRepo.GetMatchById(Intent.GetIntExtra("id", 2));
            }

            HomeTeam = UoW.TeamRepo.GetTeamById(Match.HomeTeamId);
            AwayTeam = UoW.TeamRepo.GetTeamById(Match.AwayTeamId);
            HomeTeamPlayers = HomeTeam.Players.Intersect(Match.Players, new KeyEqualityComparer<Player>(p => p.Id));
            AwayTeamPlayers = AwayTeam.Players.Intersect(Match.Players, new KeyEqualityComparer<Player>(p => p.Id));
            League = UoW.LeagueRepo.GetLeagueById(Match.LeagueId);
            Stadium = UoW.StadiumRepo.GetStadiumById(Match.StadiumId);
            Referees = Match.Referees;
            Events = UoW.EventRepo.GetEventsByMatch(Match.Id).OrderByDescending(ev => ev.Time);
            EventMessages = UoW.EventMessageRepo.GetAllEventMessage().ToList();
        }

        protected override void InitActivityProperties()
        {

        }

    }
}