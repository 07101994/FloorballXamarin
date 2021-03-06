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
using Newtonsoft.Json;
using Java.Lang;
using Floorball.Droid.Fragments;
using Newtonsoft.Json.Linq;
using Floorball.LocalDB.Tables;
using Floorball.Droid.Models;
using FloorballServer.Models.Floorball;

namespace Floorball.Droid.Adapters
{
    public class TabbedViewPagerAdapter : FragmentStatePagerAdapter
    {
        public List<TabbedViewPagerModel> Model { get; set; }

        public TabbedViewPagerAdapter(FragmentManager manager, List<TabbedViewPagerModel> model) : base(manager) 
        {
            Model = model;   
        }

        public override int Count
        {
            get
            {
                return Model.Count;
            }
        }

        public override Fragment GetItem(int position)
        {
            Fragment fr = null;

            var data = Model[position].Data as JContainer;
            
            switch (Model[position].FragmentType)
            {
                case FragmentType.Leagues:
                    fr = LeaguesFragment.Instance(data.ToObject<LeaguesModel>());
                    break;
                case FragmentType.Teams:
                    var teamsModel = data.ToObject<TeamsModel>();
                    fr = TeamsFragment.Instance(teamsModel.Teams, teamsModel.Leagues);
                    break;
                case FragmentType.Players:
                    var playersModel = data.ToObject<IEnumerable<ListModel>>();
                    fr = Fragments.ListFragment.Instance(playersModel,"players");
                    break;
                case FragmentType.TeamMatches:
                    var matchesModel = data.ToObject<MatchesModel>();
                    fr = TeamMatchesFragment.Instance(matchesModel.Teams,matchesModel.Matches,matchesModel.Leagues, matchesModel.TeamId);
                    break;
                case FragmentType.LeagueMatches:
                    var leagueMatchesModel = data.ToObject<MatchesModel>();
                    fr = LeagueMatchesFragment.Instance(leagueMatchesModel.Teams, leagueMatchesModel.Matches, leagueMatchesModel.Leagues.First());
                    break;
                case FragmentType.LeagueStats:
                    var leagueStatModel = data.ToObject<LeagueStatModel>();
                    fr = LeagueStatisticsFragment.Instance(leagueStatModel.Stats, leagueStatModel.Players, leagueStatModel.Teams);
                    break;
                case FragmentType.LeagueTable:
                    fr = LeagueTableFragment.Instance(data.ToObject<IEnumerable<Team>>());
                    break;
                case FragmentType.Events:
                    var eventModels = data.ToObject<MatchEvents>();
                    fr = EventsFragment.Instance(eventModels.Events,eventModels.Match,eventModels.HomeTeam,eventModels.AwayTeam);
                    break;
                case FragmentType.MatccPlayers:
                    var matchPlayersModel = data.ToObject<MatchPlayersModel>();
                    fr = MatchPlayersFragment.Instance(matchPlayersModel.HomeTeam, matchPlayersModel.AwayTeam, matchPlayersModel.Match, matchPlayersModel.Events);
                    break;
                case FragmentType.MatchDetail:
                    var matchDetailModel = data.ToObject<MatchDetailModel>();
                    fr = MatchDetailsFragment.Instance(matchDetailModel.Match, matchDetailModel.League, matchDetailModel.Stadium);
                    break;
                default:
                    break;
            }

            return fr;
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(Model[position].TabTitle);
        }
    }
}