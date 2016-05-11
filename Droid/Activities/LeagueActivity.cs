using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.App;
using Floorball.Droid.Fragments;
using Android.Support.V4.View;
using Java.Lang;
using Android.Support.Design.Widget;
using FloorballServer.Models.Floorball;
using Floorball.REST;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "LeagueActivity")]
    public class LeagueActivity : Android.Support.V7.App.AppCompatActivity
    {

        LeaguePageAdapter pagerAdapter;

        public League League { get; set; }

        public List<Team> Teams { get; set; }

        public List<Match> Matches { get; set; }

        public List<Statistic> Statistics { get; set; }

        public List<PlayerStatisticsModel> PlayerStatistics { get; set; }

        public List<Player> Players { get; set; }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.League);

            ViewPager pager = FindViewById<ViewPager>(Resource.Id.leaguePager);
            pagerAdapter = new LeaguePageAdapter(SupportFragmentManager);
            pager.Adapter = pagerAdapter;

            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.leaguetabs);
            tabs.SetupWithViewPager(pager);

            League = new League();
            League.Id = Intent.GetIntExtra("leagueId",0);
            League.Rounds = Intent.GetIntExtra("leagueRounds",0);
            League.Name = Intent.GetStringExtra("leagueName");
            League.ClassName = Intent.GetStringExtra("leagueClass");
            //League.Year = Intent.GetIntExtra("leagueYear");

            SupportActionBar.Title = League.Name;

            //Teams = new List<TeamModel>();
            //TeamModel model = new TeamModel();
            //model.Id = 1;
            //model.Name = "PFSE";
            //model.Get = 10;
            //model.Scored = 80;
            //model.Standing = 1;
            //model.Points = 12;
            //model.Match = 6;
            //Teams.Add(model);
            //Teams.Add(model);
            //Teams.Add(model);
            //Teams.Add(model);
            //Teams.Add(model);
            //Teams.Add(model);
            //Teams.Add(model);


            //Teams = RESTHelper.GetTeamsByLeague(League.Id);
            Teams = Manager.GetTeamsByLeague(League.Id);

            //Matches = new List<MatchModel>();
            //MatchModel matchModel = new MatchModel();
            //matchModel.HomeTeamId = 1;
            //matchModel.AwayTeamId = 1;
            //matchModel.Date = DateTime.Now.ToShortDateString();
            //matchModel.GoalsA = 10;
            //matchModel.GoalsH = 5;
            //matchModel.Round = 1;
            //MatchModel matchModel1 = new MatchModel();
            //matchModel1.HomeTeamId = 1;
            //matchModel1.AwayTeamId = 1;
            //matchModel1.Date = DateTime.Now.ToShortDateString();
            //matchModel1.GoalsA = 4;
            //matchModel1.GoalsH = 6;
            //matchModel1.Round = 2;
            //Matches.Add(matchModel);
            //Matches.Add(matchModel);
            //Matches.Add(matchModel);
            //Matches.Add(matchModel);
            //Matches.Add(matchModel1);
            //Matches.Add(matchModel1);
            //Matches.Add(matchModel1);
            //Matches.Add(matchModel1);
            //Matches.Add(matchModel1);

            //Matches = RESTHelper.GetMatchesByLeague(League.Id);
            Matches = Manager.GetMatchesByLeague(League.Id);

            //Statistics = new List<StatisticModel>();
            //StatisticModel statisticsModel = new StatisticModel();
            //statisticsModel.Id = 1;
            //statisticsModel.Name = "G";
            //statisticsModel.Number = 10;
            //statisticsModel.PlayerRegNum = 2294;
            //statisticsModel.TeamId = 1;
            //StatisticModel statisticsModel1 = new StatisticModel();
            //statisticsModel1.Id = 1;
            //statisticsModel1.Name = "A";
            //statisticsModel1.Number = 10;
            //statisticsModel1.PlayerRegNum = 2294;
            //statisticsModel1.TeamId = 1;
            //StatisticModel statisticsModel2 = new StatisticModel();
            //statisticsModel2.Id = 1;
            //statisticsModel2.Name = "P2";
            //statisticsModel2.Number = 10;
            //statisticsModel2.PlayerRegNum = 2294;
            //statisticsModel2.TeamId = 1;
            //StatisticModel statisticsModel3 = new StatisticModel();
            //statisticsModel3.Id = 1;
            //statisticsModel3.Name = "P5";
            //statisticsModel3.Number = 10;
            //statisticsModel3.PlayerRegNum = 2294;
            //statisticsModel3.TeamId = 1;
            //StatisticModel statisticsModel4 = new StatisticModel();
            //statisticsModel4.Id = 1;
            //statisticsModel4.Name = "P10";
            //statisticsModel4.Number = 10;
            //statisticsModel4.PlayerRegNum = 2294;
            //statisticsModel4.TeamId = 1;
            //Statistics.Add(statisticsModel);
            //Statistics.Add(statisticsModel);
            //Statistics.Add(statisticsModel);
            //Statistics.Add(statisticsModel);
            //Statistics.Add(statisticsModel);
            //Statistics.Add(statisticsModel);
            //Statistics.Add(statisticsModel);
            //Statistics.Add(statisticsModel1);
            //Statistics.Add(statisticsModel1);
            //Statistics.Add(statisticsModel1);
            //Statistics.Add(statisticsModel1);
            //Statistics.Add(statisticsModel2);
            //Statistics.Add(statisticsModel2);
            //Statistics.Add(statisticsModel2);
            //Statistics.Add(statisticsModel3);
            //Statistics.Add(statisticsModel4);

            //Statistics = RESTHelper.GetStatisticsByLeague(League.Id);
            Statistics = Manager.GetStatisticsByLeague(League.Id);

            PlayerStatistics = PlayerStatisticsMaker.CreatePlayerStatistics(Statistics);

            //Players = new List<PlayerModel>();
            //PlayerModel playerModel = new PlayerModel();
            //playerModel.RegNum = 2294;
            //playerModel.Number = 37;
            //playerModel.Name = "Rési Tamás";
            //Players.Add(playerModel);

            //Players = RESTHelper.GetPlayersByLeague(League.Id);
            Players = Manager.GetPlayersByLeague(League.Id);
        }

        public class LeaguePageAdapter : FragmentPagerAdapter
        {
            public LeaguePageAdapter(Android.Support.V4.App.FragmentManager manager) : base(manager)
            {

            }

            public override int Count
            {
                get
                {
                    return 3;
                }
            }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                switch (position)
                {
                    case 0:
                        return new LeagueMatchesFragment();
                    case 1:
                        return new LeagueTableFragment();
                    case 2:
                        return new LeagueStatisticsFragment();
                    default:
                        return null;
                }
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                switch (position)
                {
                    case 0:
                        return new Java.Lang.String("Mérkőzések");
                    case 1:
                        return new Java.Lang.String("Tabella");
                    case 2:
                        return new Java.Lang.String("Statisztikák");
                    default:
                        return null;
                }
             
            }

            
        }


    }
}