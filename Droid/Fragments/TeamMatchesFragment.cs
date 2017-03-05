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

namespace Floorball.Droid.Fragments
{
    class TeamMatchesFragment : MatchesFragment
    {

        public static TeamMatchesFragment Instance(IEnumerable<Team> teams, IEnumerable<Match> matches, IEnumerable<League> leagues, int teamId)
        {
            var fragment = new TeamMatchesFragment();

            Bundle args = new Bundle();
            args.PutString("matches", JsonConvert.SerializeObject(matches));
            args.PutString("teams", JsonConvert.SerializeObject(teams));
            args.PutString("leagues", JsonConvert.SerializeObject(leagues));
            args.PutInt("teamId", teamId);

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            adapter = new TeamMatchesAdapter(JsonConvert.DeserializeObject<IEnumerable<Team>>(Arguments.GetString("teams")),
               JsonConvert.DeserializeObject<IEnumerable<Match>>(Arguments.GetString("matches")), 
               JsonConvert.DeserializeObject<IEnumerable<League>>(Arguments.GetString("leagues")),
               Arguments.GetInt("teamId"));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}