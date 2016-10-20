using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Java.Lang;
using Floorball.Droid.Fragments;

namespace Floorball.Droid.Adapters
{
    public enum PagerFragmentType
    {
        Teams, Leagues
    }

    public class SexPagerAdapter : FragmentStatePagerAdapter
    {
        public PagerFragmentType PagerType { get; set; }

        public List<string> fragmentTags;

        public List<string> Years { get; set; }

        public Dictionary<int, PagerFragment> Fragments { get; set; }

        public SexPagerAdapter(FragmentManager manager, List<string> years, PagerFragmentType pagerType) : base(manager)
        {
            fragmentTags = new List<string>();
            Years = years;
            PagerType = pagerType;
            Fragments = new Dictionary<int, PagerFragment>();
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
            PagerFragment f;

            switch (position)
            {
                case 0:
                    f = PagerFragment.Instance(0, Convert.ToInt32(Years[0]),PagerType);
                    Fragments.Add(position, f);
                    return f;
                case 1:
                    f = PagerFragment.Instance(1, Convert.ToInt32(Years[0]),PagerType);
                    Fragments.Add(position, f);
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