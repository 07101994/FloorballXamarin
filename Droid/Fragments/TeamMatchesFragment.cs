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
using Newtonsoft.Json;
using Floorball.Droid.Adapters;
using Floorball.LocalDB.Tables;
using Floorball.Droid.Utils;

namespace Floorball.Droid.Fragments
{
    class TeamMatchesFragment : MatchesFragment
    {

        public static TeamMatchesFragment Instance(IEnumerable<Team> teams, IEnumerable<Match> matches, IEnumerable<League> leagues, int teamId)
        {
            var fragment = new TeamMatchesFragment();

            Bundle args = new Bundle();
            args.PutObject("matches", matches);
            args.PutObject("teams", teams);
            args.PutObject("leagues", leagues);
            args.PutInt("teamId", teamId);

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            adapter = new TeamMatchesAdapter(Arguments.GetObject<IEnumerable<Team>>("teams"),
               Arguments.GetObject<IEnumerable<Match>>("matches"),
               Arguments.GetObject<IEnumerable<League>>("leagues"),
               Arguments.GetInt("teamId"));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}