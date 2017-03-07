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
using Floorball.Droid.Fragments;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Newtonsoft.Json;
using Android.Support.Design.Widget;
using Floorball.Droid.Adapters;
using Floorball.Droid.Models;
using Floorball.Droid.Utils;

namespace Floorball.Droid.Fragments
{
    public enum FragmentType
    {
        Leagues, Players, LeagueMatches, TeamMatches, Stats, Table, Teams, Events, MatchDetails, MatchReferees
    }

    public class TabbedViewPagerFragment : Fragment
    {

        ViewPager viewPager;
        FragmentStatePagerAdapter adapter;

        public static TabbedViewPagerFragment Instance(List<TabbedViewPagerModel> model)
        {
            var fragment = new TabbedViewPagerFragment();
            var args = new Bundle();

            args.PutObject("model", model);
            fragment.Arguments = args;

            return fragment;
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View root = inflater.Inflate(Resource.Layout.ViewPagerWithTabs, container, false);

            viewPager = root.FindViewById<ViewPager>(Resource.Id.pager);
            adapter = new TabbedViewPagerAdapter(Activity.SupportFragmentManager, Arguments.GetObject<List<TabbedViewPagerModel>>("model"));
            viewPager.Adapter = adapter;

            TabLayout tabs = root.FindViewById<TabLayout>(Resource.Id.tabs);
            tabs.SetupWithViewPager(viewPager);

            return root;
        }
    }
}