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
using Floorball.Droid.Utils;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "RefereeActivity")]
    public class RefereeActivity : FloorballActivity
    {
        RecyclerView recyclerView;
        RefereeStatsAdapter adapter;

        public Referee Referee { get; set; }

        public IEnumerable<LocalDB.Tables.Event> Events { get; set; }

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
                List<LocalDB.Tables.Event> leagueEvents = new List<LocalDB.Tables.Event>();
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


        protected override void InitProperties()
        {
            base.InitProperties();

            Referee = Intent.GetObject<Referee>("referee");
            Matches = UoW.MatchRepo.GetMatchesByReferee(Referee.Id);
            List<LocalDB.Tables.Event> events = new List<LocalDB.Tables.Event>();
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