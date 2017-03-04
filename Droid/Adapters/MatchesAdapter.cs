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
using Android.Support.V7.Widget;
using Floorball.Droid.Models;
using FloorballServer.Models.Floorball;
using Floorball.Droid.ViewHolders;
using Floorball.LocalDB.Tables;

namespace Floorball.Droid.Adapters
{
    public class MatchesAdapter : AdapterWithTwoHeader<HeaderModel, HeaderModel, MatchResultModel>
    {
        public List<Match> Matches { get; set; }
       
        public List<Team> Teams { get; set; }

        public MatchesAdapter(List<Team> teams, List<Match> matches, int rounds)
        {
            Teams = teams;
            Matches = matches;

            for (int i = 0; i < rounds; i++)
            {
                ListItems.Add(new ListItem { Index = MainHeaders.Count, Type = 0 });
                MainHeaders.Add(new HeaderModel { Title = (i + 1) + ". forduló" });

                List<Match> matchesInRound = Matches.Where(m => m.Round == i + 1).OrderBy(m => m.Date).ThenBy(m => m.Time).ToList();

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
                            HomeTeam = Teams.Where(t => t.Id == matchesInRound.ElementAt(k).HomeTeamId).First().Name + " ",
                            HomeScore = matchesInRound.ElementAt(j).GoalsH.ToString(),
                            AwayTeam = Teams.Where(t => t.Id == matchesInRound.ElementAt(k).AwayTeamId).First().Name,
                            AwayScore = matchesInRound.ElementAt(j).GoalsA.ToString()
                        });

                        k++;
                    }

                    j = k;
                }
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            switch (holder.ItemViewType)
            {
                case 0:

                    var vh = holder as HeaderViewHolder;
                    vh.Header.Text = MainHeaders[ListItems[position].Index].Title;

                    break;
                case 1:

                    var vh1 = holder as HeaderViewHolder;
                    vh1.Header.Text = SubHeaders[ListItems[position].Index].Title;

                    break;
                case 2:

                    var vh2 = holder as MatchViewHolder;
                    vh2.HomeTeam.Text = Contents[ListItems[position].Index].HomeTeam;
                    vh2.AwayTeam.Text = Contents[ListItems[position].Index].AwayTeam;
                    vh2.HomeScore.Text = Contents[ListItems[position].Index].HomeScore;
                    vh2.AwayScore.Text = Contents[ListItems[position].Index].AwayScore;

                    break;
                default:
                    break;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView;
            RecyclerView.ViewHolder vh = null;

            switch (viewType)
            {
                case 0:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Header, parent, false);
                    vh = new HeaderViewHolder(itemView);

                    break;
                case 1:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.SubHeader, parent, false);
                    vh = new HeaderViewHolder(itemView);

                    break;
                case 2:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.MatchResult, parent, false);
                    vh = new MatchViewHolder(itemView);

                    break;
                default:
                    break;
            }

            return vh;
        }
    }
}