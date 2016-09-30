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
using Floorball.Droid.Adapters;
using Floorball.LocalDB;

namespace Floorball.Droid.Fragments
{
    public class TeamsFragment : ViewPagerWithTabs
    {

        public static TeamsFragment Instance()
        {
            return new TeamsFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            PagerType = PagerFragmentType.Teams;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View root = base.OnCreateView(inflater, container, savedInstanceState);

            root.FindViewById<TextView>(Resource.Id.title).Text = Resources.GetString(Resource.String.teams);

            return root;
        }

        public override void listItemSelected(string s)
        {
            foreach (var tag in SexPagerAdapter.fragmentTags)
            {
                TeamsPageFragment f = Activity.SupportFragmentManager.FindFragmentByTag(tag) as TeamsPageFragment;

                f.CreateTeams(f.View);

            }
           
        }
    }
}