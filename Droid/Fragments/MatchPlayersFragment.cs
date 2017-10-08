using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Floorball.LocalDB.Tables;
using Floorball.Droid.Utils;
using Android.Support.V4.App;
using Floorball.Droid.Adapters;
using Android.Support.V7.Widget;
using Floorball.Util;

namespace Floorball.Droid.Fragments
{
    public class MatchPlayersFragment : MainFragment
    {
        public Match Match { get; set; }
        public IEnumerable<Player> HomePlayers { get; set; }
        public IEnumerable<Player> AwayPlayers { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        MatchPlayersAdapter adapter;
        RecyclerView recyclerView;

        public static MatchPlayersFragment Instance(Team homeTeam, Team awayTeam, Match match, IEnumerable<Event> events)
        {
            var fragment = new MatchPlayersFragment();
            Bundle args = new Bundle();
            args.PutObject("match", match);
            args.PutObject("homeTeam", homeTeam);
            args.PutObject("awayTeam", awayTeam);
            args.PutObject("events", events);

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            Match = Arguments.GetObject<Match>("match");
            HomeTeam = Arguments.GetObject<Team>("homeTeam");
            AwayTeam = Arguments.GetObject<Team>("awayTeam");
            HomePlayers = HomeTeam.Players.Intersect(Match.Players, new KeyEqualityComparer<Player>(p => p.Id));
            AwayPlayers = AwayTeam.Players.Intersect(Match.Players, new KeyEqualityComparer<Player>(p => p.Id));
            Events = Arguments.GetObject<IEnumerable<Event>>("events");

            adapter = new MatchPlayersAdapter(HomePlayers, AwayPlayers, Events);
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View root = inflater.Inflate(Resource.Layout.MatchPlayers, container, false);

            root.FindViewById<TextView>(Resource.Id.homeTeamScore).Text = Match.ScoreH.ToString();
            root.FindViewById<TextView>(Resource.Id.awayTeamScore).Text = Match.ScoreA.ToString();
            root.FindViewById<TextView>(Resource.Id.homeTeamName).Text = HomeTeam.Name;
            root.FindViewById<TextView>(Resource.Id.awayTeamName).Text = AwayTeam.Name;

            SetTeamImage(HomeTeam, root.FindViewById<ImageView>(Resource.Id.homeTeamImage));
            SetTeamImage(AwayTeam, root.FindViewById<ImageView>(Resource.Id.awayTeamImage));

            //root.FindViewById<TextView>(Resource.Id.leagueName).Text = League.Name + " " + Match.Round.ToString() + ". forduló";
            //root.FindViewById<TextView>(Resource.Id.date).Text = Match.Date.ToShortDateString();
            //root.FindViewById<TextView>(Resource.Id.stadium).Text = Stadium.Name;

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            recyclerView.SetAdapter(adapter);

            return root;
        }
    }
}