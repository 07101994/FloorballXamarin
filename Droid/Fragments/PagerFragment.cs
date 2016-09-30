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
using Floorball.Droid.Adapters;

namespace Floorball.Droid.Fragments
{
    public class PagerFragment : Fragment
    {

        protected int PageCount { get; set; }
        protected int Year { get; set; }

        public static PagerFragment Instance(int pageCount, int year, PagerFragmentType pagertype)
        {
            PagerFragment fragment = new PagerFragment();

            switch (pagertype)
            {
                case PagerFragmentType.Teams:
                    fragment = new TeamsPageFragment();
                    break;
                case PagerFragmentType.Leagues:
                    fragment = new LeaguePagerFragment();
                    break;
                default:
                    break;
            }

            Bundle args = new Bundle();
            args.PutInt("pageCount", pageCount);
            args.PutInt("year", year);
            fragment.Arguments = args;

            return fragment;
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            PageCount = Arguments.GetInt("pageCount", 0);
            Year = Arguments.GetInt("year", 2015);
        }

        public virtual void YearUpdated(int year)
        {
            Year = year;
        }
        

    }
}