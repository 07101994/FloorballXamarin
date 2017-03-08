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

namespace Floorball.Droid.Activities
{
    [Activity(Label = "MatchActivity")]//, MainLauncher = true, Icon = "@mipmap/ball")]
    public class MatchActivity : FloorballActivity
    {

        public Match Match { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }
        
        public IEnumerable<Player> HomeTeamPlayers { get; set; }

        public IEnumerable<Player> AwayTeamPlayers { get; set; }

        public League League { get; set; }

        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<EventMessage> EventMessages { get; set; }

        public IEnumerable<Referee> Referees { get; set; }

        public Stadium Stadium { get; set; }


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
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.Events, TabTitle = "Események", Data = new MatchEvents { Events = CreateEvents(), Match = Match, HomeTeam = HomeTeam, AwayTeam = AwayTeam } });
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.MatchDetails, TabTitle = "Részletek", Data = new MatchDetailModel { League = League, Match = Match, Stadium = Stadium } });
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.MatchReferees, TabTitle = "Játékvezetők", Data = Referees.Select(r => new ListModel { Text = r.Name, Object = r}) });

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
                if (e.Type != "A")
                {
                    var eventModel = new MatchEventModel { Id = e.Id};

                    if (e.TeamId == HomeTeam.Id)
                    {
                        eventModel.Player = HomeTeamPlayers.Where(p => p.RegNum == e.PlayerId).First().ShortName;
                        eventModel.ViewType = 0;
                    }
                    else
                    {
                        eventModel.Player = AwayTeamPlayers.Where(p => p.RegNum == e.PlayerId).First().ShortName;
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
                            }
                        }
                    }

                    eventModel.Time = e.Time;
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

            Match = UoW.MatchRepo.GetMatchById(Intent.GetIntExtra("id", 2));
            HomeTeam = UoW.TeamRepo.GetTeamById(Match.HomeTeamId);
            AwayTeam = UoW.TeamRepo.GetTeamById(Match.AwayTeamId);
            HomeTeamPlayers = HomeTeam.Players;
            AwayTeamPlayers = AwayTeam.Players;
            League = UoW.LeagueRepo.GetLeagueById(Match.LeagueId);
            Stadium = UoW.StadiumRepo.GetStadiumById(Match.StadiumId);
            Referees = Match.Referees;
            EventMessages = UoW.EventMessageRepo.GetAllEventMessage();
            Events = UoW.EventRepo.GetEventsByMatch(Match.Id).OrderByDescending(e => e.Time);
        }

        protected override void InitActivityProperties()
        {

        }

    }
}