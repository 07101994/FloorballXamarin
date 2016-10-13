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

        public int RealEventCount { get; set; }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Manager.CreateDatabase();
            //Manager.InitDatabaseFromServer();

            //Match = JsonConvert.DeserializeObject<Match>(Intent.GetStringExtra("match"));
            Match = Manager.GetMatchById(Intent.GetIntExtra("id",2));
            HomeTeam = Manager.GetTeamById(Match.HomeTeamId);
            AwayTeam = Manager.GetTeamById(Match.AwayTeamId);
            HomeTeamPlayers = HomeTeam.Players;
            AwayTeamPlayers = AwayTeam.Players;
            League = Manager.GetLeagueById(Match.LeagueId);
            Stadium = Manager.GetStadiumById(Match.StadiumId);
            Referees = Match.Referees;
            EventMessages = Manager.GetAllEventMessage();
            Events = Manager.GetEventsByMatch(Match.Id).OrderByDescending(e => e.Time);

            // Create your application here
            SetContentView(Resource.Layout.MatchActivity);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.Title = "";
            FindViewById<TextView>(Resource.Id.toolbarTitle).Text = "Floorball";

            FindViewById<TextView>(Resource.Id.leagueName).Text = League.Name + " " + Match.Round.ToString() + ". forduló";
            FindViewById<TextView>(Resource.Id.date).Text = Match.Date.ToShortDateString();
            FindViewById<TextView>(Resource.Id.stadium).Text = Stadium.Name;

            FindViewById<TextView>(Resource.Id.homeTeamName).Text = HomeTeam.Name;
            FindViewById<TextView>(Resource.Id.awayTeamName).Text = AwayTeam.Name;

            FindViewById<TextView>(Resource.Id.homeTeamScore).Text = Match.GoalsH.ToString();
            FindViewById<TextView>(Resource.Id.awayTeamScore).Text = Match.GoalsA.ToString();

            FindViewById<TextView>(Resource.Id.actualTime).Text = Match.Time.Hours == 1 ? "Vége" : Match.Time.Minutes.ToString() + ":" + Match.Time.Seconds.ToString();

            FindViewById<ImageView>(Resource.Id.homeTeamImage).SetImageResource(Resource.Drawable.phoenix);
            FindViewById<ImageView>(Resource.Id.awayTeamImage).SetImageResource(Resource.Drawable.phoenix);

            CreateEvents();
            CreateReferees();

            ChangeTimelineHeight();
            
        }

        private void ChangeTimelineHeight()
        {
            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.timeLine);
            ViewGroup.LayoutParams parameters = layout.LayoutParameters;
            parameters.Height = RealEventCount * 100; //50*2

            layout.LayoutParameters = parameters;

        }

        private void CreateEvents()
        {
            RealEventCount = 0;

            ViewGroup eventLayout = FindViewById<LinearLayout>(Resource.Id.eventContainer);
          

            foreach (var e in Events)
            {

                if (e.Type != "A")
                {
                    ViewGroup eventItem = LayoutInflater.Inflate(Resource.Layout.EventItem, eventLayout, false) as ViewGroup;

                    ViewGroup eventCard;
                    ViewGroup relativeLayout;

                    if (e.TeamId == HomeTeam.Id)
                    {
                        relativeLayout = eventItem.FindViewById<RelativeLayout>(Resource.Id.homeTeamEventId);
                        eventCard = LayoutInflater.Inflate(Resource.Layout.EventCard, relativeLayout, false) as ViewGroup;
                        eventCard.FindViewById<TextView>(Resource.Id.playerName).Text = HomeTeamPlayers.Where(p => p.RegNum == e.PlayerId).First().ShortName;
                    }
                    else
                    {
                        relativeLayout = eventItem.FindViewById<RelativeLayout>(Resource.Id.awayTeamEventId);
                        eventCard = LayoutInflater.Inflate(Resource.Layout.EventCard, relativeLayout, false) as ViewGroup;
                        eventCard.FindViewById<TextView>(Resource.Id.playerName).Text = AwayTeamPlayers.Where(p => p.RegNum == e.PlayerId).First().ShortName;
                    }


                    if (e.Type == "P2" || e.Type == "P10")
                    {
                        eventCard.FindViewById<ImageView>(Resource.Id.eventImage).SetImageResource(Resource.Drawable.ic_numeric_2_box_grey600_24dp);
                    }
                    else
                    {
                        if (e.Type == "P5")
                        {
                            eventCard.FindViewById<ImageView>(Resource.Id.eventImage).SetImageResource(Resource.Drawable.ic_numeric_2_box_grey600_24dp);
                        }
                        else
                        {
                            if (e.Type == "G")
                            {
                                eventCard.FindViewById<ImageView>(Resource.Id.eventImage).SetImageResource(Resource.Drawable.ball);
                            }
                        }
                    }


                    eventItem.FindViewById<TextView>(Resource.Id.time).Text = e.Time.Split(':')[1] + ":" + e.Time.Split(':')[2];

                    relativeLayout.AddView(eventCard);

                    eventLayout.AddView(eventItem);
                    RealEventCount++;
                }
            }


        }

        private void CreateReferees()
        {
            ViewGroup refereeLayout = FindViewById<LinearLayout>(Resource.Id.refereesLayout);

            foreach (var referee in Referees)
            {
                ViewGroup refereeItem = LayoutInflater.Inflate(Resource.Layout.RefereeItem, refereeLayout, false) as ViewGroup;
                refereeItem.FindViewById<TextView>(Resource.Id.refereeName).Text = referee.Name;

                refereeLayout.AddView(refereeItem);
            }

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:

                    //NavUtils.NavigateUpFromSameTask(this);
                    Finish();
                    return true;

                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

    }
}