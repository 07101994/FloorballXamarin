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
using Android.Support.V4.App;
using Floorball.Droid.Fragments;

namespace Floorball.Droid.Adapters
{
    public class YearFragmentStatePageAdapter : FragmentStatePagerAdapter
    {

        List<string> years;

        public YearFragmentStatePageAdapter(Android.Support.V4.App.FragmentManager fm, List<string> years) : base(fm)
        {
            this.years = years;
        }

        public override int Count
        {
            get
            {
                return years.Count;
            }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return YearPageFragment.Instance(years[position]);
        }

        public override int GetItemPosition(Java.Lang.Object objectValue)
        {
            YearPageFragment fragment = objectValue as YearPageFragment;
            int position = years.IndexOf(fragment.Year);

            if (position >= 0)
            {
                return position;
            }
            else
            {
                return PositionNone;
            }
        }

    }
}