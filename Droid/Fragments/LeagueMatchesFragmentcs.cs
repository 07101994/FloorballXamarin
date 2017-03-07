using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Floorball.Droid.Adapters;
using Floorball.LocalDB.Tables;
using Newtonsoft.Json;
using Floorball.Droid.Utils;

namespace Floorball.Droid.Fragments
{
    public class LeagueMatchesFragment : MatchesFragment
    {

        public static LeagueMatchesFragment Instance(IEnumerable<Team> teams, IEnumerable<Match> matches, League league)
        {
            var fragment = new LeagueMatchesFragment();

            Bundle args = new Bundle();
            args.PutObject("matches", matches);
            args.PutObject("teams", teams);
            args.PutInt("rounds", league.Rounds);

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            adapter = new LeagueMatchesAdapter(Arguments.GetObject<IEnumerable<Team>>("teams"),
               Arguments.GetObject<IEnumerable<Match>>("matches"), Arguments.GetInt("rounds"));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}