﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Java.Lang;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Floorball.Droid.Adapters;
using Floorball.LocalDB;

namespace Floorball.Droid.Fragments
{
    public class TeamsFragment : MainFragment
    {

        public int ActualFragmentIndex { get; set; }

        public ViewPager YearViewPager { get; set; }

        private YearFragmentStatePageAdapter YearViewPagerAdapter { get; set; }

        List<string> years;

        private SexPageAdapter pagerAdapter;

        private ViewPager pager;

        public static TeamsFragment Instance()
        {
            return new TeamsFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            years = Manager.GetAllYear().Select(y => y.Year.ToString()).ToList();
            ActualFragmentIndex = 0;

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View root = inflater.Inflate(Resource.Layout.TeamsFragment, container, false);

            YearViewPager = root.FindViewById<ViewPager>(Resource.Id.yearPager);
            YearViewPager.Adapter = new YearFragmentStatePageAdapter(Activity.SupportFragmentManager, years);

            root.FindViewById<ImageView>(Resource.Id.rightArrow).Click += RightArrowClicked;
            root.FindViewById<ImageView>(Resource.Id.leftArrow).Click += LeftArrowClicked;


            return root;

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void LeftArrowClicked(object sender, EventArgs e)
        {
            if (ActualFragmentIndex > 0)
            {
                ActualFragmentIndex--;
                YearViewPager.CurrentItem = ActualFragmentIndex;
            }
        }

        private void RightArrowClicked(object sender, EventArgs e)
        {
            if (ActualFragmentIndex < years.Count - 1)
            {
                ActualFragmentIndex++;
                YearViewPager.CurrentItem = ActualFragmentIndex;
            }
        }

        public override void OnStart()
        {
            base.OnStart();

            pager = Activity.FindViewById<ViewPager>(Resource.Id.sexPager);
            pagerAdapter = new SexPageAdapter(Activity.SupportFragmentManager);
            pager.Adapter = pagerAdapter;

            TabLayout tabs = Activity.FindViewById<TabLayout>(Resource.Id.sexTabs);
            tabs.SetupWithViewPager(pager);
        }

        public override void listItemSelected(string s)
        {
            foreach (var tag in pagerAdapter.fragmentTags)
            {
                TeamsPageFragment f = Activity.SupportFragmentManager.FindFragmentByTag(tag) as TeamsPageFragment;

                f.CreateTeams(f.View);

            }
           
        }

        public class SexPageAdapter : FragmentPagerAdapter
        {
            public List<string> fragmentTags;

            public SexPageAdapter(FragmentManager manager) : base(manager)
            {
                fragmentTags = new List<string>();
            }

            public override int Count
            {
                get
                {
                    return 2;
                }
            }

            public override Fragment GetItem(int position)
            {
                Fragment f;

                switch (position)
                {
                    case 0:
                        f = TeamsPageFragment.Instance(0);
                        //fragmentTags.Add(f.Tag);
                        return f;
                    case 1:
                        f = TeamsPageFragment.Instance(1);
                        //fragmentTags.Add(f.Tag);
                        return f;
                    default:
                        return null;
                }
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                Java.Lang.Object o = base.InstantiateItem(container, position);

                if (o is Fragment)
                {
                    Fragment f = o as Fragment;
                    fragmentTags.Add(f.Tag);
                }

                return o;
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                switch (position)
                {
                    case 0:
                        return new Java.Lang.String("Férfi");
                    case 1:
                        return new Java.Lang.String("Női");
                    default:
                        return null;
                }

            }


        }
    }
}