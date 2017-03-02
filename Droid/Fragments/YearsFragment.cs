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
using Android.Support.V7.Widget;
using Floorball.Droid.Adapters;
using Floorball.Droid.Activities;

namespace Floorball.Droid.Fragments
{

    public class YearsFragment : MainFragment
    {
        public string Type { get; set; }

        private RecyclerView recyclerView;
        private YearsAdapter adapter;

        public static YearsFragment Instance(string type)
        {
            var fragment = new YearsFragment();
            Bundle args = new Bundle();
            args.PutString("type", type);

            fragment.Arguments = args;

            return fragment;
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            adapter = new YearsAdapter(UoW.LeagueRepo.GetAllYear().Select(y => y.ToString()));
            Type = Arguments.GetString("type");

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.RecycleView, container, false);

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            adapter.ClickedId += Adapter_YearClicked;
            recyclerView.SetAdapter(adapter);

            return root;
        }

        private void Adapter_YearClicked(object sender, int e)
        {
            Intent intent;

            if (Type == "leagues")
            {
                intent = new Intent(Activity, typeof(LeaguesActivity));
            }
            else
            {
                intent = new Intent(Activity, typeof(TeamsActivity));
            }

            intent.PutExtra("year", adapter.ListItems[e]);
            StartActivity(intent);
        }

        public override void listItemSelected(string s)
        {
            throw new NotImplementedException();
        }
    }
}