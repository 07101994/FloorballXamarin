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
using Android.Support.V4.App;
using Java.Lang;
using Android.Support.V4.View;
using Android.Support.Design.Widget;

namespace Floorball.Droid.Fragments
{
    public class TeamsFragment : MainFragment
    {

        private SexPageAdapter pagerAdapter;
           
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);



            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View root = inflater.Inflate(Resource.Layout.TeamsFragment, container, false);

            return root;

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnStart()
        {
            base.OnStart();

            ViewPager pager = Activity.FindViewById<ViewPager>(Resource.Id.sexPager);
            pagerAdapter = new SexPageAdapter(Activity.SupportFragmentManager);
            pager.Adapter = pagerAdapter;

            TabLayout tabs = Activity.FindViewById<TabLayout>(Resource.Id.sexTabs);
            tabs.SetupWithViewPager(pager);
        }

        public override void listItemSelected(string s)
        {
            throw new NotImplementedException();
        }


        public class SexPageAdapter : FragmentPagerAdapter
        {
            public SexPageAdapter(FragmentManager manager) : base(manager)
            {

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
                switch (position)
                {
                    case 0:
                        return TeamsPageFragment.Instance(0);
                    case 1:
                        return TeamsPageFragment.Instance(1);
                    default:
                        return null;
                }
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