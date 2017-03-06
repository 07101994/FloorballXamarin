using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Floorball.Droid.Models;
using Floorball.LocalDB.Tables;
using Floorball.Droid.ViewHolders;
using Android.Animation;

namespace Floorball.Droid.Adapters
{
    public class ActualAdapter : AdapterWithTwoHeader<HeaderModel, HeaderModel, LiveMatchModelBase>
    {
        public IEnumerable<League> Leagues { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public Context Context { get; set; }

        public ActualAdapter(IEnumerable<Match> liveMatches, IEnumerable<Match> soonMatches, IEnumerable<Team> teams, IEnumerable<League> leagues, Context ctx)
        {
            Teams = teams;
            Leagues = leagues;
            Context = ctx;

            Init("Élő", liveMatches);
            Init("Hamarosan", soonMatches);

        }

        private void Init(string title, IEnumerable<Match> matches)
        {
            ListItems.Add(new ListItem { Index = MainHeaders.Count, Type = 0 });
            MainHeaders.Add(new HeaderModel { Title = title });

            int i = 0;

            if (matches.Count() == 0)
            {
                ListItems.Add(new ListItem { Index = Contents.Count, Type = 3 });
                Contents.Add(new LiveMatchModelBase { Text = "3 napon belül nincs meccs." });
            }

            while (i < matches.Count())
            {

                Match actualMatch = matches.ElementAt(i);
                League actualLeague = Leagues.First(l => l.Id == actualMatch.LeagueId);

                ListItems.Add(new ListItem { Index = SubHeaders.Count, Type = 1 });
                SubHeaders.Add(new HeaderModel { Title = actualLeague.Name, Country = actualLeague.Country });

                int j = i;

                //While in the same league
                while (j < matches.Count() && matches.ElementAt(j).LeagueId == actualMatch.LeagueId)
                {

                    Team homeTeam = Teams.First(t => t.Id == actualMatch.HomeTeamId);
                    Team awayTeam = Teams.First(t => t.Id == actualMatch.AwayTeamId);

                    ListItems.Add(new ListItem { Index = Contents.Count, Type = 2 });
                    Contents.Add(new LiveMatchModel
                    {
                        Date = actualMatch.Date,
                        Time = UIHelper.GetMatchTime(actualMatch.Time, actualMatch.State),
                        HomeTeam = homeTeam.Name,
                        AwayTeam = awayTeam.Name,
                        HomeScore = actualMatch.GoalsH.ToString(),
                        AwayScore = actualMatch.GoalsA.ToString(),
                        MatchId = actualMatch.Id,
                        State = actualMatch.State
                    });

                    actualMatch = matches.ElementAt(j);

                    j++;
                }

                i = j;

            }

        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var listItem = ListItems[position];

            switch (holder.ItemViewType)
            {
                case 0:

                    var vh0 = holder as HeaderViewHolder;
                    vh0.Header.Text = MainHeaders[ListItems[position].Index].Title;

                    break;
                case 1:

                    var vh1 = holder as LeagueHeaderViewHolder;

                    int resourceId = Context.Resources.GetIdentifier(SubHeaders[ListItems[position].Index].Country.ToString().ToLower(), "drawable", Context.PackageName);

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                    {
                        vh1.Flag.SetImageDrawable(Context.Resources.GetDrawable(resourceId, Context.ApplicationContext.Theme));
                    }
                    else
                    {
                        vh1.Flag.SetImageDrawable(Context.Resources.GetDrawable(resourceId));
                    }

                    vh1.TextView.Text = SubHeaders[ListItems[position].Index].Title;

                    break;
                case 2:

                    var matchModel = (Contents[listItem.Index] as LiveMatchModel);

                    var vh2 = holder as LiveMatchViewHolder;
                    vh2.Date.Text = matchModel.Date.ToString();
                    vh2.Time.Text = matchModel.Time;
                    vh2.Time.Tag = matchModel.MatchId + "time";
                    vh2.HomeTeam.Text = matchModel.HomeTeam;
                    vh2.HomeScore.Text = matchModel.HomeScore;
                    vh2.AwayTeam.Text = matchModel.AwayTeam;
                    vh2.AwayScore.Text = matchModel.AwayScore;

                    if (matchModel.State == StateEnum.Playing)
                    {
                        MakeAnimation(vh2.Progress);
                    }

                    break;

                case 3:

                    var vh3 = holder as HeaderViewHolder;
                    vh3.Header.Text = Contents[listItem.Index].Text;

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

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CenterHeader, parent, false);
                    itemView.FindViewById<TextView>(Resource.Id.headerName).TextSize = 30;
                    vh = new HeaderViewHolder(itemView);

                    break;
                case 1:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.LeagueNameWithFlag, parent, false);
                    vh = new LeagueHeaderViewHolder(itemView);

                    break;

                case 2:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ActualTile, parent, false);
                    vh = new LiveMatchViewHolder(itemView, OnClickObject, this);

                    break;

                case 3:

                    itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.CenterHeader, parent, false);
                    vh = new HeaderViewHolder(itemView);

                    break;

                default:
                    break;
            }

            return vh;
        }

        private void MakeAnimation(View view)
        {
            int colorTo, colorFrom;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                colorFrom = Context.Resources.GetColor(Resource.Color.green, Context.ApplicationContext.Theme);
                colorTo = Context.Resources.GetColor(Resource.Color.red, Context.ApplicationContext.Theme);
            }
            else
            {
                colorFrom = Context.Resources.GetColor(Resource.Color.green);
                colorTo = Context.Resources.GetColor(Resource.Color.red);
            }
                
            ValueAnimator colorAnimation = ValueAnimator.OfObject(new ArgbEvaluator(), colorFrom, colorTo);
            colorAnimation.SetDuration(1000);
            colorAnimation.RepeatCount = ValueAnimator.Infinite;
            colorAnimation.RepeatMode = ValueAnimatorRepeatMode.Reverse;
            colorAnimation.Update += delegate
            {
                view.SetBackgroundColor(new Android.Graphics.Color(Convert.ToInt32(colorAnimation.AnimatedValue)));
            };
            colorAnimation.Start();
        }
    }
}