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
using Floorball.Droid.Models;

namespace Floorball.Droid.Adapters
{
    class TeamMatchesAdapter : MatchesAdapter
    {
        public TeamMatchesAdapter(IEnumerable<Team> teams, IEnumerable<Match> matches, IEnumerable<League> leagues, int teamId)
        {

            int i = 0;

            foreach (var league in leagues)
            {
                ListItems.Add(new ListItem { Index = MainHeaders.Count, Type = 0 });
                MainHeaders.Add(new HeaderModel { Title = league.Name});

                foreach (var match in matches.Where(m => m.LeagueId == league.Id).OrderBy(m => m.Date))
                {
                    ListItems.Add(new ListItem { Index = SubHeaders.Count, Type = 1 });
                    SubHeaders.Add(new HeaderModel { Title = match.Round.ToString() + ". forduló" });

                    ListItems.Add(new ListItem { Index = Contents.Count, Type = 2 });

                    if (teamId == match.HomeTeamId)
                    {
                        Contents.Add(new MatchResultModel
                        {
                            HomeTeam = teams.Where(t => t.Id == match.HomeTeamId).First().Name + " ",
                            HomeScore = match.GoalsH.ToString(),
                            AwayTeam = teams.Where(t => t.Id == match.AwayTeamId).First().Name,
                            AwayScore = match.GoalsA.ToString(),
                            Id = match.Id
                        });
                    }
                    else
                    {
                        Contents.Add(new MatchResultModel
                        {
                            AwayTeam = teams.Where(t => t.Id == match.HomeTeamId).First().Name + " ",
                            AwayScore = match.GoalsH.ToString(),
                            HomeTeam = teams.Where(t => t.Id == match.AwayTeamId).First().Name,
                            HomeScore = match.GoalsA.ToString(),
                            Id = match.Id
                        });
                    }

                }

            }

        }
    }
}