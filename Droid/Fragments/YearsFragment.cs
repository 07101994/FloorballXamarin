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

        private RecyclerView recyclerView;
        private YearsAdapter adapter;

        public static YearsFragment Instance()
        {
            return new YearsFragment();
        }

       

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            adapter = new YearsAdapter(UoW.LeagueRepo.GetAllYear().Select(y => y.Year.ToString()));


        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View root = inflater.Inflate(Resource.Layout.RecycleView, container, false);

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            adapter.YearClicked += Adapter_YearClicked;
            recyclerView.SetAdapter(adapter);
            //recyclerView.AddItemDecoration()

            return root;
        }

        private void Adapter_YearClicked(object sender, int e)
        {
            Intent intent = new Intent(Activity, typeof(LeaguesActivity));
            intent.PutExtra("year", adapter.Years[e]);
            StartActivity(intent);
        }

        public override void listItemSelected(string s)
        {
            throw new NotImplementedException();
        }
    }
}