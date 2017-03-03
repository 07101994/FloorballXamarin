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
using Android.Support.V7.Widget;
using Floorball.Droid.Adapters;
using Floorball.Droid.Models;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "RefereeActivity")]
    public class RefereeActivity : FloorballActivity
    {
        RecyclerView recyclerView;
        RefereeStatsAdapter adapter;

        public Referee Referee { get; set; }

        public IEnumerable<Event> Events { get; set; }

        public IEnumerable<League> Leagues { get; set; }

        public IEnumerable<Match> Matches { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RefereeStat);

            //Initialize toolbar
            InitToolbar();

            //Initialize properties
            InitProperties();

            FindViewById<TextView>(Resource.Id.refereeName).Text = Referee.Name;

            //CreateRefereeStat(Leagues, Events, Matches, FindViewById<LinearLayout>(Resource.Id.linearlayout));
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            adapter = new RefereeStatsAdapter(CreateStatModels());
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerView.SetAdapter(adapter);

        }

        private List<RefereeStatModel> CreateStatModels()
        {

            var model = new List<RefereeStatModel>();

            foreach (var league in Leagues)
            {
                List<Event> leagueEvents = new List<Event>();
                IEnumerable<int> leagueMatchIds = Matches.Where(m => m.LeagueId == league.Id).Select(m => m.Id);
                foreach (var e in Events)
                {
                    if (leagueMatchIds.Contains(e.MatchId))
                    {
                        leagueEvents.Add(e);
                    }
                }

                model.Add(new RefereeStatModel
                {
                    LeagueName = league.Name,
                    Year = league.Year,
                    NumberOfMatches = leagueMatchIds.Count(),
                    TwoMinutesPenalties = leagueEvents.Where(e => e.Type == "P2").Count(),
                    FiveMinutesPenalties = leagueEvents.Where(e => e.Type == "P5").Count(),
                    TenMinutesPenalties = leagueEvents.Where(e => e.Type == "P10").Count(),
                    FinalPenalties = leagueEvents.Where(e => e.Type == "PV").Count()

            });

            }

            return model;
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

        private void CreateRefereeStat(IEnumerable<League> leagues, IEnumerable<Event> events, IEnumerable<Match> matches, LinearLayout container)
        {

            foreach (var league in leagues)
            {
                int numberOfMatches;
                int twoMinutesPenalties;
                int fiveMinutesPenalties;
                int tenMinutesPenalties;
                int finalPenalties;

                List<Event> leagueEvents = new List<Event>();
                IEnumerable<int> leagueMatchIds = matches.Where(m => m.LeagueId == league.Id).Select(m => m.Id);
                foreach (var e in events)
                {
                    if (leagueMatchIds.Contains(e.MatchId))
                    {
                        leagueEvents.Add(e);
                    }
                }

                numberOfMatches = leagueMatchIds.Count();
                twoMinutesPenalties = leagueEvents.Where(e => e.Type == "P2").Count();
                fiveMinutesPenalties = leagueEvents.Where(e => e.Type == "P5").Count();
                tenMinutesPenalties = leagueEvents.Where(e => e.Type == "P10").Count();
                finalPenalties = leagueEvents.Where(e => e.Type == "PV").Count();
                
                ViewGroup stat = LayoutInflater.Inflate(Resource.Layout.Stat, container, false) as ViewGroup;
                stat.FindViewById<TextView>(Resource.Id.leagueName).Text = league.Name;// + " (" + league.Year.Year + "-" + (league.Year.Year + 1) + ")";
                stat.FindViewById<TextView>(Resource.Id.leagueYear).Text = " (" + league.Year.Year + "-" + (league.Year.Year + 1) + ")";

                LinearLayout statCard = stat.FindViewById<LinearLayout>(Resource.Id.statCard);

                ViewGroup appearence = LayoutInflater.Inflate(Resource.Layout.StatLine, statCard, false) as ViewGroup;
                appearence.FindViewById<TextView>(Resource.Id.statLabel).Text = "Mérkőzésszám: ";
                appearence.FindViewById<TextView>(Resource.Id.statNumber).Text = numberOfMatches.ToString();
                statCard.AddView(appearence);

                ViewGroup twoMinutesPenaltiesView = LayoutInflater.Inflate(Resource.Layout.StatLine, statCard, false) as ViewGroup;
                twoMinutesPenaltiesView.FindViewById<TextView>(Resource.Id.statLabel).Text = "2 perc: ";
                twoMinutesPenaltiesView.FindViewById<TextView>(Resource.Id.statNumber).Text = twoMinutesPenalties.ToString();
                statCard.AddView(twoMinutesPenaltiesView);

                ViewGroup fiveMinutesPenaltiesView = LayoutInflater.Inflate(Resource.Layout.StatLine, statCard, false) as ViewGroup;
                fiveMinutesPenaltiesView.FindViewById<TextView>(Resource.Id.statLabel).Text = "5 perc: ";
                fiveMinutesPenaltiesView.FindViewById<TextView>(Resource.Id.statNumber).Text = fiveMinutesPenalties.ToString();
                statCard.AddView(fiveMinutesPenaltiesView);

                ViewGroup tenMinutesPenaltiesView = LayoutInflater.Inflate(Resource.Layout.StatLine, statCard, false) as ViewGroup;
                tenMinutesPenaltiesView.FindViewById<TextView>(Resource.Id.statLabel).Text = "10 perc: ";
                tenMinutesPenaltiesView.FindViewById<TextView>(Resource.Id.statNumber).Text = tenMinutesPenalties.ToString();
                statCard.AddView(tenMinutesPenaltiesView);

                ViewGroup finalPenaltiesView = LayoutInflater.Inflate(Resource.Layout.StatLine, statCard, false) as ViewGroup;
                finalPenaltiesView.FindViewById<TextView>(Resource.Id.statLabel).Text = "Végleges: ";
                finalPenaltiesView.FindViewById<TextView>(Resource.Id.statNumber).Text = finalPenalties.ToString();
                statCard.AddView(finalPenaltiesView);

                container.AddView(stat);

            }

        }

        protected override void InitProperties()
        {
            base.InitProperties();

            Referee = JsonConvert.DeserializeObject<Referee>(Intent.GetStringExtra("referee"));
            Matches = UoW.MatchRepo.GetMatchesByReferee(Referee.Id);
            List<Event> events = new List<Event>();
            Matches.Select(m => UoW.EventRepo.GetEventsByMatch(m.Id)).ToList().ForEach(e => events.AddRange(e));
            Events = events;
            Leagues = UoW.LeagueRepo.GetLeaguesByReferee(Referee.Id).OrderByDescending(l => l.Year);
        }

        protected override void InitActivityProperties()
        {
            throw new NotImplementedException();
        }
    }
}