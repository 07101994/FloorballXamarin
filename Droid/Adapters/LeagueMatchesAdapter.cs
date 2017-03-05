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
    public class LeagueMatchesAdapter : MatchesAdapter
    {
        public LeagueMatchesAdapter(IEnumerable<Team> teams, IEnumerable<Match> matches, int rounds)
        {

            for (int i = 0; i < rounds; i++)
            {
                ListItems.Add(new ListItem { Index = MainHeaders.Count, Type = 0 });
                MainHeaders.Add(new HeaderModel { Title = (i + 1) + ". forduló" });

                List<Match> matchesInRound = matches.Where(m => m.Round == i + 1).OrderBy(m => m.Date).ThenBy(m => m.Time).ToList();

                int j = 0;

                while (j < matchesInRound.Count)
                {
                    ListItems.Add(new ListItem { Index = SubHeaders.Count, Type = 1 });
                    SubHeaders.Add(new HeaderModel { Title = matchesInRound.ElementAt(j).Date.ToString() });

                    int k = j;
                    while (k < matchesInRound.Count && matchesInRound.ElementAt(j).Date == matchesInRound.ElementAt(k).Date && matchesInRound.ElementAt(j).Time == matchesInRound.ElementAt(k).Time)
                    {
                        ListItems.Add(new ListItem { Index = Contents.Count, Type = 2 });
                        Contents.Add(new MatchResultModel
                        {
                            HomeTeam = teams.Where(t => t.Id == matchesInRound.ElementAt(k).HomeTeamId).First().Name + " ",
                            HomeScore = matchesInRound.ElementAt(j).GoalsH.ToString(),
                            AwayTeam = teams.Where(t => t.Id == matchesInRound.ElementAt(k).AwayTeamId).First().Name,
                            AwayScore = matchesInRound.ElementAt(j).GoalsA.ToString(),
                            Id = matchesInRound.ElementAt(j).Id
                        });

                        k++;
                    }

                    j = k;
                }
            }
        }

    }
}