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
using Android.Support.V4.View;
using Floorball.Droid.Adapters;
using Floorball.LocalDB;
using Android.Support.Design.Widget;

namespace Floorball.Droid.Fragments
{
    public class ViewPagerWithTabs : MainFragment
    {

        protected List<string> years;

        protected int ActualFragmentIndex { get; set; }

        protected ViewPager YearViewPager { get; set; }

        protected YearFragmentStatePageAdapter YearViewPagerAdapter { get; set; }

        protected SexPagerAdapter SexPagerAdapter { get; set; }

        protected ViewPager SexPager { get; set; }

        public PagerFragmentType PagerType { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //years = UoW.LeagueRepo.GetAllYear().Select(y => y.Year.ToString()).ToList();
            ActualFragmentIndex = 0;

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.YearPagerWithTabsFragment, container, false);
            
            //1
            YearViewPager = root.FindViewById<ViewPager>(Resource.Id.yearPager);
            YearViewPager.Adapter = new YearFragmentStatePageAdapter(Activity.SupportFragmentManager, years);
            YearViewPager.PageSelected += YearViewPager_PageSelected;

            TabLayout tabs = root.FindViewById<TabLayout>(Resource.Id.tabs);
            tabs.SetupWithViewPager(YearViewPager);

            //2
            SexPager = root.FindViewById<ViewPager>(Resource.Id.pager);
            SexPagerAdapter = new SexPagerAdapter(Activity.SupportFragmentManager, years, PagerType);
            SexPager.Adapter = SexPagerAdapter;

            tabs = root.FindViewById<TabLayout>(Resource.Id.tabs);
            tabs.SetupWithViewPager(SexPager);

            //3
            root.FindViewById<ImageView>(Resource.Id.rightArrow).Click += RightArrowClicked;
            root.FindViewById<ImageView>(Resource.Id.leftArrow).Click += LeftArrowClicked;

            return root;
        }

        public virtual void YearViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            ActualFragmentIndex = e.Position;

            foreach (var value in SexPagerAdapter.Fragments.Values)
            {
                value.YearUpdated(Convert.ToInt32(years[ActualFragmentIndex]));
            }

        }

        private void LeftArrowClicked(object sender, EventArgs e)
        {
            if (ActualFragmentIndex > 0)
            {
                YearViewPager.CurrentItem = ActualFragmentIndex - 1;
            }
        }

        private void RightArrowClicked(object sender, EventArgs e)
        {
            if (ActualFragmentIndex < years.Count - 1)
            {
                YearViewPager.CurrentItem = ActualFragmentIndex + 1;
            }
        }

        public override void listItemSelected(string s)
        {
            throw new NotImplementedException();
        }

    }
}